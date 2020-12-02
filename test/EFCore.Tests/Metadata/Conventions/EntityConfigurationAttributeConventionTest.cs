﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Metadata.Conventions
{
    public class EntityConfigurationAttributeConventionTest
    {
        [ConditionalFact]
        public void EntityConfigurationAttribute_Should_AppliConfiguration_ToEntityType()
        {
            var builder = InMemoryTestHelpers.Instance.CreateConventionBuilder();

            builder.Entity<Customer>();

            var entityType = builder.Model.FindEntityType(typeof(Customer));
            Assert.Equal(1000, entityType.FindProperty(nameof(Customer.Name)).GetMaxLength());
        }

        private class CustomerConfiguration : IEntityTypeConfiguration<Customer>
        {
            public void Configure(EntityTypeBuilder<Customer> builder)
            {
                builder.Property(c => c.Name).HasMaxLength(1000);
            }
        }

        [EntityConfiguration(typeof(CustomerConfiguration))]
        protected class Customer
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }      
    }
}
