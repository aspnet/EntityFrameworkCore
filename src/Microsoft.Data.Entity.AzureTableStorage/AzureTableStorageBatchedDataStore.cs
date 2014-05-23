﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Data.Entity.AzureTableStorage.Interfaces;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.WindowsAzure.Storage.Table;

namespace Microsoft.Data.Entity.AzureTableStorage
{
    public class AzureTableStorageBatchedDataStore : AzureTableStorageDataStore
    {
        private const int MaxBatchOperations = 100;

        /// <summary>
        ///     Provided only for testing purposes. Do not use.
        /// </summary>
        protected AzureTableStorageBatchedDataStore(AzureTableStorageConnection connection)
            : base(connection)
        {
        }

        public AzureTableStorageBatchedDataStore([NotNull] DbContextConfiguration configuration, [NotNull] AzureTableStorageConnection connection)
            : base(configuration, connection)
        {
        }

        public override async Task<int> SaveChangesAsync(IReadOnlyList<StateEntry> stateEntries, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            var tableGroups = stateEntries.GroupBy(s => s.EntityType);
            var allBatchTasks = new List<Task<IList<ITableResult>>>();

            foreach (var tableGroup in tableGroups)
            {
                var table = Connection.GetTableReference(tableGroup.Key.StorageName);
                var partitionGroups = tableGroup.GroupBy(s => ((ITableEntity)s.Entity).PartitionKey);
                foreach (var partitionGroup in partitionGroups)
                {
                    var batch = new TableBatchOperation();
                    foreach (var operation in partitionGroup.Select(GetOperation).Where(operation => operation != null))
                    {
                        //TODO An entity can only appear once in a transaction; Ensure that change tracker never returns multiple state entries for the same entity
                        // TODO allow user access to config options: Retry Policy, Secondary Storage, Timeout 
                        batch.Add(operation);
                        if (batch.Count >= MaxBatchOperations)
                        {
                            allBatchTasks.Add(table.ExecuteBatchAsync(batch, cancellationToken));
                            batch = new TableBatchOperation();
                        }
                    }
                    if (batch.Count != 0)
                    {
                        allBatchTasks.Add(table.ExecuteBatchAsync(batch, cancellationToken));
                    }
                }
            }
            await Task.WhenAll(allBatchTasks);
            return InspectBatchResults(allBatchTasks);
        }

        protected int InspectBatchResults(IList<Task<IList<ITableResult>>> arg)
        {
            return CountTableResults(arg, task =>
                {
                    var failedResult = task.Result.FirstOrDefault(result => result.HttpStatusCode >= HttpStatusCode.BadRequest);
                    if (failedResult != default(ITableResult))
                    {
                        throw new DbUpdateException("Could not add entity: " + failedResult.Result);
                    }
                    return task.Result.Count;
                });
        }
    }
}
