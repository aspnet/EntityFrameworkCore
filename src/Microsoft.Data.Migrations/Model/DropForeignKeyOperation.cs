// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Migrations.Utilities;
using Microsoft.Data.Relational;

namespace Microsoft.Data.Migrations.Model
{
    public class DropForeignKeyOperation : MigrationOperation
    {
        private readonly SchemaQualifiedName _dependentTableName;
        private readonly string _foreignKeyName;

        public DropForeignKeyOperation(SchemaQualifiedName dependentTableName, [NotNull] string foreignKeyName)
        {
            Check.NotEmpty(foreignKeyName, "foreignKeyName");

            _dependentTableName = dependentTableName;
            _foreignKeyName = foreignKeyName;
        }

        public virtual SchemaQualifiedName DependentTableName
        {
            get { return _dependentTableName; }
        }

        public virtual string ForeignKeyName
        {
            get { return _foreignKeyName; }
        }

        public override bool IsDestructiveChange
        {
            get { return true; }
        }

        public override void GenerateSql([NotNull] MigrationOperationSqlGenerator visitor, [NotNull] IndentedStringBuilder stringBuilder, bool generateIdempotentSql)
        {
            Check.NotNull(visitor, "visitor");
            Check.NotNull(stringBuilder, "stringBuilder");

            visitor.Generate(this, stringBuilder, generateIdempotentSql);
        }
    }
}
