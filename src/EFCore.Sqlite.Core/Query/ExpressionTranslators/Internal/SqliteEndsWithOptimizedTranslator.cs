﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Expressions;

namespace Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqliteEndsWithOptimizedTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo _methodInfo
            = typeof(string).GetRuntimeMethod(nameof(string.EndsWith), new[] { typeof(string) });

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (Equals(methodCallExpression.Method, _methodInfo))
            {
                var patternExpression = methodCallExpression.Arguments[0];

                var endsWithExpression = new NullCompensatedExpression(
                    Expression.Equal(
                        new SqlFunctionExpression(
                            "substr",
                            // ReSharper disable once PossibleNullReferenceException
                            methodCallExpression.Object.Type,
                            new[]
                            {
                                methodCallExpression.Object,
                                Expression.Negate(new SqlFunctionExpression("length", typeof(int), new[] { patternExpression }))
                            }),
                        patternExpression));

                return patternExpression is ConstantExpression patternConstantExpression
                    ? (string)patternConstantExpression.Value == string.Empty
                        ? (Expression)Expression.Constant(true)
                        : endsWithExpression
                    : Expression.OrElse(
                        endsWithExpression,
                        Expression.Equal(patternExpression, Expression.Constant(string.Empty)));
            }

            return null;
        }
    }
}
