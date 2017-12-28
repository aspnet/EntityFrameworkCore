// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.Logging;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Query
{
    public class WarningsTest
    {
        [Fact]
        public void Should_throw_by_default_when_transaction()
        {
            var optionsBuilder
                = new DbContextOptionsBuilder()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            using (var context = new DbContext(optionsBuilder.Options))
            {
                Assert.Equal(
                    CoreStrings.WarningAsErrorTemplate(
                        InMemoryEventId.TransactionIgnoredWarning,
                        InMemoryStrings.LogTransactionsNotSupported.GenerateMessage()),
                    Assert.Throws<InvalidOperationException>(
                        () => context.Database.BeginTransaction()).Message);
            }
        }

        [Fact]
        public void Should_throw_by_default_when_transaction_enlisted()
        {
            var optionsBuilder
                = new DbContextOptionsBuilder()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            using (var context = new DbContext(optionsBuilder.Options))
            {
#if Test20
                Assert.Equal(
                    CoreStrings.TransactionsNotSupported,
                    Assert.Throws<NotSupportedException>(
                        () => context.Database.EnlistTransaction(new CommittableTransaction())).Message);
#else
                Assert.Equal(
                    CoreStrings.WarningAsErrorTemplate(
                        InMemoryEventId.TransactionIgnoredWarning,
                        InMemoryStrings.LogTransactionsNotSupported.GenerateMessage()),
                    Assert.Throws<InvalidOperationException>(
                        () => context.Database.EnlistTransaction(new CommittableTransaction())).Message);
#endif
            }
        }

        [Fact]
        public void Should_not_throw_by_default_when_transaction_and_ignored()
        {
            var optionsBuilder
                = new DbContextOptionsBuilder()
                    .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

            using (var context = new DbContext(optionsBuilder.Options))
            {
                context.Database.BeginTransaction();
            }
        }

        [Fact]
        public void Throws_when_warning_as_error_all()
        {
            using (var context = new WarningAsErrorContext())
            {
                Assert.Equal(
                    CoreStrings.WarningAsErrorTemplate(
                        CoreEventId.FirstWithoutOrderByAndFilterWarning.ToString(),
                        CoreStrings.LogFirstWithoutOrderByAndFilter.GenerateMessage(
                            "(from WarningAsErrorEntity <generated>_1 in DbSet<WarningAsErrorEntity> select [<generated>_1]).Firs...")),
                    Assert.Throws<InvalidOperationException>(
                        () => context.WarningAsErrorEntities.FirstOrDefault()).Message);
            }
        }

        [Fact]
        public void Throws_when_warning_as_error_specific()
        {
            using (var context = new WarningAsErrorContext(toThrow: CoreEventId.FirstWithoutOrderByAndFilterWarning))
            {
                Assert.Equal(
                    CoreStrings.WarningAsErrorTemplate(
                        CoreEventId.FirstWithoutOrderByAndFilterWarning.ToString(),
                        CoreStrings.LogFirstWithoutOrderByAndFilter.GenerateMessage(
                            "(from WarningAsErrorEntity <generated>_1 in DbSet<WarningAsErrorEntity> select [<generated>_1]).Firs...")),
                    Assert.Throws<InvalidOperationException>(
                        () => context.WarningAsErrorEntities.FirstOrDefault()).Message);
            }
        }

        [Fact]
        public void Logs_by_default_for_ignored_includes()
        {
            var messages = new List<string>();
            using (var context = new WarningAsErrorContext(new FakeLoggerFactory(messages)))
            {
                context.WarningAsErrorEntities.Include(e => e.Nav).OrderBy(e => e.Id).Select(e => e.Id).ToList();

                Assert.Contains(CoreStrings.LogIgnoredInclude.GenerateMessage("[e].Nav"), messages);
            }
        }

        [Fact]
        public void Ignored_includes_can_be_configured_to_throw()
        {
            using (var context = new WarningAsErrorContext(toThrow: CoreEventId.IncludeIgnoredWarning))
            {
                Assert.Equal(
                    CoreStrings.WarningAsErrorTemplate(
                        CoreEventId.IncludeIgnoredWarning.ToString(),
                        CoreStrings.LogIgnoredInclude.GenerateMessage("[e].Nav")),
                    Assert.Throws<InvalidOperationException>(
                        ()
                            => context.WarningAsErrorEntities.Include(e => e.Nav).OrderBy(e => e.Id).Select(e => e.Id).ToList()).Message);
            }
        }

        [Fact]
        public void Throws_by_default_for_lazy_load_with_disposed_context()
        {
            var messages = new List<string>();
            var loggerFactory = new FakeLoggerFactory(messages);

            using (var context = new WarningAsErrorContext(loggerFactory))
            {
                context.Add(new WarningAsErrorEntity { Nav = new IncludedEntity() });
                context.SaveChanges();
            }

            WarningAsErrorEntity entity;
            
            using (var context = new WarningAsErrorContext(loggerFactory))
            {
                entity = context.WarningAsErrorEntities.OrderBy(e => e.Id).First();
            }

            Assert.Equal(
                CoreStrings.WarningAsErrorTemplate(
                    CoreEventId.LazyLoadOnDisposedContextWarning.ToString(),
                    CoreStrings.LazyLoadOnDisposedContextWarning.GenerateMessage("Nav", "WarningAsErrorEntity")),
                Assert.Throws<InvalidOperationException>(
                    () => entity.Nav).Message);
        }

        [Fact]
        public void Lazy_load_with_disposed_context_can_be_configured_to_log()
        {
            var messages = new List<string>();
            var loggerFactory = new FakeLoggerFactory(messages);

            using (var context = new WarningAsErrorContext(
                loggerFactory,
                CoreEventId.LazyLoadOnDisposedContextWarning))
            {
                context.Add(new WarningAsErrorEntity { Nav = new IncludedEntity() });
                context.SaveChanges();
            }

            WarningAsErrorEntity entity;

            using (var context = new WarningAsErrorContext(
                loggerFactory,
                CoreEventId.LazyLoadOnDisposedContextWarning))
            {
                entity = context.WarningAsErrorEntities.OrderBy(e => e.Id).First();
            }

            Assert.Null(entity.Nav);

            Assert.Contains(
                CoreStrings.LazyLoadOnDisposedContextWarning.GenerateMessage("Nav", "WarningAsErrorEntity"),
                messages);
        }

        [Fact]
        public void Lazy_loading_is_logged_only_when_actually_loading()
        {
            var messages = new List<string>();
            var loggerFactory = new FakeLoggerFactory(messages);

            using (var context = new WarningAsErrorContext(loggerFactory))
            {
                context.Add(new WarningAsErrorEntity { Nav = new IncludedEntity() });
                context.SaveChanges();
            }

            using (var context = new WarningAsErrorContext(loggerFactory))
            {
                var entity = context.WarningAsErrorEntities.OrderBy(e => e.Id).First();

                messages.Clear();
                Assert.NotNull(entity.Nav);

                Assert.Contains(
                    CoreStrings.NavigationLazyLoading.GenerateMessage("Nav", "WarningAsErrorEntity"),
                    messages);

                messages.Clear();
                Assert.NotNull(entity.Nav);
                Assert.DoesNotContain(
                    CoreStrings.NavigationLazyLoading.GenerateMessage("Nav", "WarningAsErrorEntity"),
                    messages);
            }
        }

        [Fact]
        public void No_throw_when_event_id_not_registered()
        {
            using (var context = new WarningAsErrorContext(toThrow: CoreEventId.SensitiveDataLoggingEnabledWarning))
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                context.WarningAsErrorEntities.FirstOrDefault();
            }
        }

        private class WarningAsErrorContext : DbContext
        {
            private readonly FakeLoggerFactory _sink;
            private readonly EventId? _toLog;
            private readonly EventId? _toThrow;

            public WarningAsErrorContext(
                FakeLoggerFactory sink = null,
                EventId? toLog = null,
                EventId? toThrow = null)
            {
                _sink = sink;
                _toLog = toLog;
                _toThrow = toThrow;
            }

            public DbSet<WarningAsErrorEntity> WarningAsErrorEntities { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder
                    .UseLoggerFactory(_sink ?? new FakeLoggerFactory(null))
                    .UseInMemoryDatabase(nameof(WarningAsErrorContext)).ConfigureWarnings(
                        c =>
                            {
                                if (_toThrow != null)
                                {
                                    c.Throw(_toThrow.Value);
                                }
                                else if (_toLog != null)
                                {
                                    c.Log(_toLog.Value);
                                }
                                else if (_sink == null)
                                {
                                    c.Default(WarningBehavior.Throw);
                                }
                            });
        }

        private class WarningAsErrorEntity
        {
            private readonly Action<object, string> _loader;
            private IncludedEntity _nav;

            public WarningAsErrorEntity()
            {
            }

            private WarningAsErrorEntity(Action<object, string> lazyLoader)
            {
                _loader = lazyLoader;
            }

            public IncludedEntity Nav
            {
                get => _loader.Load(this, ref _nav);
                set => _nav = value;
            }

            public string Id { get; set; }
        }

        private class IncludedEntity
        {
            public int Id { get; set; }
        }

        private class FakeLoggerFactory : ILoggerFactory
        {
            private readonly IList<string> _sink;

            public FakeLoggerFactory(IList<string> sink) => _sink = sink;

            public void Dispose()
            {
            }

            public ILogger CreateLogger(string categoryName) => new FakeLogger(_sink);

            private class FakeLogger : ILogger
            {
                private readonly IList<string> _sink;

                public FakeLogger(IList<string> sink) => _sink = sink;

                public void Log<TState>(
                    LogLevel logLevel,
                    EventId eventId,
                    TState state,
                    Exception exception,
                    Func<TState, Exception, string> formatter)
                {
                    _sink?.Add(formatter(state, exception));
                }

                public bool IsEnabled(LogLevel logLevel) => true;

                public IDisposable BeginScope<TState>(TState state) => null;
            }

            public void AddProvider(ILoggerProvider provider)
            {
            }
        }
    }
}
