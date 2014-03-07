﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.Materialization
{
    public interface IMaterializerFactory
    {
        IMaterializer CreateMaterializer([NotNull] IEntityType entityType);
    }
}
