// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     Relational database specific extension methods for LINQ queries.
    /// </summary>
    public static class RelationalQueryableExtensions
    {
        /// <summary>
        ///     <para>
        ///         Creates a <see cref="DbCommand" /> set up to execute this query.
        ///     </para>
        ///     <para>
        ///         This is only typically supported by queries generated by Entity Framework Core.
        ///     </para>
        ///     <para>
        ///         Warning: there is no guarantee that executing this command directly will result in the same behavior as if EF Core had
        ///         executed the command.
        ///     </para>
        ///     <para>
        ///         Note that DbCommand is an <see cref="IDisposable" /> object. The caller is responsible for disposing the returned
        ///         command.
        ///     </para>
        ///     <para>
        ///         This is only typically supported by queries generated by Entity Framework Core.
        ///     </para>
        /// </summary>
        /// <param name="source"> The query source. </param>
        /// <returns> The query string for debugging. </returns>
        public static DbCommand CreateDbCommand(this IQueryable source)
        {
            Check.NotNull(source, nameof(source));

            if (source.Provider.Execute<IEnumerable>(source.Expression) is IRelationalQueryingEnumerable queryingEnumerable)
            {
                return queryingEnumerable.CreateDbCommand();
            }

            throw new NotSupportedException(RelationalStrings.NoDbCommand);
        }

        /// <summary>
        ///     <para>
        ///         Creates a LINQ query based on a raw SQL query.
        ///     </para>
        ///     <para>
        ///         If the database provider supports composing on the supplied SQL, you can compose on top of the raw SQL query using
        ///         LINQ operators: <c>context.Blogs.FromSqlRaw("SELECT * FROM dbo.Blogs").OrderBy(b => b.Name)</c>.
        ///     </para>
        ///     <para>
        ///         As with any API that accepts SQL it is important to parameterize any user input to protect against a SQL injection
        ///         attack. You can include parameter place holders in the SQL query string and then supply parameter values as additional
        ///         arguments. Any parameter values you supply will automatically be converted to a DbParameter:
        ///     </para>
        ///     <code>context.Blogs.FromSqlRaw("SELECT * FROM [dbo].[SearchBlogs]({0})", userSuppliedSearchTerm)</code>
        ///     <para>
        ///         However, <b>never</b> pass a concatenated or interpolated string (<c>$""</c>) with non-validated user-provided values
        ///         into this method. Doing so may expose your application to SQL injection attacks. To use the interpolated string syntax,
        ///         consider using <see cref="FromSqlInterpolated{TEntity}" /> to create parameters.
        ///     </para>
        ///     <para>
        ///         This overload also accepts DbParameter instances as parameter values. This allows you to use named
        ///         parameters in the SQL query string:
        ///     </para>
        ///     <code>context.Blogs.FromSqlRaw("SELECT * FROM [dbo].[SearchBlogs]({@searchTerm})", new SqlParameter("@searchTerm", userSuppliedSearchTerm))</code>
        /// </summary>
        /// <typeparam name="TEntity"> The type of the elements of <paramref name="source" />. </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to use as the base of the raw SQL query (typically a <see cref="DbSet{TEntity}" />).
        /// </param>
        /// <param name="sql"> The raw SQL query. </param>
        /// <param name="parameters"> The values to be assigned to parameters. </param>
        /// <returns> An <see cref="IQueryable{T}" /> representing the raw SQL query. </returns>
        [StringFormatMethod("sql")]
        public static IQueryable<TEntity> FromSqlRaw<TEntity>(
            this DbSet<TEntity> source,
            [NotParameterized] string sql,
            params object[] parameters)
            where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotEmpty(sql, nameof(sql));
            Check.NotNull(parameters, nameof(parameters));

            var queryableSource = (IQueryable)source;
            return queryableSource.Provider.CreateQuery<TEntity>(
                GenerateFromSqlQueryRoot(
                    queryableSource,
                    sql,
                    parameters));
        }

        /// <summary>
        ///     <para>
        ///         Creates a LINQ query based on an interpolated string representing a SQL query.
        ///     </para>
        ///     <para>
        ///         If the database provider supports composing on the supplied SQL, you can compose on top of the raw SQL query using
        ///         LINQ operators:
        ///     </para>
        ///     <code>context.Blogs.FromSqlInterpolated($"SELECT * FROM dbo.Blogs").OrderBy(b => b.Name)</code>
        ///     <para>
        ///         As with any API that accepts SQL it is important to parameterize any user input to protect against a SQL injection
        ///         attack. You can include interpolated parameter place holders in the SQL query string. Any interpolated parameter values
        ///         you supply will automatically be converted to a DbParameter:
        ///     </para>
        ///     <code>context.Blogs.FromSqlInterpolated($"SELECT * FROM [dbo].[SearchBlogs]({userSuppliedSearchTerm})")</code>
        /// </summary>
        /// <typeparam name="TEntity"> The type of the elements of <paramref name="source" />. </typeparam>
        /// <param name="source">
        ///     An <see cref="IQueryable{T}" /> to use as the base of the interpolated string SQL query (typically a <see cref="DbSet{TEntity}" />).
        /// </param>
        /// <param name="sql"> The interpolated string representing a SQL query with parameters. </param>
        /// <returns> An <see cref="IQueryable{T}" /> representing the interpolated string SQL query. </returns>
        public static IQueryable<TEntity> FromSqlInterpolated<TEntity>(
            this DbSet<TEntity> source,
            [NotParameterized] FormattableString sql)
            where TEntity : class
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(sql, nameof(sql));
            Check.NotEmpty(sql.Format, nameof(source));

            var queryableSource = (IQueryable)source;
            return queryableSource.Provider.CreateQuery<TEntity>(
                GenerateFromSqlQueryRoot(
                    queryableSource,
                    sql.Format,
                    sql.GetArguments()));
        }

        private static FromSqlQueryRootExpression GenerateFromSqlQueryRoot(
            IQueryable source,
            string sql,
            object?[] arguments,
            [CallerMemberName] string memberName = null!)
        {
            var queryRootExpression = (QueryRootExpression)source.Expression;

            var entityType = queryRootExpression.EntityType;
            if ((entityType.BaseType != null || entityType.GetDirectlyDerivedTypes().Any())
                && entityType.FindDiscriminatorProperty() == null)
            {
                throw new InvalidOperationException(RelationalStrings.MethodOnNonTPHRootNotSupported(memberName, entityType.DisplayName()));
            }

            return new FromSqlQueryRootExpression(
                queryRootExpression.QueryProvider!,
                entityType,
                sql,
                Expression.Constant(arguments));
        }

        /// <summary>
        ///     <para>
        ///         Returns a new query which is configured to load the collections in the query results in a single database query.
        ///     </para>
        ///     <para>
        ///         This behavior generally guarantees result consistency in the face of concurrent updates
        ///         (but details may vary based on the database and transaction isolation level in use).
        ///         However, this can cause performance issues when the query loads multiple related collections.
        ///     </para>
        ///     <para>
        ///         The default query splitting behavior for queries can be controlled by
        ///         <see cref="RelationalDbContextOptionsBuilder{TBuilder,TExtension}.UseQuerySplittingBehavior(QuerySplittingBehavior)" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TEntity"> The type of entity being queried. </typeparam>
        /// <param name="source"> The source query. </param>
        /// <returns> A new query where collections will be loaded through single database query. </returns>
        public static IQueryable<TEntity> AsSingleQuery<TEntity>(
            this IQueryable<TEntity> source)
            where TEntity : class
        {
            Check.NotNull(source, nameof(source));

            return source.Provider is EntityQueryProvider
                ? source.Provider.CreateQuery<TEntity>(
                    Expression.Call(AsSingleQueryMethodInfo.MakeGenericMethod(typeof(TEntity)), source.Expression))
                : source;
        }

        internal static readonly MethodInfo AsSingleQueryMethodInfo
            = typeof(RelationalQueryableExtensions).GetRequiredDeclaredMethod(nameof(AsSingleQuery));

        /// <summary>
        ///     <para>
        ///         Returns a new query which is configured to load the collections in the query results through separate database queries.
        ///     </para>
        ///     <para>
        ///         This behavior can significantly improve performance when the query loads multiple collections.
        ///         However, since separate queries are used, this can result in inconsistent results when concurrent updates occur.
        ///         Serializable or snapshot transactions can be used to mitigate this
        ///         and achieve consistency with split queries, but that may bring other performance costs and behavioral difference.
        ///     </para>
        ///     <para>
        ///         The default query splitting behavior for queries can be controlled by
        ///         <see cref="RelationalDbContextOptionsBuilder{TBuilder, TExtension}.UseQuerySplittingBehavior(QuerySplittingBehavior)" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TEntity"> The type of entity being queried. </typeparam>
        /// <param name="source"> The source query. </param>
        /// <returns> A new query where collections will be loaded through separate database queries. </returns>
        public static IQueryable<TEntity> AsSplitQuery<TEntity>(
            this IQueryable<TEntity> source)
            where TEntity : class
        {
            Check.NotNull(source, nameof(source));

            return source.Provider is EntityQueryProvider
                ? source.Provider.CreateQuery<TEntity>(
                    Expression.Call(AsSplitQueryMethodInfo.MakeGenericMethod(typeof(TEntity)), source.Expression))
                : source;
        }

        internal static readonly MethodInfo AsSplitQueryMethodInfo
            = typeof(RelationalQueryableExtensions).GetRequiredDeclaredMethod(nameof(AsSplitQuery));
    }
}
