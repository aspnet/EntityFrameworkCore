// <auto-generated />
namespace Microsoft.Data.Entity.AzureTableStorage
{
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    internal static class Strings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Microsoft.Data.Entity.AzureTableStorage.Strings", typeof(Strings).GetTypeInfo().Assembly);

        /// <summary>
        /// The string argument '{argumentName}' cannot be empty.
        /// </summary>
        internal static string ArgumentIsEmpty
        {
            get { return GetString("ArgumentIsEmpty"); }
        }

        /// <summary>
        /// The string argument '{argumentName}' cannot be empty.
        /// </summary>
        internal static string FormatArgumentIsEmpty(object argumentName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("ArgumentIsEmpty", "argumentName"), argumentName);
        }

        /// <summary>
        /// The collection argument '{argumentName}' must contain at least one element.
        /// </summary>
        internal static string CollectionArgumentIsEmpty
        {
            get { return GetString("CollectionArgumentIsEmpty"); }
        }

        /// <summary>
        /// The collection argument '{argumentName}' must contain at least one element.
        /// </summary>
        internal static string FormatCollectionArgumentIsEmpty(object argumentName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("CollectionArgumentIsEmpty", "argumentName"), argumentName);
        }

        /// <summary>
        /// The value provided for argument '{argumentName}' must be a valid value of enum type '{enumType}'.
        /// </summary>
        internal static string InvalidEnumValue
        {
            get { return GetString("InvalidEnumValue"); }
        }

        /// <summary>
        /// The value provided for argument '{argumentName}' must be a valid value of enum type '{enumType}'.
        /// </summary>
        internal static string FormatInvalidEnumValue(object argumentName, object enumType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("InvalidEnumValue", "argumentName", "enumType"), argumentName, enumType);
        }

        /// <summary>
        /// Cannot modify an Azure Storage account from within the Entity Framework
        /// </summary>
        internal static string CannotModifyAccount
        {
            get { return GetString("CannotModifyAccount"); }
        }

        /// <summary>
        /// Cannot modify an Azure Storage account from within the Entity Framework
        /// </summary>
        internal static string FormatCannotModifyAccount()
        {
            return GetString("CannotModifyAccount");
        }

        /// <summary>
        /// Cannot access a public setter and getter for the property '{propertyName}' of type '{typeName}'
        /// </summary>
        internal static string InvalidPoco
        {
            get { return GetString("InvalidPoco"); }
        }

        /// <summary>
        /// Cannot access a public setter and getter for the property '{propertyName}' of type '{typeName}'
        /// </summary>
        internal static string FormatInvalidPoco(object propertyName, object typeName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("InvalidPoco", "propertyName", "typeName"), propertyName, typeName);
        }

        /// <summary>
        /// This database cannot be used as an AzureTableStorageDatabase
        /// </summary>
        internal static string AtsDatabaseNotInUse
        {
            get { return GetString("AtsDatabaseNotInUse"); }
        }

        /// <summary>
        /// This database cannot be used as an AzureTableStorageDatabase
        /// </summary>
        internal static string FormatAtsDatabaseNotInUse()
        {
            return GetString("AtsDatabaseNotInUse");
        }

        /// <summary>
        /// Cannot read value of type '{typeName}' from '{accessName}'
        /// </summary>
        internal static string InvalidReadType
        {
            get { return GetString("InvalidReadType"); }
        }

        /// <summary>
        /// Cannot read value of type '{typeName}' from '{accessName}'
        /// </summary>
        internal static string FormatInvalidReadType(object typeName, object accessName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("InvalidReadType", "typeName", "accessName"), typeName, accessName);
        }

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);

            System.Diagnostics.Debug.Assert(value != null);
    
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
