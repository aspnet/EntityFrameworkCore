// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

#nullable enable

namespace Microsoft.EntityFrameworkCore.Sqlite.Query.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class SqliteRegexMethodTranslator : IMethodCallTranslator
    {
        private readonly static MethodInfo _regexIsMatchMethodInfo
            = typeof(Regex).GetRuntimeMethod(nameof(Regex.IsMatch), new Type[] { typeof(string), typeof(string) });

        private readonly ISqlExpressionFactory _sqlExpressionFactory;
        private readonly IRelationalTypeMappingSource _typeMappingSource;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public SqliteRegexMethodTranslator(
            [NotNull] ISqlExpressionFactory sqlExpressionFactory,
            [NotNull] IRelationalTypeMappingSource typeMappingSource)
        {
            Check.NotNull(sqlExpressionFactory, nameof(sqlExpressionFactory));
            Check.NotNull(typeMappingSource, nameof(typeMappingSource));

            _sqlExpressionFactory = sqlExpressionFactory;
            _typeMappingSource = typeMappingSource;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual SqlExpression? Translate(
            SqlExpression? instance,
            MethodInfo method,
            IReadOnlyList<SqlExpression> arguments,
            IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            Check.NotNull(method, nameof(method));
            Check.NotNull(arguments, nameof(arguments));
            Check.NotNull(logger, nameof(logger));

            if (method.Equals(_regexIsMatchMethodInfo))
            {
                var input = arguments[0];
                var pattern = arguments[1];
                var stringTypeMapping = ExpressionExtensions.InferTypeMapping(input, pattern)
                    ?? _typeMappingSource.FindMapping(typeof(string));

                return SqliteExpression.Regexp(
                    _sqlExpressionFactory.ApplyTypeMapping(input, stringTypeMapping),
                    _sqlExpressionFactory.ApplyTypeMapping(pattern, stringTypeMapping),
                    _typeMappingSource.GetMapping(typeof(bool)));
            }

            return null;
        }
    }
}
