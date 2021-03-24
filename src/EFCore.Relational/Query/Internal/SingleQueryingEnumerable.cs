// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

#nullable enable

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class SingleQueryingEnumerable<T> : IEnumerable<T>, IAsyncEnumerable<T>, IRelationalQueryingEnumerable
    {
        private readonly RelationalQueryContext _relationalQueryContext;
        private readonly RelationalCommandCache _relationalCommandCache;
        private readonly Func<QueryContext, DbDataReader, ResultContext, SingleQueryResultCoordinator, T> _shaper;
        private readonly Type _contextType;
        private readonly IDiagnosticsLogger<DbLoggerCategory.Query> _queryLogger;
        private readonly bool _standAloneStateManager;
        private readonly bool _detailedErrorsEnabled;
        private readonly bool _concurrencyDetectionEnabled;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public SingleQueryingEnumerable(
            [NotNull] RelationalQueryContext relationalQueryContext,
            [NotNull] RelationalCommandCache relationalCommandCache,
            [NotNull] Func<QueryContext, DbDataReader, ResultContext, SingleQueryResultCoordinator, T> shaper,
            [NotNull] Type contextType,
            bool standAloneStateManager,
            bool detailedErrorsEnabled,
            bool concurrencyDetectionEnabled)
        {
            _relationalQueryContext = relationalQueryContext;
            _relationalCommandCache = relationalCommandCache;
            _shaper = shaper;
            _contextType = contextType;
            _queryLogger = relationalQueryContext.QueryLogger;
            _standAloneStateManager = standAloneStateManager;
            _detailedErrorsEnabled = detailedErrorsEnabled;
            _concurrencyDetectionEnabled = concurrencyDetectionEnabled;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            _relationalQueryContext.CancellationToken = cancellationToken;

            return new AsyncEnumerator(this);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IEnumerator<T> GetEnumerator()
            => new Enumerator(this);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual DbCommand CreateDbCommand()
            => _relationalCommandCache
                .GetRelationalCommand(_relationalQueryContext.ParameterValues)
                .CreateDbCommand(
                    new RelationalCommandParameterObject(
                        _relationalQueryContext.Connection,
                        _relationalQueryContext.ParameterValues,
                        null,
                        null,
                        null, CommandSource.LinqQuery),
                    Guid.Empty,
                    (DbCommandMethod)(-1));

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual string ToQueryString()
        {
            using var dbCommand = CreateDbCommand();
            return _relationalQueryContext.RelationalQueryStringFactory.Create(dbCommand);
        }

        private sealed class Enumerator : IEnumerator<T>
        {
            private readonly RelationalQueryContext _relationalQueryContext;
            private readonly RelationalCommandCache _relationalCommandCache;
            private readonly Func<QueryContext, DbDataReader, ResultContext, SingleQueryResultCoordinator, T> _shaper;
            private readonly Type _contextType;
            private readonly IDiagnosticsLogger<DbLoggerCategory.Query> _queryLogger;
            private readonly bool _standAloneStateManager;
            private readonly bool _detailedErrorsEnabled;
            private readonly IConcurrencyDetector? _concurrencyDetector;

            private IRelationalCommand? _relationalCommand;
            private RelationalDataReader? _dataReader;
            private SingleQueryResultCoordinator? _resultCoordinator;

            public Enumerator(SingleQueryingEnumerable<T> queryingEnumerable)
            {
                _relationalQueryContext = queryingEnumerable._relationalQueryContext;
                _relationalCommandCache = queryingEnumerable._relationalCommandCache;
                _shaper = queryingEnumerable._shaper;
                _contextType = queryingEnumerable._contextType;
                _queryLogger = queryingEnumerable._queryLogger;
                _standAloneStateManager = queryingEnumerable._standAloneStateManager;
                _detailedErrorsEnabled = queryingEnumerable._detailedErrorsEnabled;
                Current = default!;

                _concurrencyDetector = queryingEnumerable._concurrencyDetectionEnabled
                    ? _relationalQueryContext.ConcurrencyDetector
                    : null;
            }

            public T Current { get; private set; }

            object IEnumerator.Current
                => Current!;

            public bool MoveNext()
            {
                try
                {
                    _concurrencyDetector?.EnterCriticalSection();

                    try
                    {
                        if (_dataReader == null)
                        {
                            _relationalQueryContext.ExecutionStrategyFactory.Create()
                                .Execute(this, (_, enumerator) => InitializeReader(enumerator), null);
                        }

                        var hasNext = _resultCoordinator!.HasNext ?? _dataReader!.Read();
                        Current = default!;

                        if (hasNext)
                        {
                            while (true)
                            {
                                _resultCoordinator.ResultReady = true;
                                _resultCoordinator.HasNext = null;
                                Current = _shaper(
                                    _relationalQueryContext, _dataReader!.DbDataReader, _resultCoordinator.ResultContext,
                                    _resultCoordinator);
                                if (_resultCoordinator.ResultReady)
                                {
                                    // We generated a result so null out previously stored values
                                    _resultCoordinator.ResultContext.Values = null;
                                    break;
                                }

                                if (!_dataReader.Read())
                                {
                                    _resultCoordinator.HasNext = false;
                                    // Enumeration has ended, materialize last element
                                    _resultCoordinator.ResultReady = true;
                                    Current = _shaper(
                                        _relationalQueryContext, _dataReader.DbDataReader, _resultCoordinator.ResultContext,
                                        _resultCoordinator);

                                    break;
                                }
                            }
                        }

                        return hasNext;
                    }
                    finally
                    {
                        _concurrencyDetector?.ExitCriticalSection();
                    }
                }
                catch (Exception exception)
                {
                    _queryLogger.QueryIterationFailed(_contextType, exception);

                    throw;
                }
            }

            private static bool InitializeReader(Enumerator enumerator)
            {
                EntityFrameworkEventSource.Log.QueryExecuting();

                var relationalCommandTemplate = enumerator._relationalCommandCache.GetRelationalCommand(
                    enumerator._relationalQueryContext.ParameterValues);

                var relationalCommand = enumerator._relationalCommand = enumerator._relationalQueryContext.Connection.RentCommand();
                relationalCommand.PopulateFromTemplate(relationalCommandTemplate);

                enumerator._dataReader = relationalCommand.ExecuteReader(
                    new RelationalCommandParameterObject(
                        enumerator._relationalQueryContext.Connection,
                        enumerator._relationalQueryContext.ParameterValues,
                        enumerator._relationalCommandCache.ReaderColumns,
                        enumerator._relationalQueryContext.Context,
                        enumerator._relationalQueryContext.CommandLogger,
                        enumerator._detailedErrorsEnabled, CommandSource.LinqQuery));

                enumerator._resultCoordinator = new SingleQueryResultCoordinator();

                enumerator._relationalQueryContext.InitializeStateManager(enumerator._standAloneStateManager);

                return false;
            }

            public void Dispose()
            {
                if (_dataReader is not null)
                {
                    _relationalQueryContext.Connection.ReturnCommand(_relationalCommand!);
                    _dataReader.Dispose();
                    _dataReader = null;
                }
            }

            public void Reset()
                => throw new NotSupportedException(CoreStrings.EnumerableResetNotSupported);
        }

        private sealed class AsyncEnumerator : IAsyncEnumerator<T>
        {
            private readonly RelationalQueryContext _relationalQueryContext;
            private readonly RelationalCommandCache _relationalCommandCache;
            private readonly Func<QueryContext, DbDataReader, ResultContext, SingleQueryResultCoordinator, T> _shaper;
            private readonly Type _contextType;
            private readonly IDiagnosticsLogger<DbLoggerCategory.Query> _queryLogger;
            private readonly bool _standAloneStateManager;
            private readonly bool _detailedErrorsEnabled;
            private readonly IConcurrencyDetector? _concurrencyDetector;

            private IRelationalCommand? _relationalCommand;
            private RelationalDataReader? _dataReader;
            private SingleQueryResultCoordinator? _resultCoordinator;

            public AsyncEnumerator(SingleQueryingEnumerable<T> queryingEnumerable)
            {
                _relationalQueryContext = queryingEnumerable._relationalQueryContext;
                _relationalCommandCache = queryingEnumerable._relationalCommandCache;
                _shaper = queryingEnumerable._shaper;
                _contextType = queryingEnumerable._contextType;
                _queryLogger = queryingEnumerable._queryLogger;
                _standAloneStateManager = queryingEnumerable._standAloneStateManager;
                _detailedErrorsEnabled = queryingEnumerable._detailedErrorsEnabled;
                Current = default!;

                _concurrencyDetector = queryingEnumerable._concurrencyDetectionEnabled
                    ? _relationalQueryContext.ConcurrencyDetector
                    : null;
            }

            public T Current { get; private set; }

            public async ValueTask<bool> MoveNextAsync()
            {
                try
                {
                    _concurrencyDetector?.EnterCriticalSection();

                    try
                    {
                        if (_dataReader == null)
                        {
                            await _relationalQueryContext.ExecutionStrategyFactory.Create()
                                .ExecuteAsync(
                                    this,
                                    (_, enumerator, cancellationToken) => InitializeReaderAsync(enumerator, cancellationToken),
                                    null,
                                    _relationalQueryContext.CancellationToken)
                                .ConfigureAwait(false);
                        }

                        var hasNext = _resultCoordinator!.HasNext
                            ?? await _dataReader!.ReadAsync(_relationalQueryContext.CancellationToken).ConfigureAwait(false);
                        Current = default!;

                        if (hasNext)
                        {
                            while (true)
                            {
                                _resultCoordinator.ResultReady = true;
                                _resultCoordinator.HasNext = null;
                                Current = _shaper(
                                    _relationalQueryContext, _dataReader!.DbDataReader, _resultCoordinator.ResultContext,
                                    _resultCoordinator);
                                if (_resultCoordinator.ResultReady)
                                {
                                    // We generated a result so null out previously stored values
                                    _resultCoordinator.ResultContext.Values = null;
                                    break;
                                }

                                if (!await _dataReader.ReadAsync(_relationalQueryContext.CancellationToken).ConfigureAwait(false))
                                {
                                    _resultCoordinator.HasNext = false;
                                    // Enumeration has ended, materialize last element
                                    _resultCoordinator.ResultReady = true;
                                    Current = _shaper(
                                        _relationalQueryContext, _dataReader.DbDataReader, _resultCoordinator.ResultContext,
                                        _resultCoordinator);

                                    break;
                                }
                            }
                        }

                        return hasNext;
                    }
                    finally
                    {
                        _concurrencyDetector?.ExitCriticalSection();
                    }
                }
                catch (Exception exception)
                {
                    _queryLogger.QueryIterationFailed(_contextType, exception);

                    throw;
                }
            }

            private static async Task<bool> InitializeReaderAsync(AsyncEnumerator enumerator, CancellationToken cancellationToken)
            {
                EntityFrameworkEventSource.Log.QueryExecuting();

                var relationalCommandTemplate = enumerator._relationalCommandCache.GetRelationalCommand(
                    enumerator._relationalQueryContext.ParameterValues);

                var relationalCommand = enumerator._relationalCommand = enumerator._relationalQueryContext.Connection.RentCommand();
                relationalCommand.PopulateFromTemplate(relationalCommandTemplate);

                enumerator._dataReader = await relationalCommand.ExecuteReaderAsync(
                    new RelationalCommandParameterObject(
                        enumerator._relationalQueryContext.Connection,
                        enumerator._relationalQueryContext.ParameterValues,
                        enumerator._relationalCommandCache.ReaderColumns,
                        enumerator._relationalQueryContext.Context,
                        enumerator._relationalQueryContext.CommandLogger,
                        enumerator._detailedErrorsEnabled, CommandSource.LinqQuery),
                    cancellationToken)
                    .ConfigureAwait(false);

                enumerator._resultCoordinator = new SingleQueryResultCoordinator();

                enumerator._relationalQueryContext.InitializeStateManager(enumerator._standAloneStateManager);

                return false;
            }

            public ValueTask DisposeAsync()
            {
                if (_dataReader is not null)
                {
                    _relationalQueryContext.Connection.ReturnCommand(_relationalCommand!);

                    var dataReader = _dataReader;
                    _dataReader = null;

                    return dataReader.DisposeAsync();
                }

                return default;
            }
        }
    }
}
