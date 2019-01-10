﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Cosmos.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Cosmos.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Cosmos.Storage.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Update;
using Newtonsoft.Json.Linq;

namespace Microsoft.EntityFrameworkCore.Cosmos.Update.Internal
{
    public class DocumentSource
    {
        private readonly string _collectionId;
        private readonly CosmosDatabase _database;
        private readonly IProperty _idProperty;

        public DocumentSource(IEntityType entityType, CosmosDatabase database)
        {
            _collectionId = entityType.Cosmos().ContainerName;
            _database = database;
            _idProperty = entityType.FindProperty(StoreKeyConvention.IdPropertyName);
        }

        public string GetCollectionId()
            => _collectionId;

        public string GetId(IUpdateEntry entry)
            => entry.GetCurrentValue<string>(_idProperty);

        public JObject CreateDocument(IUpdateEntry entry)
        {
            var document = new JObject();
            foreach (var property in entry.EntityType.GetProperties())
            {
                var storeName = property.Cosmos().PropertyName;
                if (storeName != "")
                {
                    var value = entry.GetCurrentValue(property);
                    document[storeName] = value != null ? JToken.FromObject(value) : null;
                }
                else if (entry.HasTemporaryValue(property))
                {
                    ((InternalEntityEntry)entry)[property] = entry.GetCurrentValue(property);
                }
            }

            foreach (var ownedNavigation in entry.EntityType.GetNavigations())
            {
                var fk = ownedNavigation.ForeignKey;
                if (!fk.IsOwnership
                    || ownedNavigation.IsDependentToPrincipal()
                    || fk.DeclaringEntityType.IsDocumentRoot())
                {
                    continue;
                }

                var nestedValue = entry.GetCurrentValue(ownedNavigation);
                if (nestedValue == null)
                {
                    document[ownedNavigation.Name] = null;
                }
                else if (fk.IsUnique)
                {
                    var dependentEntry = ((InternalEntityEntry)entry).StateManager.TryGetEntry(nestedValue, fk.DeclaringEntityType);
                    document[ownedNavigation.Name] = _database.GetDocumentSource(dependentEntry.EntityType).CreateDocument(dependentEntry);
                }
                else
                {
                    var array = new JArray();
                    foreach (var dependent in (IEnumerable)nestedValue)
                    {
                        var dependentEntry = ((InternalEntityEntry)entry).StateManager.TryGetEntry(dependent, fk.DeclaringEntityType);
                        array.Add(_database.GetDocumentSource(dependentEntry.EntityType).CreateDocument(dependentEntry));
                    }

                    document[ownedNavigation.Name] = array;
                }
            }

            return document;
        }

        public JObject UpdateDocument(JObject document, IUpdateEntry entry)
        {
            var anyPropertyUpdated = false;
            foreach (var property in entry.EntityType.GetProperties())
            {
                if (entry.EntityState == EntityState.Added
                    || entry.IsModified(property))
                {
                    var storeName = property.Cosmos().PropertyName;
                    if (storeName != "")
                    {
                        var value = entry.GetCurrentValue(property);
                        document[storeName] = value != null ? JToken.FromObject(value) : null;
                        anyPropertyUpdated = true;
                    }
                    else if (entry.HasTemporaryValue(property))
                    {
                        ((InternalEntityEntry)entry)[property] = entry.GetCurrentValue(property);
                    }
                }
            }

            foreach (var ownedNavigation in entry.EntityType.GetNavigations())
            {
                var fk = ownedNavigation.ForeignKey;
                if (!fk.IsOwnership
                    || ownedNavigation.IsDependentToPrincipal()
                    || fk.DeclaringEntityType.IsDocumentRoot())
                {
                    continue;
                }

                var nestedDocumentSource = _database.GetDocumentSource(fk.DeclaringEntityType);
                var nestedValue = entry.GetCurrentValue(ownedNavigation);
                if (nestedValue == null)
                {
                    if (document[ownedNavigation.Name] != null)
                    {
                        document[ownedNavigation.Name] = null;
                        anyPropertyUpdated = true;
                    }
                }
                else if (fk.IsUnique)
                {
                    var nestedEntry = ((InternalEntityEntry)entry).StateManager.TryGetEntry(nestedValue, fk.DeclaringEntityType);
                    if (nestedEntry == null)
                    {
                        return document;
                    }

                    if (document[ownedNavigation.Name] is JObject nestedDocument)
                    {
                        nestedDocument = nestedDocumentSource.UpdateDocument(nestedDocument, nestedEntry);
                    }
                    else
                    {
                        nestedDocument = nestedDocumentSource.CreateDocument(nestedEntry);
                    }

                    if (nestedDocument != null)
                    {
                        document[ownedNavigation.Name] = nestedDocument;
                        anyPropertyUpdated = true;
                    }
                }
                else
                {
                    var array = new JArray();
                    foreach (var dependent in (IEnumerable)nestedValue)
                    {
                        var dependentEntry = ((InternalEntityEntry)entry).StateManager.TryGetEntry(dependent, fk.DeclaringEntityType);
                        if (dependentEntry == null)
                        {
                            continue;
                        }

                        array.Add(_database.GetDocumentSource(dependentEntry.EntityType).CreateDocument(dependentEntry));
                    }

                    document[ownedNavigation.Name] = array;
                    anyPropertyUpdated = true;
                }
            }

            return anyPropertyUpdated ? document : null;
        }
    }
}
