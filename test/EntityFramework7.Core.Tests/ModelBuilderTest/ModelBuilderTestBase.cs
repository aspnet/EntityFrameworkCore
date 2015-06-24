// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Data.Entity.FunctionalTests.TestUtilities;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Xunit;

// ReSharper disable once CheckNamespace

namespace Microsoft.Data.Entity.Tests
{
    public abstract partial class ModelBuilderTest
    {
        // TODO: add convention-less tests

        public abstract class ModelBuilderTestBase
        {
            protected void AssertEqual(IEnumerable<string> expectedNames, IEnumerable<string> actualNames)
            {
                Assert.Equal(
                    new SortedSet<string>(expectedNames, StringComparer.Ordinal),
                    new SortedSet<string>(actualNames, StringComparer.Ordinal),
                    StringComparer.Ordinal);
            }

            protected void AssertEqual(IEnumerable<IProperty> expectedProperties, IEnumerable<IProperty> actualProperties)
            {
                Assert.Equal(
                    new SortedSet<IProperty>(expectedProperties, PropertyComparer.Instance),
                    new SortedSet<IProperty>(actualProperties, PropertyComparer.Instance),
                    PropertyComparer.Instance);
            }

            protected void AssertEqual(IEnumerable<INavigation> expectedNavigations, IEnumerable<INavigation> actualNavigations)
            {
                Assert.Equal(
                    new SortedSet<INavigation>(expectedNavigations, NavigationComparer.Instance),
                    new SortedSet<INavigation>(actualNavigations, NavigationComparer.Instance),
                    NavigationComparer.Instance);
            }

            protected void AssertEqual(IEnumerable<IKey> expectedKeys, IEnumerable<IKey> actualKeys)
            {
                Assert.Equal(
                    new SortedSet<IKey>(expectedKeys, KeyComparer.Instance),
                    new SortedSet<IKey>(actualKeys, KeyComparer.Instance),
                    KeyComparer.Instance);
            }

            protected void AssertEqual(IEnumerable<IForeignKey> expectedForeignKeys, IEnumerable<IForeignKey> actualForeignKeys)
            {
                Assert.Equal(
                    new SortedSet<IForeignKey>(expectedForeignKeys, ForeignKeyComparer.Instance),
                    new SortedSet<IForeignKey>(actualForeignKeys, ForeignKeyComparer.Instance),
                    ForeignKeyComparer.Instance);
            }

            protected void AssertEqual(IEnumerable<IIndex> expectedIndexes, IEnumerable<IIndex> actualIndexes)
            {
                Assert.Equal(
                    new SortedSet<IIndex>(expectedIndexes, IndexComparer.Instance),
                    new SortedSet<IIndex>(actualIndexes, IndexComparer.Instance),
                    IndexComparer.Instance);
            }

            protected TestModelBuilder CreateModelBuilder()
            {
                return CreateModelBuilder(new Model());
            }

            protected virtual TestModelBuilder CreateModelBuilder(Model model)
            {
                return CreateTestModelBuilder(TestHelpers.Instance.CreateConventionBuilder(model));
            }

            protected TestModelBuilder HobNobBuilder()
            {
                var builder = CreateModelBuilder(new Model());

                builder.Entity<Hob>().Key(e => new { e.Id1, e.Id2 });
                builder.Entity<Nob>().Key(e => new { e.Id1, e.Id2 });

                return builder;
            }

            protected abstract TestModelBuilder CreateTestModelBuilder(ModelBuilder modelBuilder);
        }

        public abstract class TestModelBuilder
        {
            protected TestModelBuilder(ModelBuilder modelBuilder)
            {
                ModelBuilder = modelBuilder;
            }

            public virtual Model Model => ModelBuilder.Model;
            protected ModelBuilder ModelBuilder { get; }

            public abstract TestEntityTypeBuilder<TEntity> Entity<TEntity>()
                where TEntity : class;

            public abstract TestModelBuilder Entity<TEntity>(Action<TestEntityTypeBuilder<TEntity>> buildAction)
                where TEntity : class;

            public abstract TestModelBuilder Ignore<TEntity>()
                where TEntity : class;
        }

        public abstract class TestEntityTypeBuilder<TEntity>
            where TEntity : class
        {
            public abstract EntityType Metadata { get; }
            public abstract TestEntityTypeBuilder<TEntity> Annotation(string annotation, object value);

            public abstract TestEntityTypeBuilder<TEntity> BaseEntity<TBaseEntity>()
                where TBaseEntity : class;

            public abstract TestEntityTypeBuilder<TEntity> BaseEntity(string baseEntityTypeName);
            public abstract TestKeyBuilder Key(Expression<Func<TEntity, object>> keyExpression);
            public abstract TestKeyBuilder Key(params string[] propertyNames);
            public abstract TestKeyBuilder AlternateKey(Expression<Func<TEntity, object>> keyExpression);
            public abstract TestKeyBuilder AlternateKey(params string[] propertyNames);

            public abstract TestPropertyBuilder<TProperty> Property<TProperty>(
                Expression<Func<TEntity, TProperty>> propertyExpression);

            public abstract TestPropertyBuilder<TProperty> Property<TProperty>(string propertyName);

            public abstract TestEntityTypeBuilder<TEntity> Ignore(
                Expression<Func<TEntity, object>> propertyExpression);

            public abstract TestEntityTypeBuilder<TEntity> Ignore(string propertyName);

            public abstract TestIndexBuilder Index(Expression<Func<TEntity, object>> indexExpression);
            public abstract TestIndexBuilder Index(params string[] propertyNames);

            public abstract TestReferenceNavigationBuilder<TEntity, TRelatedEntity> Reference<TRelatedEntity>(
                Expression<Func<TEntity, TRelatedEntity>> reference = null)
                where TRelatedEntity : class;

            public abstract TestCollectionNavigationBuilder<TEntity, TRelatedEntity> Collection<TRelatedEntity>(
                Expression<Func<TEntity, IEnumerable<TRelatedEntity>>> collection = null)
                where TRelatedEntity : class;
        }

