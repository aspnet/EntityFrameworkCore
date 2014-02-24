﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.DependencyInjection;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Identity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Storage;
using Moq;
using Xunit;

namespace Microsoft.Data.Entity
{
    public class EntityConfigurationTest
    {
        [Fact]
        public void Members_check_arguments()
        {
            var configuration = new EntityConfiguration();

            Assert.Equal(
                "value",
                Assert.Throws<ArgumentNullException>(() => configuration.ActiveIdentityGenerators = null).ParamName);

            Assert.Equal(
                "value",
                Assert.Throws<ArgumentNullException>(() => configuration.DataStore = null).ParamName);

            Assert.Equal(
                "value",
                Assert.Throws<ArgumentNullException>(() => configuration.IdentityGeneratorFactory = null).ParamName);

            Assert.Equal(
                "value",
                Assert.Throws<ArgumentNullException>(() => configuration.ChangeTrackerFactory = null).ParamName);
        }

        [Fact]
        public void Throws_if_no_data_store()
        {
            Assert.Equal(
                Strings.MissingConfigurationItem(typeof(DataStore)),
                Assert.Throws<InvalidOperationException>(() => new EntityConfiguration().DataStore).Message);
        }

        private class FakeDataStore : DataStore
        {
            public override Task<int> SaveChangesAsync(IEnumerable<EntityEntry> entityEntries)
            {
                return Task.FromResult(0);
            }
        }

        [Fact]
        public void Can_set_data_store()
        {
            var dataStore = new FakeDataStore();
            var entityConfiguration = new EntityConfiguration { DataStore = dataStore };

            Assert.Same(dataStore, entityConfiguration.DataStore);
        }

        [Fact]
        public void Can_provide_data_store_from_service_provider()
        {
            var serviceProvider = new ServiceProvider();
            var dataStore = new FakeDataStore();
            serviceProvider.AddInstance<DataStore>(dataStore);
            var entityConfiguration = new EntityConfiguration(serviceProvider);

            Assert.Same(dataStore, entityConfiguration.DataStore);
        }

        [Fact]
        public void Throws_if_no_IdentityGeneratorFactory_registered()
        {
            Assert.Equal(
                Strings.MissingConfigurationItem(typeof(IdentityGeneratorFactory)),
                Assert.Throws<InvalidOperationException>(
                    () => new EntityConfiguration(new ServiceProvider()).IdentityGeneratorFactory).Message);
        }

        [Fact]
        public void Can_provide_IdentityGeneratorFactory_from_service_provider()
        {
            var serviceProvider = new ServiceProvider();
            var configuration = new EntityConfiguration(serviceProvider);

            var factory = new Mock<IdentityGeneratorFactory>().Object;
            serviceProvider.AddInstance<IdentityGeneratorFactory>(factory);

            Assert.Same(factory, configuration.IdentityGeneratorFactory);
        }

        [Fact]
        public void Can_set_IdentityGeneratorFactory()
        {
            var configuration = new EntityConfiguration();

            var factory = new Mock<IdentityGeneratorFactory>().Object;
            configuration.IdentityGeneratorFactory = factory;

            Assert.Same(factory, configuration.IdentityGeneratorFactory);
        }

        [Fact]
        public void Can_set_IdentityGeneratorFactory_to_override_service_provider_default()
        {
            var serviceProvider = new ServiceProvider();
            var configuration = new EntityConfiguration(serviceProvider);
            serviceProvider.AddInstance<IdentityGeneratorFactory>(new Mock<IdentityGeneratorFactory>().Object);

            var factory = new Mock<IdentityGeneratorFactory>().Object;
            configuration.IdentityGeneratorFactory = factory;

            Assert.Same(factory, configuration.IdentityGeneratorFactory);
        }

