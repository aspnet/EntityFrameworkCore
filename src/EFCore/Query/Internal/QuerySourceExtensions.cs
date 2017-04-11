// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Utilities;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Clauses.ResultOperators;

namespace Microsoft.EntityFrameworkCore.Query.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class QuerySourceExtensions
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static bool HasGeneratedItemName([NotNull] this IQuerySource querySource)
        {
            Check.NotNull(querySource, nameof(querySource));
            Check.NotEmpty(querySource.ItemName, nameof(querySource.ItemName));

            return querySource.ItemName.StartsWith("<generated>_", StringComparison.Ordinal);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static GroupJoinClause TryGetFlattenedGroupJoinClause([NotNull] this AdditionalFromClause additionalFromClause)
            => (additionalFromClause.FromExpression is SubQueryExpression subQueryExpression
                && subQueryExpression.QueryModel.IsIdentityQuery()
                && subQueryExpression.QueryModel.ResultOperators.Count == 1
                && subQueryExpression.QueryModel.ResultOperators[0] is DefaultIfEmptyResultOperator
                && subQueryExpression.QueryModel.MainFromClause.FromExpression is QuerySourceReferenceExpression subqueryQsre
                && subqueryQsre.ReferencedQuerySource is GroupJoinClause groupJoinClause)
                ? groupJoinClause
                : null;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static SubQueryExpression TryGetUnderlyingSubQueryExpression([NotNull] this IQuerySource querySource)
        {
            Check.NotNull(querySource, nameof(querySource));
            
            switch (querySource)
            {
                case FromClauseBase fromClauseBase:
                    return fromClauseBase.FromExpression as SubQueryExpression;
                case JoinClause joinClause:
                    return joinClause.InnerSequence as SubQueryExpression;
                case GroupJoinClause groupJoinClause:
                    return groupJoinClause.JoinClause.InnerSequence as SubQueryExpression;
                default:
                    return null;
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static GroupResultOperator TryGetUnderlyingGroupResultOperator([NotNull] this IQuerySource querySource)
        {
            Check.NotNull(querySource, nameof(querySource));

            return querySource.TryGetUnderlyingSubQueryExpression()?.QueryModel.ResultOperators.LastOrDefault() as GroupResultOperator;
        }
    }
}
