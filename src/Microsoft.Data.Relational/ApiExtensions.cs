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

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Data.Relational;
using Microsoft.Data.Relational.Model;
using Microsoft.Data.Relational.Utilities;

namespace Microsoft.Data.Entity.Metadata
{
    public static class MetadataExtensions
    {
        public static class Annotations
        {
            public const string StorageTypeName = "StorageTypeName";
            public const string ColumnDefaultValue = "ColumnDefaultValue";
            public const string ColumnDefaultSql = "ColumnDefaultSql";
            public const string IsClustered = "IsClustered";
            public const string CascadeDelete = "CascadeDelete";
        }

        public static ModelBuilder.EntityBuilder<TEntity> ToTable<TEntity>(
            [NotNull] this ModelBuilder.EntityBuilder<TEntity> entityBuilder,
            SchemaQualifiedName tableName)
            where TEntity : class
        {
            Check.NotNull(entityBuilder, "entityBuilder");

            entityBuilder.StorageName(tableName);

            return entityBuilder;
        }

        public static ModelBuilder.EntityBuilder<TEntity>.PropertiesBuilder.PropertyBuilder ColumnName<TEntity>(
            [NotNull] this ModelBuilder.EntityBuilder<TEntity>.PropertiesBuilder.PropertyBuilder propertyBuilder,
            [NotNull] string columnName)
            where TEntity : class
        {
            Check.NotNull(propertyBuilder, "propertyBuilder");

            propertyBuilder.StorageName(columnName);

            return propertyBuilder;
        }

        public static ModelBuilder.EntityBuilder<TEntity>.PropertiesBuilder.PropertyBuilder ColumnType<TEntity>(
            [NotNull] this ModelBuilder.EntityBuilder<TEntity>.PropertiesBuilder.PropertyBuilder propertyBuilder,
            [NotNull] string typeName)
            where TEntity : class
        {
            Check.NotNull(propertyBuilder, "propertyBuilder");

            propertyBuilder.Annotation(Annotations.StorageTypeName, typeName);

            return propertyBuilder;
        }

        public static ModelBuilder.EntityBuilder<TEntity>.PropertiesBuilder.PropertyBuilder ColumnDefaultSql<TEntity>(
            [NotNull] this ModelBuilder.EntityBuilder<TEntity>.PropertiesBuilder.PropertyBuilder propertyBuilder,
            [NotNull] string columnDefaultSql)
            where TEntity : class
        {
            Check.NotNull(propertyBuilder, "propertyBuilder");

            propertyBuilder.Annotation(Annotations.ColumnDefaultSql, columnDefaultSql);

            return propertyBuilder;
        }

        public static ModelBuilder.EntityBuilder<TEntity>.ForeignKeysBuilder.ForeignKeyBuilder CascadeDelete<TEntity>(
            [NotNull] this ModelBuilder.EntityBuilder<TEntity>.ForeignKeysBuilder.ForeignKeyBuilder foreignKeyBuilder,
            bool cascadeDelete)
            where TEntity : class
        {
            Check.NotNull(foreignKeyBuilder, "foreignKeyBuilder");

            foreignKeyBuilder.Annotation(Annotations.CascadeDelete, cascadeDelete.ToString());

            return foreignKeyBuilder;
        }

        public static string ColumnType([NotNull] this IProperty property)
        {
            Check.NotNull(property, "property");

            return property[Annotations.StorageTypeName];
        }

        public static object ColumnDefaultValue([NotNull] this IProperty property)
        {
            Check.NotNull(property, "property");

            return property[Annotations.ColumnDefaultValue];
        }

        public static string ColumnDefaultSql([NotNull] this IProperty property)
        {
            Check.NotNull(property, "property");

            return property[Annotations.ColumnDefaultSql];
        }

        public static bool IsClustered([NotNull] this IKey primaryKey)
        {
            Check.NotNull(primaryKey, "primaryKey");

            var isClusteredString = primaryKey[Annotations.IsClustered];

            bool isClustered;
            if (isClusteredString == null
                || !bool.TryParse(isClusteredString, out isClustered))
            {
                isClustered = false;
            }

            return isClustered;
        }

        public static bool CascadeDelete([NotNull] this IForeignKey foreignKey)
        {
            Check.NotNull(foreignKey, "foreignKey");

            var cascadeDeleteString = foreignKey[Annotations.CascadeDelete];

            bool cascadeDelete;
            if (cascadeDeleteString == null
                || !bool.TryParse(cascadeDeleteString, out cascadeDelete))
            {
                cascadeDelete = false;
            }

            return cascadeDelete;
        }

        public static IEnumerable<Column> GetStoreGeneratedColumns([NotNull] this Table table)
        {
            Check.NotNull(table, "table");

            return table.Columns.Where(
                c => c.ValueGenerationStrategy == StoreValueGenerationStrategy.Identity ||
                     c.ValueGenerationStrategy == StoreValueGenerationStrategy.Computed);
        }
    }
}
