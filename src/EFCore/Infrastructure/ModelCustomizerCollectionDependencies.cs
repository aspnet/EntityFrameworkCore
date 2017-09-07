// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="ModelCustomizerCollection" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    ///     <para>
    ///         Do not construct instances of this class directly from either provider or application code as the
    ///         constructor signature may change as new dependencies are added. Instead, use this type in
    ///         your constructor so that an instance will be created and injected automatically by the
    ///         dependency injection container. To create an instance with some dependent services replaced,
    ///         first resolve the object from the dependency injection container, then replace selected
    ///         services using the 'With...' methods. Do not call the constructor at any point in this process.
    ///     </para>
    /// </summary>
    public sealed class ModelCustomizerCollectionDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="ModelCustomizerCollection" />.
        ///     </para>
        ///     <para>
        ///         This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///         directly from your code. This API may change or be removed in future releases.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from either provider or application code as it may change
        ///         as new dependencies are added. Instead, use this type in your constructor so that an instance
        ///         will be created and injected automatically by the dependency injection container. To create
        ///         an instance with some dependent services replaced, first resolve the object from the dependency
        ///         injection container, then replace selected services using the 'With...' methods. Do not call
        ///         the constructor at any point in this process.
        ///     </para>
        /// </summary>
        public ModelCustomizerCollectionDependencies(
            [NotNull] IModelCustomizer modelCustomizer,
            [NotNull] IEnumerable<IAdditionalModelCustomizer> additionalModelCustomizers)
        {
            Check.NotNull(modelCustomizer, nameof(modelCustomizer));
            Check.NotNull(additionalModelCustomizers, nameof(additionalModelCustomizers));

            ModelCustomizer = modelCustomizer;
            AdditionalModelCustomizers = additionalModelCustomizers;
        }

        /// <summary>
        ///     Gets the <see cref="IModelCustomizer" /> that will perform additional configuration of the model
        ///     in addition to what is discovered by convention.
        /// </summary>
        public IModelCustomizer ModelCustomizer { get; }

        /// <summary>
        ///     Gets the <see cref="IModelCustomizer" /> collection that will perform additional configuration of the model
        ///     in addition to what is discovered by convention.
        /// </summary>
        public IEnumerable<IAdditionalModelCustomizer> AdditionalModelCustomizers { get; }

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="modelCustomizer"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public ModelCustomizerCollectionDependencies With([NotNull] IModelCustomizer modelCustomizer)
            => new ModelCustomizerCollectionDependencies(modelCustomizer, AdditionalModelCustomizers);

        /// <summary>
        ///     Clones this dependency parameter object with one service replaced.
        /// </summary>
        /// <param name="additionalModelCustomizers"> A replacement for the current dependency of this type. </param>
        /// <returns> A new parameter object with the given service replaced. </returns>
        public ModelCustomizerCollectionDependencies With([NotNull] IEnumerable<IAdditionalModelCustomizer> additionalModelCustomizers)
            => new ModelCustomizerCollectionDependencies(ModelCustomizer, additionalModelCustomizers);
    }
}
