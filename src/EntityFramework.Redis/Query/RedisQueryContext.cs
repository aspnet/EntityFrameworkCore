// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.Redis.Utilities;
using Microsoft.Framework.Logging;

namespace Microsoft.Data.Entity.Redis.Query
{
    public class RedisQueryContext : QueryContext
    {
        protected RedisDatabase _redisDatabase;

        public RedisQueryContext(
            [NotNull] ILogger logger,
            [NotNull] IQueryBuffer queryBuffer,
            [NotNull] StateManager stateManager,
            [NotNull] RedisDatabase redisDatabase)
            : base(
                Check.NotNull(logger, "logger"),
                Check.NotNull(queryBuffer, "queryBuffer"),
                Check.NotNull(stateManager, "stateManager"))
        {
            Check.NotNull(redisDatabase, "redisDatabase");

            _redisDatabase = redisDatabase;
        }

        public virtual IEnumerable<TResult> GetMaterializedResults<TResult>([NotNull] RedisQuery redisQuery)
        {
            Check.NotNull(redisQuery, "redisQuery");

            return _redisDatabase.GetResults(redisQuery)
                    .Select(objectArrayFromHash
                        => (TResult)QueryBuffer
                            .GetEntity(redisQuery.EntityType, new ObjectArrayValueReader(objectArrayFromHash)));
        }

        public virtual IAsyncEnumerable<TResult> GetMaterializedResultsAsync<TResult>([NotNull] RedisQuery redisQuery)
        {
            Check.NotNull(redisQuery, "redisQuery");

            return _redisDatabase.GetResultsAsync(redisQuery)
                    .Select(objectArrayFromHash
                        => (TResult)QueryBuffer
                            .GetEntity(redisQuery.EntityType, new ObjectArrayValueReader(objectArrayFromHash)));
        }

        public virtual IEnumerable<object[]> GetResultsFromRedis([NotNull] RedisQuery redisQuery)
        {
            Check.NotNull(redisQuery, "redisQuery");

            return _redisDatabase.GetResults(redisQuery);
        }

        public virtual IAsyncEnumerable<object[]> GetResultsFromRedisAsync([NotNull] RedisQuery redisQuery)
        {
            Check.NotNull(redisQuery, "redisQuery");

            return _redisDatabase.GetResultsAsync(redisQuery);
        }
    }
}
