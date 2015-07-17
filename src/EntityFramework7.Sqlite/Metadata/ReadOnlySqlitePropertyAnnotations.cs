﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;

namespace Microsoft.Data.Entity.Sqlite.Metadata
{
    public class ReadOnlySqlitePropertyAnnotations : ReadOnlyRelationalPropertyAnnotations, ISqlitePropertyAnnotations
    {
        protected const string SqliteNameAnnotation = SqliteAnnotationNames.Prefix + RelationalAnnotationNames.ColumnName;
        protected const string SqliteColumnTypeAnnotation = SqliteAnnotationNames.Prefix + RelationalAnnotationNames.ColumnType;

        protected const string SqliteGeneratedValueSqlAnnotation = SqliteAnnotationNames.Prefix
                                                                   + RelationalAnnotationNames.GeneratedValueSql;

        public ReadOnlySqlitePropertyAnnotations([NotNull] IProperty property)
            : base(property)
        {
        }

        public override string ColumnName => Property[SqliteNameAnnotation] as string ?? base.ColumnName;
        public override string ColumnType => Property[SqliteColumnTypeAnnotation] as string ?? base.ColumnType;

        public override string GeneratedValueSql => Property[SqliteGeneratedValueSqlAnnotation] as string
                                                  ?? base.GeneratedValueSql;
    }
}
