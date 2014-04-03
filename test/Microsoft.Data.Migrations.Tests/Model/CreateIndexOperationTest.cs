// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Migrations.Model;
using Moq;
using Xunit;

namespace Microsoft.Data.Migrations.Tests.Model
{
    public class CreateIndexOperationTest
    {
        [Fact]
        public void Create_and_initialize_operation()
        {
            var createIndexOperation = new CreateIndexOperation(
                "dbo.MyTable", "MyIndex", new[] { "Foo", "Bar" },
                isUnique: true, isClustered: true);

            Assert.Equal("dbo.MyTable", createIndexOperation.TableName);
            Assert.Equal("MyIndex", createIndexOperation.IndexName);
            Assert.Equal(new[] { "Foo", "Bar" }, createIndexOperation.ColumnNames);
            Assert.True(createIndexOperation.IsUnique);
            Assert.True(createIndexOperation.IsClustered);
            Assert.False(createIndexOperation.IsDestructiveChange);
        }

        [Fact]
        public void Dispatches_visitor()
        {
            var createIndexOperation = new CreateIndexOperation(
                "dbo.MyTable", "MyIndex", new[] { "Foo", "Bar" },
                isUnique: true, isClustered: true);
            var mockVisitor = new Mock<MigrationOperationSqlGenerator>();
            var builder = new Mock<IndentedStringBuilder>();
            createIndexOperation.GenerateSql(mockVisitor.Object, builder.Object, false);

            mockVisitor.Verify(g => g.Generate(createIndexOperation, builder.Object, false), Times.Once());
        }
    }
}