        public class TestKeyBuilder
        {
            public TestKeyBuilder(KeyBuilder keyBuilder)
            {
                KeyBuilder = keyBuilder;
            }

            private KeyBuilder KeyBuilder { get; }
            public Key Metadata => KeyBuilder.Metadata;

            public virtual TestKeyBuilder Annotation(string annotation, object value)
            {
                return new TestKeyBuilder(KeyBuilder.Annotation(annotation, value));
            }
        }

        public class TestIndexBuilder
        {
            public TestIndexBuilder(IndexBuilder indexBuilder)
            {
                IndexBuilder = indexBuilder;
            }

            private IndexBuilder IndexBuilder { get; }
            public Index Metadata => IndexBuilder.Metadata;

            public virtual TestIndexBuilder Annotation(string annotation, object value)
            {
                return new TestIndexBuilder(IndexBuilder.Annotation(annotation, value));
            }

            public virtual TestIndexBuilder Unique(bool isUnique = true)
            {
                return new TestIndexBuilder(IndexBuilder.Unique(isUnique));
            }
        }

        public abstract class TestPropertyBuilder<TProperty>
        {
            public abstract Property Metadata { get; }
            public abstract TestPropertyBuilder<TProperty> Annotation(string annotation, object value);
            public abstract TestPropertyBuilder<TProperty> Required(bool isRequired = true);
            public abstract TestPropertyBuilder<TProperty> MaxLength(int maxLength);
            public abstract TestPropertyBuilder<TProperty> ConcurrencyToken(bool isConcurrencyToken = true);

            public abstract TestPropertyBuilder<TProperty> ValueGeneratedNever();
            public abstract TestPropertyBuilder<TProperty> ValueGeneratedOnAdd();
            public abstract TestPropertyBuilder<TProperty> ValueGeneratedOnAddOrUpdate();
        }

        public abstract class TestCollectionNavigationBuilder<TEntity, TRelatedEntity>
            where TEntity : class
            where TRelatedEntity : class
        {
            public abstract TestReferenceCollectionBuilder<TEntity, TRelatedEntity> InverseReference(
                Expression<Func<TRelatedEntity, TEntity>> reference = null);
        }

        public abstract class TestReferenceNavigationBuilder<TEntity, TRelatedEntity>
            where TEntity : class
            where TRelatedEntity : class
        {
            public abstract TestReferenceCollectionBuilder<TRelatedEntity, TEntity> InverseCollection(
                Expression<Func<TRelatedEntity, IEnumerable<TEntity>>> collection = null);

            public abstract TestReferenceReferenceBuilder<TEntity, TRelatedEntity> InverseReference(
                Expression<Func<TRelatedEntity, TEntity>> reference = null);
        }

        public abstract class TestReferenceCollectionBuilder<TEntity, TRelatedEntity>
            where TEntity : class
            where TRelatedEntity : class
        {
            public abstract ForeignKey Metadata { get; }

            public abstract TestReferenceCollectionBuilder<TEntity, TRelatedEntity> ForeignKey(
                Expression<Func<TRelatedEntity, object>> foreignKeyExpression);

            public abstract TestReferenceCollectionBuilder<TEntity, TRelatedEntity> PrincipalKey(
                Expression<Func<TEntity, object>> keyExpression);

            public abstract TestReferenceCollectionBuilder<TEntity, TRelatedEntity> ForeignKey(
                params string[] foreignKeyPropertyNames);

            public abstract TestReferenceCollectionBuilder<TEntity, TRelatedEntity> PrincipalKey(
                params string[] keyPropertyNames);

            public abstract TestReferenceCollectionBuilder<TEntity, TRelatedEntity> Annotation(
                string annotation, object value);

            public abstract TestReferenceCollectionBuilder<TEntity, TRelatedEntity> Required(bool isRequired = true);
        }

        public abstract class TestReferenceReferenceBuilder<TEntity, TRelatedEntity>
            where TEntity : class
            where TRelatedEntity : class
        {
            public abstract ForeignKey Metadata { get; }

            public abstract TestReferenceReferenceBuilder<TEntity, TRelatedEntity> Annotation(
                string annotation, object value);

            public abstract TestReferenceReferenceBuilder<TEntity, TRelatedEntity> ForeignKey<TDependentEntity>(
                Expression<Func<TDependentEntity, object>> foreignKeyExpression);

            public abstract TestReferenceReferenceBuilder<TEntity, TRelatedEntity> PrincipalKey<TPrincipalEntity>(
                Expression<Func<TPrincipalEntity, object>> keyExpression);

            public abstract TestReferenceReferenceBuilder<TEntity, TRelatedEntity> ForeignKey(
                Type dependentEntityType, params string[] foreignKeyPropertyNames);

            public abstract TestReferenceReferenceBuilder<TEntity, TRelatedEntity> PrincipalKey(
                Type principalEntityType, params string[] keyPropertyNames);

            public abstract TestReferenceReferenceBuilder<TEntity, TRelatedEntity> Required(bool isRequired = true);
        }
    }
}
