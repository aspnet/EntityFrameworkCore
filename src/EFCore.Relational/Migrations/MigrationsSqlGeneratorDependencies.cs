// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.EntityFrameworkCore.Migrations
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="MigrationsSqlGenerator" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    ///     <para>
    ///         Do not construct instances of this class directly from either provider or application code as the
    ///         constructor signature may change as new dependencies are added. Instead, use this type in
    ///         your constructor so that an instance will be created and injected automatically by the
    ///         dependency injection container. To create an instance with some dependent services replaced,
    ///         first resolve the object from the dependency injection container, then replace selected
    ///         services using the 'With...' methods. Do not call the constructor at any point in this process.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Scoped" />. This means that each
    ///         <see cref="DbContext" /> instance will use its own instance of this service.
    ///         The implementation may depend on other services registered with any lifetime.
    ///         The implementation does not need to be thread-safe.
    ///     </para>
    /// </summary>
    public sealed class MigrationsSqlGeneratorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="MigrationsSqlGenerator" />.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from either provider or application code as it may change
        ///         as new dependencies are added. Instead, use this type in your constructor so that an instance
        ///         will be created and injected automatically by the dependency injection container. To create
        ///         an instance with some dependent services replaced, first resolve the object from the dependency
        ///         injection container, then replace selected services using the 'With...' methods. Do not call
        ///         the constructor at any point in this process.
        ///     </para>
        ///     <para>
        ///         This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///         the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///         any release. You should only use it directly in your code with extreme caution and knowing that
        ///         doing so can result in application failures when updating to a new Entity Framework Core release.
        ///     </para>
        /// </summary>
        [EntityFrameworkInternal]
        public MigrationsSqlGeneratorDependencies(
            [NotNull] IRelationalCommandBuilderFactory commandBuilderFactory,
            [NotNull] IUpdateSqlGenerator updateSqlGenerator,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper,
            [NotNull] IRelationalTypeMappingSource typeMappingSource,
            [NotNull] ICurrentDbContext currentContext,
            [NotNull] ILoggingOptions loggingOptions,
            [NotNull] IDiagnosticsLogger<DbLoggerCategory.Migrations> logger,
            [NotNull] IDiagnosticsLogger<DbLoggerCategory.Database.Command> commandLogger,
            [NotNull] IMigrationsAnnotationProvider migrationsAnnotations,
            [NotNull] IRelationalAnnotationProvider relationalAnnotations)
        {
            Check.NotNull(commandBuilderFactory, nameof(commandBuilderFactory));
            Check.NotNull(updateSqlGenerator, nameof(updateSqlGenerator));
            Check.NotNull(sqlGenerationHelper, nameof(sqlGenerationHelper));
            Check.NotNull(typeMappingSource, nameof(typeMappingSource));
            Check.NotNull(currentContext, nameof(currentContext));
            Check.NotNull(loggingOptions, nameof(loggingOptions));
            Check.NotNull(logger, nameof(logger));
            Check.NotNull(commandLogger, nameof(commandLogger));
            Check.NotNull(migrationsAnnotations, nameof(migrationsAnnotations));
            Check.NotNull(relationalAnnotations, nameof(relationalAnnotations));

            CommandBuilderFactory = commandBuilderFactory;
            SqlGenerationHelper = sqlGenerationHelper;
            UpdateSqlGenerator = updateSqlGenerator;
            TypeMappingSource = typeMappingSource;
            CurrentContext = currentContext;
            LoggingOptions = loggingOptions;
            Logger = logger;
            CommandLogger = commandLogger;
            MigrationsAnnotations = migrationsAnnotations;
            RelationalAnnotations = relationalAnnotations;
        }

        /// <summary>
        ///     The command builder factory.
        /// </summary>
        public IRelationalCommandBuilderFactory CommandBuilderFactory { get; }

        /// <summary>
        ///     High level SQL generator.
        /// </summary>
        public IUpdateSqlGenerator UpdateSqlGenerator { get; }

        /// <summary>
        ///     Helpers for SQL generation.
        /// </summary>
        public ISqlGenerationHelper SqlGenerationHelper { get; }

        /// <summary>
        ///     The type mapper.
        /// </summary>
        public IRelationalTypeMappingSource TypeMappingSource { get; }

        /// <summary>
        ///     Contains the <see cref="DbContext" /> currently in use.
        /// </summary>
        public ICurrentDbContext CurrentContext { get; }

        /// <summary>
        ///     The logging options.
        /// </summary>
        public ILoggingOptions LoggingOptions { get; }

        /// <summary>
        ///     The database command logger.
        ///     A logger for commands.
        /// </summary>
        public IDiagnosticsLogger<DbLoggerCategory.Database.Command> CommandLogger { get; }

        /// <summary>
        ///     A logger for migrations.
        /// </summary>
        public IDiagnosticsLogger<DbLoggerCategory.Migrations> Logger { get; }

        /// <summary>
        ///     The migrations annotations to use.
        /// </summary>
        public IMigrationsAnnotationProvider MigrationsAnnotations { get; }

        /// <summary>
        ///     The relational annotations to use.
        /// </summary>
        public IRelationalAnnotationProvider RelationalAnnotations { get; }

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="commandBuilderFactory"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] IRelationalCommandBuilderFactory commandBuilderFactory)
            => new MigrationsSqlGeneratorDependencies(
                commandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                LoggingOptions,
                Logger,
                CommandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="updateSqlGenerator"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] IUpdateSqlGenerator updateSqlGenerator)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                updateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                LoggingOptions,
                Logger,
                CommandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="sqlGenerationHelper"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] ISqlGenerationHelper sqlGenerationHelper)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                sqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                LoggingOptions,
                Logger,
                CommandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="typeMappingSource"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] IRelationalTypeMappingSource typeMappingSource)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                typeMappingSource,
                CurrentContext,
                LoggingOptions,
                Logger,
                CommandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="currentContext"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] ICurrentDbContext currentContext)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                currentContext,
                LoggingOptions,
                Logger,
                CommandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="loggingOptions"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] ILoggingOptions loggingOptions)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                loggingOptions,
                Logger,
                CommandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="logger"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] IDiagnosticsLogger<DbLoggerCategory.Migrations> logger)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                LoggingOptions,
                logger,
                CommandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="commandLogger"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] IDiagnosticsLogger<DbLoggerCategory.Database.Command> commandLogger)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                LoggingOptions,
                Logger,
                commandLogger,
                MigrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="migrationsAnnotations"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] IMigrationsAnnotationProvider migrationsAnnotations)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                LoggingOptions,
                Logger,
                CommandLogger,
                migrationsAnnotations,
                RelationalAnnotations);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="relationalAnnotations"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public MigrationsSqlGeneratorDependencies With([NotNull] IRelationalAnnotationProvider relationalAnnotations)
            => new MigrationsSqlGeneratorDependencies(
                CommandBuilderFactory,
                UpdateSqlGenerator,
                SqlGenerationHelper,
                TypeMappingSource,
                CurrentContext,
                LoggingOptions,
                Logger,
                CommandLogger,
                MigrationsAnnotations,
                relationalAnnotations);
    }
}
