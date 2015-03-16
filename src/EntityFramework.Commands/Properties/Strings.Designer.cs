// <auto-generated />
namespace Microsoft.Data.Entity.Commands
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
	using JetBrains.Annotations;

    public static class Strings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("EntityFramework.Commands.Strings", typeof(Strings).GetTypeInfo().Assembly);

        /// <summary>
        /// The name '{migrationName}' is used by an existing migration.
        /// </summary>
        public static string DuplicateMigrationName([CanBeNull] object migrationName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("DuplicateMigrationName", "migrationName"), migrationName);
        }

        /// <summary>
        /// More than one DbContext was found. Specify which one to use.
        /// </summary>
        public static string MultipleContexts
        {
            get { return GetString("MultipleContexts"); }
        }

        /// <summary>
        /// More than one DbContext named '{name}' was found. Specify which one to use by providing its fully qualified name.
        /// </summary>
        public static string MultipleContextsWithName([CanBeNull] object name)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("MultipleContextsWithName", "name"), name);
        }

        /// <summary>
        /// More than one DbContext named '{name}' was found. Specify which one to use by providing its fully qualified name using its exact case.
        /// </summary>
        public static string MultipleContextsWithQualifiedName([CanBeNull] object name)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("MultipleContextsWithQualifiedName", "name"), name);
        }

        /// <summary>
        /// No DbContext was found. Ensure that you're using the correct assembly and that the type is neither abstract nor generic.
        /// </summary>
        public static string NoContext
        {
            get { return GetString("NoContext"); }
        }

        /// <summary>
        /// No DbContext named '{name}' was found.
        /// </summary>
        public static string NoContextWithName([CanBeNull] object name)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("NoContextWithName", "name"), name);
        }

        /// <summary>
        /// Using context '{name}'.
        /// </summary>
        public static string LogUseContext([CanBeNull] object name)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("LogUseContext", "name"), name);
        }

        /// <summary>
        /// A manual migration deletion was detected.
        /// </summary>
        public static string ManuallyDeleted
        {
            get { return GetString("ManuallyDeleted"); }
        }

        /// <summary>
        /// No file named '{file}' was found. You must manually remove the migration class '{migrationClass}'.
        /// </summary>
        public static string NoMigrationFile([CanBeNull] object file, [CanBeNull] object migrationClass)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("NoMigrationFile", "file", "migrationClass"), file, migrationClass);
        }

        /// <summary>
        /// No file named '{file}' was found.
        /// </summary>
        public static string NoMigrationMetadataFile([CanBeNull] object file)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("NoMigrationMetadataFile", "file"), file);
        }

        /// <summary>
        /// No ModelSnapshot was found.
        /// </summary>
        public static string NoSnapshot
        {
            get { return GetString("NoSnapshot"); }
        }

        /// <summary>
        /// No file named '{file}' was found. You must manually remove the model snapshot class '{snapshotClass}'.
        /// </summary>
        public static string NoSnapshotFile([CanBeNull] object file, [CanBeNull] object snapshotClass)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("NoSnapshotFile", "file", "snapshotClass"), file, snapshotClass);
        }

        /// <summary>
        /// Removing migration '{name}'.
        /// </summary>
        public static string RemovingMigration([CanBeNull] object name)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("RemovingMigration", "name"), name);
        }

        /// <summary>
        /// Removing model snapshot.
        /// </summary>
        public static string RemovingSnapshot
        {
            get { return GetString("RemovingSnapshot"); }
        }

        /// <summary>
        /// Reverting model snapshot.
        /// </summary>
        public static string RevertingSnapshot
        {
            get { return GetString("RevertingSnapshot"); }
        }

        /// <summary>
        /// The migration '{name}' has already been applied to the database. Unapply it and try again. If the migration has been applied to other databases, consider reverting its changes using a new migration.
        /// </summary>
        public static string UnapplyMigration([CanBeNull] object name)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("UnapplyMigration", "name"), name);
        }

        /// <summary>
        /// The current CSharpMigrationOperationGenerator cannot scaffold operations of type '{operationType}'. Configure your services to use one that can.
        /// </summary>
        public static string UnknownOperation([CanBeNull] object operationType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("UnknownOperation", "operationType"), operationType);
        }

        /// <summary>
        /// The current CSharpHelper cannot scaffold literals of type '{literalType}'. Configure your services to use one that can.
        /// </summary>
        public static string UnknownLiteral([CanBeNull] object literalType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("UnknownLiteral", "literalType"), literalType);
        }

        /// <summary>
        /// Unable to find asssembly with name {assemblyName}.
        /// </summary>
        public static string CannotFindAssembly([CanBeNull] object assemblyName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("CannotFindAssembly", "assemblyName"), assemblyName);
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
