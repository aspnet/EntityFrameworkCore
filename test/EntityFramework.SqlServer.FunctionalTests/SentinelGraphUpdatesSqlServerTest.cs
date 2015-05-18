// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.Data.Entity.SqlServer.FunctionalTests
{
    public class SentinelGraphUpdatesSqlServerTest : GraphUpdatesSqlServerTestBase<SentinelGraphUpdatesSqlServerTest.SentinelGraphUpdatesSqlServerFixture>
    {
        public SentinelGraphUpdatesSqlServerTest(SentinelGraphUpdatesSqlServerFixture fixture)
            : base(fixture)
        {
        }

        public class SentinelGraphUpdatesSqlServerFixture : GraphUpdatesSqlServerFixtureBase
        {
            protected override string DatabaseName => "SentinelGraphUpdatesTest";

            public override int IntSentinel => -1;

            protected override void OnModelCreating(ModelBuilder model)
            {
                base.OnModelCreating(model);

                model.Sequence("StartAtZeroSequence").Start(0);
                model.ForSqlServer().UseSequence("StartAtZeroSequence");

                SetSentinelValues(model);
            }
        }
    }
}