        [Fact]
        public void Can_set_IdentityGeneratorFactory_but_fallback_to_service_provider_default()
        {
            var serviceProvider = new ServiceProvider();
            var configuration = new EntityConfiguration(serviceProvider);

            var generator1 = new Mock<IIdentityGenerator>().Object;
            var defaultFactoryMock = new Mock<IdentityGeneratorFactory>();
            defaultFactoryMock.Setup(m => m.Create(It.Is<IProperty>(p => p.Name == "Foo"))).Returns(generator1);
            serviceProvider.AddInstance<IdentityGeneratorFactory>(defaultFactoryMock.Object);

            var generator2 = new Mock<IIdentityGenerator>().Object;
            var customFactoryMock = new Mock<IdentityGeneratorFactory>();
            customFactoryMock.Setup(m => m.Create(It.Is<IProperty>(p => p.Name == "Goo"))).Returns(generator2);

            configuration.IdentityGeneratorFactory = new OverridingIdentityGeneratorFactory(
                customFactoryMock.Object, configuration.IdentityGeneratorFactory);

            // Should get overridden generator
            Assert.Same(generator2, configuration.IdentityGeneratorFactory.Create(new Property("Goo", typeof(int))));
            customFactoryMock.Verify(m => m.Create(It.IsAny<IProperty>()), Times.Once);
            defaultFactoryMock.Verify(m => m.Create(It.IsAny<IProperty>()), Times.Never);

            // Should fall back to the service provider
            Assert.Same(generator1, configuration.IdentityGeneratorFactory.Create(new Property("Foo", typeof(int))));
            customFactoryMock.Verify(m => m.Create(It.IsAny<IProperty>()), Times.Exactly(2));
            defaultFactoryMock.Verify(m => m.Create(It.IsAny<IProperty>()), Times.Once);
        }

        [Fact]
        public void Throws_if_no_ChangeTrackerFactory_registered()
        {
            Assert.Equal(
                Strings.MissingConfigurationItem(typeof(ChangeTrackerFactory)),
                Assert.Throws<InvalidOperationException>(
                    () => new EntityConfiguration(new ServiceProvider()).ChangeTrackerFactory).Message);
        }

        [Fact]
        public void Can_provide_ChangeTrackerFactory_from_service_provider()
        {
            var serviceProvider = new ServiceProvider();
            var configuration = new EntityConfiguration(serviceProvider);

            var factory = new Mock<ChangeTrackerFactory>().Object;
            serviceProvider.AddInstance<ChangeTrackerFactory>(factory);

            Assert.Same(factory, configuration.ChangeTrackerFactory);
        }

        [Fact]
        public void Can_set_ChangeTrackerFactory()
        {
            var configuration = new EntityConfiguration();

            var factory = new Mock<ChangeTrackerFactory>().Object;
            configuration.ChangeTrackerFactory = factory;

            Assert.Same(factory, configuration.ChangeTrackerFactory);
        }

        [Fact]
        public void Throws_if_no_ActiveIdentityGenerators_registered()
        {
            Assert.Equal(
                Strings.MissingConfigurationItem(typeof(ActiveIdentityGenerators)),
                Assert.Throws<InvalidOperationException>(
                    () => new EntityConfiguration(new ServiceProvider()).ActiveIdentityGenerators).Message);
        }

        [Fact]
        public void Can_provide_ActiveIdentityGenerators_from_service_provider()
        {
            var serviceProvider = new ServiceProvider();
            var configuration = new EntityConfiguration(serviceProvider);

            var factory = new Mock<ActiveIdentityGenerators>().Object;
            serviceProvider.AddInstance<ActiveIdentityGenerators>(factory);

            Assert.Same(factory, configuration.ActiveIdentityGenerators);
        }

        [Fact]
        public void Can_set_ActiveIdentityGenerators()
        {
            var configuration = new EntityConfiguration();

            var factory = new Mock<ActiveIdentityGenerators>().Object;
            configuration.ActiveIdentityGenerators = factory;

            Assert.Same(factory, configuration.ActiveIdentityGenerators);
        }
    }
}
