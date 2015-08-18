﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Data.Entity.Relational.Design.Templating;
using Microsoft.Data.Entity.Relational.Design.Templating.Compilation;
using Xunit;

namespace Microsoft.Data.Entity.Relational.Design.ReverseEngineering.Tests
{
    public class ReverseEngineeringGeneratorTests
    {
        [Fact]
        public void Throws_exceptions_for_incorrect_configuration()
        {
            var configuration = new ReverseEngineeringConfiguration
            {
                Provider = null,
                ConnectionString = null,
                CustomTemplatePath = null,
                ProjectPath = null,
                RelativeOutputPath = null
            };
            Assert.Equal(Strings.ProviderRequired,
                Assert.Throws<ArgumentException>(
                    () => GetGenerator().GenerateAsync(configuration).GetAwaiter().GetResult()).Message);

            configuration.Provider = new NullDatabaseMetadataModelProvider();
            Assert.Equal(Strings.ConnectionStringRequired,
                Assert.Throws<ArgumentException>(
                    () => GetGenerator().GenerateAsync(configuration).GetAwaiter().GetResult()).Message);

            configuration.ConnectionString = "NonEmptyConnectionString";
            Assert.Equal(Strings.ProjectPathRequired,
                Assert.Throws<ArgumentException>(
                    () => GetGenerator().GenerateAsync(configuration).GetAwaiter().GetResult()).Message);

            configuration.ProjectPath = "NonEmptyProjectPath";
            Assert.Equal(Strings.RootNamespaceRequired,
                Assert.Throws<ArgumentException>(
                    () => GetGenerator().GenerateAsync(configuration).GetAwaiter().GetResult()).Message);

            configuration.RelativeOutputPath = @"\AnAbsolutePath";
            Assert.Equal(Strings.NotRelativePath(@"\AnAbsolutePath", "NonEmptyProjectPath"),
                Assert.Throws<ArgumentException>(
                    () => GetGenerator().GenerateAsync(configuration).GetAwaiter().GetResult()).Message);

            configuration.RelativeOutputPath = @"Looks\Like\A\RelativePath\..\..\..\..\..\But\Is\Not";
            Assert.Equal(Strings.NotRelativePath(@"Looks\Like\A\RelativePath\..\..\..\..\..\But\Is\Not", "NonEmptyProjectPath"),
                Assert.Throws<ArgumentException>(
                    () => GetGenerator().GenerateAsync(configuration).GetAwaiter().GetResult()).Message);

            configuration.RelativeOutputPath = @"A\Real\RelativePath";
            Assert.Equal(Strings.RootNamespaceRequired,
                Assert.Throws<ArgumentException>(
                    () => GetGenerator().GenerateAsync(configuration).GetAwaiter().GetResult()).Message);
        }

        private ReverseEngineeringGenerator GetGenerator()
            => new ReverseEngineeringGenerator(new NullLogger(), new NullFileService(),
                new CodeGeneration.CSharpCodeGeneratorHelper(), new Utilities.ModelUtilities(),
                new RazorTemplating(new RoslynCompilationService(), new MetadataReferencesProvider()));
    }
}
