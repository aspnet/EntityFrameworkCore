// <auto-generated />

using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.SqlServer.Internal
{
    /// <summary>
    ///		This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class SqlServerStrings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Microsoft.EntityFrameworkCore.SqlServer.Properties.SqlServerStrings", typeof(SqlServerStrings).GetTypeInfo().Assembly);

        /// <summary>
        ///     Identity value generation cannot be used for the property '{property}' on entity type '{entityType}' because the property type is '{propertyType}'. Identity value generation can only be used with signed integer properties.
        /// </summary>
        public static string IdentityBadType([CanBeNull] object property, [CanBeNull] object entityType, [CanBeNull] object propertyType)
            => string.Format(
                GetString("IdentityBadType", nameof(property), nameof(entityType), nameof(propertyType)),
                property, entityType, propertyType);

        /// <summary>
        ///     Data type '{dataType}' is not supported in this form. Either specify the length explicitly in the type name, for example as '{dataType}(16)', or remove the data type and use APIs such as HasMaxLength to allow EF choose the data type.
        /// </summary>
        public static string UnqualifiedDataType([CanBeNull] object dataType)
            => string.Format(
                GetString("UnqualifiedDataType", nameof(dataType)),
                dataType);

        /// <summary>
        ///     Data type '{dataType}' for property '{property}' is not supported in this form. Either specify the length explicitly in the type name, for example as '{dataType}(16)', or remove the data type and use APIs such as HasMaxLength to allow EF choose the data type.
        /// </summary>
        public static string UnqualifiedDataTypeOnProperty([CanBeNull] object dataType, [CanBeNull] object property)
            => string.Format(
                GetString("UnqualifiedDataTypeOnProperty", nameof(dataType), nameof(property)),
                dataType, property);

        /// <summary>
        ///     SQL Server sequences cannot be used to generate values for the property '{property}' on entity type '{entityType}' because the property type is '{propertyType}'. Sequences can only be used with integer properties.
        /// </summary>
        public static string SequenceBadType([CanBeNull] object property, [CanBeNull] object entityType, [CanBeNull] object propertyType)
            => string.Format(
                GetString("SequenceBadType", nameof(property), nameof(entityType), nameof(propertyType)),
                property, entityType, propertyType);

        /// <summary>
        ///     SQL Server requires the table name to be specified for rename index operations. Specify table name in the call to MigrationBuilder.RenameIndex.
        /// </summary>
        public static string IndexTableRequired
            => GetString("IndexTableRequired");

        /// <summary>
        ///     To set memory-optimized on a table on or off the table needs to be dropped and recreated.
        /// </summary>
        public static string AlterMemoryOptimizedTable
            => GetString("AlterMemoryOptimizedTable");

        /// <summary>
        ///     To change the IDENTITY property of a column, the column needs to be dropped and recreated.
        /// </summary>
        public static string AlterIdentityColumn
            => GetString("AlterIdentityColumn");

        /// <summary>
        ///     An exception has been raised that is likely due to a transient failure. Consider enabling transient error resiliency by adding 'EnableRetryOnFailure()' to the 'UseSqlServer' call.
        /// </summary>
        public static string TransientExceptionDetected
            => GetString("TransientExceptionDetected");

        /// <summary>
        ///     The property '{property}' on entity type '{entityType}' is configured to use 'SequenceHiLo' value generator, which is only intended for keys. If this was intentional configure an alternate key on the property, otherwise call 'ValueGeneratedNever' or configure store generation for this property.
        /// </summary>
        public static string NonKeyValueGeneration([CanBeNull] object property, [CanBeNull] object entityType)
            => string.Format(
                GetString("NonKeyValueGeneration", nameof(property), nameof(entityType)),
                property, entityType);

        /// <summary>
        ///     The properties {properties} are configured to use 'Identity' value generator and are mapped to the same table '{table}'. Only one column per table can be configured as 'Identity'. Call 'ValueGeneratedNever' for properties that should not use 'Identity'.
        /// </summary>
        public static string MultipleIdentityColumns([CanBeNull] object properties, [CanBeNull] object table)
            => string.Format(
                GetString("MultipleIdentityColumns", nameof(properties), nameof(table)),
                properties, table);

        /// <summary>
        ///     Cannot use table '{table}' for entity type '{entityType}' since it is being used for entity type '{otherEntityType}' and entity type '{memoryOptimizedEntityType}' is marked as memory-optimized, but entity type '{nonMemoryOptimizedEntityType}' is not.
        /// </summary>
        public static string IncompatibleTableMemoryOptimizedMismatch([CanBeNull] object table, [CanBeNull] object entityType, [CanBeNull] object otherEntityType, [CanBeNull] object memoryOptimizedEntityType, [CanBeNull] object nonMemoryOptimizedEntityType)
            => string.Format(
                GetString("IncompatibleTableMemoryOptimizedMismatch", nameof(table), nameof(entityType), nameof(otherEntityType), nameof(memoryOptimizedEntityType), nameof(nonMemoryOptimizedEntityType)),
                table, entityType, otherEntityType, memoryOptimizedEntityType, nonMemoryOptimizedEntityType);

        /// <summary>
        ///     The database name could not be determined. To use EnsureDeleted, the connection string must specify Initial Catalog.
        /// </summary>
        public static string NoInitialCatalog
            => GetString("NoInitialCatalog");

        /// <summary>
        ///     '{entityType1}.{property1}' and '{entityType2}.{property2}' are both mapped to column '{columnName}' in '{table}' but are configured with different value generation strategies.
        /// </summary>
        public static string DuplicateColumnNameValueGenerationStrategyMismatch([CanBeNull] object entityType1, [CanBeNull] object property1, [CanBeNull] object entityType2, [CanBeNull] object property2, [CanBeNull] object columnName, [CanBeNull] object table)
            => string.Format(
                GetString("DuplicateColumnNameValueGenerationStrategyMismatch", nameof(entityType1), nameof(property1), nameof(entityType2), nameof(property2), nameof(columnName), nameof(table)),
                entityType1, property1, entityType2, property2, columnName, table);

        /// <summary>
        ///     The specified table '{table}' is not valid. Specify tables using the format '[schema].[table]'.
        /// </summary>
        public static string InvalidTableToIncludeInScaffolding([CanBeNull] object table)
            => string.Format(
                GetString("InvalidTableToIncludeInScaffolding", nameof(table)),
                table);

        /// <summary>
        ///     The 'FreeText' method is not supported because the query has switched to client-evaluation. Inspect the log to determine which query expressions are triggering client-evaluation.
        /// </summary>
        public static string FreeTextFunctionOnClient
            => GetString("FreeTextFunctionOnClient");

        /// <summary>
        ///     The expression passed to the 'propertyReference' parameter of the 'FreeText' method is not a valid reference to a property. The expression should represent a reference to a full-text indexed property on the object referenced in the from clause: 'from e in context.Entities where EF.Functions.FreeText(e.SomeProperty, textToSearchFor) select e'
        /// </summary>
        public static string InvalidColumnNameForFreeText
            => GetString("InvalidColumnNameForFreeText");

        /// <summary>
        ///     Include property '{entityType}.{property}' cannot be defined multiple times
        /// </summary>
        public static string IncludePropertyDuplicated([CanBeNull] object entityType, [CanBeNull] object property)
            => string.Format(
                GetString("IncludePropertyDuplicated", nameof(entityType), nameof(property)),
                entityType, property);

        /// <summary>
        ///     Include property '{entityType}.{property}' is already included in the index
        /// </summary>
        public static string IncludePropertyInIndex([CanBeNull] object entityType, [CanBeNull] object property)
            => string.Format(
                GetString("IncludePropertyInIndex", nameof(entityType), nameof(property)),
                entityType, property);

        /// <summary>
        ///     Include property '{entityType}.{property}' not found
        /// </summary>
        public static string IncludePropertyNotFound([CanBeNull] object entityType, [CanBeNull] object property)
            => string.Format(
                GetString("IncludePropertyNotFound", nameof(entityType), nameof(property)),
                entityType, property);

        /// <summary>
        ///     The 'Contains' method is not supported because the query has switched to client-evaluation. Inspect the log to determine which query expressions are triggering client-evaluation.
        /// </summary>
        public static string ContainsFunctionOnClient
            => GetString("ContainsFunctionOnClient");

        /// <summary>
        ///     The keys {key1} on '{entityType1}' and {key2} on '{entityType2}' are both mapped to '{table}.{keyName}' but with different clustering.
        /// </summary>
        public static string DuplicateKeyMismatchedClustering([CanBeNull] object key1, [CanBeNull] object entityType1, [CanBeNull] object key2, [CanBeNull] object entityType2, [CanBeNull] object table, [CanBeNull] object keyName)
            => string.Format(
                GetString("DuplicateKeyMismatchedClustering", nameof(key1), nameof(entityType1), nameof(key2), nameof(entityType2), nameof(table), nameof(keyName)),
                key1, entityType1, key2, entityType2, table, keyName);

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);
            for (var i = 0; i < formatterNames.Length; i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }

            return value;
        }
    }
}

