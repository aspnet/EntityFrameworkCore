// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class RelationalMetadataExtensionsTest
    {
        [ConditionalFact]
        public void Can_get_and_set_fixed_length()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.IsFixedLength());

            property.SetIsFixedLength(true);

            Assert.True(property.IsFixedLength());

            property.SetIsFixedLength(false);

            Assert.False(property.IsFixedLength());

            property.SetIsFixedLength(null);

            Assert.Null(property.IsFixedLength());
        }

        [ConditionalFact]
        public void Can_get_and_set_index_filter()
        {
            var modelBuilder = new ModelBuilder();

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .HasFilter("[Id] % 2 = 0")
                .Metadata;

            Assert.Equal("[Id] % 2 = 0", index.GetFilter());

            index.SetFilter("[Id] % 3 = 0");

            Assert.Equal("[Id] % 3 = 0", index.GetFilter());
        }

        [ConditionalFact]
        public void Can_get_and_set_column_name()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Equal("Name", property.GetColumnBaseName());

            property.SetColumnName("Eman");

            Assert.Equal("Name", property.Name);
            Assert.Equal("Eman", property.GetColumnBaseName());

            property.SetColumnName(null);

            Assert.Equal("Name", property.GetColumnBaseName());
        }

        [ConditionalFact]
        public void Can_get_and_set_table_name()
        {
            var modelBuilder = new ModelBuilder();

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Equal("Customer", entityType.GetTableName());

            entityType.SetTableName("Customizer");

            Assert.Equal("Customer", entityType.DisplayName());
            Assert.Equal("Customizer", entityType.GetTableName());

            entityType.SetTableName(null);

            Assert.Null(entityType.GetTableName());
        }

        [ConditionalFact]
        public void Can_get_and_set_schema_name_on_entity_type()
        {
            var modelBuilder = new ModelBuilder();

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Null(entityType.GetSchema());

            entityType.SetSchema("db0");

            Assert.Equal("db0", entityType.GetSchema());

            entityType.SetSchema(null);

            Assert.Null(entityType.GetSchema());
        }

        [ConditionalFact]
        public void Can_get_table_and_schema_name_for_non_owned_entity_types_with_defining_navigation()
        {
            var modelBuilder = new ModelBuilder();

            var orderType = modelBuilder
                .Entity<Order>()
                .Metadata;

            var customerType = modelBuilder.Model.AddEntityType(typeof(Customer), nameof(Order.Customer), orderType);

            Assert.Equal("Order_Customer", customerType.GetTableName());

            orderType.SetTableName(null);

            Assert.Equal("Customer_Customer", customerType.GetTableName());

            customerType.SetTableName("Customizer");

            Assert.Equal("Customizer", customerType.GetTableName());
        }

        [ConditionalFact]
        public void Gets_model_schema_if_schema_on_entity_type_not_set()
        {
            var modelBuilder = new ModelBuilder();

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Null(entityType.GetSchema());

            modelBuilder.Model.SetDefaultSchema("db0");

            Assert.Equal("db0", entityType.GetSchema());

            modelBuilder.Model.SetDefaultSchema(null);

            Assert.Null(entityType.GetSchema());
        }

        [ConditionalFact]
        public void Can_get_and_set_column_type()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.GetColumnType());

            property.SetColumnType("nvarchar(max)");

            Assert.Equal("nvarchar(max)", property.GetColumnType());

            property.SetColumnType(null);

            Assert.Null(property.GetColumnType());
        }

        [ConditionalFact]
        public void Can_get_and_set_column_default_expression()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.GetDefaultValueSql());

            property.SetDefaultValueSql("newsequentialid()");

            Assert.Equal("newsequentialid()", property.GetDefaultValueSql());

            property.SetDefaultValueSql(null);

            Assert.Null(property.GetDefaultValueSql());
        }

        [ConditionalFact]
        public void Can_get_and_set_column_computed_expression()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.GetComputedColumnSql());

            property.SetComputedColumnSql("newsequentialid()");

            Assert.Equal("newsequentialid()", property.GetComputedColumnSql());

            property.SetComputedColumnSql(null);

            Assert.Null(property.GetComputedColumnSql());
        }

        [ConditionalFact]
        public void Can_get_and_set_column_default_value()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.AlternateId)
                .Metadata;

            Assert.Null(property.GetDefaultValue());

            var guid = new Guid("{3FDFC4F5-AEAB-4D72-9C96-201E004349FA}");

            property.SetDefaultValue(guid);

            Assert.Equal(guid, property.GetDefaultValue());

            property.SetDefaultValue(null);

            Assert.Null(property.GetDefaultValue());
        }

        [ConditionalFact]
        public void Can_get_and_set_column_default_value_of_enum_type()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.EnumValue)
                .Metadata;

            Assert.Null(property.GetDefaultValue());

            property.SetDefaultValue(MyEnum.Mon);

            Assert.Equal(typeof(MyEnum), property.GetDefaultValue().GetType());
            Assert.Equal(MyEnum.Mon, property.GetDefaultValue());

            property.SetDefaultValue(null);

            Assert.Null(property.GetDefaultValue());
        }

        [ConditionalFact]
        public void Throws_when_setting_column_default_value_of_wrong_type()
        {
            var modelBuilder = new ModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.GetDefaultValue());

            var guid = new Guid("{3FDFC4F5-AEAB-4D72-9C96-201E004349FA}");

            Assert.Equal(
                RelationalStrings.IncorrectDefaultValueType(
                    guid, typeof(Guid), property.Name, property.ClrType, property.DeclaringEntityType.DisplayName()),
                Assert.Throws<InvalidOperationException>(() => property.SetDefaultValue(guid)).Message);
        }

        [ConditionalFact]
        public void Can_get_and_set_column_key_name()
        {
            var modelBuilder = new ModelBuilder();

            var key = modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id)
                .Metadata;

            Assert.Equal("PK_Customer", key.GetName());

            key.SetName("PrimaryKey");

            Assert.Equal("PrimaryKey", key.GetName());

            key.SetName(null);

            Assert.Equal("PK_Customer", key.GetName());
        }

        [ConditionalFact]
        public void Can_get_and_set_column_foreign_key_name()
        {
            var modelBuilder = new ModelBuilder();

            modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id);

            var foreignKey = modelBuilder
                .Entity<Order>()
                .HasOne<Customer>()
                .WithOne()
                .HasForeignKey<Order>(e => e.CustomerId)
                .Metadata;

            Assert.Equal("FK_Order_Customer_CustomerId", foreignKey.GetConstraintName());

            foreignKey.SetConstraintName("FK");

            Assert.Equal("FK", foreignKey.GetConstraintName());

            foreignKey.SetConstraintName(null);

            Assert.Equal("FK_Order_Customer_CustomerId", foreignKey.GetConstraintName());
        }

        [ConditionalFact]
        public void Can_get_and_set_index_name()
        {
            var modelBuilder = new ModelBuilder();

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .Metadata;

#pragma warning disable CS0618 // Type or member is obsolete
            Assert.Equal("IX_Customer_Id", index.GetName());

            index.SetName("MyIndex");

            Assert.Equal("MyIndex", index.GetName());

            index.SetName(null);

            Assert.Equal("IX_Customer_Id", index.GetName());
#pragma warning restore CS0618 // Type or member is obsolete
        }

        [ConditionalFact]
        public void Can_get_and_set_discriminator()
        {
            var modelBuilder = new ModelBuilder();

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Null(entityType.GetDiscriminatorProperty());

            var property = entityType.AddProperty("D", typeof(string));

            entityType.SetDiscriminatorProperty(property);

            Assert.Same(property, entityType.GetDiscriminatorProperty());

            entityType.SetDiscriminatorProperty(null);

            Assert.Null(entityType.GetDiscriminatorProperty());
        }

        [ConditionalFact]
        public void Can_get_and_set_schema_name_on_model()
        {
            var modelBuilder = new ModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.GetDefaultSchema());

            model.SetDefaultSchema("db0");

            Assert.Equal("db0", model.GetDefaultSchema());

            model.SetDefaultSchema(null);

            Assert.Null(model.GetDefaultSchema());
        }

        [ConditionalFact]
        public void Can_get_and_set_dbfunction()
        {
            var testMethod = typeof(TestDbFunctions).GetTypeInfo().GetDeclaredMethod(nameof(TestDbFunctions.MethodA));

            var modelBuilder = new ModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.FindDbFunction(testMethod));

            var dbFunc = model.AddDbFunction(testMethod);

            Assert.NotNull(dbFunc);
            Assert.NotNull(dbFunc.Name);
            Assert.Null(dbFunc.Schema);
            Assert.NotNull(((IConventionDbFunction)dbFunc).Builder);

            Assert.Same(dbFunc, model.RemoveDbFunction(testMethod));

            Assert.Null(((IConventionDbFunction)dbFunc).Builder);
        }

        [ConditionalFact]
        public void Can_get_and_set_sequence()
        {
            var modelBuilder = new ModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.FindSequence("Foo"));

            var sequence = model.AddSequence("Foo");

            Assert.Equal("Foo", model.FindSequence("Foo").Name);

            Assert.Equal("Foo", sequence.Name);
            Assert.Null(sequence.Schema);
            Assert.Equal(1, sequence.IncrementBy);
            Assert.Equal(1, sequence.StartValue);
            Assert.Null(sequence.MinValue);
            Assert.Null(sequence.MaxValue);
            Assert.Same(typeof(long), sequence.Type);
            Assert.False(sequence.IsCyclic);
            Assert.NotNull(((IConventionSequence)sequence).Builder);

            Assert.Same(sequence, model.FindSequence("Foo"));

            sequence.StartValue = 1729;
            sequence.IncrementBy = 11;
            sequence.MinValue = 2001;
            sequence.MaxValue = 2010;
            sequence.Type = typeof(int);

            Assert.Equal("Foo", sequence.Name);
            Assert.Null(sequence.Schema);
            Assert.Equal(11, sequence.IncrementBy);
            Assert.Equal(1729, sequence.StartValue);
            Assert.Equal(2001, sequence.MinValue);
            Assert.Equal(2010, sequence.MaxValue);
            Assert.Same(typeof(int), sequence.Type);

            Assert.Same(sequence, model.RemoveSequence("Foo"));

            Assert.Null(((IConventionSequence)sequence).Builder);
        }

        [ConditionalFact]
        public void Can_get_and_set_sequence_with_schema_name()
        {
            var modelBuilder = new ModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.FindSequence("Foo", "Smoo"));

            var sequence = model.AddSequence("Foo", "Smoo");

            Assert.Equal("Foo", model.FindSequence("Foo", "Smoo").Name);
            Assert.Equal("Foo", model.FindSequence("Foo", "Smoo").Name);

            Assert.Equal("Foo", sequence.Name);
            Assert.Equal("Smoo", sequence.Schema);
            Assert.Equal(1, sequence.IncrementBy);
            Assert.Equal(1, sequence.StartValue);
            Assert.Null(sequence.MinValue);
            Assert.Null(sequence.MaxValue);
            Assert.Same(typeof(long), sequence.Type);

            Assert.Same(sequence, model.FindSequence("Foo", "Smoo"));

            sequence.StartValue = 1729;
            sequence.IncrementBy = 11;
            sequence.MinValue = 2001;
            sequence.MaxValue = 2010;
            sequence.Type = typeof(int);

            Assert.Equal("Foo", sequence.Name);
            Assert.Equal("Smoo", sequence.Schema);
            Assert.Equal(11, sequence.IncrementBy);
            Assert.Equal(1729, sequence.StartValue);
            Assert.Equal(2001, sequence.MinValue);
            Assert.Equal(2010, sequence.MaxValue);
            Assert.Same(typeof(int), sequence.Type);
        }

        [ConditionalFact]
        public void Sequence_is_in_model_schema_if_schema_not_specified()
        {
            var modelBuilder = new ModelBuilder();
            var model = modelBuilder.Model;
            model.SetDefaultSchema("Smoo");

            Assert.Null(model.FindSequence("Foo"));

            var sequence = model.AddSequence("Foo");

            Assert.Same(sequence, model.FindSequence("Foo"));

            Assert.Equal("Foo", sequence.Name);
            Assert.Equal("Smoo", sequence.Schema);
            Assert.Equal(1, sequence.IncrementBy);
            Assert.Equal(1, sequence.StartValue);
            Assert.Null(sequence.MinValue);
            Assert.Null(sequence.MaxValue);
            Assert.Same(typeof(long), sequence.Type);
        }

        [ConditionalFact]
        public void Returns_same_sequence_if_schema_not_specified_explicitly()
        {
            var modelBuilder = new ModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.FindSequence("Foo"));

            var sequence = model.AddSequence("Foo");

            Assert.Same(sequence, model.FindSequence("Foo"));

            Assert.Equal("Foo", sequence.Name);
            Assert.Null(sequence.Schema);
            Assert.Equal(1, sequence.IncrementBy);
            Assert.Equal(1, sequence.StartValue);
            Assert.Null(sequence.MinValue);
            Assert.Null(sequence.MaxValue);
            Assert.Same(typeof(long), sequence.Type);

            model.SetDefaultSchema("Smoo");

            Assert.Same(sequence, model.FindSequence("Foo"));

            sequence.StartValue = 1729;
            sequence.IncrementBy = 11;
            sequence.MinValue = 2001;
            sequence.MaxValue = 2010;
            sequence.Type = typeof(int);

            Assert.Equal("Foo", sequence.Name);
            Assert.Equal("Smoo", sequence.Schema);
            Assert.Equal(11, sequence.IncrementBy);
            Assert.Equal(1729, sequence.StartValue);
            Assert.Equal(2001, sequence.MinValue);
            Assert.Equal(2010, sequence.MaxValue);
            Assert.Same(typeof(int), sequence.Type);
        }

        [ConditionalFact]
        public void Can_get_multiple_sequences()
        {
            var modelBuilder = new ModelBuilder();
            var model = modelBuilder.Model;

            model.AddSequence("Fibonacci");
            model.AddSequence("Golomb");

            var sequences = model.GetSequences();

            Assert.Equal(2, sequences.Count());
            Assert.Contains(sequences, s => s.Name == "Fibonacci");
            Assert.Contains(sequences, s => s.Name == "Golomb");
        }

        private enum MyEnum : byte
        {
            Son,
            Mon,
            Tue
        }

        private class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Guid AlternateId { get; set; }
            public MyEnum? EnumValue { get; set; }
        }

        private class SpecialCustomer : Customer
        {
        }

        private class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }
        }
    }
}
