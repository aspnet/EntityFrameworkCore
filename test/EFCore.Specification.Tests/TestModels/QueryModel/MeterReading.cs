﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.EntityFrameworkCore.TestModels.QueryModel
{
    public class MeterReading
    {
        public int Id { get; set; }
        public MeterReadingStatus? ReadingStatus { get; set; }
        public MeterReadingDetail MeterReadingDetails { get; set; }
    }
}