namespace Microsoft.EntityFrameworkCore.SqlServer.Internal
{
    /// <summary>
    ///		This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class SqlServerResources
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Microsoft.EntityFrameworkCore.SqlServer.Properties.SqlServerStrings", typeof(SqlServerResources).GetTypeInfo().Assembly);

        /// <summary>
        ///     No type was specified for the decimal column '{property}' on entity type '{entityType}'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values using 'HasColumnType()'.
        /// </summary>
        public static EventDefinition<string, string> LogDefaultDecimalTypeColumn([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogDefaultDecimalTypeColumn;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogDefaultDecimalTypeColumn,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqlServerEventId.DecimalTypeDefaultWarning,
                        LogLevel.Warning,
                        "SqlServerEventId.DecimalTypeDefaultWarning",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqlServerEventId.DecimalTypeDefaultWarning,
                            _resourceManager.GetString("LogDefaultDecimalTypeColumn"))));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     The property '{property}' on entity type '{entityType}' is of type 'byte', but is set up to use a SQL Server identity column. This requires that values starting at 255 and counting down will be used for temporary key values. A temporary key value is needed for every entity inserted in a single call to 'SaveChanges'. Care must be taken that these values do not collide with real key values.
        /// </summary>
        public static EventDefinition<string, string> LogByteIdentityColumn([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogByteIdentityColumn;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogByteIdentityColumn,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqlServerEventId.ByteIdentityColumnWarning,
                        LogLevel.Warning,
                        "SqlServerEventId.ByteIdentityColumnWarning",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqlServerEventId.ByteIdentityColumnWarning,
                            _resourceManager.GetString("LogByteIdentityColumn"))));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     Found default schema {defaultSchema}.
        /// </summary>
        public static EventDefinition<string> LogFoundDefaultSchema([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundDefaultSchema;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundDefaultSchema,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqlServerEventId.DefaultSchemaFound,
                        LogLevel.Debug,
                        "SqlServerEventId.DefaultSchemaFound",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqlServerEventId.DefaultSchemaFound,
                            _resourceManager.GetString("LogFoundDefaultSchema"))));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     Found type alias with name: {alias} which maps to underlying data type {dataType}.
        /// </summary>
        public static EventDefinition<string, string> LogFoundTypeAlias([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundTypeAlias;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundTypeAlias,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqlServerEventId.TypeAliasFound,
                        LogLevel.Debug,
                        "SqlServerEventId.TypeAliasFound",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqlServerEventId.TypeAliasFound,
                            _resourceManager.GetString("LogFoundTypeAlias"))));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     Found column with table: {tableName}, column name: {columnName}, ordinal: {ordinal}, data type: {dataType}, maximum length: {maxLength}, precision: {precision}, scale: {scale}, nullable: {isNullable}, identity: {isIdentity}, default value: {defaultValue}, computed value: {computedValue}
        /// </summary>
        public static FallbackEventDefinition LogFoundColumn([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundColumn;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundColumn,
                    () => new FallbackEventDefinition(
                        logger.Options,
                        SqlServerEventId.ColumnFound,
                        LogLevel.Debug,
                        "SqlServerEventId.ColumnFound",
                        _resourceManager.GetString("LogFoundColumn")));
            }

            return (FallbackEventDefinition)definition;
        }

        /// <summary>
        ///     Found foreign key on table: {tableName}, name: {foreignKeyName}, principal table: {principalTableName}, delete action: {deleteAction}.
        /// </summary>
        public static EventDefinition<string, string, string, string> LogFoundForeignKey([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundForeignKey;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundForeignKey,
                    () => new EventDefinition<string, string, string, string>(
                        logger.Options,
                        SqlServerEventId.ForeignKeyFound,
                        LogLevel.Debug,
                        "SqlServerEventId.ForeignKeyFound",
                        level => LoggerMessage.Define<string, string, string, string>(
                            level,
                            SqlServerEventId.ForeignKeyFound,
                            _resourceManager.GetString("LogFoundForeignKey"))));
            }

            return (EventDefinition<string, string, string, string>)definition;
        }

        /// <summary>
        ///     For foreign key {fkName} on table {tableName}, unable to model the end of the foreign key on principal table {principaltableName}. This is usually because the principal table was not included in the selection set.
        /// </summary>
        public static EventDefinition<string, string, string> LogPrincipalTableNotInSelectionSet([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogPrincipalTableNotInSelectionSet;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogPrincipalTableNotInSelectionSet,
                    () => new EventDefinition<string, string, string>(
                        logger.Options,
                        SqlServerEventId.ForeignKeyReferencesMissingPrincipalTableWarning,
                        LogLevel.Warning,
                        "SqlServerEventId.ForeignKeyReferencesMissingPrincipalTableWarning",
                        level => LoggerMessage.Define<string, string, string>(
                            level,
                            SqlServerEventId.ForeignKeyReferencesMissingPrincipalTableWarning,
                            _resourceManager.GetString("LogPrincipalTableNotInSelectionSet"))));
            }

            return (EventDefinition<string, string, string>)definition;
        }

        /// <summary>
        ///     Unable to find a schema in the database matching the selected schema {schema}.
        /// </summary>
        public static EventDefinition<string> LogMissingSchema([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogMissingSchema;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogMissingSchema,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqlServerEventId.MissingSchemaWarning,
                        LogLevel.Warning,
                        "SqlServerEventId.MissingSchemaWarning",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqlServerEventId.MissingSchemaWarning,
                            _resourceManager.GetString("LogMissingSchema"))));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     Unable to find a table in the database matching the selected table {table}.
        /// </summary>
        public static EventDefinition<string> LogMissingTable([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogMissingTable;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogMissingTable,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqlServerEventId.MissingTableWarning,
                        LogLevel.Warning,
                        "SqlServerEventId.MissingTableWarning",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqlServerEventId.MissingTableWarning,
                            _resourceManager.GetString("LogMissingTable"))));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     Found sequence name: {name}, data type: {dataType}, cyclic: {isCyclic}, increment: {increment}, start: {start}, minimum: {min}, maximum: {max}.
        /// </summary>
        public static FallbackEventDefinition LogFoundSequence([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundSequence;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundSequence,
                    () => new FallbackEventDefinition(
                        logger.Options,
                        SqlServerEventId.SequenceFound,
                        LogLevel.Debug,
                        "SqlServerEventId.SequenceFound",
                        _resourceManager.GetString("LogFoundSequence")));
            }

            return (FallbackEventDefinition)definition;
        }

        /// <summary>
        ///     Found table with name: {name}.
        /// </summary>
        public static EventDefinition<string> LogFoundTable([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundTable;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundTable,
                    () => new EventDefinition<string>(
                        logger.Options,
                        SqlServerEventId.TableFound,
                        LogLevel.Debug,
                        "SqlServerEventId.TableFound",
                        level => LoggerMessage.Define<string>(
                            level,
                            SqlServerEventId.TableFound,
                            _resourceManager.GetString("LogFoundTable"))));
            }

            return (EventDefinition<string>)definition;
        }

        /// <summary>
        ///     Found index with name: {indexName}, table: {tableName}, is unique: {isUnique}.
        /// </summary>
        public static EventDefinition<string, string, bool> LogFoundIndex([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundIndex;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundIndex,
                    () => new EventDefinition<string, string, bool>(
                        logger.Options,
                        SqlServerEventId.IndexFound,
                        LogLevel.Debug,
                        "SqlServerEventId.IndexFound",
                        level => LoggerMessage.Define<string, string, bool>(
                            level,
                            SqlServerEventId.IndexFound,
                            _resourceManager.GetString("LogFoundIndex"))));
            }

            return (EventDefinition<string, string, bool>)definition;
        }

        /// <summary>
        ///     Found primary key with name: {primaryKeyName}, table: {tableName}.
        /// </summary>
        public static EventDefinition<string, string> LogFoundPrimaryKey([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundPrimaryKey;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundPrimaryKey,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqlServerEventId.PrimaryKeyFound,
                        LogLevel.Debug,
                        "SqlServerEventId.PrimaryKeyFound",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqlServerEventId.PrimaryKeyFound,
                            _resourceManager.GetString("LogFoundPrimaryKey"))));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     Found unique constraint with name: {uniqueConstraintName}, table: {tableName}.
        /// </summary>
        public static EventDefinition<string, string> LogFoundUniqueConstraint([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundUniqueConstraint;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogFoundUniqueConstraint,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqlServerEventId.UniqueConstraintFound,
                        LogLevel.Debug,
                        "SqlServerEventId.UniqueConstraintFound",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqlServerEventId.UniqueConstraintFound,
                            _resourceManager.GetString("LogFoundUniqueConstraint"))));
            }

            return (EventDefinition<string, string>)definition;
        }

        /// <summary>
        ///     For foreign key {foreignKeyName} on table {tableName}, unable to find the column called {principalColumnName} on the foreign key's principal table, {principaltableName}. Skipping foreign key.
        /// </summary>
        public static EventDefinition<string, string, string, string> LogPrincipalColumnNotFound([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogPrincipalColumnNotFound;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogPrincipalColumnNotFound,
                    () => new EventDefinition<string, string, string, string>(
                        logger.Options,
                        SqlServerEventId.ForeignKeyPrincipalColumnMissingWarning,
                        LogLevel.Warning,
                        "SqlServerEventId.ForeignKeyPrincipalColumnMissingWarning",
                        level => LoggerMessage.Define<string, string, string, string>(
                            level,
                            SqlServerEventId.ForeignKeyPrincipalColumnMissingWarning,
                            _resourceManager.GetString("LogPrincipalColumnNotFound"))));
            }

            return (EventDefinition<string, string, string, string>)definition;
        }

        /// <summary>
        ///     Skipping foreign key '{foreignKeyName}' on table '{tableName}' since all of its columns reference themselves.
        /// </summary>
        public static EventDefinition<string, string> LogReflexiveConstraintIgnored([NotNull] IDiagnosticsLogger logger)
        {
            var definition = ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogReflexiveConstraintIgnored;
            if (definition == null)
            {
                definition = LazyInitializer.EnsureInitialized<EventDefinitionBase>(
                    ref ((Diagnostics.Internal.SqlServerLoggingDefinitions)logger.Definitions).LogReflexiveConstraintIgnored,
                    () => new EventDefinition<string, string>(
                        logger.Options,
                        SqlServerEventId.ReflexiveConstraintIgnored,
                        LogLevel.Debug,
                        "SqlServerEventId.ReflexiveConstraintIgnored",
                        level => LoggerMessage.Define<string, string>(
                            level,
                            SqlServerEventId.ReflexiveConstraintIgnored,
                            _resourceManager.GetString("LogReflexiveConstraintIgnored"))));
            }

            return (EventDefinition<string, string>)definition;
        }
    }
}
