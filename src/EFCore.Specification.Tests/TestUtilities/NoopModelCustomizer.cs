// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class NoopModelCustomizer : IAdditionalModelCustomizer
    {
        public void Customize(ModelBuilder modelBuilder, DbContext context)
        {
        }
    }
}
