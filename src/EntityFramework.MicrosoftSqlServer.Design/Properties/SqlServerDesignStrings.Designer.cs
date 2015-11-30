// <auto-generated />
namespace Microsoft.Data.Entity.Internal
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using JetBrains.Annotations;

    public static class SqlServerDesignStrings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("EntityFramework.MicrosoftSqlServer.Design.SqlServerDesignStrings", typeof(SqlServerDesignStrings).GetTypeInfo().Assembly);

        /// <summary>
        /// Unable to interpret the string {sqlServerStringLiteral} as a SQLServer string literal.
        /// </summary>
        public static string CannotInterpretSqlServerStringLiteral([CanBeNull] object sqlServerStringLiteral)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("CannotInterpretSqlServerStringLiteral", "sqlServerStringLiteral"), sqlServerStringLiteral);
        }

        /// <summary>
        /// For column {columnId}. This column is set up as an Identity column, but the SQL Server data type is {sqlServerDataType}. This will be mapped to CLR type byte which does not allow the SqlServerValueGenerationStrategy.IdentityColumn setting. Generating a matching Property but ignoring the Identity setting.
        /// </summary>
        public static string DataTypeDoesNotAllowSqlServerIdentityStrategy([CanBeNull] object columnId, [CanBeNull] object sqlServerDataType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("DataTypeDoesNotAllowSqlServerIdentityStrategy", "columnId", "sqlServerDataType"), columnId, sqlServerDataType);
        }

        /// <summary>
        /// For column {columnId} unable to convert default value {defaultValue} into type {propertyType}. Will not generate code setting a default value for the property {propertyName} on entity type {entityTypeName}.
        /// </summary>
        public static string UnableToConvertDefaultValue([CanBeNull] object columnId, [CanBeNull] object defaultValue, [CanBeNull] object propertyType, [CanBeNull] object propertyName, [CanBeNull] object entityTypeName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("UnableToConvertDefaultValue", "columnId", "defaultValue", "propertyType", "propertyName", "entityTypeName"), columnId, defaultValue, propertyType, propertyName, entityTypeName);
        }

        /// <summary>
        /// For index {indexName}. Unable to find table [{schemaName}].[{tablename}]. Skipping column.
        /// </summary>
        public static string UnableToFindIndexForColumn([CanBeNull] object indexName, [CanBeNull] object schemaName, [CanBeNull] object tablename)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("UnableToFindIndexForColumn", "indexName", "schemaName", "tablename"), indexName, schemaName, tablename);
        }

        /// <summary>
        /// For column {columnName}. Unable to find table [{schemaName}].[{tablename}]. Skipping column.
        /// </summary>
        public static string UnableToFindTableForColumn([CanBeNull] object columnName, [CanBeNull] object schemaName, [CanBeNull] object tablename)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("UnableToFindTableForColumn", "columnName", "schemaName", "tablename"), columnName, schemaName, tablename);
        }

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);

            Debug.Assert(value != null);

            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }

            return value;
        }
    }
}
