﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Update;

namespace Microsoft.EntityFrameworkCore.Diagnostics
{
    /// <summary>
    ///     A <see cref="DiagnosticSource" /> event payload class for events that have
    ///     an entity type.
    /// </summary>
    public class UpdateEntryEventData : EventData
    {
        /// <summary>
        ///     Constructs the event payload.
        /// </summary>
        /// <param name="eventDefinition"> The event definition. </param>
        /// <param name="messageGenerator"> A delegate that generates a log message for this event. </param>
        /// <param name="entityEntry"> The entry for the entity instance on which the property value has changed. </param>
        public UpdateEntryEventData(
            EventDefinitionBase eventDefinition,
            Func<EventDefinitionBase, EventData, string> messageGenerator,
            IUpdateEntry entityEntry)
            : base(eventDefinition, messageGenerator)
        {
            EntityEntry = entityEntry;
        }

        /// <summary>
        ///     The entry for the entity instance on which the property value has changed.
        /// </summary>
        public virtual IUpdateEntry EntityEntry { get; }
    }
}
