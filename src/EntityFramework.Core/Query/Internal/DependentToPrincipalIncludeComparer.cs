// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking.Internal;
using Microsoft.Data.Entity.Storage;

namespace Microsoft.Data.Entity.Query.Internal
{
    public class DependentToPrincipalIncludeComparer<TKey> : IIncludeKeyComparer
    {
        private readonly TKey _dependentKeyValue;
        private readonly IPrincipalKeyValueFactory<TKey> _principalKeyValueFactory;

        public DependentToPrincipalIncludeComparer(
            [NotNull] TKey dependentKeyValue,
            [NotNull] IPrincipalKeyValueFactory<TKey> principalKeyValueFactory)
        {
            _dependentKeyValue = dependentKeyValue;
            _principalKeyValueFactory = principalKeyValueFactory;
        }

        public virtual bool ShouldInclude(ValueBuffer valueBuffer)
            => ((TKey)_principalKeyValueFactory.CreateFromBuffer(valueBuffer)).Equals(_dependentKeyValue);
    }
}
