﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.InMemory.Internal;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class NullabilityCheckInMemoryTest : IClassFixture<InMemoryFixture>
    {
        public NullabilityCheckInMemoryTest(InMemoryFixture fixture)
            => Fixture = fixture;

        protected InMemoryFixture Fixture { get; }

        [ConditionalFact]
        public void IsRequired_for_property_throws_while_inserting_null_value()
        {
            Assert.Equal(
                InMemoryStrings.NullabilityErrorException($"{{'{nameof(SomeEntity.Property)}'}}", nameof(SomeEntity), "{Id: 1}"),
                Assert.Throws<DbUpdateException>(
                    () =>
                    {
                        var modelBuilder = InMemoryTestHelpers.Instance.CreateConventionBuilder();
                        modelBuilder.Entity<SomeEntity>(eb => eb.Property(p => p.Property).IsRequired());

                        var optionsBuilder = new DbContextOptionsBuilder()
                            .UseModel(modelBuilder.FinalizeModel())
                            .UseInMemoryDatabase(nameof(NullabilityCheckInMemoryTest))
                            .UseInternalServiceProvider(Fixture.ServiceProvider)
                            .EnableNullabilityCheck();

                        using var context = new DbContext(optionsBuilder.Options);
                        context.Add(new SomeEntity { Id = 1 });
                        context.SaveChanges();
                    }).Message);
        }

        [ConditionalFact]
        public void RequiredAttribute_for_property_throws_while_inserting_null_value()
        {
            Assert.Equal(
                InMemoryStrings.NullabilityErrorException($"{{'{nameof(EntityWithRequiredAttribute.RequiredProperty)}'}}", nameof(EntityWithRequiredAttribute), "{Id: 1}"),
                Assert.Throws<DbUpdateException>(
                    () =>
                    {
                        var modelBuilder = InMemoryTestHelpers.Instance.CreateConventionBuilder();
                        modelBuilder.Entity<EntityWithRequiredAttribute>();

                        var optionsBuilder = new DbContextOptionsBuilder()
                            .UseModel(modelBuilder.FinalizeModel())
                            .UseInMemoryDatabase(nameof(NullabilityCheckInMemoryTest))
                            .UseInternalServiceProvider(Fixture.ServiceProvider)
                            .EnableNullabilityCheck();

                        using var context = new DbContext(optionsBuilder.Options);
                        context.Add(new EntityWithRequiredAttribute { Id = 1 });
                        context.SaveChanges();
                    }).Message);
        }

        [ConditionalFact]
        public void RequiredAttribute_And_IsRequired_for_properties_throws_while_inserting_null_values()
        {
            Assert.Equal(
                InMemoryStrings.NullabilityErrorException(
                    $"{{'{nameof(AnotherEntityWithRequiredAttribute.Property)}', '{nameof(AnotherEntityWithRequiredAttribute.RequiredProperty)}'}}",
                    nameof(AnotherEntityWithRequiredAttribute),
                    "{Id: 1}"),
                Assert.Throws<DbUpdateException>(
                    () =>
                    {
                        var modelBuilder = InMemoryTestHelpers.Instance.CreateConventionBuilder();
                        modelBuilder.Entity<AnotherEntityWithRequiredAttribute>(eb => eb.Property(p => p.Property).IsRequired());

                        var optionsBuilder = new DbContextOptionsBuilder()
                            .UseModel(modelBuilder.FinalizeModel())
                            .UseInMemoryDatabase(nameof(NullabilityCheckInMemoryTest))
                            .UseInternalServiceProvider(Fixture.ServiceProvider)
                            .EnableNullabilityCheck();

                        using var context = new DbContext(optionsBuilder.Options);
                        context.Add(new AnotherEntityWithRequiredAttribute { Id = 1 });
                        context.SaveChanges();
                    }).Message);
        }

        [ConditionalFact]
        public void Can_insert_null_value_with_IsRequired_for_property_if_nullability_check_is_disabled()
        {
            var modelBuilder = InMemoryTestHelpers.Instance.CreateConventionBuilder();
            modelBuilder.Entity<SomeEntity>(eb => eb.Property(p => p.Property).IsRequired());

            var optionsBuilder = new DbContextOptionsBuilder()
                .UseModel(modelBuilder.FinalizeModel())
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(InMemoryFixture.DefaultNullabilityCheckProvider)
                .EnableNullabilityCheck(false);

            using var context = new DbContext(optionsBuilder.Options);
            context.Add(new SomeEntity { Id = 1 });
            context.SaveChanges();

            Assert.NotNull(context.Set<SomeEntity>().SingleOrDefault());
        }

        [ConditionalFact]
        public void Can_insert_null_value_with_RequiredAttribute_for_property_if_nullability_check_is_disabled()
        {
            var modelBuilder = InMemoryTestHelpers.Instance.CreateConventionBuilder();
            modelBuilder.Entity<EntityWithRequiredAttribute>();

            var optionsBuilder = new DbContextOptionsBuilder()
                .UseModel(modelBuilder.FinalizeModel())
                .UseInMemoryDatabase(nameof(NullabilityCheckInMemoryTest))
                .UseInternalServiceProvider(InMemoryFixture.DefaultNullabilityCheckProvider)
                .EnableNullabilityCheck(false);

            using var context = new DbContext(optionsBuilder.Options);
            context.Add(new EntityWithRequiredAttribute { Id = 1 });
            context.SaveChanges();

            Assert.NotNull(context.Set<EntityWithRequiredAttribute>().SingleOrDefault());
        }

        [ConditionalFact]
        public void Can_insert_null_values_with_RequiredAttribute_and_IsRequired_for_properties_if_nullability_check_is_disabled()
        {
            var modelBuilder = InMemoryTestHelpers.Instance.CreateConventionBuilder();
            modelBuilder.Entity<AnotherEntityWithRequiredAttribute>(eb => eb.Property(p => p.Property).IsRequired());

            var optionsBuilder = new DbContextOptionsBuilder()
                .UseModel(modelBuilder.FinalizeModel())
                .UseInMemoryDatabase(nameof(NullabilityCheckInMemoryTest))
                .UseInternalServiceProvider(InMemoryFixture.DefaultNullabilityCheckProvider)
                .EnableNullabilityCheck(false);

            using var context = new DbContext(optionsBuilder.Options);
            context.Add(new AnotherEntityWithRequiredAttribute { Id = 1 });
            context.SaveChanges();

            Assert.NotNull(context.Set<AnotherEntityWithRequiredAttribute>().SingleOrDefault());
        }

        private class EntityWithRequiredAttribute
        {
            public int Id { get; set; }

            [Required]
            public string RequiredProperty { get; set; }
        }

        private class SomeEntity
        {
            public int Id { get; set; }

            public string Property { get; set; }
        }

        private class AnotherEntityWithRequiredAttribute
        {
            public int Id { get; set; }

            [Required]
            public string RequiredProperty { get; set; }

            public string Property { get; set; }
        }
    }
}