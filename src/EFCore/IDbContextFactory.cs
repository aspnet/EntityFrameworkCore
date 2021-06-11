// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     Defines a factory for creating <see cref="DbContext" /> instances.
    /// </summary>
    /// <typeparam name="TContext"> The <see cref="DbContext" /> type to create. </typeparam>
    public interface IDbContextFactory<TContext>
        where TContext : DbContext
    {
        /// <summary>
        ///     <para>
        ///         Creates a new <see cref="DbContext" /> instance.
        ///     </para>
        ///     <para>
        ///         The caller is responsible for disposing the context; it will not be disposed by any dependency injection container.
        ///     </para>
        /// </summary>
        /// <returns> A new context instance. </returns>
        TContext CreateDbContext();

        /// <summary>
        ///     <para>
        ///         Creates a new <see cref="DbContext" /> instance in an async context.
        ///     </para>
        ///     <para>
        ///         The caller is responsible for disposing the context; it will not be disposed by any dependency injection container.
        ///     </para>
        /// </summary>
        /// <param name="cancellationToken"> A <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns> A task containing the created context that represents the asynchronous operation. </returns>
        /// <exception cref="OperationCanceledException"> If the <see cref="CancellationToken" /> is canceled. </exception>
        Task<TContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(CreateDbContext());
    }
}
