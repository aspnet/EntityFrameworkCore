// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     Base class for the snapshot of the <see cref="IModel" /> state generated by Migrations.
    /// </summary>
    public abstract class ModelSnapshot
    {
        private IModel _model;

        private IModel CreateModel()
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            var model = new Model();
            var modelBuilder = new ModelBuilder(model);

            BuildModel(modelBuilder);

            return model;
#pragma warning restore EF1001 // Internal EF Core API usage.
        }

        /// <summary>
        ///     The snapshot model.
        /// </summary>
        public virtual IModel Model => _model ??= CreateModel();

        /// <summary>
        ///     Called lazily by <see cref="Model" /> to build the model snapshot
        ///     the first time it is requested.
        /// </summary>
        /// <param name="modelBuilder"> The <see cref="ModelBuilder" /> to use to build the model. </param>
        protected abstract void BuildModel([NotNull] ModelBuilder modelBuilder);
    }
}
