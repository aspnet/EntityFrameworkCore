// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.ChangeTracking.Internal;

namespace Microsoft.Data.Entity.ChangeTracking
{
    /// <summary>
    ///     <para>
    ///         Provides access to change tracking information and operations for a given property.
    ///     </para>
    ///     <para>
    ///         Instances of this class are returned from methods when using the <see cref="ChangeTracker" /> API and it is
    ///         not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    /// <typeparam name="TEntity"> The type of the entity the property belongs to. </typeparam>
    /// <typeparam name="TProperty"> The type of the property. </typeparam>
    public class PropertyEntry<TEntity, TProperty> : PropertyEntry
        where TEntity : class
    {
        /// <summary>
        ///     <para>
        ///         Initializes a new instance of the <see cref="PropertyEntry{TEntity, TProperty}" /> class.
        ///     </para>
        ///     <para>
        ///         Instances of this class are returned from methods when using the <see cref="ChangeTracker" /> API and it is
        ///         not designed to be directly constructed in your application code.
        ///     </para>
        /// </summary>
        /// <param name="internalEntry">  The internal entry tracking information about the entity the property belongs to. </param>
        /// <param name="name"> The name of the property. </param>
        public PropertyEntry([NotNull] InternalEntityEntry internalEntry, [NotNull] string name)
            : base(internalEntry, name)
        {
        }

        /// <summary>
        ///     Gets or sets the value currently assigned to this property. If the current value is set using this property,
        ///     the change tracker is aware of the change and <see cref="ChangeTracker.DetectChanges" /> is not required
        ///     for the context to detect the change.
        /// </summary>
        public new virtual TProperty CurrentValue
        {
            get { return (TProperty)base.CurrentValue; }
            [param: CanBeNull] set { base.CurrentValue = value; }
        }

        /// <summary>
        ///     Gets or sets the value that was assigned to this property when it was retrieved from the database.
        ///     This property is populated when an entity is retrieved from the database, but setting it may be
        ///     useful in disconnected scenarios where entities are retrieved with one context instance and
        ///     saved with a different context instance.
        /// </summary>
        public new virtual TProperty OriginalValue
        {
            get { return (TProperty)base.OriginalValue; }
            [param: CanBeNull] set { base.OriginalValue = value; }
        }
    }
}
