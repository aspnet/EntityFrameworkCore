// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Sqlite.Query.SqlExpressions.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Sqlite.Query.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class SqliteSqlNullabilityProcessor : SqlNullabilityProcessor
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public SqliteSqlNullabilityProcessor(
            [NotNull] RelationalParameterBasedSqlProcessorDependencies dependencies,
            bool useRelationalNulls)
            : base(dependencies, useRelationalNulls)
        {
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected override SqlExpression VisitCustomSqlExpression(
            SqlExpression sqlExpression,
            bool allowOptimizedExpansion,
            out bool nullable)
            => sqlExpression switch
            {
                SqliteGlobExpression globExpression => VisitGlob(globExpression, allowOptimizedExpansion, out nullable),
                SqliteRegexpExpression regexpExpression => VisitRegexp(regexpExpression, allowOptimizedExpansion, out nullable),
                _ => base.VisitCustomSqlExpression(sqlExpression, allowOptimizedExpansion, out nullable)
            };

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected virtual SqlExpression VisitGlob(
            [NotNull] SqliteGlobExpression globExpression,
            bool allowOptimizedExpansion,
            out bool nullable)
        {
            Check.NotNull(globExpression, nameof(globExpression));

            var match = Visit(globExpression.Match, out var matchNullable);
            var pattern = Visit(globExpression.Pattern, out var patternNullable);

            nullable = matchNullable || patternNullable;

            return globExpression.Update(match, pattern);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected virtual SqlExpression VisitRegexp(
            [NotNull] SqliteRegexpExpression regexpExpression,
            bool allowOptimizedExpansion,
            out bool nullable)
        {
            Check.NotNull(regexpExpression, nameof(regexpExpression));

            var match = Visit(regexpExpression.Match, out var matchNullable);
            var pattern = Visit(regexpExpression.Pattern, out var patternNullable);

            nullable = matchNullable || patternNullable;

            return regexpExpression.Update(match, pattern);
        }
    }
}
