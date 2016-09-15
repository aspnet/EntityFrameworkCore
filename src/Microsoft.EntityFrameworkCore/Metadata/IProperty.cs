// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.EntityFrameworkCore.Metadata
{
    /// <summary>
    ///     Represents a scalar property of an entity.
    /// </summary>
    public interface IProperty : IStructuralProperty, IPropertyBase
    {
        /// <summary>
        ///     TODO: ComplexType docs
        /// </summary>
        new IEntityType DeclaringType { get; }

        /// <summary>
        ///     TODO: ComplexType docs
        /// </summary>
        new string Name { get; }
    }
}
