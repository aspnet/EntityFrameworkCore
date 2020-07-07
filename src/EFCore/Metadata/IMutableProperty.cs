// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.EntityFrameworkCore.Metadata
{
    /// <summary>
    ///     <para>
    ///         Represents a scalar property of an entity.
    ///     </para>
    ///     <para>
    ///         This interface is used during model creation and allows the metadata to be modified.
    ///         Once the model is built, <see cref="IProperty" /> represents a read-only view of the same metadata.
    ///     </para>
    /// </summary>
    public interface IMutableProperty : IProperty, IMutablePropertyBase
    {
        /// <summary>
        ///     Gets the type that this property belongs to.
        /// </summary>
        new IMutableEntityType DeclaringEntityType { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether this property can contain <see langword="null" />.
        /// </summary>
        new bool IsNullable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating when a value for this property will be generated by the database. Even when the
        ///     property is set to be generated by the database, EF may still attempt to save a specific value (rather than
        ///     having one generated by the database) when the entity is added and a value is assigned, or the property is
        ///     marked as modified for an existing entity. See <see cref="PropertyExtensions.GetBeforeSaveBehavior" />
        ///     and <see cref="PropertyExtensions.GetAfterSaveBehavior" /> for more information.
        /// </summary>
        new ValueGenerated ValueGenerated { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this property is used as a concurrency token. When a property is configured
        ///     as a concurrency token the value in the database will be checked when an instance of this entity type
        ///     is updated or deleted during <see cref="DbContext.SaveChanges()" /> to ensure it has not changed since
        ///     the instance was retrieved from the database. If it has changed, an exception will be thrown and the
        ///     changes will not be applied to the database.
        /// </summary>
        new bool IsConcurrencyToken { get; set; }
    }
}
