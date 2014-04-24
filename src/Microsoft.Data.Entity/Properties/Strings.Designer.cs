// <auto-generated />
namespace Microsoft.Data.Entity
{
    using System.Globalization;
    using System.Reflection;
    using System.Resources;

    internal static class Strings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Microsoft.Data.Entity.Strings", typeof(Strings).GetTypeInfo().Assembly);

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
        /// A circular model foreign key dependency was detected: {cycle}.
        /// </summary>
        internal static string CircularDependency
        {
            get { return GetString("CircularDependency"); }
        }

        /// <summary>
        /// A circular model foreign key dependency was detected: {cycle}.
        /// </summary>
        internal static string FormatCircularDependency(object cycle)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("CircularDependency", "cycle"), cycle);
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
        /// The properties expression '{expression}' is not valid. The expression should represent a property access: 't =&gt; t.MyProperty'. When specifying multiple properties use an anonymous type: 't =&gt; new {{ t.MyProperty1, t.MyProperty2 }}'.
        /// </summary>
        internal static string InvalidPropertiesExpression
        {
            get { return GetString("InvalidPropertiesExpression"); }
        }

        /// <summary>
        /// The properties expression '{expression}' is not valid. The expression should represent a property access: 't =&gt; t.MyProperty'. When specifying multiple properties use an anonymous type: 't =&gt; new {{ t.MyProperty1, t.MyProperty2 }}'.
        /// </summary>
        internal static string FormatInvalidPropertiesExpression(object expression)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("InvalidPropertiesExpression", "expression"), expression);
        }

        /// <summary>
        /// The expression '{expression}' is not a valid property expression. The expression should represent a property access: 't =&gt; t.MyProperty'.
        /// </summary>
        internal static string InvalidPropertyExpression
        {
            get { return GetString("InvalidPropertyExpression"); }
        }

        /// <summary>
        /// The expression '{expression}' is not a valid property expression. The expression should represent a property access: 't =&gt; t.MyProperty'.
        /// </summary>
        internal static string FormatInvalidPropertyExpression(object expression)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("InvalidPropertyExpression", "expression"), expression);
        }

        /// <summary>
        /// A service of type '{serviceType}' has not been configured. Either configure the service explicitly, or ensure one is available from the current IServiceProvider.
        /// </summary>
        internal static string MissingConfigurationItem
        {
            get { return GetString("MissingConfigurationItem"); }
        }

        /// <summary>
        /// A service of type '{serviceType}' has not been configured. Either configure the service explicitly, or ensure one is available from the current IServiceProvider.
        /// </summary>
        internal static string FormatMissingConfigurationItem(object serviceType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("MissingConfigurationItem", "serviceType"), serviceType);
        }

        /// <summary>
        /// The instance of entity type '{entityType}' cannot be tracked because another instance of this type with the same key is already being tracked. For new entities consider using an IIdentityGenerator to generate unique key values.
        /// </summary>
        internal static string IdentityConflict
        {
            get { return GetString("IdentityConflict"); }
        }

        /// <summary>
        /// The instance of entity type '{entityType}' cannot be tracked because another instance of this type with the same key is already being tracked. For new entities consider using an IIdentityGenerator to generate unique key values.
        /// </summary>
        internal static string FormatIdentityConflict(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("IdentityConflict", "entityType"), entityType);
        }

        /// <summary>
        /// Cannot start tracking StateEntry for entity type '{entityType}' because it was created by a different StateManager instance.
        /// </summary>
        internal static string WrongStateManager
        {
            get { return GetString("WrongStateManager"); }
        }

        /// <summary>
        /// Cannot start tracking StateEntry for entity type '{entityType}' because it was created by a different StateManager instance.
        /// </summary>
        internal static string FormatWrongStateManager(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("WrongStateManager", "entityType"), entityType);
        }

        /// <summary>
        /// Cannot start tracking StateEntry for entity type '{entityType}' because another StateEntry is already tracking the same entity.
        /// </summary>
        internal static string MultipleStateEntries
        {
            get { return GetString("MultipleStateEntries"); }
        }

        /// <summary>
        /// Cannot start tracking StateEntry for entity type '{entityType}' because another StateEntry is already tracking the same entity.
        /// </summary>
        internal static string FormatMultipleStateEntries(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("MultipleStateEntries", "entityType"), entityType);
        }

        /// <summary>
        /// The entity type '{entityType}' was not found. Ensure that the entity type '{entityType}' has been added to the model.
        /// </summary>
        internal static string EntityTypeNotFound
        {
            get { return GetString("EntityTypeNotFound"); }
        }

        /// <summary>
        /// The entity type '{entityType}' was not found. Ensure that the entity type '{entityType}' has been added to the model.
        /// </summary>
        internal static string FormatEntityTypeNotFound(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("EntityTypeNotFound", "entityType"), entityType);
        }

        /// <summary>
        /// The property '{property}' on entity type '{entityType}' could not be found. Ensure that the property exists and has been included in the model.
        /// </summary>
        internal static string PropertyNotFound
        {
            get { return GetString("PropertyNotFound"); }
        }

        /// <summary>
        /// The property '{property}' on entity type '{entityType}' could not be found. Ensure that the property exists and has been included in the model.
        /// </summary>
        internal static string FormatPropertyNotFound(object property, object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("PropertyNotFound", "property", "entityType"), property, entityType);
        }

        /// <summary>
        /// Sequence contains no elements.
        /// </summary>
        internal static string EmptySequence
        {
            get { return GetString("EmptySequence"); }
        }

        /// <summary>
        /// Sequence contains no elements.
        /// </summary>
        internal static string FormatEmptySequence()
        {
            return GetString("EmptySequence");
        }

        /// <summary>
        /// Sequence contains more than one element.
        /// </summary>
        internal static string MoreThanOneElement
        {
            get { return GetString("MoreThanOneElement"); }
        }

        /// <summary>
        /// Sequence contains more than one element.
        /// </summary>
        internal static string FormatMoreThanOneElement()
        {
            return GetString("MoreThanOneElement");
        }

        /// <summary>
        /// Sequence contains more than one matching element.
        /// </summary>
        internal static string MoreThanOneMatch
        {
            get { return GetString("MoreThanOneMatch"); }
        }

        /// <summary>
        /// Sequence contains more than one matching element.
        /// </summary>
        internal static string FormatMoreThanOneMatch()
        {
            return GetString("MoreThanOneMatch");
        }

        /// <summary>
        /// Sequence contains no matching element.
        /// </summary>
        internal static string NoMatch
        {
            get { return GetString("NoMatch"); }
        }

        /// <summary>
        /// Sequence contains no matching element.
        /// </summary>
        internal static string FormatNoMatch()
        {
            return GetString("NoMatch");
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
        /// The properties provided for the argument '{argumentName}' are declared on different entity types.
        /// </summary>
        internal static string InconsistentEntityType
        {
            get { return GetString("InconsistentEntityType"); }
        }

        /// <summary>
        /// The properties provided for the argument '{argumentName}' are declared on different entity types.
        /// </summary>
        internal static string FormatInconsistentEntityType(object argumentName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("InconsistentEntityType", "argumentName"), argumentName);
        }

        /// <summary>
        /// The entity type '{entityType}' requires a key to be defined.
        /// </summary>
        internal static string EntityRequiresKey
        {
            get { return GetString("EntityRequiresKey"); }
        }

        /// <summary>
        /// The entity type '{entityType}' requires a key to be defined.
        /// </summary>
        internal static string FormatEntityRequiresKey(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("EntityRequiresKey", "entityType"), entityType);
        }

        /// <summary>
        /// The specified key properties are not declared on the entity type '{entityType}'. Ensure key properties are declared on the target entity type.
        /// </summary>
        internal static string KeyPropertiesWrongEntity
        {
            get { return GetString("KeyPropertiesWrongEntity"); }
        }

        /// <summary>
        /// The specified key properties are not declared on the entity type '{entityType}'. Ensure key properties are declared on the target entity type.
        /// </summary>
        internal static string FormatKeyPropertiesWrongEntity(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("KeyPropertiesWrongEntity", "entityType"), entityType);
        }

        /// <summary>
        /// The specified foreign key properties are not declared on the entity type '{entityType}'. Ensure foreign key properties are declared on the target entity type.
        /// </summary>
        internal static string ForeignKeyPropertiesWrongEntity
        {
            get { return GetString("ForeignKeyPropertiesWrongEntity"); }
        }

        /// <summary>
        /// The specified foreign key properties are not declared on the entity type '{entityType}'. Ensure foreign key properties are declared on the target entity type.
        /// </summary>
        internal static string FormatForeignKeyPropertiesWrongEntity(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("ForeignKeyPropertiesWrongEntity", "entityType"), entityType);
        }

        /// <summary>
        /// The source IQueryable doesn't implement IAsyncEnumerable{genericParameter}. Only sources that implement IAsyncEnumerable can be used for Entity Framework asynchronous operations.
        /// </summary>
        internal static string IQueryableNotAsync
        {
            get { return GetString("IQueryableNotAsync"); }
        }

        /// <summary>
        /// The source IQueryable doesn't implement IAsyncEnumerable{genericParameter}. Only sources that implement IAsyncEnumerable can be used for Entity Framework asynchronous operations.
        /// </summary>
        internal static string FormatIQueryableNotAsync(object genericParameter)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("IQueryableNotAsync", "genericParameter"), genericParameter);
        }

        /// <summary>
        /// The provider for the source IQueryable doesn't implement IAsyncQueryProvider. Only providers that implement IEntityQueryProvider can be used for Entity Framework asynchronous operations.
        /// </summary>
        internal static string IQueryableProviderNotAsync
        {
            get { return GetString("IQueryableProviderNotAsync"); }
        }

        /// <summary>
        /// The provider for the source IQueryable doesn't implement IAsyncQueryProvider. Only providers that implement IEntityQueryProvider can be used for Entity Framework asynchronous operations.
        /// </summary>
        internal static string FormatIQueryableProviderNotAsync()
        {
            return GetString("IQueryableProviderNotAsync");
        }

        /// <summary>
        /// Lazy original value tracking cannot be turned on for entity type '{entityType}'. Entities that do not implement both INotifyPropertyChanging and INotifyPropertyChanged require original values to be stored eagerly in order to correct detect changes made to entities.
        /// </summary>
        internal static string EagerOriginalValuesRequired
        {
            get { return GetString("EagerOriginalValuesRequired"); }
        }

        /// <summary>
        /// Lazy original value tracking cannot be turned on for entity type '{entityType}'. Entities that do not implement both INotifyPropertyChanging and INotifyPropertyChanged require original values to be stored eagerly in order to correct detect changes made to entities.
        /// </summary>
        internal static string FormatEagerOriginalValuesRequired(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("EagerOriginalValuesRequired", "entityType"), entityType);
        }

        /// <summary>
        /// The original value for property '{property}' of entity type '{entityType}' cannot be accessed because it is not being tracked. To access all original values set 'UseLazyOriginalValues' to false on the entity type.
        /// </summary>
        internal static string OriginalValueNotTracked
        {
            get { return GetString("OriginalValueNotTracked"); }
        }

        /// <summary>
        /// The original value for property '{property}' of entity type '{entityType}' cannot be accessed because it is not being tracked. To access all original values set 'UseLazyOriginalValues' to false on the entity type.
        /// </summary>
        internal static string FormatOriginalValueNotTracked(object property, object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("OriginalValueNotTracked", "property", "entityType"), property, entityType);
        }

        /// <summary>
        /// The property '{entityType}.{property}' is annotated with backing field '{field}' but that field cannot be found.
        /// </summary>
        internal static string MissingBackingField
        {
            get { return GetString("MissingBackingField"); }
        }

        /// <summary>
        /// The property '{entityType}.{property}' is annotated with backing field '{field}' but that field cannot be found.
        /// </summary>
        internal static string FormatMissingBackingField(object entityType, object property, object field)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("MissingBackingField", "entityType", "property", "field"), entityType, property, field);
        }

        /// <summary>
        /// The annotated backing field '{field}' of type '{fieldType}' cannot be used for the property '{entityType}.{property}' of type '{propertyType}'. Only backing fields of types that are assignable from the property type can be used.
        /// </summary>
        internal static string BadBackingFieldType
        {
            get { return GetString("BadBackingFieldType"); }
        }

        /// <summary>
        /// The annotated backing field '{field}' of type '{fieldType}' cannot be used for the property '{entityType}.{property}' of type '{propertyType}'. Only backing fields of types that are assignable from the property type can be used.
        /// </summary>
        internal static string FormatBadBackingFieldType(object field, object fieldType, object entityType, object property, object propertyType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("BadBackingFieldType", "field", "fieldType", "entityType", "property", "propertyType"), field, fieldType, entityType, property, propertyType);
        }

        /// <summary>
        /// No backing field could be discovered for property '{entityType}.{property}' and the property does not have a setter. Either use a backing field name that can be matched by convention, annotate the property with a backing field, or define a property setter.
        /// </summary>
        internal static string NoFieldOrSetter
        {
            get { return GetString("NoFieldOrSetter"); }
        }

        /// <summary>
        /// No backing field could be discovered for property '{entityType}.{property}' and the property does not have a setter. Either use a backing field name that can be matched by convention, annotate the property with a backing field, or define a property setter.
        /// </summary>
        internal static string FormatNoFieldOrSetter(object entityType, object property)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("NoFieldOrSetter", "entityType", "property"), entityType, property);
        }

        /// <summary>
        /// The CLR entity materializer cannot be used for entity type '{entityType}' because it is a shadow-state entity type.  Materialization to a CLR type is only possible for entity types that have a corresponding CLR type.
        /// </summary>
        internal static string NoClrType
        {
            get { return GetString("NoClrType"); }
        }

        /// <summary>
        /// The CLR entity materializer cannot be used for entity type '{entityType}' because it is a shadow-state entity type.  Materialization to a CLR type is only possible for entity types that have a corresponding CLR type.
        /// </summary>
        internal static string FormatNoClrType(object entityType)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("NoClrType", "entityType"), entityType);
        }

        /// <summary>
        /// The data stores {storeNames}are configured. A context can only be configured to use a single data store.
        /// </summary>
        internal static string MultipleDataStoresConfigured
        {
            get { return GetString("MultipleDataStoresConfigured"); }
        }

        /// <summary>
        /// The data stores {storeNames}are configured. A context can only be configured to use a single data store.
        /// </summary>
        internal static string FormatMultipleDataStoresConfigured(object storeNames)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("MultipleDataStoresConfigured", "storeNames"), storeNames);
        }

        /// <summary>
        /// No data stores are configured. Configure a data store using OnConfiguring or by creating an EntityConfiguration with a data store configured and passing it to the context.
        /// </summary>
        internal static string NoDataStoreConfigured
        {
            get { return GetString("NoDataStoreConfigured"); }
        }

        /// <summary>
        /// No data stores are configured. Configure a data store using OnConfiguring or by creating an EntityConfiguration with a data store configured and passing it to the context.
        /// </summary>
        internal static string FormatNoDataStoreConfigured()
        {
            return GetString("NoDataStoreConfigured");
        }

        /// <summary>
        /// No data stores are available. Ensure that data store services are added inside the call to AddEntityFramework on your ServiceCollection.
        /// </summary>
        internal static string NoDataStoreService
        {
            get { return GetString("NoDataStoreService"); }
        }

        /// <summary>
        /// No data stores are available. Ensure that data store services are added inside the call to AddEntityFramework on your ServiceCollection.
        /// </summary>
        internal static string FormatNoDataStoreService()
        {
            return GetString("NoDataStoreService");
        }

        /// <summary>
        /// The data stores {storeNames}are available. A context can only be configured to use a single data store. Configure a data store using OnConfiguring or by creating an EntityConfiguration with a data store configured and passing it to the context.
        /// </summary>
        internal static string MultipleDataStoresAvailable
        {
            get { return GetString("MultipleDataStoresAvailable"); }
        }

        /// <summary>
        /// The data stores {storeNames}are available. A context can only be configured to use a single data store. Configure a data store using OnConfiguring or by creating an EntityConfiguration with a data store configured and passing it to the context.
        /// </summary>
        internal static string FormatMultipleDataStoresAvailable(object storeNames)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("MultipleDataStoresAvailable", "storeNames"), storeNames);
        }

        /// <summary>
        /// Cannot change the EntityConfiguration by calling '{memberName}' because it is locked. Use EntityConfigurationBuilder to create EntityConfigurations.
        /// </summary>
        internal static string EntityConfigurationLocked
        {
            get { return GetString("EntityConfigurationLocked"); }
        }

        /// <summary>
        /// Cannot change the EntityConfiguration by calling '{memberName}' because it is locked. Use EntityConfigurationBuilder to create EntityConfigurations.
        /// </summary>
        internal static string FormatEntityConfigurationLocked(object memberName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("EntityConfigurationLocked", "memberName"), memberName);
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
