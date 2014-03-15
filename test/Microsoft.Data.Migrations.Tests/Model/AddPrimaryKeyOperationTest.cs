﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Migrations.Model;
using Microsoft.Data.Relational.Model;
using Moq;
using Xunit;

namespace Microsoft.Data.Migrations.Tests.Model
{
    public class AddPrimaryKeyOperationTest
    {
        [Fact]
        public void Create_and_initialize_operation()
        {
            var table = new Table("foo.bar");
            var column = new Column("C", "int");
            table.AddColumn(column);
            var primaryKey = new PrimaryKey("PK", new[] { column });

            var addPrimaryKeyOperation = new AddPrimaryKeyOperation(primaryKey, table);

            Assert.Same(primaryKey, addPrimaryKeyOperation.PrimaryKey);
            Assert.Same(table, addPrimaryKeyOperation.Table);
        }

        [Fact]
        public void Is_not_destructive_change()
        {
            var table = new Table("foo.bar");
            var column = new Column("C", "int");
            table.AddColumn(column);
            var primaryKey = new PrimaryKey("PK", new[] { column });

            var addPrimaryKeyOperation = new AddPrimaryKeyOperation(primaryKey, table);

            Assert.False(addPrimaryKeyOperation.IsDestructiveChange);
        }

        [Fact]
        public void Dispatches_sql_generation()
        {
            var table = new Table("foo.bar");
            var column = new Column("C", "int");
            table.AddColumn(column);
            var primaryKey = new PrimaryKey("PK", new[] { column });

            var addPrimaryKeyOperation = new AddPrimaryKeyOperation(primaryKey, table);
            var mockSqlGenerator = new Mock<MigrationOperationSqlGenerator>();
            var stringBuilder = new IndentedStringBuilder();

            addPrimaryKeyOperation.GenerateOperationSql(mockSqlGenerator.Object, stringBuilder, true);

            mockSqlGenerator.Verify(
                g => g.Generate(addPrimaryKeyOperation, stringBuilder, true), Times.Once());
        }
    }
}
