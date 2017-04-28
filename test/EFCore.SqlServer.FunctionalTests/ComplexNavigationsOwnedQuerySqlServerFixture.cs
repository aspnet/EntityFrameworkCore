// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Specification.Tests;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.ComplexNavigationsModel;
using Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests
{
    public class ComplexNavigationsOwnedQuerySqlServerFixture
        : ComplexNavigationsOwnedQueryFixtureBase<SqlServerTestStore>
    {
        public static readonly string DatabaseName = "ComplexNavigationsOwned";

        private readonly DbContextOptions _options;

        private readonly string _connectionString
            = SqlServerTestStore.CreateConnectionString(DatabaseName);

        public TestSqlLoggerFactory TestSqlLoggerFactory { get; } = new TestSqlLoggerFactory();

        public ComplexNavigationsOwnedQuerySqlServerFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                .AddSingleton<ILoggerFactory>(TestSqlLoggerFactory)
                .BuildServiceProvider();

            _options = new DbContextOptionsBuilder()
                .EnableSensitiveDataLogging()
                .UseSqlServer(_connectionString, b => b.ApplyConfiguration())
                .UseInternalServiceProvider(serviceProvider)
                .EnableSensitiveDataLogging()
                .Options;
        }

        public override SqlServerTestStore CreateTestStore()
        {
            return SqlServerTestStore.GetOrCreateShared(DatabaseName, () =>
                {
                    using (var context = new ComplexNavigationsContext(_options))
                    {
                        context.Database.EnsureCreated();
                        ComplexNavigationsModelInitializer.Seed(context);
                    }
                });
        }

        public override ComplexNavigationsContext CreateContext(SqlServerTestStore testStore)
        {
            var context = new ComplexNavigationsContext(_options);

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            context.Database.UseTransaction(testStore.Transaction);

            return context;
        }

        protected override void Configure(ReferenceOwnershipBuilder<Level1, Level2> l2)
        {
            base.Configure(l2);

            l2.ForSqlServerToTable("Level2");
        }

        protected override void Configure(ReferenceOwnershipBuilder<Level2, Level3> l3)
        {
            base.Configure(l3);

            l3.ForSqlServerToTable("Level3");
        }

        protected override void Configure(ReferenceOwnershipBuilder<Level3, Level4> l4)
        {
            base.Configure(l4);

            l4.ForSqlServerToTable("Level4");
        }
    }
}
