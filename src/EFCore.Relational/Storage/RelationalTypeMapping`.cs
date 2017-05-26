// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    ///     <para>
    ///         Represents the mapping between a specified .NET type and a database type.
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    public abstract class RelationalTypeMapping<T> : RelationalTypeMapping
    {
        /// <summary>
        ///     Initializes a new instance of the class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        public RelationalTypeMapping([NotNull] string storeType)
            : this(storeType, dbType: null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="System.Data.DbType" /> to be used. </param>
        public RelationalTypeMapping(
            [NotNull] string storeType,
            [CanBeNull] DbType? dbType)
            : this(storeType, dbType, unicode: false, size: null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RelationalTypeMapping" /> class.
        /// </summary>
        /// <param name="storeType"> The name of the database type. </param>
        /// <param name="dbType"> The <see cref="System.Data.DbType" /> to be used. </param>
        /// <param name="unicode"> A value indicating whether the type should handle Unicode data or not. </param>
        /// <param name="size"> The size of data the property is configured to store, or null if no size is configured. </param>
        /// <param name="hasNonDefaultUnicode"> A value indicating whether the Unicode setting has been manually configured to a non-default value. </param>
        /// <param name="hasNonDefaultSize"> A value indicating whether the size setting has been manually configured to a non-default value. </param>
        public RelationalTypeMapping(
            [NotNull] string storeType,
            [CanBeNull] DbType? dbType,
            bool unicode,
            int? size,
            bool hasNonDefaultUnicode = false,
            bool hasNonDefaultSize = false)
            : base(storeType, typeof(T), dbType, unicode, size, hasNonDefaultUnicode, hasNonDefaultSize)
        {
        }

//LAJLAJ        /// <summary>
//LAJLAJ        ///     Creates a copy of this mapping.
//LAJLAJ        /// </summary>
//LAJLAJ        /// <param name="storeType"> The name of the database type. </param>
//LAJLAJ        /// <param name="size"> The size of data the property is configured to store, or null if no size is configured. </param>
//LAJLAJ        /// <returns> The newly created mapping. </returns>
//LAJLAJ        public abstract RelationalTypeMapping<T> CreateCopyT([NotNull] string storeType, int? size);

        /// <summary>
        ///     Generates the SQL representation of a literal value.
        /// </summary>
        /// <param name="value">The literal value.</param>
        /// <returns>
        ///     The generated string.
        /// </returns>
        public override string GenerateSqlLiteral([CanBeNull]object value)
        {
            if (value == null)
            {
                return "NULL";
            }

            //LAJLAJ - should never reach here - how to display error?
            return "INVALID INVALID INVALID";
        }
    }
}
