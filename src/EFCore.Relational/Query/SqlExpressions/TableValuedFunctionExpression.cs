// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Microsoft.EntityFrameworkCore.Query.SqlExpressions
{
    public class TableValuedFunctionExpression : TableExpressionBase
    {
        internal TableValuedFunctionExpression(string name, string schema, Expression[] parameters, [NotNull] string alias)
            : base(alias)
        {
            Name = name;
            Schema = schema;
            Parameters = parameters;
        }

        public override void Print(ExpressionPrinter expressionPrinter)
        {
            if (!string.IsNullOrEmpty(Schema))
            {
                expressionPrinter.Append(Schema).Append(".");
            }

            expressionPrinter.Append(Name).Append("()").Append(" AS ").Append(Alias);
        }

        public string Name { get; }
        public string Schema { get; }
        public Expression[] Parameters { get; }

        public override bool Equals(object obj)
            // This should be reference equal only.
            => obj != null && ReferenceEquals(this, obj);

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Name, Schema, Parameters);
    }
}
