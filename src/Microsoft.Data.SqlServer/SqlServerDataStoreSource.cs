﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNet.DependencyInjection;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.SqlServer.Utilities;

namespace Microsoft.Data.SqlServer
{
    public class SqlServerDataStoreSource : DataStoreSource
    {
        public override DataStore GetDataStore(ContextConfiguration configuration)
        {
            Check.NotNull(configuration, "configuration");

            // TODO: Use GetRequiredService, by sharing source if possible
            return configuration.Services.ServiceProvider.GetService<SqlServerDataStore>();
        }

        public override bool IsConfigured(ContextConfiguration configuration)
        {
            Check.NotNull(configuration, "configuration");

            // TODO: Consider finding connection string in config file by convention
            return configuration.EntityConfiguration.Extensions().OfType<SqlServerConfigurationExtension>().Any();
        }

        public override bool IsAvailable(ContextConfiguration configuration)
        {
            Check.NotNull(configuration, "configuration");

            // TODO: Consider finding connection string in config file by convention
            return IsConfigured(configuration);
        }

        public override string Name
        {
            get { return typeof(SqlServerDataStore).Name; }
        }
    }
}
