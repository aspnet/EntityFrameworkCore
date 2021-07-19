// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.EntityFrameworkCore.ValueGeneration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     SQL Server specific extension methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class SqlServerServiceCollectionExtensions
    {
        /// <summary>
        ///     <para>
        ///         Adds the services required by the Microsoft SQL Server database provider for Entity Framework
        ///         to an <see cref="IServiceCollection" />.
        ///     </para>
        ///     <para>
        ///         Calling this method is no longer necessary when building most applications, including those that
        ///         use dependency injection in ASP.NET or elsewhere.
        ///         It is only needed when building the internal service provider for use with
        ///         the <see cref="DbContextOptionsBuilder.UseInternalServiceProvider" /> method.
        ///         This is not recommend other than for some advanced scenarios.
        ///     </para>
        /// </summary>
        /// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
        /// <returns>
        ///     The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddEntityFrameworkSqlServer(this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<LoggingDefinitions, SqlServerLoggingDefinitions>()
                .TryAdd<IDatabaseProvider, DatabaseProvider<SqlServerOptionsExtension>>()
                .TryAdd<IValueGeneratorCache>(p => p.GetRequiredService<ISqlServerValueGeneratorCache>())
                .TryAdd<IRelationalTypeMappingSource, SqlServerTypeMappingSource>()
                .TryAdd<ISqlGenerationHelper, SqlServerSqlGenerationHelper>()
                .TryAdd<IRelationalAnnotationProvider, SqlServerAnnotationProvider>()
                .TryAdd<IMigrationsAnnotationProvider, SqlServerMigrationsAnnotationProvider>()
                .TryAdd<IModelValidator, SqlServerModelValidator>()
                .TryAdd<IProviderConventionSetBuilder, SqlServerConventionSetBuilder>()
                .TryAdd<IUpdateSqlGenerator>(p => p.GetRequiredService<ISqlServerUpdateSqlGenerator>())
                .TryAdd<IEvaluatableExpressionFilter, SqlServerEvaluatableExpressionFilter>()
                .TryAdd<IRelationalTransactionFactory, SqlServerTransactionFactory>()
                .TryAdd<IModificationCommandBatchFactory, SqlServerModificationCommandBatchFactory>()
                .TryAdd<IValueGeneratorSelector, SqlServerValueGeneratorSelector>()
                .TryAdd<IRelationalConnection>(p => p.GetRequiredService<ISqlServerConnection>())
                .TryAdd<IMigrationsSqlGenerator, SqlServerMigrationsSqlGenerator>()
                .TryAdd<IRelationalDatabaseCreator, SqlServerDatabaseCreator>()
                .TryAdd<IHistoryRepository, SqlServerHistoryRepository>()
                .TryAdd<IExecutionStrategyFactory, SqlServerExecutionStrategyFactory>()
                .TryAdd<IRelationalQueryStringFactory, SqlServerQueryStringFactory>()
                .TryAdd<ICompiledQueryCacheKeyGenerator, SqlServerCompiledQueryCacheKeyGenerator>()
                .TryAdd<IQueryCompilationContextFactory, SqlServerQueryCompilationContextFactory>()
                .TryAdd<IMethodCallTranslatorProvider, SqlServerMethodCallTranslatorProvider>()
                .TryAdd<IMemberTranslatorProvider, SqlServerMemberTranslatorProvider>()
                .TryAdd<IQuerySqlGeneratorFactory, SqlServerQuerySqlGeneratorFactory>()
                .TryAdd<IRelationalSqlTranslatingExpressionVisitorFactory, SqlServerSqlTranslatingExpressionVisitorFactory>()
                .TryAdd<IRelationalParameterBasedSqlProcessorFactory, SqlServerParameterBasedSqlProcessorFactory>()
                .TryAdd<IQueryRootCreator, SqlServerQueryRootCreator>()
                .TryAdd<IQueryableMethodTranslatingExpressionVisitorFactory, SqlServerQueryableMethodTranslatingExpressionVisitorFactory>()
                .TryAddProviderSpecificServices(
                    b => b
                        .TryAddSingleton<ISqlServerValueGeneratorCache, SqlServerValueGeneratorCache>()
                        .TryAddSingleton<ISqlServerUpdateSqlGenerator, SqlServerUpdateSqlGenerator>()
                        .TryAddSingleton<ISqlServerSequenceValueGeneratorFactory, SqlServerSequenceValueGeneratorFactory>()
                        .TryAddScoped<ISqlServerConnection, SqlServerConnection>())
                .TryAddCoreServices();

            return serviceCollection;
        }
    }
}
