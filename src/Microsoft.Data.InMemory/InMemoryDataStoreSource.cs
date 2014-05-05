// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.InMemory.Utilities;

namespace Microsoft.Data.InMemory
{
    public class InMemoryDataStoreSource
        : DataStoreSource<InMemoryDataStore, InMemoryConfigurationExtension, InMemoryDataStoreCreator, InMemoryConnection>
    {
        public override bool IsAvailable(DbContextConfiguration configuration)
        {
            Check.NotNull(configuration, "configuration");

            return true;
        }

        public override string Name
        {
            get { return typeof(InMemoryDataStore).Name; }
        }
    }
}
