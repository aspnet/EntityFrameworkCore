﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Query
{
    public class ComplexNavigationsQuerySqlServerTest : ComplexNavigationsQueryTestBase<ComplexNavigationsQuerySqlServerFixture>
    {
        public ComplexNavigationsQuerySqlServerTest(
            ComplexNavigationsQuerySqlServerFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        private bool SupportsOffset => TestEnvironment.GetFlag(nameof(SqlServerCondition.SupportsOffset)) ?? true;

        public override async Task Entity_equality_empty(bool isAsync)
        {
            await base.Entity_equality_empty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE ([l0].[Id] = 0) AND [l0].[Id] IS NOT NULL");
        }

        public override async Task Key_equality_when_sentinel_ef_property(bool isAsync)
        {
            await base.Key_equality_when_sentinel_ef_property(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE ([l0].[Id] = 0) AND [l0].[Id] IS NOT NULL");
        }

        public override async Task Key_equality_using_property_method_required(bool isAsync)
        {
            await base.Key_equality_using_property_method_required(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
WHERE [l0].[Id] > 7");
        }

        public override async Task Key_equality_using_property_method_required2(bool isAsync)
        {
            await base.Key_equality_using_property_method_required2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
WHERE [l0].[Id] > 7");
        }

        public override async Task Key_equality_using_property_method_nested(bool isAsync)
        {
            await base.Key_equality_using_property_method_nested(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
WHERE ([l0].[Id] = 7) AND [l0].[Id] IS NOT NULL");
        }

        public override async Task Key_equality_using_property_method_nested2(bool isAsync)
        {
            await base.Key_equality_using_property_method_nested2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
WHERE [l0].[Id] = 7");
        }

        public override async Task Key_equality_using_property_method_and_member_expression1(bool isAsync)
        {
            await base.Key_equality_using_property_method_and_member_expression1(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
WHERE ([l0].[Id] = 7) AND [l0].[Id] IS NOT NULL");
        }

        public override async Task Key_equality_using_property_method_and_member_expression2(bool isAsync)
        {
            await base.Key_equality_using_property_method_and_member_expression2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
WHERE ([l0].[Id] = 7) AND [l0].[Id] IS NOT NULL");
        }

        public override async Task Key_equality_using_property_method_and_member_expression3(bool isAsync)
        {
            await base.Key_equality_using_property_method_and_member_expression3(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
WHERE [l0].[Id] = 7");
        }

        public override async Task Key_equality_navigation_converted_to_FK(bool isAsync)
        {
            await base.Key_equality_navigation_converted_to_FK(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
WHERE [l0].[Id] = 1");
        }

        public override async Task Key_equality_two_conditions_on_same_navigation(bool isAsync)
        {
            await base.Key_equality_two_conditions_on_same_navigation(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
WHERE (([l0].[Id] = 1) AND [l0].[Id] IS NOT NULL) OR (([l0].[Id] = 2) AND [l0].[Id] IS NOT NULL)");
        }

        public override async Task Key_equality_two_conditions_on_same_navigation2(bool isAsync)
        {
            await base.Key_equality_two_conditions_on_same_navigation2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
WHERE ([l0].[Id] = 1) OR ([l0].[Id] = 2)");
        }

        public override async Task Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql(bool isAsync)
        {
            await base.Multi_level_include_one_to_many_optional_and_one_to_many_optional_produces_valid_sql(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id], [t].[Id0]");
        }

        public override async Task Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times(bool isAsync)
        {
            await base.Multi_level_include_correct_PK_is_chosen_as_the_join_predicate_for_queries_that_join_same_table_multiple_times(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t0].[Id], [t0].[Date], [t0].[Level1_Optional_Id], [t0].[Level1_Required_Id], [t0].[Name], [t0].[OneToMany_Optional_Inverse2Id], [t0].[OneToMany_Optional_Self_Inverse2Id], [t0].[OneToMany_Required_Inverse2Id], [t0].[OneToMany_Required_Self_Inverse2Id], [t0].[OneToOne_Optional_PK_Inverse2Id], [t0].[OneToOne_Optional_Self2Id], [t0].[Id0], [t0].[Level2_Optional_Id], [t0].[Level2_Required_Id], [t0].[Name0], [t0].[OneToMany_Optional_Inverse3Id], [t0].[OneToMany_Optional_Self_Inverse3Id], [t0].[OneToMany_Required_Inverse3Id], [t0].[OneToMany_Required_Self_Inverse3Id], [t0].[OneToOne_Optional_PK_Inverse3Id], [t0].[OneToOne_Optional_Self3Id], [t0].[Id00], [t0].[Date0], [t0].[Level1_Optional_Id0], [t0].[Level1_Required_Id0], [t0].[Name00], [t0].[OneToMany_Optional_Inverse2Id0], [t0].[OneToMany_Optional_Self_Inverse2Id0], [t0].[OneToMany_Required_Inverse2Id0], [t0].[OneToMany_Required_Self_Inverse2Id0], [t0].[OneToOne_Optional_PK_Inverse2Id0], [t0].[OneToOne_Optional_Self2Id0], [t0].[Id1], [t0].[Level2_Optional_Id0], [t0].[Level2_Required_Id0], [t0].[Name1], [t0].[OneToMany_Optional_Inverse3Id0], [t0].[OneToMany_Optional_Self_Inverse3Id0], [t0].[OneToMany_Required_Inverse3Id0], [t0].[OneToMany_Required_Self_Inverse3Id0], [t0].[OneToOne_Optional_PK_Inverse3Id0], [t0].[OneToOne_Optional_Self3Id0]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [t].[Id] AS [Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name] AS [Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id0] AS [Id00], [t].[Date] AS [Date0], [t].[Level1_Optional_Id] AS [Level1_Optional_Id0], [t].[Level1_Required_Id] AS [Level1_Required_Id0], [t].[Name0] AS [Name00], [t].[OneToMany_Optional_Inverse2Id] AS [OneToMany_Optional_Inverse2Id0], [t].[OneToMany_Optional_Self_Inverse2Id] AS [OneToMany_Optional_Self_Inverse2Id0], [t].[OneToMany_Required_Inverse2Id] AS [OneToMany_Required_Inverse2Id0], [t].[OneToMany_Required_Self_Inverse2Id] AS [OneToMany_Required_Self_Inverse2Id0], [t].[OneToOne_Optional_PK_Inverse2Id] AS [OneToOne_Optional_PK_Inverse2Id0], [t].[OneToOne_Optional_Self2Id] AS [OneToOne_Optional_Self2Id0], [t].[Id1], [t].[Level2_Optional_Id0], [t].[Level2_Required_Id0], [t].[Name1], [t].[OneToMany_Optional_Inverse3Id0], [t].[OneToMany_Optional_Self_Inverse3Id0], [t].[OneToMany_Required_Inverse3Id0], [t].[OneToMany_Required_Self_Inverse3Id0], [t].[OneToOne_Optional_PK_Inverse3Id0], [t].[OneToOne_Optional_Self3Id0]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN (
        SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id0], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name] AS [Name0], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id1], [l3].[Level2_Optional_Id] AS [Level2_Optional_Id0], [l3].[Level2_Required_Id] AS [Level2_Required_Id0], [l3].[Name] AS [Name1], [l3].[OneToMany_Optional_Inverse3Id] AS [OneToMany_Optional_Inverse3Id0], [l3].[OneToMany_Optional_Self_Inverse3Id] AS [OneToMany_Optional_Self_Inverse3Id0], [l3].[OneToMany_Required_Inverse3Id] AS [OneToMany_Required_Inverse3Id0], [l3].[OneToMany_Required_Self_Inverse3Id] AS [OneToMany_Required_Self_Inverse3Id0], [l3].[OneToOne_Optional_PK_Inverse3Id] AS [OneToOne_Optional_PK_Inverse3Id0], [l3].[OneToOne_Optional_Self3Id] AS [OneToOne_Optional_Self3Id0]
        FROM [LevelThree] AS [l1]
        INNER JOIN [LevelTwo] AS [l2] ON [l1].[OneToMany_Required_Inverse3Id] = [l2].[Id]
        LEFT JOIN [LevelThree] AS [l3] ON [l2].[Id] = [l3].[OneToMany_Optional_Inverse3Id]
    ) AS [t] ON [l0].[Id] = [t].[OneToMany_Optional_Inverse3Id]
) AS [t0] ON [l].[Id] = [t0].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t0].[Id], [t0].[Id0], [t0].[Id00], [t0].[Id1]");
        }

        public override void Multi_level_include_with_short_circuiting()
        {
            base.Multi_level_include_with_short_circuiting();

            AssertSql(
                @"SELECT [f].[Name], [f].[LabelDefaultText], [f].[PlaceholderDefaultText], [m].[DefaultText], [m0].[DefaultText], [t].[Text], [t].[ComplexNavigationStringDefaultText], [t].[LanguageName], [t].[Name], [t].[CultureString], [t0].[Text], [t0].[ComplexNavigationStringDefaultText], [t0].[LanguageName], [t0].[Name], [t0].[CultureString]
FROM [Fields] AS [f]
LEFT JOIN [MultilingualStrings] AS [m] ON [f].[LabelDefaultText] = [m].[DefaultText]
LEFT JOIN [MultilingualStrings] AS [m0] ON [f].[PlaceholderDefaultText] = [m0].[DefaultText]
LEFT JOIN (
    SELECT [g].[Text], [g].[ComplexNavigationStringDefaultText], [g].[LanguageName], [l].[Name], [l].[CultureString]
    FROM [Globalizations] AS [g]
    LEFT JOIN [Languages] AS [l] ON [g].[LanguageName] = [l].[Name]
) AS [t] ON [m].[DefaultText] = [t].[ComplexNavigationStringDefaultText]
LEFT JOIN (
    SELECT [g0].[Text], [g0].[ComplexNavigationStringDefaultText], [g0].[LanguageName], [l0].[Name], [l0].[CultureString]
    FROM [Globalizations] AS [g0]
    LEFT JOIN [Languages] AS [l0] ON [g0].[LanguageName] = [l0].[Name]
) AS [t0] ON [m0].[DefaultText] = [t0].[ComplexNavigationStringDefaultText]
ORDER BY [f].[Name], [t].[Text], [t0].[Text]");
        }

        public override async Task Join_navigation_key_access_optional(bool isAsync)
        {
            await base.Join_navigation_key_access_optional(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [t].[Id] AS [Id2]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Optional_Id] = [l1].[Id]
) AS [t] ON [l].[Id] = [t].[Id0]");
        }

        public override async Task Join_navigation_key_access_required(bool isAsync)
        {
            await base.Join_navigation_key_access_required(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [t].[Id] AS [Id2]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
    FROM [LevelTwo] AS [l0]
    INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
) AS [t] ON [l].[Id] = [t].[Id0]");
        }

        public override async Task Navigation_key_access_optional_comparison(bool isAsync)
        {
            await base.Navigation_key_access_optional_comparison(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelOne] AS [l0] ON [l].[OneToOne_Optional_PK_Inverse2Id] = [l0].[Id]
WHERE [l0].[Id] > 5");
        }

        public override async Task Navigation_key_access_required_comparison(bool isAsync)
        {
            await base.Navigation_key_access_required_comparison(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Id] = [l0].[Id]
WHERE [l0].[Id] > 5");
        }

        public override async Task Navigation_inside_method_call_translated_to_join(bool isAsync)
        {
            await base.Navigation_inside_method_call_translated_to_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
WHERE [l0].[Name] IS NOT NULL AND ([l0].[Name] LIKE N'L%')");
        }

        public override async Task Navigation_inside_method_call_translated_to_join2(bool isAsync)
        {
            await base.Navigation_inside_method_call_translated_to_join2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse3Id], [l].[OneToMany_Optional_Self_Inverse3Id], [l].[OneToMany_Required_Inverse3Id], [l].[OneToMany_Required_Self_Inverse3Id], [l].[OneToOne_Optional_PK_Inverse3Id], [l].[OneToOne_Optional_Self3Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
WHERE [l0].[Name] IS NOT NULL AND ([l0].[Name] LIKE N'L%')");
        }

        public override async Task Optional_navigation_inside_method_call_translated_to_join(bool isAsync)
        {
            await base.Optional_navigation_inside_method_call_translated_to_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE [l0].[Name] IS NOT NULL AND ([l0].[Name] LIKE N'L%')");
        }

        public override async Task Optional_navigation_inside_property_method_translated_to_join(bool isAsync)
        {
            await base.Optional_navigation_inside_property_method_translated_to_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE ([l0].[Name] = N'L2 01') AND [l0].[Name] IS NOT NULL");
        }

        public override async Task Optional_navigation_inside_nested_method_call_translated_to_join(bool isAsync)
        {
            await base.Optional_navigation_inside_nested_method_call_translated_to_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE UPPER([l0].[Name]) IS NOT NULL AND (UPPER([l0].[Name]) LIKE N'L%')");
        }

        public override async Task Method_call_on_optional_navigation_translates_to_null_conditional_properly_for_arguments(bool isAsync)
        {
            await base.Method_call_on_optional_navigation_translates_to_null_conditional_properly_for_arguments(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE (([l0].[Name] = N'') AND [l0].[Name] IS NOT NULL) OR ([l0].[Name] IS NOT NULL AND (([l0].[Name] LIKE [l0].[Name] + N'%') AND (LEFT([l0].[Name], LEN([l0].[Name])) = [l0].[Name])))");
        }

        public override async Task Optional_navigation_inside_method_call_translated_to_join_keeps_original_nullability(bool isAsync)
        {
            await base.Optional_navigation_inside_method_call_translated_to_join_keeps_original_nullability(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE DATEADD(day, CAST(10.0E0 AS int), [l0].[Date]) > '2000-02-01T00:00:00.0000000'");
        }

        public override async Task Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability(bool isAsync)
        {
            await base.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE DATEADD(month, CAST(2 AS int), DATEADD(day, CAST(15.0E0 AS int), DATEADD(day, CAST(10.0E0 AS int), [l0].[Date]))) > '2002-02-01T00:00:00.0000000'");
        }

        public override async Task
            Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability_also_for_arguments(bool isAsync)
        {
            await base.Optional_navigation_inside_nested_method_call_translated_to_join_keeps_original_nullability_also_for_arguments(
                isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE DATEADD(day, CAST(CAST([l0].[Id] AS float) AS int), DATEADD(day, CAST(15.0E0 AS int), [l0].[Date])) > '2002-02-01T00:00:00.0000000'");
        }

        public override async Task Join_navigation_in_outer_selector_translated_to_extra_join(bool isAsync)
        {
            await base.Join_navigation_in_outer_selector_translated_to_extra_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [l1].[Id] AS [Id2]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
INNER JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Id]");
        }

        public override async Task Join_navigation_in_outer_selector_translated_to_extra_join_nested(bool isAsync)
        {
            await base.Join_navigation_in_outer_selector_translated_to_extra_join_nested(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [l2].[Id] AS [Id3]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
INNER JOIN [LevelThree] AS [l2] ON [l1].[Id] = [l2].[Id]");
        }

        public override async Task Join_navigation_in_outer_selector_translated_to_extra_join_nested2(bool isAsync)
        {
            await base.Join_navigation_in_outer_selector_translated_to_extra_join_nested2(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id3], [l2].[Id] AS [Id1]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Optional_Id] = [l1].[Id]
INNER JOIN [LevelOne] AS [l2] ON [l1].[Id] = [l2].[Id]");
        }

        public override async Task Join_navigation_in_inner_selector(bool isAsync)
        {
            await base.Join_navigation_in_inner_selector(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id2], [t].[Id] AS [Id1]
FROM [LevelTwo] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Id] = [t].[Id0]");
        }

        public override async Task Join_navigations_in_inner_selector_translated_without_collision(bool isAsync)
        {
            await base.Join_navigations_in_inner_selector_translated_without_collision(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id2], [t].[Id] AS [Id1], [t0].[Id] AS [Id3]
FROM [LevelTwo] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Id] = [t].[Id0]
INNER JOIN (
    SELECT [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id], [l3].[Id] AS [Id0], [l3].[Date], [l3].[Level1_Optional_Id], [l3].[Level1_Required_Id], [l3].[Name] AS [Name0], [l3].[OneToMany_Optional_Inverse2Id], [l3].[OneToMany_Optional_Self_Inverse2Id], [l3].[OneToMany_Required_Inverse2Id], [l3].[OneToMany_Required_Self_Inverse2Id], [l3].[OneToOne_Optional_PK_Inverse2Id], [l3].[OneToOne_Optional_Self2Id]
    FROM [LevelThree] AS [l2]
    LEFT JOIN [LevelTwo] AS [l3] ON [l2].[Level2_Optional_Id] = [l3].[Id]
) AS [t0] ON [l].[Id] = [t0].[Id0]");
        }

        public override async Task Join_navigation_non_key_join(bool isAsync)
        {
            await base.Join_navigation_non_key_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id2], [l].[Name] AS [Name2], [t].[Id] AS [Id1], [t].[Name] AS [Name1]
FROM [LevelTwo] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Name] = [t].[Name0]");
        }

        public override async Task Join_with_orderby_on_inner_sequence_navigation_non_key_join(bool isAsync)
        {
            await base.Join_with_orderby_on_inner_sequence_navigation_non_key_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id2], [l].[Name] AS [Name2], [t].[Id] AS [Id1], [t].[Name] AS [Name1]
FROM [LevelTwo] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Name] = [t].[Name0]");
        }

        public override async Task Join_navigation_self_ref(bool isAsync)
        {
            await base.Join_navigation_self_ref(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [t].[Id] AS [Id2]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Self_Inverse1Id] AS [OneToMany_Optional_Self_Inverse1Id0], [l1].[OneToMany_Required_Self_Inverse1Id] AS [OneToMany_Required_Self_Inverse1Id0], [l1].[OneToOne_Optional_Self1Id] AS [OneToOne_Optional_Self1Id0]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelOne] AS [l1] ON [l0].[OneToMany_Optional_Self_Inverse1Id] = [l1].[Id]
) AS [t] ON [l].[Id] = [t].[Id0]");
        }

        public override async Task Join_navigation_nested(bool isAsync)
        {
            await base.Join_navigation_nested(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id3], [t].[Id] AS [Id1]
FROM [LevelThree] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l2].[Id] AS [Id1], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Required_Id]
    LEFT JOIN [LevelThree] AS [l2] ON [l1].[Id] = [l2].[Level2_Optional_Id]
) AS [t] ON [l].[Id] = [t].[Id1]");
        }

        public override async Task Join_navigation_nested2(bool isAsync)
        {
            await base.Join_navigation_nested2(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id3], [t].[Id] AS [Id1]
FROM [LevelThree] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l2].[Id] AS [Id1], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Required_Id]
    LEFT JOIN [LevelThree] AS [l2] ON [l1].[Id] = [l2].[Level2_Optional_Id]
) AS [t] ON [l].[Id] = [t].[Id1]");
        }

        public override async Task Join_navigation_deeply_nested_non_key_join(bool isAsync)
        {
            await base.Join_navigation_deeply_nested_non_key_join(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id4], [l].[Name] AS [Name4], [t].[Id] AS [Id1], [t].[Name] AS [Name1]
FROM [LevelFour] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l2].[Id] AS [Id1], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id], [l3].[Id] AS [Id2], [l3].[Level3_Optional_Id], [l3].[Level3_Required_Id], [l3].[Name] AS [Name2], [l3].[OneToMany_Optional_Inverse4Id], [l3].[OneToMany_Optional_Self_Inverse4Id], [l3].[OneToMany_Required_Inverse4Id], [l3].[OneToMany_Required_Self_Inverse4Id], [l3].[OneToOne_Optional_PK_Inverse4Id], [l3].[OneToOne_Optional_Self4Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Required_Id]
    LEFT JOIN [LevelThree] AS [l2] ON [l1].[Id] = [l2].[Level2_Optional_Id]
    LEFT JOIN [LevelFour] AS [l3] ON [l2].[Id] = [l3].[Id]
) AS [t] ON [l].[Name] = [t].[Name2]");
        }

        public override async Task Join_navigation_deeply_nested_required(bool isAsync)
        {
            await base.Join_navigation_deeply_nested_required(isAsync);

            AssertSql(
                @"SELECT [t].[Id] AS [Id4], [t].[Name] AS [Name4], [l].[Id] AS [Id1], [l].[Name] AS [Name1]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Level3_Optional_Id], [l0].[Level3_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse4Id], [l0].[OneToMany_Optional_Self_Inverse4Id], [l0].[OneToMany_Required_Inverse4Id], [l0].[OneToMany_Required_Self_Inverse4Id], [l0].[OneToOne_Optional_PK_Inverse4Id], [l0].[OneToOne_Optional_Self4Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id2], [l3].[Date] AS [Date0], [l3].[Name] AS [Name2], [l3].[OneToMany_Optional_Self_Inverse1Id], [l3].[OneToMany_Required_Self_Inverse1Id], [l3].[OneToOne_Optional_Self1Id]
    FROM [LevelFour] AS [l0]
    INNER JOIN [LevelThree] AS [l1] ON [l0].[Level3_Required_Id] = [l1].[Id]
    INNER JOIN [LevelTwo] AS [l2] ON [l1].[Level2_Required_Id] = [l2].[Id]
    INNER JOIN [LevelOne] AS [l3] ON [l2].[Id] = [l3].[Id]
) AS [t] ON [l].[Name] = [t].[Name2]");
        }

        public override async Task Multiple_complex_includes(bool isAsync)
        {
            await base.Multiple_complex_includes(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id0], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name] AS [Name0], [l3].[OneToMany_Optional_Inverse3Id], [l3].[OneToMany_Optional_Self_Inverse3Id], [l3].[OneToMany_Required_Inverse3Id], [l3].[OneToMany_Required_Self_Inverse3Id], [l3].[OneToOne_Optional_PK_Inverse3Id], [l3].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l2]
    LEFT JOIN [LevelThree] AS [l3] ON [l2].[Id] = [l3].[Level2_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l1].[Id], [t].[Id]");
        }

        public override async Task Multiple_complex_includes_self_ref(bool isAsync)
        {
            await base.Multiple_complex_includes_self_ref(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [t].[Id0], [t].[Date0], [t].[Name0], [t].[OneToMany_Optional_Self_Inverse1Id0], [t].[OneToMany_Required_Self_Inverse1Id0], [t].[OneToOne_Optional_Self1Id0]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelOne] AS [l0] ON [l].[OneToOne_Optional_Self1Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Self_Inverse1Id]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Name], [l2].[OneToMany_Optional_Self_Inverse1Id], [l2].[OneToMany_Required_Self_Inverse1Id], [l2].[OneToOne_Optional_Self1Id], [l3].[Id] AS [Id0], [l3].[Date] AS [Date0], [l3].[Name] AS [Name0], [l3].[OneToMany_Optional_Self_Inverse1Id] AS [OneToMany_Optional_Self_Inverse1Id0], [l3].[OneToMany_Required_Self_Inverse1Id] AS [OneToMany_Required_Self_Inverse1Id0], [l3].[OneToOne_Optional_Self1Id] AS [OneToOne_Optional_Self1Id0]
    FROM [LevelOne] AS [l2]
    LEFT JOIN [LevelOne] AS [l3] ON [l2].[OneToOne_Optional_Self1Id] = [l3].[Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Self_Inverse1Id]
ORDER BY [l].[Id], [l1].[Id], [t].[Id]");
        }

        public override async Task Multiple_complex_include_select(bool isAsync)
        {
            await base.Multiple_complex_include_select(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id0], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name] AS [Name0], [l3].[OneToMany_Optional_Inverse3Id], [l3].[OneToMany_Optional_Self_Inverse3Id], [l3].[OneToMany_Required_Inverse3Id], [l3].[OneToMany_Required_Self_Inverse3Id], [l3].[OneToOne_Optional_PK_Inverse3Id], [l3].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l2]
    LEFT JOIN [LevelThree] AS [l3] ON [l2].[Id] = [l3].[Level2_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l1].[Id], [t].[Id]");
        }

        public override async Task Select_nav_prop_reference_optional1(bool isAsync)
        {
            await base.Select_nav_prop_reference_optional1(isAsync);

            AssertSql(
                @"SELECT [l0].[Name]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Select_nav_prop_reference_optional1_via_DefaultIfEmpty(bool isAsync)
        {
            await base.Select_nav_prop_reference_optional1_via_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l0].[Name]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Select_nav_prop_reference_optional2(bool isAsync)
        {
            await base.Select_nav_prop_reference_optional2(isAsync);

            AssertSql(
                @"SELECT [l0].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Select_nav_prop_reference_optional2_via_DefaultIfEmpty(bool isAsync)
        {
            await base.Select_nav_prop_reference_optional2_via_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l0].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Select_nav_prop_reference_optional3(bool isAsync)
        {
            await base.Select_nav_prop_reference_optional3(isAsync);

            AssertSql(
                @"SELECT [l0].[Name]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelOne] AS [l0] ON [l].[Level1_Optional_Id] = [l0].[Id]");
        }

        public override async Task Where_nav_prop_reference_optional1(bool isAsync)
        {
            await base.Where_nav_prop_reference_optional1(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE (([l0].[Name] = N'L2 05') AND [l0].[Name] IS NOT NULL) OR (([l0].[Name] = N'L2 07') AND [l0].[Name] IS NOT NULL)");
        }

        public override async Task Where_nav_prop_reference_optional1_via_DefaultIfEmpty(bool isAsync)
        {
            await base.Where_nav_prop_reference_optional1_via_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[Level1_Optional_Id]
WHERE (([l0].[Name] = N'L2 05') AND [l0].[Name] IS NOT NULL) OR (([l1].[Name] = N'L2 07') AND [l1].[Name] IS NOT NULL)");
        }

        public override async Task Where_nav_prop_reference_optional2(bool isAsync)
        {
            await base.Where_nav_prop_reference_optional2(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE (([l0].[Name] = N'L2 05') AND [l0].[Name] IS NOT NULL) OR (([l0].[Name] <> N'L2 42') OR [l0].[Name] IS NULL)");
        }

        public override async Task Where_nav_prop_reference_optional2_via_DefaultIfEmpty(bool isAsync)
        {
            await base.Where_nav_prop_reference_optional2_via_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[Level1_Optional_Id]
WHERE (([l0].[Name] = N'L2 05') AND [l0].[Name] IS NOT NULL) OR (([l1].[Name] <> N'L2 42') OR [l1].[Name] IS NULL)");
        }

        public override async Task Select_multiple_nav_prop_reference_optional(bool isAsync)
        {
            await base.Select_multiple_nav_prop_reference_optional(isAsync);

            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]");
        }

        public override async Task Where_multiple_nav_prop_reference_optional_member_compared_to_value(bool isAsync)
        {
            await base.Where_multiple_nav_prop_reference_optional_member_compared_to_value(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
WHERE ([l1].[Name] <> N'L3 05') OR [l1].[Name] IS NULL");
        }

        public override async Task Where_multiple_nav_prop_reference_optional_member_compared_to_null(bool isAsync)
        {
            await base.Where_multiple_nav_prop_reference_optional_member_compared_to_null(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
WHERE [l1].[Name] IS NOT NULL");
        }

        public override async Task Where_multiple_nav_prop_reference_optional_compared_to_null1(bool isAsync)
        {
            await base.Where_multiple_nav_prop_reference_optional_compared_to_null1(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
WHERE [l1].[Id] IS NULL");
        }

        public override async Task Where_multiple_nav_prop_reference_optional_compared_to_null2(bool isAsync)
        {
            await base.Where_multiple_nav_prop_reference_optional_compared_to_null2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse3Id], [l].[OneToMany_Optional_Self_Inverse3Id], [l].[OneToMany_Required_Inverse3Id], [l].[OneToMany_Required_Self_Inverse3Id], [l].[OneToOne_Optional_PK_Inverse3Id], [l].[OneToOne_Optional_Self3Id]
FROM [LevelThree] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Level2_Optional_Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Optional_Id] = [l1].[Id]
WHERE [l1].[Id] IS NULL");
        }

        public override async Task Where_multiple_nav_prop_reference_optional_compared_to_null3(bool isAsync)
        {
            await base.Where_multiple_nav_prop_reference_optional_compared_to_null3(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
WHERE [l1].[Id] IS NOT NULL");
        }

        public override async Task Where_multiple_nav_prop_reference_optional_compared_to_null4(bool isAsync)
        {
            await base.Where_multiple_nav_prop_reference_optional_compared_to_null4(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse3Id], [l].[OneToMany_Optional_Self_Inverse3Id], [l].[OneToMany_Required_Inverse3Id], [l].[OneToMany_Required_Self_Inverse3Id], [l].[OneToOne_Optional_PK_Inverse3Id], [l].[OneToOne_Optional_Self3Id]
FROM [LevelThree] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Level2_Optional_Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Optional_Id] = [l1].[Id]
WHERE [l1].[Id] IS NOT NULL");
        }

        public override async Task Where_multiple_nav_prop_reference_optional_compared_to_null5(bool isAsync)
        {
            await base.Where_multiple_nav_prop_reference_optional_compared_to_null5(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Required_Id]
WHERE [l2].[Id] IS NULL");
        }

        public override async Task Select_multiple_nav_prop_reference_required(bool isAsync)
        {
            await base.Select_multiple_nav_prop_reference_required(isAsync);

            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]");
        }

        public override async Task Select_multiple_nav_prop_reference_required2(bool isAsync)
        {
            await base.Select_multiple_nav_prop_reference_required2(isAsync);

            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]");
        }

        public override async Task Select_multiple_nav_prop_optional_required(bool isAsync)
        {
            await base.Select_multiple_nav_prop_optional_required(isAsync);

            AssertSql(
                @"SELECT [l1].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]");
        }

        public override async Task Where_multiple_nav_prop_optional_required(bool isAsync)
        {
            await base.Where_multiple_nav_prop_optional_required(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
WHERE ([l1].[Name] <> N'L3 05') OR [l1].[Name] IS NULL");
        }

        public override async Task SelectMany_navigation_comparison1(bool isAsync)
        {
            await base.SelectMany_navigation_comparison1(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [l0].[Id] AS [Id2]
FROM [LevelOne] AS [l]
CROSS JOIN [LevelOne] AS [l0]
WHERE [l].[Id] = [l0].[Id]");
        }

        public override async Task SelectMany_navigation_comparison2(bool isAsync)
        {
            await base.SelectMany_navigation_comparison2(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [l0].[Id] AS [Id2]
FROM [LevelOne] AS [l]
CROSS JOIN [LevelTwo] AS [l0]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Optional_Id] = [l1].[Id]
WHERE ([l].[Id] = [l1].[Id]) AND [l1].[Id] IS NOT NULL");
        }

        public override async Task SelectMany_navigation_comparison3(bool isAsync)
        {
            await base.SelectMany_navigation_comparison3(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [l0].[Id] AS [Id2]
FROM [LevelOne] AS [l]
CROSS JOIN [LevelTwo] AS [l0]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[Level1_Optional_Id]
WHERE ([l1].[Id] = [l0].[Id]) AND [l1].[Id] IS NOT NULL");
        }

        public override async Task Where_complex_predicate_with_with_nav_prop_and_OrElse1(bool isAsync)
        {
            await base.Where_complex_predicate_with_with_nav_prop_and_OrElse1(isAsync);

            AssertSql(
                @"SELECT [l].[Id] AS [Id1], [l0].[Id] AS [Id2]
FROM [LevelOne] AS [l]
CROSS JOIN [LevelTwo] AS [l0]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[Level1_Optional_Id]
INNER JOIN [LevelOne] AS [l2] ON [l0].[Level1_Required_Id] = [l2].[Id]
WHERE (([l1].[Name] = N'L2 01') AND [l1].[Name] IS NOT NULL) OR (([l2].[Name] <> N'Bar') OR [l2].[Name] IS NULL)");
        }

        public override async Task Where_complex_predicate_with_with_nav_prop_and_OrElse2(bool isAsync)
        {
            await base.Where_complex_predicate_with_with_nav_prop_and_OrElse2(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
WHERE (([l1].[Name] = N'L3 05') AND [l1].[Name] IS NOT NULL) OR (([l0].[Name] <> N'L2 05') OR [l0].[Name] IS NULL)");
        }

        public override async Task Where_complex_predicate_with_with_nav_prop_and_OrElse3(bool isAsync)
        {
            await base.Where_complex_predicate_with_with_nav_prop_and_OrElse3(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l2] ON [l1].[Id] = [l2].[Level2_Optional_Id]
WHERE (([l0].[Name] <> N'L2 05') OR [l0].[Name] IS NULL) OR (([l2].[Name] = N'L3 05') AND [l2].[Name] IS NOT NULL)");
        }

        public override async Task Where_complex_predicate_with_with_nav_prop_and_OrElse4(bool isAsync)
        {
            await base.Where_complex_predicate_with_with_nav_prop_and_OrElse4(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelThree] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Level2_Optional_Id] = [l0].[Id]
INNER JOIN [LevelTwo] AS [l1] ON [l].[Level2_Required_Id] = [l1].[Id]
LEFT JOIN [LevelOne] AS [l2] ON [l1].[Level1_Optional_Id] = [l2].[Id]
WHERE (([l0].[Name] <> N'L2 05') OR [l0].[Name] IS NULL) OR (([l2].[Name] = N'L1 05') AND [l2].[Name] IS NOT NULL)");
        }

        public override async Task Complex_navigations_with_predicate_projected_into_anonymous_type(bool isAsync)
        {
            await base.Complex_navigations_with_predicate_projected_into_anonymous_type(isAsync);

            AssertSql(
                @"SELECT [l].[Name], [l2].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[Level2_Optional_Id]
WHERE ((([l1].[Id] = [l2].[Id]) AND ([l1].[Id] IS NOT NULL AND [l2].[Id] IS NOT NULL)) OR ([l1].[Id] IS NULL AND [l2].[Id] IS NULL)) AND (([l2].[Id] <> 7) OR [l2].[Id] IS NULL)");
        }

        public override async Task Complex_navigations_with_predicate_projected_into_anonymous_type2(bool isAsync)
        {
            await base.Complex_navigations_with_predicate_projected_into_anonymous_type2(isAsync);

            AssertSql(
                @"SELECT [l].[Name], [l2].[Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
LEFT JOIN [LevelOne] AS [l2] ON [l0].[Level1_Optional_Id] = [l2].[Id]
WHERE (([l1].[Id] = [l2].[Id]) AND [l2].[Id] IS NOT NULL) AND (([l2].[Id] <> 7) OR [l2].[Id] IS NULL)");
        }

        public override async Task Optional_navigation_projected_into_DTO(bool isAsync)
        {
            await base.Optional_navigation_projected_into_DTO(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Name], CASE
    WHEN [l0].[Id] IS NOT NULL THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END, [l0].[Id], [l0].[Name]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task OrderBy_nav_prop_reference_optional(bool isAsync)
        {
            await base.OrderBy_nav_prop_reference_optional(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
ORDER BY [l0].[Name], [l].[Id]");
        }

        public override async Task OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty(bool isAsync)
        {
            await base.OrderBy_nav_prop_reference_optional_via_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
ORDER BY [l0].[Name], [l].[Id]");
        }

        public override async Task Result_operator_nav_prop_reference_optional_Sum(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_Sum(isAsync);

            AssertSql(
                @"SELECT SUM([l0].[Level1_Required_Id])
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Result_operator_nav_prop_reference_optional_Min(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_Min(isAsync);

            AssertSql(
                @"SELECT MIN([l0].[Level1_Required_Id])
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Result_operator_nav_prop_reference_optional_Max(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_Max(isAsync);

            AssertSql(
                @"SELECT MAX([l0].[Level1_Required_Id])
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Result_operator_nav_prop_reference_optional_Average(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_Average(isAsync);

            AssertSql(
                @"SELECT AVG(CAST([l0].[Level1_Required_Id] AS float))
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Result_operator_nav_prop_reference_optional_Average_with_identity_selector(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_Average_with_identity_selector(isAsync);

            AssertSql(
                @"SELECT AVG(CAST([l0].[Level1_Required_Id] AS float))
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Result_operator_nav_prop_reference_optional_Average_without_selector(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_Average_without_selector(isAsync);

            AssertSql(
                @"SELECT AVG(CAST([l0].[Level1_Required_Id] AS float))
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty(bool isAsync)
        {
            await base.Result_operator_nav_prop_reference_optional_via_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT SUM(CASE
    WHEN [l0].[Id] IS NULL THEN 0
    ELSE [l0].[Level1_Required_Id]
END)
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Include_with_optional_navigation(bool isAsync)
        {
            await base.Include_with_optional_navigation(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE ([l0].[Name] <> N'L2 05') OR [l0].[Name] IS NULL");
        }

        public override async Task Include_nested_with_optional_navigation(bool isAsync)
        {
            await base.Include_nested_with_optional_navigation(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [t].[Id], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id0], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN (
    SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id0], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name] AS [Name0], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
    FROM [LevelThree] AS [l1]
    LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Required_Id]
) AS [t] ON [l0].[Id] = [t].[OneToMany_Required_Inverse3Id]
WHERE ([l0].[Name] <> N'L2 09') OR [l0].[Name] IS NULL
ORDER BY [l].[Id], [t].[Id]");
        }

        public override async Task Include_with_groupjoin_skip_and_take(bool isAsync)
        {
            await base.Include_with_groupjoin_skip_and_take(isAsync);

            if (SupportsOffset)
            {
                AssertSql(
                    @"SELECT [e].[Id], [e].[Date], [e].[Name], [e].[OneToMany_Optional_Self_Inverse1Id], [e].[OneToMany_Required_Self_Inverse1Id], [e].[OneToOne_Optional_Self1Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [e]
LEFT JOIN [LevelTwo] AS [l2] ON [e].[Id] = [l2].[Level1_Optional_Id]
WHERE ([e].[Name] <> N'L1 03') OR [e].[Name] IS NULL
ORDER BY [e].[Id]",
                    //
                    @"SELECT [e1].[Id], [e1].[Date], [e1].[Name], [e1].[OneToMany_Optional_Self_Inverse1Id], [e1].[OneToMany_Required_Self_Inverse1Id], [e1].[OneToOne_Optional_Self1Id], [l21].[Id], [l21].[Date], [l21].[Level1_Optional_Id], [l21].[Level1_Required_Id], [l21].[Name], [l21].[OneToMany_Optional_Inverse2Id], [l21].[OneToMany_Optional_Self_Inverse2Id], [l21].[OneToMany_Required_Inverse2Id], [l21].[OneToMany_Required_Self_Inverse2Id], [l21].[OneToOne_Optional_PK_Inverse2Id], [l21].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [e1]
LEFT JOIN [LevelTwo] AS [l21] ON [e1].[Id] = [l21].[Level1_Optional_Id]
WHERE ([e1].[Name] <> N'L1 03') OR [e1].[Name] IS NULL
ORDER BY [e1].[Id]",
                    //
                    @"SELECT [e.OneToMany_Optional1].[Id], [e.OneToMany_Optional1].[Date], [e.OneToMany_Optional1].[Level1_Optional_Id], [e.OneToMany_Optional1].[Level1_Required_Id], [e.OneToMany_Optional1].[Name], [e.OneToMany_Optional1].[OneToMany_Optional_Inverse2Id], [e.OneToMany_Optional1].[OneToMany_Optional_Self_Inverse2Id], [e.OneToMany_Optional1].[OneToMany_Required_Inverse2Id], [e.OneToMany_Optional1].[OneToMany_Required_Self_Inverse2Id], [e.OneToMany_Optional1].[OneToOne_Optional_PK_Inverse2Id], [e.OneToMany_Optional1].[OneToOne_Optional_Self2Id], [l.OneToOne_Optional_FK2].[Id], [l.OneToOne_Optional_FK2].[Level2_Optional_Id], [l.OneToOne_Optional_FK2].[Level2_Required_Id], [l.OneToOne_Optional_FK2].[Name], [l.OneToOne_Optional_FK2].[OneToMany_Optional_Inverse3Id], [l.OneToOne_Optional_FK2].[OneToMany_Optional_Self_Inverse3Id], [l.OneToOne_Optional_FK2].[OneToMany_Required_Inverse3Id], [l.OneToOne_Optional_FK2].[OneToMany_Required_Self_Inverse3Id], [l.OneToOne_Optional_FK2].[OneToOne_Optional_PK_Inverse3Id], [l.OneToOne_Optional_FK2].[OneToOne_Optional_Self3Id]
FROM [LevelTwo] AS [e.OneToMany_Optional1]
LEFT JOIN [LevelThree] AS [l.OneToOne_Optional_FK2] ON [e.OneToMany_Optional1].[Id] = [l.OneToOne_Optional_FK2].[Level2_Optional_Id]");
            }
        }

        public override async Task Join_flattening_bug_4539(bool isAsync)
        {
            await base.Join_flattening_bug_4539(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Date], [l2].[Name], [l2].[OneToMany_Optional_Self_Inverse1Id], [l2].[OneToMany_Required_Self_Inverse1Id], [l2].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
CROSS JOIN [LevelTwo] AS [l1]
INNER JOIN [LevelOne] AS [l2] ON [l1].[Level1_Required_Id] = [l2].[Id]");
        }

        public override async Task Query_source_materialization_bug_4547(bool isAsync)
        {
            await base.Query_source_materialization_bug_4547(isAsync);

            AssertSql(
                @"SELECT [l0].[Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Id] = (
    SELECT TOP(1) [l2].[Id]
    FROM [LevelTwo] AS [l1]
    LEFT JOIN [LevelThree] AS [l2] ON [l1].[Id] = [l2].[Level2_Optional_Id]
    ORDER BY [l2].[Id])");
        }

        public override async Task SelectMany_navigation_property(bool isAsync)
        {
            await base.SelectMany_navigation_property(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]");
        }

        public override async Task SelectMany_navigation_property_and_projection(bool isAsync)
        {
            await base.SelectMany_navigation_property_and_projection(isAsync);

            AssertSql(
                @"SELECT [l0].[Name]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]");
        }

        public override async Task SelectMany_navigation_property_and_filter_before(bool isAsync)
        {
            await base.SelectMany_navigation_property_and_filter_before(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
WHERE [l].[Id] = 1");
        }

        public override async Task SelectMany_navigation_property_and_filter_after(bool isAsync)
        {
            await base.SelectMany_navigation_property_and_filter_after(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
WHERE [l0].[Id] <> 6");
        }

        public override async Task SelectMany_nested_navigation_property_required(bool isAsync)
        {
            await base.SelectMany_nested_navigation_property_required(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]");
        }

        public override async Task SelectMany_nested_navigation_property_optional_and_projection(bool isAsync)
        {
            await base.SelectMany_nested_navigation_property_optional_and_projection(isAsync);

            AssertSql(
                @"SELECT [l1].[Name]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]");
        }

        public override async Task Multiple_SelectMany_calls(bool isAsync)
        {
            await base.Multiple_SelectMany_calls(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]");
        }

        public override async Task SelectMany_navigation_property_with_another_navigation_in_subquery(bool isAsync)
        {
            await base.SelectMany_navigation_property_with_another_navigation_in_subquery(isAsync);

            AssertSql(
                @"SELECT [t].[Id], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l0].[Id] AS [Id0], [l0].[OneToMany_Optional_Inverse2Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]");
        }

        [ConditionalFact]
        public void Multiple_complex_includes_from_sql()
        {
            using (var context = CreateContext())
            {
                var query = context.LevelOne.FromSqlRaw("SELECT * FROM [LevelOne]")
                    .Include(e => e.OneToOne_Optional_FK1)
                    .ThenInclude(e => e.OneToMany_Optional2)
                    .Include(e => e.OneToMany_Optional1)
                    .ThenInclude(e => e.OneToOne_Optional_FK2);

                var results = query.ToList();

                Assert.Equal(13, results.Count);

                AssertSql(
                    @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM (
    SELECT * FROM [LevelOne]
) AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id0], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name] AS [Name0], [l3].[OneToMany_Optional_Inverse3Id], [l3].[OneToMany_Optional_Self_Inverse3Id], [l3].[OneToMany_Required_Inverse3Id], [l3].[OneToMany_Required_Self_Inverse3Id], [l3].[OneToOne_Optional_PK_Inverse3Id], [l3].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l2]
    LEFT JOIN [LevelThree] AS [l3] ON [l2].[Id] = [l3].[Level2_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l1].[Id], [t].[Id]");
            }
        }

        public override async Task Where_navigation_property_to_collection(bool isAsync)
        {
            await base.Where_navigation_property_to_collection(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK1] ON [l1].[Id] = [l1.OneToOne_Required_FK1].[Level1_Required_Id]
WHERE (
    SELECT COUNT(*)
    FROM [LevelThree] AS [l]
    WHERE [l1.OneToOne_Required_FK1].[Id] = [l].[OneToMany_Optional_Inverse3Id]
) > 0");
        }

        public override async Task Where_navigation_property_to_collection2(bool isAsync)
        {
            await base.Where_navigation_property_to_collection2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse3Id], [l].[OneToMany_Optional_Self_Inverse3Id], [l].[OneToMany_Required_Inverse3Id], [l].[OneToMany_Required_Self_Inverse3Id], [l].[OneToOne_Optional_PK_Inverse3Id], [l].[OneToOne_Optional_Self3Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
WHERE (
    SELECT COUNT(*)
    FROM [LevelThree] AS [l1]
    WHERE ([l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]) AND [l1].[OneToMany_Optional_Inverse3Id] IS NOT NULL) > 0");
        }

        public override async Task Where_navigation_property_to_collection_of_original_entity_type(bool isAsync)
        {
            await base.Where_navigation_property_to_collection_of_original_entity_type(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[OneToMany_Required_Inverse2Id] = [l0].[Id]
WHERE (
    SELECT COUNT(*)
    FROM [LevelTwo] AS [l1]
    WHERE ([l0].[Id] = [l1].[OneToMany_Optional_Inverse2Id]) AND [l1].[OneToMany_Optional_Inverse2Id] IS NOT NULL) > 0");
        }

        public override async Task Complex_multi_include_with_order_by_and_paging(bool isAsync)
        {
            await base.Complex_multi_include_with_order_by_and_paging(isAsync);

            if (SupportsOffset)
            {
                AssertSql(
                    @"@__p_0='0'
@__p_1='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id]
FROM (
    SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
    ORDER BY [l].[Name]
    OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
) AS [t]
LEFT JOIN [LevelTwo] AS [l0] ON [t].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[OneToMany_Required_Inverse3Id]
ORDER BY [t].[Name], [t].[Id], [l1].[Id], [l2].[Id]");
            }
        }

        public override async Task Complex_multi_include_with_order_by_and_paging_joins_on_correct_key(bool isAsync)
        {
            await base.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key(isAsync);

            if (SupportsOffset)
            {
                AssertSql(
                    @"@__p_0='0'
@__p_1='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id], [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse3Id], [l3].[OneToMany_Optional_Self_Inverse3Id], [l3].[OneToMany_Required_Inverse3Id], [l3].[OneToMany_Required_Self_Inverse3Id], [l3].[OneToOne_Optional_PK_Inverse3Id], [l3].[OneToOne_Optional_Self3Id]
FROM (
    SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
    ORDER BY [l].[Name]
    OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
) AS [t]
LEFT JOIN [LevelTwo] AS [l0] ON [t].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[OneToMany_Optional_Inverse3Id]
LEFT JOIN [LevelThree] AS [l3] ON [l1].[Id] = [l3].[OneToMany_Required_Inverse3Id]
ORDER BY [t].[Name], [t].[Id], [l2].[Id], [l3].[Id]");
            }
        }

        public override async Task Complex_multi_include_with_order_by_and_paging_joins_on_correct_key2(bool isAsync)
        {
            await base.Complex_multi_include_with_order_by_and_paging_joins_on_correct_key2(isAsync);

            if (SupportsOffset)
            {
                AssertSql(
                    @"@__p_0='0'
@__p_1='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
FROM (
    SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
    ORDER BY [l].[Name]
    OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY
) AS [t]
LEFT JOIN [LevelTwo] AS [l0] ON [t].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Optional_Inverse4Id]
ORDER BY [t].[Name], [t].[Id], [l2].[Id]");
            }
        }

        public override async Task Multiple_include_with_multiple_optional_navigations(bool isAsync)
        {
            await base.Multiple_include_with_multiple_optional_navigations(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id], [l3].[Id], [l3].[Date], [l3].[Level1_Optional_Id], [l3].[Level1_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse2Id], [l3].[OneToMany_Optional_Self_Inverse2Id], [l3].[OneToMany_Required_Inverse2Id], [l3].[OneToMany_Required_Self_Inverse2Id], [l3].[OneToOne_Optional_PK_Inverse2Id], [l3].[OneToOne_Optional_Self2Id], [l4].[Id], [l4].[Level2_Optional_Id], [l4].[Level2_Required_Id], [l4].[Name], [l4].[OneToMany_Optional_Inverse3Id], [l4].[OneToMany_Optional_Self_Inverse3Id], [l4].[OneToMany_Required_Inverse3Id], [l4].[OneToMany_Required_Self_Inverse3Id], [l4].[OneToOne_Optional_PK_Inverse3Id], [l4].[OneToOne_Optional_Self3Id], [l5].[Id], [l5].[Level2_Optional_Id], [l5].[Level2_Required_Id], [l5].[Name], [l5].[OneToMany_Optional_Inverse3Id], [l5].[OneToMany_Optional_Self_Inverse3Id], [l5].[OneToMany_Required_Inverse3Id], [l5].[OneToMany_Required_Self_Inverse3Id], [l5].[OneToOne_Optional_PK_Inverse3Id], [l5].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[Level2_Optional_Id]
LEFT JOIN [LevelTwo] AS [l3] ON [l].[Id] = [l3].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l4] ON [l3].[Id] = [l4].[Level2_Optional_Id]
LEFT JOIN [LevelThree] AS [l5] ON [l0].[Id] = [l5].[OneToMany_Optional_Inverse3Id]
WHERE ([l1].[Name] <> N'Foo') OR [l1].[Name] IS NULL
ORDER BY [l].[Id], [l5].[Id]");
        }

        public override async Task Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level(bool isAsync)
        {
            await base.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level(isAsync);

            AssertSql(
                @"SELECT DISTINCT [l].[Name]
FROM [LevelOne] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l0]
    WHERE [l0].[Level1_Required_Id] = [l].[Id])");
        }

        public override async Task Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level_join(bool isAsync)
        {
            await base.Correlated_subquery_doesnt_project_unnecessary_columns_in_top_level_join(isAsync);

            AssertSql(
                @"SELECT [l].[Name] AS [Name1], [t].[Id] AS [Id2]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Optional_Id] = [l1].[Id]
) AS [t] ON [l].[Id] = [t].[Id0]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l2]
    WHERE [l2].[Level1_Required_Id] = [l].[Id])");
        }

        public override async Task Correlated_nested_subquery_doesnt_project_unnecessary_columns_in_top_level(bool isAsync)
        {
            await base.Correlated_nested_subquery_doesnt_project_unnecessary_columns_in_top_level(isAsync);

            AssertSql(
                @"SELECT DISTINCT [l].[Name]
FROM [LevelOne] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l0]
    WHERE EXISTS (
        SELECT 1
        FROM [LevelThree] AS [l1]))");
        }

        public override async Task Correlated_nested_two_levels_up_subquery_doesnt_project_unnecessary_columns_in_top_level(bool isAsync)
        {
            await base.Correlated_nested_two_levels_up_subquery_doesnt_project_unnecessary_columns_in_top_level(isAsync);

            AssertSql(
                @"SELECT DISTINCT [l].[Name]
FROM [LevelOne] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [LevelTwo] AS [l0]
    WHERE EXISTS (
        SELECT 1
        FROM [LevelThree] AS [l1]))");
        }

        public override async Task GroupJoin_on_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(bool isAsync)
        {
            await base.GroupJoin_on_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l2]
    WHERE ([l2].[Name] <> N'L2 01') OR [l2].[Name] IS NULL
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1].[Id]");
        }

        public override async Task GroupJoin_on_complex_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(
            bool isAsync)
        {
            await base.GroupJoin_on_complex_subquery_and_set_operation_on_grouping_but_nothing_from_grouping_is_projected(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN (
    SELECT [l1.OneToOne_Required_FK1].[Id], [l1.OneToOne_Required_FK1].[Date], [l1.OneToOne_Required_FK1].[Level1_Optional_Id], [l1.OneToOne_Required_FK1].[Level1_Required_Id], [l1.OneToOne_Required_FK1].[Name], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_PK_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK1] ON [l10].[Id] = [l1.OneToOne_Required_FK1].[Level1_Required_Id]
    WHERE ([l10].[Name] <> N'L1 01') OR [l10].[Name] IS NULL
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1].[Id]");
        }

        public override async Task Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin1(bool isAsync)
        {
            await base.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin1(isAsync);

            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM (
    SELECT [l1.OneToOne_Required_FK1].[Id], [l1.OneToOne_Required_FK1].[Date], [l1.OneToOne_Required_FK1].[Level1_Optional_Id], [l1.OneToOne_Required_FK1].[Level1_Required_Id], [l1.OneToOne_Required_FK1].[Name], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_PK_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK1] ON [l10].[Id] = [l1.OneToOne_Required_FK1].[Level1_Required_Id]
) AS [t]",
                //
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l1]");
        }

        public override async Task Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin2(bool isAsync)
        {
            await base.Null_protection_logic_work_for_inner_key_access_of_manually_created_GroupJoin2(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN (
    SELECT [l1.OneToOne_Required_FK1].[Id], [l1.OneToOne_Required_FK1].[Date], [l1.OneToOne_Required_FK1].[Level1_Optional_Id], [l1.OneToOne_Required_FK1].[Level1_Required_Id], [l1.OneToOne_Required_FK1].[Name], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_PK_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK1] ON [l10].[Id] = [l1.OneToOne_Required_FK1].[Level1_Required_Id]
) AS [t] ON [l1].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1].[Id]");
        }

        public override async Task Null_protection_logic_work_for_outer_key_access_of_manually_created_GroupJoin(bool isAsync)
        {
            await base.Null_protection_logic_work_for_outer_key_access_of_manually_created_GroupJoin(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [l1.OneToOne_Required_FK1].[Id], [l1.OneToOne_Required_FK1].[Date], [l1.OneToOne_Required_FK1].[Level1_Optional_Id], [l1.OneToOne_Required_FK1].[Level1_Required_Id], [l1.OneToOne_Required_FK1].[Name], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Optional_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToMany_Required_Self_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_PK_Inverse2Id], [l1.OneToOne_Required_FK1].[OneToOne_Optional_Self2Id], [l10].[Id], [l10].[Date], [l10].[Name], [l10].[OneToMany_Optional_Self_Inverse1Id], [l10].[OneToMany_Required_Self_Inverse1Id], [l10].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Required_FK1] ON [l1].[Id] = [l1.OneToOne_Required_FK1].[Level1_Required_Id]
LEFT JOIN [LevelOne] AS [l10] ON [l1.OneToOne_Required_FK1].[Level1_Optional_Id] = [l10].[Id]");
        }

        public override async Task SelectMany_where_with_subquery(bool isAsync)
        {
            await base.SelectMany_where_with_subquery(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Required_Inverse2Id]
WHERE EXISTS (
    SELECT 1
    FROM [LevelThree] AS [l1]
    WHERE [l0].[Id] = [l1].[OneToMany_Required_Inverse3Id])");
        }

        public override async Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access1(bool isAsync)
        {
            await base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access1(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
ORDER BY [l0].[Id]");
        }

        public override async Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access2(bool isAsync)
        {
            await base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access2(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
ORDER BY [l0].[Id]");
        }

        public override async Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access3(bool isAsync)
        {
            await base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access3(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
ORDER BY [l0].[Id]");
        }

        public override async Task Order_by_key_of_navigation_similar_to_projected_gets_optimized_into_FK_access(bool isAsync)
        {
            await base.Order_by_key_of_navigation_similar_to_projected_gets_optimized_into_FK_access(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
ORDER BY [l0].[Id]");
        }

        public override async Task Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(bool isAsync)
        {
            await base.Order_by_key_of_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(isAsync);

            AssertSql(
                @"@__p_0='10'

SELECT [l1].[Name]
FROM (
    SELECT TOP(@__p_0) [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id] AS [Id0]
    FROM [LevelThree] AS [l]
    INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
    ORDER BY [l0].[Id]
) AS [t]
INNER JOIN [LevelOne] AS [l1] ON [t].[Level1_Required_Id] = [l1].[Id]
ORDER BY [t].[Id]");
        }

        public override async Task Order_by_key_of_anonymous_type_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(bool isAsync)
        {
            await base.Order_by_key_of_anonymous_type_projected_navigation_doesnt_get_optimized_into_FK_access_subquery(isAsync);

            AssertSql(
                @"@__p_0='10'

SELECT TOP(@__p_0) [l0].[Name]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
ORDER BY [l0].[Id]");
        }

        public override async Task Optional_navigation_take_optional_navigation(bool isAsync)
        {
            await base.Optional_navigation_take_optional_navigation(isAsync);

            AssertSql(
                @"@__p_0='10'

SELECT [l1].[Name]
FROM (
    SELECT TOP(@__p_0) [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id] AS [Id0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    ORDER BY [l0].[Id]
) AS [t]
LEFT JOIN [LevelThree] AS [l1] ON [t].[Id] = [l1].[Level2_Optional_Id]
ORDER BY [t].[Id]");
        }

        public override async Task Projection_select_correct_table_from_subquery_when_materialization_is_not_required(bool isAsync)
        {
            await base.Projection_select_correct_table_from_subquery_when_materialization_is_not_required(isAsync);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l].[Name]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
WHERE ([l0].[Name] = N'L1 03') AND [l0].[Name] IS NOT NULL
ORDER BY [l].[Id]");
        }

        public override async Task Projection_select_correct_table_with_anonymous_projection_in_subquery(bool isAsync)
        {
            await base.Projection_select_correct_table_with_anonymous_projection_in_subquery(isAsync);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l].[Name]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
WHERE (([l0].[Name] = N'L1 03') AND [l0].[Name] IS NOT NULL) AND (([l1].[Name] = N'L3 08') AND [l1].[Name] IS NOT NULL)
ORDER BY [l0].[Id]");
        }

        public override async Task Projection_select_correct_table_in_subquery_when_materialization_is_not_required_in_multiple_joins(bool isAsync)
        {
            await base.Projection_select_correct_table_in_subquery_when_materialization_is_not_required_in_multiple_joins(isAsync);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l0].[Name]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
WHERE (([l0].[Name] = N'L1 03') AND [l0].[Name] IS NOT NULL) AND (([l1].[Name] = N'L3 08') AND [l1].[Name] IS NOT NULL)
ORDER BY [l0].[Id]");
        }

        public override async Task Where_predicate_on_optional_reference_navigation(bool isAsync)
        {
            await base.Where_predicate_on_optional_reference_navigation(isAsync);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [l].[Name]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
WHERE ([l0].[Name] = N'L2 03') AND [l0].[Name] IS NOT NULL
ORDER BY [l].[Id]");
        }

        public override async Task SelectMany_with_Include1(bool isAsync)
        {
            await base.SelectMany_with_Include1(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l0].[Id], [l1].[Id]");
        }

        public override async Task Orderby_SelectMany_with_Include1(bool isAsync)
        {
            await base.Orderby_SelectMany_with_Include1(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l0].[Id], [l1].[Id]");
        }

        public override async Task SelectMany_with_Include2(bool isAsync)
        {
            await base.SelectMany_with_Include2(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]");
        }

        public override async Task SelectMany_with_Include_ThenInclude(bool isAsync)
        {
            await base.SelectMany_with_Include_ThenInclude(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l].[Id], [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Optional_Inverse4Id]
ORDER BY [l].[Id], [l0].[Id], [l2].[Id]");
        }

        public override async Task Multiple_SelectMany_with_Include(bool isAsync)
        {
            await base.Multiple_SelectMany_with_Include(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [l].[Id], [l0].[Id], [l3].[Id], [l3].[Level3_Optional_Id], [l3].[Level3_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse4Id], [l3].[OneToMany_Optional_Self_Inverse4Id], [l3].[OneToMany_Required_Inverse4Id], [l3].[OneToMany_Required_Self_Inverse4Id], [l3].[OneToOne_Optional_PK_Inverse4Id], [l3].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Required_Id]
LEFT JOIN [LevelFour] AS [l3] ON [l1].[Id] = [l3].[OneToMany_Optional_Inverse4Id]
ORDER BY [l].[Id], [l0].[Id], [l1].[Id], [l3].[Id]");
        }

        public override async Task SelectMany_with_string_based_Include1(bool isAsync)
        {
            await base.SelectMany_with_string_based_Include1(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]");
        }

        public override async Task SelectMany_with_string_based_Include2(bool isAsync)
        {
            await base.SelectMany_with_string_based_Include2(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Required_Id]");
        }

        public override async Task Multiple_SelectMany_with_string_based_Include(bool isAsync)
        {
            await base.Multiple_SelectMany_with_string_based_Include(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Required_Id]");
        }

        public override async Task Required_navigation_with_Include(bool isAsync)
        {
            await base.Required_navigation_with_Include(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id]
FROM [LevelThree] AS [l1]
INNER JOIN [LevelTwo] AS [l] ON [l1].[Level2_Required_Id] = [l].[Id]
INNER JOIN [LevelOne] AS [l0] ON [l].[OneToMany_Required_Inverse2Id] = [l0].[Id]");
        }

        public override async Task Required_navigation_with_Include_ThenInclude(bool isAsync)
        {
            await base.Required_navigation_with_Include_ThenInclude(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse3Id], [l].[OneToMany_Optional_Self_Inverse3Id], [l].[OneToMany_Required_Inverse3Id], [l].[OneToMany_Required_Self_Inverse3Id], [l].[OneToOne_Optional_PK_Inverse3Id], [l].[OneToOne_Optional_Self3Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
FROM [LevelFour] AS [l2]
INNER JOIN [LevelThree] AS [l] ON [l2].[Level3_Required_Id] = [l].[Id]
INNER JOIN [LevelTwo] AS [l0] ON [l].[OneToMany_Required_Inverse3Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[OneToMany_Optional_Inverse2Id] = [l1].[Id]");
        }

        public override async Task Multiple_required_navigations_with_Include(bool isAsync)
        {
            await base.Multiple_required_navigations_with_Include(isAsync);

            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Date], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Name], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Optional_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Optional_Self_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Required_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Required_Self_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToOne_Optional_PK_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToOne_Optional_Self2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Name], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Optional_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Optional_Self_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Required_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Required_Self_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToOne_Optional_PK_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToOne_Optional_Self3Id]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse4] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse4].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3] ON [l4.OneToOne_Required_FK_Inverse4].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id]
LEFT JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2] ON [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id] = [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Optional_Id]");
        }

        public override async Task Multiple_required_navigation_using_multiple_selects_with_Include(bool isAsync)
        {
            await base.Multiple_required_navigation_using_multiple_selects_with_Include(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse3Id], [l0].[OneToMany_Optional_Self_Inverse3Id], [l0].[OneToMany_Required_Inverse3Id], [l0].[OneToMany_Required_Self_Inverse3Id], [l0].[OneToOne_Optional_PK_Inverse3Id], [l0].[OneToOne_Optional_Self3Id]
FROM [LevelFour] AS [l1]
INNER JOIN [LevelThree] AS [l2] ON [l1].[Level3_Required_Id] = [l2].[Id]
INNER JOIN [LevelTwo] AS [l] ON [l2].[Level2_Required_Id] = [l].[Id]
LEFT JOIN [LevelThree] AS [l0] ON [l].[Id] = [l0].[Level2_Optional_Id]");
        }

        public override async Task Multiple_required_navigation_with_string_based_Include(bool isAsync)
        {
            await base.Multiple_required_navigation_with_string_based_Include(isAsync);

            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Date], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Name], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Optional_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Optional_Self_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Required_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Required_Self_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToOne_Optional_PK_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToOne_Optional_Self2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Name], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Optional_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Optional_Self_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Required_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Required_Self_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToOne_Optional_PK_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToOne_Optional_Self3Id]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse4] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse4].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3] ON [l4.OneToOne_Required_FK_Inverse4].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id]
LEFT JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2] ON [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id] = [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Optional_Id]");
        }

        public override async Task Multiple_required_navigation_using_multiple_selects_with_string_based_Include(bool isAsync)
        {
            await base.Multiple_required_navigation_using_multiple_selects_with_string_based_Include(isAsync);

            AssertSql(
                @"SELECT [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Date], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Level1_Optional_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Level1_Required_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Name], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Optional_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Optional_Self_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Required_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToMany_Required_Self_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToOne_Optional_PK_Inverse2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[OneToOne_Optional_Self2Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Optional_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Required_Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Name], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Optional_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Optional_Self_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Required_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToMany_Required_Self_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToOne_Optional_PK_Inverse3Id], [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[OneToOne_Optional_Self3Id]
FROM [LevelFour] AS [l4]
INNER JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse4] ON [l4].[Level3_Required_Id] = [l4.OneToOne_Required_FK_Inverse4].[Id]
INNER JOIN [LevelTwo] AS [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3] ON [l4.OneToOne_Required_FK_Inverse4].[Level2_Required_Id] = [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id]
LEFT JOIN [LevelThree] AS [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2] ON [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3].[Id] = [l4.OneToOne_Required_FK_Inverse4.OneToOne_Required_FK_Inverse3.OneToOne_Optional_FK2].[Level2_Optional_Id]");
        }

        public override async Task Optional_navigation_with_Include(bool isAsync)
        {
            await base.Optional_navigation_with_Include(isAsync);

            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK1].[Id], [l1.OneToOne_Optional_FK1].[Date], [l1.OneToOne_Optional_FK1].[Level1_Optional_Id], [l1.OneToOne_Optional_FK1].[Level1_Required_Id], [l1.OneToOne_Optional_FK1].[Name], [l1.OneToOne_Optional_FK1].[OneToMany_Optional_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Optional_Self_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Required_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Required_Self_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToOne_Optional_PK_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToOne_Optional_Self2Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[Level2_Optional_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[Level2_Required_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[Name], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[OneToMany_Optional_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[OneToMany_Optional_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[OneToMany_Required_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[OneToMany_Required_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[OneToOne_Optional_PK_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2] ON [l1.OneToOne_Optional_FK1].[Id] = [l1.OneToOne_Optional_FK1.OneToOne_Optional_FK2].[Level2_Optional_Id]");
        }

        public override async Task Optional_navigation_with_Include_ThenInclude(bool isAsync)
        {
            await base.Optional_navigation_with_Include_ThenInclude(isAsync);

            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK1].[Id], [l1.OneToOne_Optional_FK1].[Date], [l1.OneToOne_Optional_FK1].[Level1_Optional_Id], [l1.OneToOne_Optional_FK1].[Level1_Required_Id], [l1.OneToOne_Optional_FK1].[Name], [l1.OneToOne_Optional_FK1].[OneToMany_Optional_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Optional_Self_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Required_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Required_Self_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToOne_Optional_PK_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]
ORDER BY [l1.OneToOne_Optional_FK1].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Level2_Optional_Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Level2_Required_Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Name], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Optional_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Optional_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Required_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Required_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToOne_Optional_PK_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToOne_Optional_Self3Id], [l.OneToOne_Optional_FK3].[Id], [l.OneToOne_Optional_FK3].[Level3_Optional_Id], [l.OneToOne_Optional_FK3].[Level3_Required_Id], [l.OneToOne_Optional_FK3].[Name], [l.OneToOne_Optional_FK3].[OneToMany_Optional_Inverse4Id], [l.OneToOne_Optional_FK3].[OneToMany_Optional_Self_Inverse4Id], [l.OneToOne_Optional_FK3].[OneToMany_Required_Inverse4Id], [l.OneToOne_Optional_FK3].[OneToMany_Required_Self_Inverse4Id], [l.OneToOne_Optional_FK3].[OneToOne_Optional_PK_Inverse4Id], [l.OneToOne_Optional_FK3].[OneToOne_Optional_Self4Id]
FROM [LevelThree] AS [l1.OneToOne_Optional_FK1.OneToMany_Optional2]
LEFT JOIN [LevelFour] AS [l.OneToOne_Optional_FK3] ON [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Id] = [l.OneToOne_Optional_FK3].[Level3_Optional_Id]
INNER JOIN (
    SELECT DISTINCT [l1.OneToOne_Optional_FK10].[Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK10] ON [l10].[Id] = [l1.OneToOne_Optional_FK10].[Level1_Optional_Id]
) AS [t] ON [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Optional_Inverse3Id] = [t].[Id]
ORDER BY [t].[Id]");
        }

        public override async Task Multiple_optional_navigation_with_Include(bool isAsync)
        {
            await base.Multiple_optional_navigation_with_Include(isAsync);

            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Level2_Optional_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Level2_Required_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Name], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Optional_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Optional_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Required_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Required_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToOne_Optional_PK_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2] ON [l1.OneToOne_Optional_FK1].[Id] = [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToOne_Optional_PK_Inverse3Id]
ORDER BY [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Level3_Optional_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Level3_Required_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Name], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Required_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Required_Self_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToOne_Optional_Self4Id]
FROM [LevelFour] AS [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3]
INNER JOIN (
    SELECT DISTINCT [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK20].[Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK10] ON [l10].[Id] = [l1.OneToOne_Optional_FK10].[Level1_Optional_Id]
    LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK20] ON [l1.OneToOne_Optional_FK10].[Id] = [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK20].[OneToOne_Optional_PK_Inverse3Id]
) AS [t] ON [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id] = [t].[Id]
ORDER BY [t].[Id]");
        }

        public override async Task Multiple_optional_navigation_with_string_based_Include(bool isAsync)
        {
            await base.Multiple_optional_navigation_with_string_based_Include(isAsync);

            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Level2_Optional_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Level2_Required_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Name], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Optional_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Optional_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Required_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToMany_Required_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToOne_Optional_PK_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2] ON [l1.OneToOne_Optional_FK1].[Id] = [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[OneToOne_Optional_PK_Inverse3Id]
ORDER BY [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Level3_Optional_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Level3_Required_Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[Name], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Required_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Required_Self_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToOne_Optional_Self4Id]
FROM [LevelFour] AS [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3]
INNER JOIN (
    SELECT DISTINCT [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK20].[Id]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK10] ON [l10].[Id] = [l1.OneToOne_Optional_FK10].[Level1_Optional_Id]
    LEFT JOIN [LevelThree] AS [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK20] ON [l1.OneToOne_Optional_FK10].[Id] = [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK20].[OneToOne_Optional_PK_Inverse3Id]
) AS [t] ON [l1.OneToOne_Optional_FK1.OneToOne_Optional_PK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id] = [t].[Id]
ORDER BY [t].[Id]");
        }

        public override async Task Optional_navigation_with_order_by_and_Include(bool isAsync)
        {
            await base.Optional_navigation_with_order_by_and_Include(isAsync);

            AssertSql(
                @"SELECT [l1.OneToOne_Optional_FK1].[Id], [l1.OneToOne_Optional_FK1].[Date], [l1.OneToOne_Optional_FK1].[Level1_Optional_Id], [l1.OneToOne_Optional_FK1].[Level1_Required_Id], [l1.OneToOne_Optional_FK1].[Name], [l1.OneToOne_Optional_FK1].[OneToMany_Optional_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Optional_Self_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Required_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToMany_Required_Self_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToOne_Optional_PK_Inverse2Id], [l1.OneToOne_Optional_FK1].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]
ORDER BY [l1.OneToOne_Optional_FK1].[Name], [l1.OneToOne_Optional_FK1].[Id]",
                //
                @"SELECT [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Level2_Optional_Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Level2_Required_Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[Name], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Optional_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Optional_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Required_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Required_Self_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToOne_Optional_PK_Inverse3Id], [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToOne_Optional_Self3Id]
FROM [LevelThree] AS [l1.OneToOne_Optional_FK1.OneToMany_Optional2]
INNER JOIN (
    SELECT DISTINCT [l1.OneToOne_Optional_FK10].[Id], [l1.OneToOne_Optional_FK10].[Name]
    FROM [LevelOne] AS [l10]
    LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK10] ON [l10].[Id] = [l1.OneToOne_Optional_FK10].[Level1_Optional_Id]
) AS [t] ON [l1.OneToOne_Optional_FK1.OneToMany_Optional2].[OneToMany_Optional_Inverse3Id] = [t].[Id]
ORDER BY [t].[Name], [t].[Id]");
        }

        public override async Task Optional_navigation_with_Include_and_order(bool isAsync)
        {
            await base.Optional_navigation_with_Include_and_order(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l0]
LEFT JOIN [LevelTwo] AS [l] ON [l0].[Id] = [l].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Name], [l0].[Id]");
        }

        public override async Task SelectMany_with_order_by_and_Include(bool isAsync)
        {
            await base.SelectMany_with_order_by_and_Include(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l0].[Name], [l].[Id], [l0].[Id], [l1].[Id]");
        }

        public override async Task SelectMany_with_Include_and_order_by(bool isAsync)
        {
            await base.SelectMany_with_Include_and_order_by(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l0].[Name], [l].[Id], [l0].[Id], [l1].[Id]");
        }

        public override async Task SelectMany_with_navigation_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            await base.SelectMany_with_navigation_and_explicit_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
WHERE [l0].[Id] IS NOT NULL");
        }

        public override async Task SelectMany_with_navigation_and_Distinct(bool isAsync)
        {
            await base.SelectMany_with_navigation_and_Distinct(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT DISTINCT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l0]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[OneToMany_Optional_Inverse2Id]
WHERE CAST(1 AS bit) = CAST(1 AS bit)
ORDER BY [l].[Id], [t].[Id], [l1].[Id]");
        }

        public override async Task SelectMany_with_navigation_filter_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            await base.SelectMany_with_navigation_filter_and_explicit_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l0]
    WHERE [l0].[Id] > 5
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
WHERE [t].[Id] IS NOT NULL");
        }

        public override async Task SelectMany_with_nested_navigation_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            await base.SelectMany_with_nested_navigation_and_explicit_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
WHERE [l1].[Id] IS NOT NULL");
        }

        public override async Task SelectMany_with_nested_navigation_filter_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            await base.SelectMany_with_nested_navigation_filter_and_explicit_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN (
    SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelThree] AS [l1]
    WHERE [l1].[Id] > 5
) AS [t] ON [l0].[Id] = [t].[OneToMany_Optional_Inverse3Id]
WHERE [t].[Id] IS NOT NULL");
        }

        public override async Task SelectMany_with_nested_required_navigation_filter_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            await base.SelectMany_with_nested_required_navigation_filter_and_explicit_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN (
    SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelThree] AS [l1]
    WHERE [l1].[Id] > 5
) AS [t] ON [l0].[Id] = [t].[OneToMany_Required_Inverse3Id]
WHERE [t].[Id] IS NOT NULL");
        }

        public override async Task SelectMany_with_nested_navigations_and_additional_joins_outside_of_SelectMany(bool isAsync)
        {
            await base.SelectMany_with_nested_navigations_and_additional_joins_outside_of_SelectMany(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id2], [t].[Date0], [t].[Level1_Optional_Id0], [t].[Level1_Required_Id0], [t].[Name2], [t].[OneToMany_Optional_Inverse2Id0], [t].[OneToMany_Optional_Self_Inverse2Id0], [t].[OneToMany_Required_Inverse2Id0], [t].[OneToMany_Required_Self_Inverse2Id0], [t].[OneToOne_Optional_PK_Inverse2Id0], [t].[OneToOne_Optional_Self2Id0]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Level3_Optional_Id], [l0].[Level3_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse4Id], [l0].[OneToMany_Optional_Self_Inverse4Id], [l0].[OneToMany_Required_Inverse4Id], [l0].[OneToMany_Required_Self_Inverse4Id], [l0].[OneToOne_Optional_PK_Inverse4Id], [l0].[OneToOne_Optional_Self4Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id2], [l3].[Date] AS [Date0], [l3].[Level1_Optional_Id] AS [Level1_Optional_Id0], [l3].[Level1_Required_Id] AS [Level1_Required_Id0], [l3].[Name] AS [Name2], [l3].[OneToMany_Optional_Inverse2Id] AS [OneToMany_Optional_Inverse2Id0], [l3].[OneToMany_Optional_Self_Inverse2Id] AS [OneToMany_Optional_Self_Inverse2Id0], [l3].[OneToMany_Required_Inverse2Id] AS [OneToMany_Required_Inverse2Id0], [l3].[OneToMany_Required_Self_Inverse2Id] AS [OneToMany_Required_Self_Inverse2Id0], [l3].[OneToOne_Optional_PK_Inverse2Id] AS [OneToOne_Optional_PK_Inverse2Id0], [l3].[OneToOne_Optional_Self2Id] AS [OneToOne_Optional_Self2Id0]
    FROM [LevelFour] AS [l0]
    INNER JOIN [LevelThree] AS [l1] ON [l0].[Level3_Required_Id] = [l1].[Id]
    LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Level2_Optional_Id] = [l2].[Id]
    INNER JOIN [LevelTwo] AS [l3] ON [l2].[Id] = [l3].[OneToMany_Required_Self_Inverse2Id]
) AS [t] ON [l].[Id] = [t].[Level1_Optional_Id0]");
        }

        public override async Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany(bool isAsync)
        {
            await base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id2], [t].[Date0], [t].[Level1_Optional_Id0], [t].[Level1_Required_Id0], [t].[Name2], [t].[OneToMany_Optional_Inverse2Id0], [t].[OneToMany_Optional_Self_Inverse2Id0], [t].[OneToMany_Required_Inverse2Id0], [t].[OneToMany_Required_Self_Inverse2Id0], [t].[OneToOne_Optional_PK_Inverse2Id0], [t].[OneToOne_Optional_Self2Id0]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT [l0].[Id], [l0].[Level3_Optional_Id], [l0].[Level3_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse4Id], [l0].[OneToMany_Optional_Self_Inverse4Id], [l0].[OneToMany_Required_Inverse4Id], [l0].[OneToMany_Required_Self_Inverse4Id], [l0].[OneToOne_Optional_PK_Inverse4Id], [l0].[OneToOne_Optional_Self4Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id2], [l3].[Date] AS [Date0], [l3].[Level1_Optional_Id] AS [Level1_Optional_Id0], [l3].[Level1_Required_Id] AS [Level1_Required_Id0], [l3].[Name] AS [Name2], [l3].[OneToMany_Optional_Inverse2Id] AS [OneToMany_Optional_Inverse2Id0], [l3].[OneToMany_Optional_Self_Inverse2Id] AS [OneToMany_Optional_Self_Inverse2Id0], [l3].[OneToMany_Required_Inverse2Id] AS [OneToMany_Required_Inverse2Id0], [l3].[OneToMany_Required_Self_Inverse2Id] AS [OneToMany_Required_Self_Inverse2Id0], [l3].[OneToOne_Optional_PK_Inverse2Id] AS [OneToOne_Optional_PK_Inverse2Id0], [l3].[OneToOne_Optional_Self2Id] AS [OneToOne_Optional_Self2Id0]
    FROM [LevelFour] AS [l0]
    INNER JOIN [LevelThree] AS [l1] ON [l0].[Level3_Required_Id] = [l1].[Id]
    LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Level2_Optional_Id] = [l2].[Id]
    LEFT JOIN [LevelTwo] AS [l3] ON [l2].[Id] = [l3].[OneToMany_Required_Self_Inverse2Id]
) AS [t] ON [l].[Id] = [t].[Level1_Optional_Id0]");
        }

        public override async Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany2(bool isAsync)
        {
            await base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany2(isAsync);

            AssertSql(
                @"SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id], [l3].[Date], [l3].[Name], [l3].[OneToMany_Optional_Self_Inverse1Id], [l3].[OneToMany_Required_Self_Inverse1Id], [l3].[OneToOne_Optional_Self1Id]
FROM [LevelFour] AS [l]
INNER JOIN [LevelThree] AS [l0] ON [l].[Level3_Required_Id] = [l0].[Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Level2_Optional_Id] = [l1].[Id]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Required_Self_Inverse2Id]
INNER JOIN [LevelOne] AS [l3] ON [l2].[Level1_Optional_Id] = [l3].[Id]");
        }

        public override async Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany3(bool isAsync)
        {
            await base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany3(isAsync);

            AssertSql(
                @"SELECT [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [l3].[Id], [l3].[Date], [l3].[Level1_Optional_Id], [l3].[Level1_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse2Id], [l3].[OneToMany_Optional_Self_Inverse2Id], [l3].[OneToMany_Required_Inverse2Id], [l3].[OneToMany_Required_Self_Inverse2Id], [l3].[OneToOne_Optional_PK_Inverse2Id], [l3].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Required_Inverse4Id]
INNER JOIN [LevelTwo] AS [l3] ON [l2].[Id] = [l3].[Id]");
        }

        public override async Task SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany4(bool isAsync)
        {
            await base.SelectMany_with_nested_navigations_explicit_DefaultIfEmpty_and_additional_joins_outside_of_SelectMany4(isAsync);

            AssertSql(
                @"SELECT [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [l3].[Id], [l3].[Date], [l3].[Level1_Optional_Id], [l3].[Level1_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse2Id], [l3].[OneToMany_Optional_Self_Inverse2Id], [l3].[OneToMany_Required_Inverse2Id], [l3].[OneToMany_Required_Self_Inverse2Id], [l3].[OneToOne_Optional_PK_Inverse2Id], [l3].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Required_Inverse4Id]
LEFT JOIN [LevelTwo] AS [l3] ON [l2].[Id] = [l3].[Id]");
        }

        public override async Task Multiple_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_joined_together(bool isAsync)
        {
            await base.Multiple_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_joined_together(isAsync);

            AssertSql(
                @"SELECT [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [t].[Id2], [t].[Date0], [t].[Level1_Optional_Id0], [t].[Level1_Required_Id0], [t].[Name2], [t].[OneToMany_Optional_Inverse2Id0], [t].[OneToMany_Optional_Self_Inverse2Id0], [t].[OneToMany_Required_Inverse2Id0], [t].[OneToMany_Required_Self_Inverse2Id0], [t].[OneToOne_Optional_PK_Inverse2Id0], [t].[OneToOne_Optional_Self2Id0]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Required_Inverse4Id]
INNER JOIN (
    SELECT [l3].[Id], [l3].[Level3_Optional_Id], [l3].[Level3_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse4Id], [l3].[OneToMany_Optional_Self_Inverse4Id], [l3].[OneToMany_Required_Inverse4Id], [l3].[OneToMany_Required_Self_Inverse4Id], [l3].[OneToOne_Optional_PK_Inverse4Id], [l3].[OneToOne_Optional_Self4Id], [l4].[Id] AS [Id0], [l4].[Level2_Optional_Id], [l4].[Level2_Required_Id], [l4].[Name] AS [Name0], [l4].[OneToMany_Optional_Inverse3Id], [l4].[OneToMany_Optional_Self_Inverse3Id], [l4].[OneToMany_Required_Inverse3Id], [l4].[OneToMany_Required_Self_Inverse3Id], [l4].[OneToOne_Optional_PK_Inverse3Id], [l4].[OneToOne_Optional_Self3Id], [l5].[Id] AS [Id1], [l5].[Date], [l5].[Level1_Optional_Id], [l5].[Level1_Required_Id], [l5].[Name] AS [Name1], [l5].[OneToMany_Optional_Inverse2Id], [l5].[OneToMany_Optional_Self_Inverse2Id], [l5].[OneToMany_Required_Inverse2Id], [l5].[OneToMany_Required_Self_Inverse2Id], [l5].[OneToOne_Optional_PK_Inverse2Id], [l5].[OneToOne_Optional_Self2Id], [l6].[Id] AS [Id2], [l6].[Date] AS [Date0], [l6].[Level1_Optional_Id] AS [Level1_Optional_Id0], [l6].[Level1_Required_Id] AS [Level1_Required_Id0], [l6].[Name] AS [Name2], [l6].[OneToMany_Optional_Inverse2Id] AS [OneToMany_Optional_Inverse2Id0], [l6].[OneToMany_Optional_Self_Inverse2Id] AS [OneToMany_Optional_Self_Inverse2Id0], [l6].[OneToMany_Required_Inverse2Id] AS [OneToMany_Required_Inverse2Id0], [l6].[OneToMany_Required_Self_Inverse2Id] AS [OneToMany_Required_Self_Inverse2Id0], [l6].[OneToOne_Optional_PK_Inverse2Id] AS [OneToOne_Optional_PK_Inverse2Id0], [l6].[OneToOne_Optional_Self2Id] AS [OneToOne_Optional_Self2Id0]
    FROM [LevelFour] AS [l3]
    INNER JOIN [LevelThree] AS [l4] ON [l3].[Level3_Required_Id] = [l4].[Id]
    LEFT JOIN [LevelTwo] AS [l5] ON [l4].[Level2_Optional_Id] = [l5].[Id]
    LEFT JOIN [LevelTwo] AS [l6] ON [l5].[Id] = [l6].[OneToMany_Required_Self_Inverse2Id]
) AS [t] ON [l2].[Id] = [t].[Id2]");
        }

        public override async Task
            SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_same_navs(bool isAsync)
        {
            await base.SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_same_navs(isAsync);

            AssertSql(
                @"SELECT [l4].[Id], [l4].[Date], [l4].[Name], [l4].[OneToMany_Optional_Self_Inverse1Id], [l4].[OneToMany_Required_Self_Inverse1Id], [l4].[OneToOne_Optional_Self1Id]
FROM [LevelFour] AS [l]
INNER JOIN [LevelThree] AS [l0] ON [l].[Level3_Required_Id] = [l0].[Id]
INNER JOIN [LevelTwo] AS [l1] ON [l0].[Level2_Required_Id] = [l1].[Id]
LEFT JOIN [LevelThree] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Required_Inverse3Id]
LEFT JOIN [LevelTwo] AS [l3] ON [l2].[Level2_Required_Id] = [l3].[Id]
LEFT JOIN [LevelOne] AS [l4] ON [l3].[Id] = [l4].[Id]");
        }

        public override async Task SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_different_navs(bool isAsync)
        {
            await base.SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_followed_by_Select_required_navigation_using_different_navs(isAsync);

            AssertSql(
                @"SELECT [l3].[Id], [l3].[Date], [l3].[Name], [l3].[OneToMany_Optional_Self_Inverse1Id], [l3].[OneToMany_Required_Self_Inverse1Id], [l3].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Level2_Required_Id] = [l2].[Id]
LEFT JOIN [LevelOne] AS [l3] ON [l2].[Id] = [l3].[Id]");
        }

        public override async Task Complex_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_with_other_query_operators_composed_on_top(bool isAsync)
        {
            await base.Complex_SelectMany_with_nested_navigations_and_explicit_DefaultIfEmpty_with_other_query_operators_composed_on_top(isAsync);

            AssertSql(
                @"SELECT [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [l14].[Name], [l].[Id], [t].[Id], [t].[Id0], [t1].[Id], [t1].[Date], [t1].[Level1_Optional_Id], [t1].[Level1_Required_Id], [t1].[Name], [t1].[OneToMany_Optional_Inverse2Id], [t1].[OneToMany_Optional_Self_Inverse2Id], [t1].[OneToMany_Required_Inverse2Id], [t1].[OneToMany_Required_Self_Inverse2Id], [t1].[OneToOne_Optional_PK_Inverse2Id], [t1].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Required_Inverse4Id]
INNER JOIN (
    SELECT [l3].[Id], [l3].[Level3_Optional_Id], [l3].[Level3_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse4Id], [l3].[OneToMany_Optional_Self_Inverse4Id], [l3].[OneToMany_Required_Inverse4Id], [l3].[OneToMany_Required_Self_Inverse4Id], [l3].[OneToOne_Optional_PK_Inverse4Id], [l3].[OneToOne_Optional_Self4Id], [l4].[Id] AS [Id0], [l4].[Level2_Optional_Id], [l4].[Level2_Required_Id], [l4].[Name] AS [Name0], [l4].[OneToMany_Optional_Inverse3Id], [l4].[OneToMany_Optional_Self_Inverse3Id], [l4].[OneToMany_Required_Inverse3Id], [l4].[OneToMany_Required_Self_Inverse3Id], [l4].[OneToOne_Optional_PK_Inverse3Id], [l4].[OneToOne_Optional_Self3Id], [l5].[Id] AS [Id1], [l5].[Date], [l5].[Level1_Optional_Id], [l5].[Level1_Required_Id], [l5].[Name] AS [Name1], [l5].[OneToMany_Optional_Inverse2Id], [l5].[OneToMany_Optional_Self_Inverse2Id], [l5].[OneToMany_Required_Inverse2Id], [l5].[OneToMany_Required_Self_Inverse2Id], [l5].[OneToOne_Optional_PK_Inverse2Id], [l5].[OneToOne_Optional_Self2Id], [l6].[Id] AS [Id2], [l6].[Date] AS [Date0], [l6].[Level1_Optional_Id] AS [Level1_Optional_Id0], [l6].[Level1_Required_Id] AS [Level1_Required_Id0], [l6].[Name] AS [Name2], [l6].[OneToMany_Optional_Inverse2Id] AS [OneToMany_Optional_Inverse2Id0], [l6].[OneToMany_Optional_Self_Inverse2Id] AS [OneToMany_Optional_Self_Inverse2Id0], [l6].[OneToMany_Required_Inverse2Id] AS [OneToMany_Required_Inverse2Id0], [l6].[OneToMany_Required_Self_Inverse2Id] AS [OneToMany_Required_Self_Inverse2Id0], [l6].[OneToOne_Optional_PK_Inverse2Id] AS [OneToOne_Optional_PK_Inverse2Id0], [l6].[OneToOne_Optional_Self2Id] AS [OneToOne_Optional_Self2Id0]
    FROM [LevelFour] AS [l3]
    INNER JOIN [LevelThree] AS [l4] ON [l3].[Level3_Required_Id] = [l4].[Id]
    LEFT JOIN [LevelTwo] AS [l5] ON [l4].[Level2_Optional_Id] = [l5].[Id]
    LEFT JOIN [LevelTwo] AS [l6] ON [l5].[Id] = [l6].[OneToMany_Required_Self_Inverse2Id]
) AS [t] ON [l2].[Id] = [t].[Id2]
LEFT JOIN (
    SELECT [l7].[Id], [l7].[Level3_Optional_Id], [l7].[Level3_Required_Id], [l7].[Name], [l7].[OneToMany_Optional_Inverse4Id], [l7].[OneToMany_Optional_Self_Inverse4Id], [l7].[OneToMany_Required_Inverse4Id], [l7].[OneToMany_Required_Self_Inverse4Id], [l7].[OneToOne_Optional_PK_Inverse4Id], [l7].[OneToOne_Optional_Self4Id], [l8].[Id] AS [Id0], [l8].[Level2_Optional_Id], [l8].[Level2_Required_Id], [l8].[Name] AS [Name0], [l8].[OneToMany_Optional_Inverse3Id], [l8].[OneToMany_Optional_Self_Inverse3Id], [l8].[OneToMany_Required_Inverse3Id], [l8].[OneToMany_Required_Self_Inverse3Id], [l8].[OneToOne_Optional_PK_Inverse3Id], [l8].[OneToOne_Optional_Self3Id], [l9].[Id] AS [Id1], [l9].[Date], [l9].[Level1_Optional_Id], [l9].[Level1_Required_Id], [l9].[Name] AS [Name1], [l9].[OneToMany_Optional_Inverse2Id], [l9].[OneToMany_Optional_Self_Inverse2Id], [l9].[OneToMany_Required_Inverse2Id], [l9].[OneToMany_Required_Self_Inverse2Id], [l9].[OneToOne_Optional_PK_Inverse2Id], [l9].[OneToOne_Optional_Self2Id], [l10].[Id] AS [Id2], [l10].[Level2_Optional_Id] AS [Level2_Optional_Id0], [l10].[Level2_Required_Id] AS [Level2_Required_Id0], [l10].[Name] AS [Name2], [l10].[OneToMany_Optional_Inverse3Id] AS [OneToMany_Optional_Inverse3Id0], [l10].[OneToMany_Optional_Self_Inverse3Id] AS [OneToMany_Optional_Self_Inverse3Id0], [l10].[OneToMany_Required_Inverse3Id] AS [OneToMany_Required_Inverse3Id0], [l10].[OneToMany_Required_Self_Inverse3Id] AS [OneToMany_Required_Self_Inverse3Id0], [l10].[OneToOne_Optional_PK_Inverse3Id] AS [OneToOne_Optional_PK_Inverse3Id0], [l10].[OneToOne_Optional_Self3Id] AS [OneToOne_Optional_Self3Id0]
    FROM [LevelFour] AS [l7]
    INNER JOIN [LevelThree] AS [l8] ON [l7].[Level3_Required_Id] = [l8].[Id]
    INNER JOIN [LevelTwo] AS [l9] ON [l8].[Level2_Required_Id] = [l9].[Id]
    LEFT JOIN [LevelThree] AS [l10] ON [l9].[Id] = [l10].[OneToMany_Required_Inverse3Id]
) AS [t0] ON [t].[Id2] = [t0].[Id2]
LEFT JOIN [LevelThree] AS [l11] ON [l2].[OneToMany_Optional_Inverse4Id] = [l11].[Id]
LEFT JOIN [LevelThree] AS [l12] ON [t].[Id2] = [l12].[Level2_Optional_Id]
LEFT JOIN [LevelTwo] AS [l13] ON [t0].[Level2_Optional_Id0] = [l13].[Id]
LEFT JOIN [LevelThree] AS [l14] ON [l13].[Id] = [l14].[Level2_Required_Id]
LEFT JOIN (
    SELECT [l15].[Id], [l15].[Date], [l15].[Level1_Optional_Id], [l15].[Level1_Required_Id], [l15].[Name], [l15].[OneToMany_Optional_Inverse2Id], [l15].[OneToMany_Optional_Self_Inverse2Id], [l15].[OneToMany_Required_Inverse2Id], [l15].[OneToMany_Required_Self_Inverse2Id], [l15].[OneToOne_Optional_PK_Inverse2Id], [l15].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l15]
    WHERE [l15].[Id] <> 42
) AS [t1] ON [t].[Id2] = [t1].[OneToMany_Optional_Self_Inverse2Id]
WHERE ([l11].[Name] <> N'Foo') OR [l11].[Name] IS NULL
ORDER BY [l12].[Id], [l].[Id], [t].[Id], [t].[Id0], [t1].[Id]");
        }

        public override async Task Multiple_SelectMany_with_navigation_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            await base.Multiple_SelectMany_with_navigation_and_explicit_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN (
    SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelThree] AS [l1]
    WHERE [l1].[Id] > 5
) AS [t] ON [l0].[Id] = [t].[OneToMany_Optional_Inverse3Id]
WHERE [t].[Id] IS NOT NULL");
        }

        public override async Task SelectMany_with_navigation_filter_paging_and_explicit_DefaultIfEmpty(bool isAsync)
        {
            await base.SelectMany_with_navigation_filter_paging_and_explicit_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
    FROM (
        SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], ROW_NUMBER() OVER(PARTITION BY [l0].[OneToMany_Required_Inverse2Id] ORDER BY [l0].[Id]) AS [row]
        FROM [LevelTwo] AS [l0]
        WHERE [l0].[Id] > 5
    ) AS [t]
    WHERE [t].[row] <= 3
) AS [t0] ON [l].[Id] = [t0].[OneToMany_Required_Inverse2Id]
WHERE [t0].[Id] IS NOT NULL");
        }

        public override async Task Select_join_subquery_containing_filter_and_distinct(bool isAsync)
        {
            await base.Select_join_subquery_containing_filter_and_distinct(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN (
    SELECT DISTINCT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l0]
    WHERE [l0].[Id] > 2
) AS [t] ON [l].[Id] = [t].[Level1_Optional_Id]");
        }

        public override async Task Select_join_with_key_selector_being_a_subquery(bool isAsync)
        {
            await base.Select_join_with_key_selector_being_a_subquery(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = (
    SELECT TOP(1) [l1].[Id]
    FROM [LevelTwo] AS [l1]
    ORDER BY [l1].[Id])");
        }

        public override async Task Contains_with_subquery_optional_navigation_and_constant_item(bool isAsync)
        {
            await base.Contains_with_subquery_optional_navigation_and_constant_item(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]
WHERE 1 IN (
    SELECT DISTINCT [l].[Id]
    FROM [LevelThree] AS [l]
    WHERE [l1.OneToOne_Optional_FK1].[Id] = [l].[OneToMany_Optional_Inverse3Id]
)");
        }

        public override async Task Required_navigation_on_a_subquery_with_First_in_projection(bool isAsync)
        {
            await base.Required_navigation_on_a_subquery_with_First_in_projection(isAsync);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [l0].[Name]
    FROM [LevelTwo] AS [l]
    INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
    ORDER BY [l].[Id])
FROM [LevelTwo] AS [l1]
WHERE [l1].[Id] = 7");
        }

        public override async Task Required_navigation_on_a_subquery_with_complex_projection_and_First(bool isAsync)
        {
            await base.Required_navigation_on_a_subquery_with_complex_projection_and_First(isAsync);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [l1].[Name]
    FROM [LevelTwo] AS [l]
    INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
    INNER JOIN [LevelOne] AS [l1] ON [l].[Level1_Required_Id] = [l1].[Id]
    ORDER BY [l].[Id])
FROM [LevelTwo] AS [l2]
WHERE [l2].[Id] = 7");
        }

        public override async Task Required_navigation_on_a_subquery_with_First_in_predicate(bool isAsync)
        {
            await base.Required_navigation_on_a_subquery_with_First_in_predicate(isAsync);

            AssertSql(
                @"SELECT [l2o].[Id], [l2o].[Date], [l2o].[Level1_Optional_Id], [l2o].[Level1_Required_Id], [l2o].[Name], [l2o].[OneToMany_Optional_Inverse2Id], [l2o].[OneToMany_Optional_Self_Inverse2Id], [l2o].[OneToMany_Required_Inverse2Id], [l2o].[OneToMany_Required_Self_Inverse2Id], [l2o].[OneToOne_Optional_PK_Inverse2Id], [l2o].[OneToOne_Optional_Self2Id]
FROM [LevelTwo] AS [l2o]
WHERE [l2o].[Id] = 7",
                //
                @"SELECT TOP(1) [l2i.OneToOne_Required_FK_Inverse20].[Name]
FROM [LevelTwo] AS [l2i0]
INNER JOIN [LevelOne] AS [l2i.OneToOne_Required_FK_Inverse20] ON [l2i0].[Level1_Required_Id] = [l2i.OneToOne_Required_FK_Inverse20].[Id]
ORDER BY [l2i0].[Id]");
        }

        public override async Task Manually_created_left_join_propagates_nullability_to_navigations(bool isAsync)
        {
            await base.Manually_created_left_join_propagates_nullability_to_navigations(isAsync);

            AssertSql(
                @"SELECT [l1].[Name]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
WHERE ([l1].[Name] <> N'L3 02') OR [l1].[Name] IS NULL");
        }

        public override async Task Optional_navigation_propagates_nullability_to_manually_created_left_join1(bool isAsync)
        {
            await base.Optional_navigation_propagates_nullability_to_manually_created_left_join1(isAsync);

            AssertSql(
                @"SELECT [l0].[Id] AS [Id1], [l1].[Id] AS [Id2]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]");
        }

        public override async Task Optional_navigation_propagates_nullability_to_manually_created_left_join2(bool isAsync)
        {
            await base.Optional_navigation_propagates_nullability_to_manually_created_left_join2(isAsync);

            AssertSql(
                @"SELECT [l].[Name] AS [Name1], [t].[Name0] AS [Name2]
FROM [LevelThree] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Level2_Required_Id] = [t].[Id0]");
        }

        public override async Task Null_reference_protection_complex(bool isAsync)
        {
            await base.Null_reference_protection_complex(isAsync);

            AssertSql(
                @"SELECT [t].[Name0]
FROM [LevelThree] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Level2_Required_Id] = [t].[Id0]");
        }

        public override async Task Null_reference_protection_complex_materialization(bool isAsync)
        {
            await base.Null_reference_protection_complex_materialization(isAsync);

            AssertSql(
                @"SELECT [t].[Id0], [t].[Date0], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelThree] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Level2_Required_Id] = [t].[Id0]");
        }

        public override async Task Null_reference_protection_complex_client_eval(bool isAsync)
        {
            await base.Null_reference_protection_complex_client_eval(isAsync);

            AssertSql(
                @"SELECT [t].[Name0]
FROM [LevelThree] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
) AS [t] ON [l].[Level2_Required_Id] = [t].[Id0]");
        }

        public override async Task GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened(bool isAsync)
        {
            await base.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened(isAsync);

            AssertSql(
                @"SELECT [t].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
    FROM [LevelTwo] AS [l0]
    INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
) AS [t] ON [l].[Id] = [t].[Level1_Optional_Id]");
        }

        public override async Task GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened2(bool isAsync)
        {
            await base.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened2(isAsync);

            AssertSql(
                @"SELECT [t].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
    FROM [LevelTwo] AS [l0]
    INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
) AS [t] ON [l].[Id] = [t].[Level1_Optional_Id]");
        }

        public override async Task GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened3(bool isAsync)
        {
            await base.GroupJoin_with_complex_subquery_with_joins_does_not_get_flattened3(isAsync);

            AssertSql(
                @"SELECT [t].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
) AS [t] ON [l].[Id] = [t].[Level1_Required_Id]");
        }

        public override async Task GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping1(bool isAsync)
        {
            await base.GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping1(isAsync);

            AssertSql(
                @"SELECT [l1_outer].[Id], [l1_outer].[Date], [l1_outer].[Name], [l1_outer].[OneToMany_Optional_Self_Inverse1Id], [l1_outer].[OneToMany_Required_Self_Inverse1Id], [l1_outer].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1_outer]
LEFT JOIN (
    SELECT [l2_inner].[Id], [l2_inner].[Date], [l2_inner].[Level1_Optional_Id], [l2_inner].[Level1_Required_Id], [l2_inner].[Name], [l2_inner].[OneToMany_Optional_Inverse2Id], [l2_inner].[OneToMany_Optional_Self_Inverse2Id], [l2_inner].[OneToMany_Required_Inverse2Id], [l2_inner].[OneToMany_Required_Self_Inverse2Id], [l2_inner].[OneToOne_Optional_PK_Inverse2Id], [l2_inner].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l2_inner]
    INNER JOIN [LevelOne] AS [l1_inner] ON [l2_inner].[Level1_Required_Id] = [l1_inner].[Id]
) AS [t] ON [l1_outer].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1_outer].[Id]");
        }

        public override async Task GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping2(bool isAsync)
        {
            await base.GroupJoin_with_complex_subquery_with_joins_with_reference_to_grouping2(isAsync);

            AssertSql(
                @"SELECT [l1_outer].[Id], [l1_outer].[Date], [l1_outer].[Name], [l1_outer].[OneToMany_Optional_Self_Inverse1Id], [l1_outer].[OneToMany_Required_Self_Inverse1Id], [l1_outer].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1_outer]
LEFT JOIN (
    SELECT [l2_inner].[Id], [l2_inner].[Date], [l2_inner].[Level1_Optional_Id], [l2_inner].[Level1_Required_Id], [l2_inner].[Name], [l2_inner].[OneToMany_Optional_Inverse2Id], [l2_inner].[OneToMany_Optional_Self_Inverse2Id], [l2_inner].[OneToMany_Required_Inverse2Id], [l2_inner].[OneToMany_Required_Self_Inverse2Id], [l2_inner].[OneToOne_Optional_PK_Inverse2Id], [l2_inner].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l2_inner]
    INNER JOIN [LevelOne] AS [l1_inner] ON [l2_inner].[Level1_Required_Id] = [l1_inner].[Id]
) AS [t] ON [l1_outer].[Id] = [t].[Level1_Optional_Id]
ORDER BY [l1_outer].[Id]");
        }

        public override async Task GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer(bool isAsync)
        {
            await base.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer(isAsync);

            AssertSql(
                @"@__p_0='2'

SELECT [l1].[Name]
FROM (
    SELECT TOP(@__p_0) [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    ORDER BY [l].[Id]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
ORDER BY [t].[Id]");
        }

        public override async Task GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer_with_client_method(bool isAsync)
        {
            await base.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_outer_with_client_method(isAsync);

            AssertSql(
                @"SELECT [l2_outer].[Level1_Optional_Id], [l2_outer].[Name]
FROM [LevelTwo] AS [l2_outer]",
                //
                @"@__p_0='2'

SELECT TOP(@__p_0) [l10].[Id], [l10].[Date], [l10].[Name], [l10].[OneToMany_Optional_Self_Inverse1Id], [l10].[OneToMany_Required_Self_Inverse1Id], [l10].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l10]
LEFT JOIN [LevelTwo] AS [l20] ON [l10].[Id] = [l20].[Level1_Optional_Id]
ORDER BY [l10].[Id]");
        }

        public override async Task GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_inner(bool isAsync)
        {
            await base.GroupJoin_on_a_subquery_containing_another_GroupJoin_projecting_inner(isAsync);

            AssertSql(
                @"@__p_0='2'

SELECT [l1].[Name]
FROM (
    SELECT TOP(@__p_0) [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id] AS [Id0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    ORDER BY [l].[Id]
) AS [t]
LEFT JOIN [LevelOne] AS [l1] ON [t].[Level1_Optional_Id] = [l1].[Id]
ORDER BY [t].[Id0]");
        }

        public override async Task GroupJoin_on_a_subquery_containing_another_GroupJoin_with_orderby_on_inner_sequence_projecting_inner(bool isAsync)
        {
            await base.GroupJoin_on_a_subquery_containing_another_GroupJoin_with_orderby_on_inner_sequence_projecting_inner(isAsync);

            AssertSql(
                @"@__p_0='2'

SELECT [l1].[Name]
FROM (
    SELECT TOP(@__p_0) [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [l].[Id] AS [Id0]
    FROM [LevelOne] AS [l]
    LEFT JOIN (
        SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
        FROM [LevelTwo] AS [l0]
    ) AS [t] ON [l].[Id] = [t].[Level1_Optional_Id]
    ORDER BY [l].[Id]
) AS [t0]
LEFT JOIN [LevelOne] AS [l1] ON [t0].[Level1_Optional_Id] = [l1].[Id]
ORDER BY [t0].[Id0]");
        }

        public override async Task GroupJoin_on_left_side_being_a_subquery(bool isAsync)
        {
            await base.GroupJoin_on_left_side_being_a_subquery(isAsync);

            AssertSql(
                @"@__p_0='2'

SELECT [t].[Id], [l1].[Name] AS [Brand]
FROM (
    SELECT TOP(@__p_0) [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Name] AS [Name0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    ORDER BY [l0].[Name], [l].[Id]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
ORDER BY [t].[Name0], [t].[Id]");
        }

        public override async Task GroupJoin_on_right_side_being_a_subquery(bool isAsync)
        {
            await base.GroupJoin_on_right_side_being_a_subquery(isAsync);

            AssertSql(
                @"@__p_0='2'

SELECT [l].[Id], [t].[Name]
FROM [LevelTwo] AS [l]
LEFT JOIN (
    SELECT TOP(@__p_0) [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id], [l1].[Name] AS [Name0]
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
    ORDER BY [l1].[Name]
) AS [t] ON [l].[Level1_Optional_Id] = [t].[Id]");
        }

        public override async Task GroupJoin_in_subquery_with_client_result_operator(bool isAsync)
        {
            await base.GroupJoin_in_subquery_with_client_result_operator(isAsync);

            AssertSql(
                @"SELECT [l].[Name]
FROM [LevelOne] AS [l]
WHERE ((
    SELECT COUNT(*)
    FROM (
        SELECT DISTINCT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id]
        FROM [LevelOne] AS [l0]
        LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
    ) AS [t]) > 7) AND ([l].[Id] < 3)");
        }

        public override async Task GroupJoin_in_subquery_with_client_projection(bool isAsync)
        {
            await base.GroupJoin_in_subquery_with_client_projection(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Name]
FROM [LevelOne] AS [l1]
WHERE [l1].[Id] < 3",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner0]
LEFT JOIN [LevelTwo] AS [l2_inner0] ON [l1_inner0].[Id] = [l2_inner0].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner0]
LEFT JOIN [LevelTwo] AS [l2_inner0] ON [l1_inner0].[Id] = [l2_inner0].[Level1_Optional_Id]");
        }

        public override async Task GroupJoin_in_subquery_with_client_projection_nested1(bool isAsync)
        {
            await base.GroupJoin_in_subquery_with_client_projection_nested1(isAsync);

            AssertSql(
                @"SELECT [l1_outer].[Id], [l1_outer].[Name]
FROM [LevelOne] AS [l1_outer]
WHERE [l1_outer].[Id] < 2",
                //
                @"SELECT 1
FROM [LevelOne] AS [l1_middle0]
LEFT JOIN [LevelTwo] AS [l2_middle0] ON [l1_middle0].[Id] = [l2_middle0].[Level1_Optional_Id]
ORDER BY [l1_middle0].[Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_inner2]
LEFT JOIN [LevelTwo] AS [l2_inner2] ON [l1_inner2].[Id] = [l2_inner2].[Level1_Optional_Id]");
        }

        public override async Task GroupJoin_in_subquery_with_client_projection_nested2(bool isAsync)
        {
            await base.GroupJoin_in_subquery_with_client_projection_nested2(isAsync);

            AssertSql(
                @"SELECT [l1_outer].[Id], [l1_outer].[Name]
FROM [LevelOne] AS [l1_outer]
WHERE [l1_outer].[Id] < 2",
                //
                @"SELECT COUNT(*)
FROM [LevelOne] AS [l1_middle0]
LEFT JOIN [LevelTwo] AS [l2_middle0] ON [l1_middle0].[Id] = [l2_middle0].[Level1_Optional_Id]
WHERE (
    SELECT COUNT(*)
    FROM [LevelOne] AS [l1_inner0]
    LEFT JOIN [LevelTwo] AS [l2_inner0] ON [l1_inner0].[Id] = [l2_inner0].[Level1_Optional_Id]
) > 7");
        }

        public override async Task GroupJoin_reference_to_group_in_OrderBy(bool isAsync)
        {
            await base.GroupJoin_reference_to_group_in_OrderBy(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]");
        }

        public override async Task GroupJoin_client_method_on_outer(bool isAsync)
        {
            await base.GroupJoin_client_method_on_outer(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task GroupJoin_client_method_in_OrderBy(bool isAsync)
        {
            await base.GroupJoin_client_method_in_OrderBy(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l2].[Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]");
        }

        public override async Task GroupJoin_without_DefaultIfEmpty(bool isAsync)
        {
            await base.GroupJoin_without_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task GroupJoin_with_subquery_on_inner(bool isAsync)
        {
            await base.GroupJoin_with_subquery_on_inner(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]");
        }

        public override async Task GroupJoin_with_subquery_on_inner_and_no_DefaultIfEmpty(bool isAsync)
        {
            await base.GroupJoin_with_subquery_on_inner_and_no_DefaultIfEmpty(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [l1].[Id]");
        }

        public override async Task Optional_navigation_in_subquery_with_unrelated_projection(bool isAsync)
        {
            await base.Optional_navigation_in_subquery_with_unrelated_projection(isAsync);

            AssertSql(
                @"@__p_0='15'

SELECT TOP(@__p_0) [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE ([l0].[Name] <> N'Foo') OR [l0].[Name] IS NULL
ORDER BY [l].[Id]");
        }

        public override async Task Explicit_GroupJoin_in_subquery_with_unrelated_projection(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_unrelated_projection(isAsync);

            AssertSql(
                @"@__p_0='15'

SELECT TOP(@__p_0) [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE ([l0].[Name] <> N'Foo') OR [l0].[Name] IS NULL
ORDER BY [l].[Id]");
        }

        public override async Task Explicit_GroupJoin_in_subquery_with_unrelated_projection2(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_unrelated_projection2(isAsync);

            AssertSql(
                @"SELECT [t].[Id]
FROM (
    SELECT DISTINCT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    WHERE ([l0].[Name] <> N'Foo') OR [l0].[Name] IS NULL
) AS [t]");
        }

        public override async Task Explicit_GroupJoin_in_subquery_with_unrelated_projection3(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_unrelated_projection3(isAsync);

            AssertSql(
                @"SELECT DISTINCT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE ([l0].[Name] <> N'Foo') OR [l0].[Name] IS NULL");
        }

        public override async Task Explicit_GroupJoin_in_subquery_with_unrelated_projection4(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_unrelated_projection4(isAsync);

            AssertSql(
                @"@__p_0='20'

SELECT TOP(@__p_0) [t].[Id]
FROM (
    SELECT DISTINCT [l].[Id]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    WHERE ([l0].[Name] <> N'Foo') OR [l0].[Name] IS NULL
) AS [t]
ORDER BY [t].[Id]");
        }

        public override async Task Explicit_GroupJoin_in_subquery_with_scalar_result_operator(bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_scalar_result_operator(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
WHERE (
    SELECT COUNT(*)
    FROM [LevelOne] AS [l0]
    LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]) > 4");
        }

        public override async Task Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause(
            bool isAsync)
        {
            await base.Explicit_GroupJoin_in_subquery_with_multiple_result_operator_distinct_count_materializes_main_clause(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
WHERE (
    SELECT COUNT(*)
    FROM (
        SELECT DISTINCT [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id]
        FROM [LevelOne] AS [l0]
        LEFT JOIN [LevelTwo] AS [l1] ON [l0].[Id] = [l1].[Level1_Optional_Id]
    ) AS [t]) > 4");
        }

        public override async Task Where_on_multilevel_reference_in_subquery_with_outer_projection(bool isAsync)
        {
            await base.Where_on_multilevel_reference_in_subquery_with_outer_projection(isAsync);

            AssertSql(
                @"@__p_0='0'
@__p_1='10'

SELECT [l].[Name]
FROM [LevelThree] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[OneToMany_Required_Inverse3Id] = [l0].[Id]
INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
WHERE ([l1].[Name] = N'L1 03') AND [l1].[Name] IS NOT NULL
ORDER BY [l].[Level2_Required_Id]
OFFSET @__p_0 ROWS FETCH NEXT @__p_1 ROWS ONLY");
        }

        public override async Task Join_condition_optimizations_applied_correctly_when_anonymous_type_with_single_property(bool isAsync)
        {
            await base.Join_condition_optimizations_applied_correctly_when_anonymous_type_with_single_property(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l2] ON ([l1].[OneToMany_Optional_Self_Inverse1Id] = [l2].[Level1_Optional_Id]) OR ([l1].[OneToMany_Optional_Self_Inverse1Id] IS NULL AND [l2].[Level1_Optional_Id] IS NULL)");
        }

        public override async Task Join_condition_optimizations_applied_correctly_when_anonymous_type_with_multiple_properties(bool isAsync)
        {
            await base.Join_condition_optimizations_applied_correctly_when_anonymous_type_with_multiple_properties(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON ((([l].[OneToMany_Optional_Self_Inverse1Id] = [l0].[Level1_Optional_Id]) AND ([l].[OneToMany_Optional_Self_Inverse1Id] IS NOT NULL AND [l0].[Level1_Optional_Id] IS NOT NULL)) OR ([l].[OneToMany_Optional_Self_Inverse1Id] IS NULL AND [l0].[Level1_Optional_Id] IS NULL)) AND ((([l].[OneToOne_Optional_Self1Id] = [l0].[OneToMany_Optional_Self_Inverse2Id]) AND ([l].[OneToOne_Optional_Self1Id] IS NOT NULL AND [l0].[OneToMany_Optional_Self_Inverse2Id] IS NOT NULL)) OR ([l].[OneToOne_Optional_Self1Id] IS NULL AND [l0].[OneToMany_Optional_Self_Inverse2Id] IS NULL))");
        }

        public override async Task Navigation_filter_navigation_grouping_ordering_by_group_key(bool isAsync)
        {
            await base.Navigation_filter_navigation_grouping_ordering_by_group_key(isAsync);

            AssertSql(
                @"@__level1Id_0='1'

SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l2.OneToMany_Required_Self_Inverse2].[Name]
FROM [LevelTwo] AS [l2]
INNER JOIN [LevelTwo] AS [l2.OneToMany_Required_Self_Inverse2] ON [l2].[OneToMany_Required_Self_Inverse2Id] = [l2.OneToMany_Required_Self_Inverse2].[Id]
WHERE [l2].[OneToMany_Required_Inverse2Id] = @__level1Id_0
ORDER BY [l2.OneToMany_Required_Self_Inverse2].[Name]");
        }

        public override async Task Nested_group_join_with_take(bool isAsync)
        {
            await base.Nested_group_join_with_take(isAsync);

            AssertSql(
                @"@__p_0='2'

SELECT [l1].[Name]
FROM (
    SELECT TOP(@__p_0) [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id] AS [Id0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    ORDER BY [l].[Id]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
ORDER BY [t].[Id0]");
        }

        public override async Task Navigation_with_same_navigation_compared_to_null(bool isAsync)
        {
            await base.Navigation_with_same_navigation_compared_to_null(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[OneToMany_Required_Inverse2Id] = [l0].[Id]
WHERE ([l0].[Name] <> N'L1 07') OR [l0].[Name] IS NULL");
        }

        public override async Task Multi_level_navigation_compared_to_null(bool isAsync)
        {
            await base.Multi_level_navigation_compared_to_null(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelThree] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[OneToMany_Optional_Inverse3Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
WHERE [l1].[Id] IS NOT NULL");
        }

        public override async Task Multi_level_navigation_with_same_navigation_compared_to_null(bool isAsync)
        {
            await base.Multi_level_navigation_with_same_navigation_compared_to_null(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelThree] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[OneToMany_Optional_Inverse3Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
WHERE (([l1].[Name] <> N'L1 07') OR [l1].[Name] IS NULL) AND [l1].[Id] IS NOT NULL");
        }

        public override async Task Navigations_compared_to_each_other1(bool isAsync)
        {
            await base.Navigations_compared_to_each_other1(isAsync);

            AssertSql(
                @"SELECT [l].[Name]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[OneToMany_Required_Inverse2Id] = [l0].[Id]
WHERE [l0].[Id] = [l0].[Id]");
        }

        public override async Task Navigations_compared_to_each_other2(bool isAsync)
        {
            await base.Navigations_compared_to_each_other2(isAsync);

            AssertSql(
                @"SELECT [l].[Name]
FROM [LevelTwo] AS [l]
INNER JOIN [LevelOne] AS [l0] ON [l].[OneToMany_Required_Inverse2Id] = [l0].[Id]
LEFT JOIN [LevelOne] AS [l1] ON [l].[OneToOne_Optional_PK_Inverse2Id] = [l1].[Id]
WHERE ([l0].[Id] = [l1].[Id]) AND [l1].[Id] IS NOT NULL");
        }

        public override async Task Navigations_compared_to_each_other3(bool isAsync)
        {
            await base.Navigations_compared_to_each_other3(isAsync);

            AssertSql(
                @"SELECT [l].[Name]
FROM [LevelTwo] AS [l]
WHERE EXISTS (
    SELECT 1
    FROM [LevelThree] AS [l0]
    WHERE ([l].[Id] = [l0].[OneToMany_Optional_Inverse3Id]) AND [l0].[OneToMany_Optional_Inverse3Id] IS NOT NULL)");
        }

        public override async Task Navigations_compared_to_each_other4(bool isAsync)
        {
            await base.Navigations_compared_to_each_other4(isAsync);

            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelTwo] AS [l2]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Required_FK2] ON [l2].[Id] = [l2.OneToOne_Required_FK2].[Level2_Required_Id]
WHERE EXISTS (
    SELECT 1
    FROM [LevelFour] AS [l]
    LEFT JOIN [LevelThree] AS [i.OneToOne_Optional_PK_Inverse4] ON [l].[OneToOne_Optional_PK_Inverse4Id] = [i.OneToOne_Optional_PK_Inverse4].[Id]
    WHERE [l2.OneToOne_Required_FK2].[Id] = [l].[OneToMany_Optional_Inverse4Id])");
        }

        public override async Task Navigations_compared_to_each_other5(bool isAsync)
        {
            await base.Navigations_compared_to_each_other5(isAsync);

            AssertSql(
                @"SELECT [l2].[Name]
FROM [LevelTwo] AS [l2]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Required_FK2] ON [l2].[Id] = [l2.OneToOne_Required_FK2].[Level2_Required_Id]
LEFT JOIN [LevelThree] AS [l2.OneToOne_Required_FK2.OneToOne_Optional_PK2] ON [l2].[Id] = [l2.OneToOne_Required_FK2.OneToOne_Optional_PK2].[OneToOne_Optional_PK_Inverse3Id]
WHERE EXISTS (
    SELECT 1
    FROM [LevelFour] AS [l]
    LEFT JOIN [LevelThree] AS [i.OneToOne_Optional_PK_Inverse4] ON [l].[OneToOne_Optional_PK_Inverse4Id] = [i.OneToOne_Optional_PK_Inverse4].[Id]
    WHERE [l2.OneToOne_Required_FK2].[Id] = [l].[OneToMany_Optional_Inverse4Id])");
        }

        public override async Task Level4_Include(bool isAsync)
        {
            await base.Level4_Include(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse3Id], [l0].[OneToMany_Optional_Self_Inverse3Id], [l0].[OneToMany_Required_Inverse3Id], [l0].[OneToMany_Required_Self_Inverse3Id], [l0].[OneToOne_Optional_PK_Inverse3Id], [l0].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l2] ON [l1].[Id] = [l2].[Id]
LEFT JOIN [LevelThree] AS [l3] ON [l2].[Id] = [l3].[Id]
LEFT JOIN [LevelFour] AS [l4] ON [l3].[Id] = [l4].[Id]
LEFT JOIN [LevelThree] AS [l5] ON [l4].[Level3_Required_Id] = [l5].[Id]
LEFT JOIN [LevelTwo] AS [l] ON [l5].[Level2_Required_Id] = [l].[Id]
LEFT JOIN [LevelThree] AS [l0] ON [l].[Id] = [l0].[Level2_Optional_Id]
WHERE ([l2].[Id] IS NOT NULL AND [l3].[Id] IS NOT NULL) AND [l4].[Id] IS NOT NULL");
        }

        public override async Task Comparing_collection_navigation_on_optional_reference_to_null(bool isAsync)
        {
            await base.Comparing_collection_navigation_on_optional_reference_to_null(isAsync);

            AssertSql(
                @"SELECT [l].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
WHERE [l0].[Id] IS NULL");
        }

        public override async Task Select_subquery_with_client_eval_and_navigation1(bool isAsync)
        {
            await base.Select_subquery_with_client_eval_and_navigation1(isAsync);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [l0].[Name]
    FROM [LevelTwo] AS [l]
    INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
    ORDER BY [l].[Id])
FROM [LevelTwo] AS [l1]");
        }

        public override async Task Select_subquery_with_client_eval_and_navigation2(bool isAsync)
        {
            await base.Select_subquery_with_client_eval_and_navigation2(isAsync);

            AssertSql(
                @"SELECT CASE
    WHEN ((
        SELECT TOP(1) [l0].[Name]
        FROM [LevelTwo] AS [l]
        INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
        ORDER BY [l].[Id]) = N'L1 02') AND (
        SELECT TOP(1) [l0].[Name]
        FROM [LevelTwo] AS [l]
        INNER JOIN [LevelOne] AS [l0] ON [l].[Level1_Required_Id] = [l0].[Id]
        ORDER BY [l].[Id]) IS NOT NULL THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END
FROM [LevelTwo] AS [l1]");
        }

        public override async Task Select_subquery_with_client_eval_and_multi_level_navigation(bool isAsync)
        {
            await base.Select_subquery_with_client_eval_and_multi_level_navigation(isAsync);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [l1].[Name]
    FROM [LevelThree] AS [l]
    INNER JOIN [LevelTwo] AS [l0] ON [l].[Level2_Required_Id] = [l0].[Id]
    INNER JOIN [LevelOne] AS [l1] ON [l0].[Level1_Required_Id] = [l1].[Id]
    ORDER BY [l].[Id])
FROM [LevelThree] AS [l2]");
        }

        public override async Task Member_doesnt_get_pushed_down_into_subquery_with_result_operator(bool isAsync)
        {
            await base.Member_doesnt_get_pushed_down_into_subquery_with_result_operator(isAsync);

            AssertSql(
                @"SELECT (
    SELECT [t].[Name]
    FROM (
        SELECT DISTINCT [l].[Id], [l].[Level2_Optional_Id], [l].[Level2_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse3Id], [l].[OneToMany_Optional_Self_Inverse3Id], [l].[OneToMany_Required_Inverse3Id], [l].[OneToMany_Required_Self_Inverse3Id], [l].[OneToOne_Optional_PK_Inverse3Id], [l].[OneToOne_Optional_Self3Id]
        FROM [LevelThree] AS [l]
    ) AS [t]
    ORDER BY [t].[Id]
    OFFSET 1 ROWS FETCH NEXT 1 ROWS ONLY)
FROM [LevelOne] AS [l0]
WHERE [l0].[Id] < 3");
        }

        public override async Task Subquery_with_Distinct_Skip_FirstOrDefault_without_OrderBy(bool isAsync)
        {
            await base.Subquery_with_Distinct_Skip_FirstOrDefault_without_OrderBy(isAsync);

            AssertSql(
                "");
        }

        public override async Task Project_collection_navigation(bool isAsync)
        {
            await base.Project_collection_navigation(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l0].[Id]");
        }

        public override async Task Project_collection_navigation_nested(bool isAsync)
        {
            await base.Project_collection_navigation_nested(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l1].[Id]");
        }

        public override async Task Project_collection_navigation_using_ef_property(bool isAsync)
        {
            await base.Project_collection_navigation_using_ef_property(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l1].[Id]");
        }

        public override async Task Project_collection_navigation_nested_anonymous(bool isAsync)
        {
            await base.Project_collection_navigation_nested_anonymous(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l1].[Id]");
        }

        public override async Task Project_collection_navigation_count(bool isAsync)
        {
            await base.Project_collection_navigation_count(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], (
    SELECT COUNT(*)
    FROM [LevelThree] AS [l]
    WHERE [l1.OneToOne_Optional_FK1].[Id] = [l].[OneToMany_Optional_Inverse3Id]
) AS [Count]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]");
        }

        public override async Task Project_collection_navigation_composed(bool isAsync)
        {
            await base.Project_collection_navigation_composed(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l0]
    WHERE ([l0].[Name] <> N'Foo') OR [l0].[Name] IS NULL
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
WHERE [l].[Id] < 3
ORDER BY [l].[Id], [t].[Id]");
        }

        public override async Task Project_collection_and_root_entity(bool isAsync)
        {
            await base.Project_collection_and_root_entity(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l0].[Id]");
        }

        public override async Task Project_collection_and_include(bool isAsync)
        {
            await base.Project_collection_and_include(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l0].[Id], [l1].[Id]");
        }

        public override async Task Project_navigation_and_collection(bool isAsync)
        {
            await base.Project_navigation_and_collection(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l1].[Id]");
        }

        public override async Task Include_inside_subquery(bool isAsync)
        {
            await base.Include_inside_subquery(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
OUTER APPLY (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
    WHERE [l0].[Id] > 0
) AS [t]
WHERE [l].[Id] < 3
ORDER BY [l].[Id], [t].[Id], [t].[Id0]");
        }

        public override async Task Select_optional_navigation_property_string_concat(bool isAsync)
        {
            await base.Select_optional_navigation_property_string_concat(isAsync);

            AssertSql(
                @"SELECT ([l].[Name] + N' ') + CASE
    WHEN [t].[Id] IS NOT NULL THEN [t].[Name]
    ELSE N'NULL'
END
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l0]
    WHERE [l0].[Id] > 5
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]");
        }

        public override async Task Include_collection_with_multiple_orderbys_member(bool isAsync)
        {
            await base.Include_collection_with_multiple_orderbys_member(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse3Id], [l0].[OneToMany_Optional_Self_Inverse3Id], [l0].[OneToMany_Required_Inverse3Id], [l0].[OneToMany_Required_Self_Inverse3Id], [l0].[OneToOne_Optional_PK_Inverse3Id], [l0].[OneToOne_Optional_Self3Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelThree] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Name], [l].[Level1_Required_Id], [l].[Id], [l0].[Id]");
        }

        public override async Task Include_collection_with_multiple_orderbys_property(bool isAsync)
        {
            await base.Include_collection_with_multiple_orderbys_property(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse3Id], [l0].[OneToMany_Optional_Self_Inverse3Id], [l0].[OneToMany_Required_Inverse3Id], [l0].[OneToMany_Required_Self_Inverse3Id], [l0].[OneToOne_Optional_PK_Inverse3Id], [l0].[OneToOne_Optional_Self3Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelThree] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Level1_Required_Id], [l].[Name], [l].[Id], [l0].[Id]");
        }

        public override async Task Include_collection_with_multiple_orderbys_methodcall(bool isAsync)
        {
            await base.Include_collection_with_multiple_orderbys_methodcall(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse3Id], [l0].[OneToMany_Optional_Self_Inverse3Id], [l0].[OneToMany_Required_Inverse3Id], [l0].[OneToMany_Required_Self_Inverse3Id], [l0].[OneToOne_Optional_PK_Inverse3Id], [l0].[OneToOne_Optional_Self3Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelThree] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse3Id]
ORDER BY ABS([l].[Level1_Required_Id]), [l].[Name], [l].[Id], [l0].[Id]");
        }

        public override async Task Include_collection_with_multiple_orderbys_complex(bool isAsync)
        {
            await base.Include_collection_with_multiple_orderbys_complex(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse3Id], [l0].[OneToMany_Optional_Self_Inverse3Id], [l0].[OneToMany_Required_Inverse3Id], [l0].[OneToMany_Required_Self_Inverse3Id], [l0].[OneToOne_Optional_PK_Inverse3Id], [l0].[OneToOne_Optional_Self3Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelThree] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse3Id]
ORDER BY ABS([l].[Level1_Required_Id]) + 7, [l].[Name], [l].[Id], [l0].[Id]");
        }

        public override async Task Include_collection_with_multiple_orderbys_complex_repeated(bool isAsync)
        {
            await base.Include_collection_with_multiple_orderbys_complex_repeated(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Level2_Optional_Id], [l0].[Level2_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse3Id], [l0].[OneToMany_Optional_Self_Inverse3Id], [l0].[OneToMany_Required_Inverse3Id], [l0].[OneToMany_Required_Self_Inverse3Id], [l0].[OneToOne_Optional_PK_Inverse3Id], [l0].[OneToOne_Optional_Self3Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelThree] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse3Id]
ORDER BY -[l].[Level1_Required_Id], [l].[Name], [l].[Id], [l0].[Id]");
        }

        public override async Task String_include_multiple_derived_navigation_with_same_name_and_same_type(bool isAsync)
        {
            await base.String_include_multiple_derived_navigation_with_same_name_and_same_type(isAsync);

            AssertSql(
                @"SELECT [i].[Id], [i].[Discriminator], [i].[InheritanceBase2Id], [i].[InheritanceBase2Id1], [i].[Name], [i0].[Id], [i0].[DifferentTypeReference_InheritanceDerived1Id], [i0].[InheritanceDerived1Id], [i0].[InheritanceDerived1Id1], [i0].[InheritanceDerived2Id], [i0].[Name], [i0].[SameTypeReference_InheritanceDerived1Id], [i0].[SameTypeReference_InheritanceDerived2Id], [i1].[Id], [i1].[DifferentTypeReference_InheritanceDerived1Id], [i1].[InheritanceDerived1Id], [i1].[InheritanceDerived1Id1], [i1].[InheritanceDerived2Id], [i1].[Name], [i1].[SameTypeReference_InheritanceDerived1Id], [i1].[SameTypeReference_InheritanceDerived2Id]
FROM [InheritanceOne] AS [i]
LEFT JOIN [InheritanceLeafOne] AS [i0] ON [i].[Id] = [i0].[SameTypeReference_InheritanceDerived1Id]
LEFT JOIN [InheritanceLeafOne] AS [i1] ON [i].[Id] = [i1].[SameTypeReference_InheritanceDerived2Id]
WHERE [i].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')");
        }

        public override async Task String_include_multiple_derived_navigation_with_same_name_and_different_type(bool isAsync)
        {
            await base.String_include_multiple_derived_navigation_with_same_name_and_different_type(isAsync);

            AssertSql(
                @"SELECT [i].[Id], [i].[Discriminator], [i].[InheritanceBase2Id], [i].[InheritanceBase2Id1], [i].[Name], [i0].[Id], [i0].[DifferentTypeReference_InheritanceDerived1Id], [i0].[InheritanceDerived1Id], [i0].[InheritanceDerived1Id1], [i0].[InheritanceDerived2Id], [i0].[Name], [i0].[SameTypeReference_InheritanceDerived1Id], [i0].[SameTypeReference_InheritanceDerived2Id], [i1].[Id], [i1].[DifferentTypeReference_InheritanceDerived2Id], [i1].[InheritanceDerived2Id], [i1].[Name]
FROM [InheritanceOne] AS [i]
LEFT JOIN [InheritanceLeafOne] AS [i0] ON [i].[Id] = [i0].[DifferentTypeReference_InheritanceDerived1Id]
LEFT JOIN [InheritanceLeafTwo] AS [i1] ON [i].[Id] = [i1].[DifferentTypeReference_InheritanceDerived2Id]
WHERE [i].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')");
        }

        public override async Task String_include_multiple_derived_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(bool isAsync)
        {
            await base.String_include_multiple_derived_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(isAsync);

            AssertSql(
                @"SELECT [i].[Id], [i].[Discriminator], [i].[InheritanceBase2Id], [i].[InheritanceBase2Id1], [i].[Name], [i0].[Id], [i0].[DifferentTypeReference_InheritanceDerived1Id], [i0].[InheritanceDerived1Id], [i0].[InheritanceDerived1Id1], [i0].[InheritanceDerived2Id], [i0].[Name], [i0].[SameTypeReference_InheritanceDerived1Id], [i0].[SameTypeReference_InheritanceDerived2Id], [i1].[Id], [i1].[DifferentTypeReference_InheritanceDerived2Id], [i1].[InheritanceDerived2Id], [i1].[Name], [i2].[Id], [i2].[InheritanceLeaf2Id], [i2].[Name]
FROM [InheritanceOne] AS [i]
LEFT JOIN [InheritanceLeafOne] AS [i0] ON [i].[Id] = [i0].[DifferentTypeReference_InheritanceDerived1Id]
LEFT JOIN [InheritanceLeafTwo] AS [i1] ON [i].[Id] = [i1].[DifferentTypeReference_InheritanceDerived2Id]
LEFT JOIN [InheritanceTwo] AS [i2] ON [i1].[Id] = [i2].[InheritanceLeaf2Id]
WHERE [i].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')
ORDER BY [i].[Id], [i2].[Id]");
        }

        public override async Task String_include_multiple_derived_collection_navigation_with_same_name_and_same_type(bool isAsync)
        {
            await base.String_include_multiple_derived_collection_navigation_with_same_name_and_same_type(isAsync);

            AssertSql(
                @"SELECT [i].[Id], [i].[Discriminator], [i].[InheritanceBase2Id], [i].[InheritanceBase2Id1], [i].[Name], [i0].[Id], [i0].[DifferentTypeReference_InheritanceDerived1Id], [i0].[InheritanceDerived1Id], [i0].[InheritanceDerived1Id1], [i0].[InheritanceDerived2Id], [i0].[Name], [i0].[SameTypeReference_InheritanceDerived1Id], [i0].[SameTypeReference_InheritanceDerived2Id], [i1].[Id], [i1].[DifferentTypeReference_InheritanceDerived1Id], [i1].[InheritanceDerived1Id], [i1].[InheritanceDerived1Id1], [i1].[InheritanceDerived2Id], [i1].[Name], [i1].[SameTypeReference_InheritanceDerived1Id], [i1].[SameTypeReference_InheritanceDerived2Id]
FROM [InheritanceOne] AS [i]
LEFT JOIN [InheritanceLeafOne] AS [i0] ON [i].[Id] = [i0].[InheritanceDerived1Id1]
LEFT JOIN [InheritanceLeafOne] AS [i1] ON [i].[Id] = [i1].[InheritanceDerived2Id]
WHERE [i].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')
ORDER BY [i].[Id], [i0].[Id], [i1].[Id]");
        }

        public override async Task String_include_multiple_derived_collection_navigation_with_same_name_and_different_type(bool isAsync)
        {
            await base.String_include_multiple_derived_collection_navigation_with_same_name_and_different_type(isAsync);

            AssertSql(
                @"SELECT [i].[Id], [i].[Discriminator], [i].[InheritanceBase2Id], [i].[InheritanceBase2Id1], [i].[Name], [i0].[Id], [i0].[DifferentTypeReference_InheritanceDerived1Id], [i0].[InheritanceDerived1Id], [i0].[InheritanceDerived1Id1], [i0].[InheritanceDerived2Id], [i0].[Name], [i0].[SameTypeReference_InheritanceDerived1Id], [i0].[SameTypeReference_InheritanceDerived2Id], [i1].[Id], [i1].[DifferentTypeReference_InheritanceDerived2Id], [i1].[InheritanceDerived2Id], [i1].[Name]
FROM [InheritanceOne] AS [i]
LEFT JOIN [InheritanceLeafOne] AS [i0] ON [i].[Id] = [i0].[InheritanceDerived1Id]
LEFT JOIN [InheritanceLeafTwo] AS [i1] ON [i].[Id] = [i1].[InheritanceDerived2Id]
WHERE [i].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')
ORDER BY [i].[Id], [i0].[Id], [i1].[Id]");
        }

        public override async Task String_include_multiple_derived_collection_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(bool isAsync)
        {
            await base.String_include_multiple_derived_collection_navigation_with_same_name_and_different_type_nested_also_includes_partially_matching_navigation_chains(isAsync);

            AssertSql(
                @"SELECT [i].[Id], [i].[Discriminator], [i].[InheritanceBase2Id], [i].[InheritanceBase2Id1], [i].[Name], [i0].[Id], [i0].[DifferentTypeReference_InheritanceDerived1Id], [i0].[InheritanceDerived1Id], [i0].[InheritanceDerived1Id1], [i0].[InheritanceDerived2Id], [i0].[Name], [i0].[SameTypeReference_InheritanceDerived1Id], [i0].[SameTypeReference_InheritanceDerived2Id], [t].[Id], [t].[DifferentTypeReference_InheritanceDerived2Id], [t].[InheritanceDerived2Id], [t].[Name], [t].[Id0], [t].[InheritanceLeaf2Id], [t].[Name0]
FROM [InheritanceOne] AS [i]
LEFT JOIN [InheritanceLeafOne] AS [i0] ON [i].[Id] = [i0].[InheritanceDerived1Id]
LEFT JOIN (
    SELECT [i1].[Id], [i1].[DifferentTypeReference_InheritanceDerived2Id], [i1].[InheritanceDerived2Id], [i1].[Name], [i2].[Id] AS [Id0], [i2].[InheritanceLeaf2Id], [i2].[Name] AS [Name0]
    FROM [InheritanceLeafTwo] AS [i1]
    LEFT JOIN [InheritanceTwo] AS [i2] ON [i1].[Id] = [i2].[InheritanceLeaf2Id]
) AS [t] ON [i].[Id] = [t].[InheritanceDerived2Id]
WHERE [i].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')
ORDER BY [i].[Id], [i0].[Id], [t].[Id], [t].[Id0]");
        }

        public override async Task String_include_multiple_derived_navigations_complex(bool isAsync)
        {
            await base.String_include_multiple_derived_navigations_complex(isAsync);

            AssertSql(
                @"SELECT [i].[Id], [i].[InheritanceLeaf2Id], [i].[Name], [t].[Id], [t].[Discriminator], [t].[InheritanceBase2Id], [t].[InheritanceBase2Id1], [t].[Name], [i1].[Id], [i1].[DifferentTypeReference_InheritanceDerived1Id], [i1].[InheritanceDerived1Id], [i1].[InheritanceDerived1Id1], [i1].[InheritanceDerived2Id], [i1].[Name], [i1].[SameTypeReference_InheritanceDerived1Id], [i1].[SameTypeReference_InheritanceDerived2Id], [i2].[Id], [i2].[DifferentTypeReference_InheritanceDerived2Id], [i2].[InheritanceDerived2Id], [i2].[Name], [t0].[Id], [t0].[Discriminator], [t0].[InheritanceBase2Id], [t0].[InheritanceBase2Id1], [t0].[Name], [t0].[Id0], [t0].[DifferentTypeReference_InheritanceDerived1Id], [t0].[InheritanceDerived1Id], [t0].[InheritanceDerived1Id1], [t0].[InheritanceDerived2Id], [t0].[Name0], [t0].[SameTypeReference_InheritanceDerived1Id], [t0].[SameTypeReference_InheritanceDerived2Id], [t0].[Id1], [t0].[DifferentTypeReference_InheritanceDerived1Id0], [t0].[InheritanceDerived1Id0], [t0].[InheritanceDerived1Id10], [t0].[InheritanceDerived2Id0], [t0].[Name1], [t0].[SameTypeReference_InheritanceDerived1Id0], [t0].[SameTypeReference_InheritanceDerived2Id0]
FROM [InheritanceTwo] AS [i]
LEFT JOIN (
    SELECT [i0].[Id], [i0].[Discriminator], [i0].[InheritanceBase2Id], [i0].[InheritanceBase2Id1], [i0].[Name]
    FROM [InheritanceOne] AS [i0]
    WHERE [i0].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')
) AS [t] ON [i].[Id] = [t].[InheritanceBase2Id]
LEFT JOIN [InheritanceLeafOne] AS [i1] ON [t].[Id] = [i1].[InheritanceDerived1Id]
LEFT JOIN [InheritanceLeafTwo] AS [i2] ON [t].[Id] = [i2].[InheritanceDerived2Id]
LEFT JOIN (
    SELECT [i3].[Id], [i3].[Discriminator], [i3].[InheritanceBase2Id], [i3].[InheritanceBase2Id1], [i3].[Name], [i4].[Id] AS [Id0], [i4].[DifferentTypeReference_InheritanceDerived1Id], [i4].[InheritanceDerived1Id], [i4].[InheritanceDerived1Id1], [i4].[InheritanceDerived2Id], [i4].[Name] AS [Name0], [i4].[SameTypeReference_InheritanceDerived1Id], [i4].[SameTypeReference_InheritanceDerived2Id], [i5].[Id] AS [Id1], [i5].[DifferentTypeReference_InheritanceDerived1Id] AS [DifferentTypeReference_InheritanceDerived1Id0], [i5].[InheritanceDerived1Id] AS [InheritanceDerived1Id0], [i5].[InheritanceDerived1Id1] AS [InheritanceDerived1Id10], [i5].[InheritanceDerived2Id] AS [InheritanceDerived2Id0], [i5].[Name] AS [Name1], [i5].[SameTypeReference_InheritanceDerived1Id] AS [SameTypeReference_InheritanceDerived1Id0], [i5].[SameTypeReference_InheritanceDerived2Id] AS [SameTypeReference_InheritanceDerived2Id0]
    FROM [InheritanceOne] AS [i3]
    LEFT JOIN [InheritanceLeafOne] AS [i4] ON [i3].[Id] = [i4].[SameTypeReference_InheritanceDerived1Id]
    LEFT JOIN [InheritanceLeafOne] AS [i5] ON [i3].[Id] = [i5].[SameTypeReference_InheritanceDerived2Id]
    WHERE [i3].[Discriminator] IN (N'InheritanceBase1', N'InheritanceDerived1', N'InheritanceDerived2')
) AS [t0] ON [i].[Id] = [t0].[InheritanceBase2Id1]
ORDER BY [i].[Id], [i1].[Id], [i2].[Id], [t0].[Id]");
        }

        public override async Task Include_reference_collection_order_by_reference_navigation(bool isAsync)
        {
            await base.Include_reference_collection_order_by_reference_navigation(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l0].[Id], [l].[Id], [l1].[Id]");
        }

        public override async Task Nav_rewrite_doesnt_apply_null_protection_for_function_arguments(bool isAsync)
        {
            await base.Nav_rewrite_doesnt_apply_null_protection_for_function_arguments(isAsync);

            AssertSql(
                @"SELECT [l0].[Level1_Required_Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToOne_Optional_PK_Inverse2Id]
WHERE [l0].[Id] IS NOT NULL");
        }

        public override async Task Accessing_optional_property_inside_result_operator_subquery(bool isAsync)
        {
            await base.Accessing_optional_property_inside_result_operator_subquery(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [l1.OneToOne_Optional_FK1].[Name]
FROM [LevelOne] AS [l1]
LEFT JOIN [LevelTwo] AS [l1.OneToOne_Optional_FK1] ON [l1].[Id] = [l1.OneToOne_Optional_FK1].[Level1_Optional_Id]");
        }

        public override async Task Include_after_SelectMany_and_reference_navigation(bool isAsync)
        {
            await base.Include_after_SelectMany_and_reference_navigation(isAsync);

            AssertSql(
                @"SELECT [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Level2_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Level2_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Optional_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Optional_Self_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Required_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Required_Self_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToOne_Optional_PK_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Required1] ON [l1].[Id] = [l1.OneToMany_Required1].[OneToMany_Required_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2] ON [l1.OneToMany_Required1].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Level2_Optional_Id]
ORDER BY [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Id]",
                //
                @"SELECT [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Level3_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToOne_Optional_Self4Id]
FROM [LevelFour] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3]
INNER JOIN (
    SELECT DISTINCT [l1.OneToMany_Required1.OneToOne_Optional_FK20].[Id]
    FROM [LevelOne] AS [l10]
    INNER JOIN [LevelTwo] AS [l1.OneToMany_Required10] ON [l10].[Id] = [l1.OneToMany_Required10].[OneToMany_Required_Inverse2Id]
    LEFT JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK20] ON [l1.OneToMany_Required10].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK20].[Level2_Optional_Id]
) AS [t] ON [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id] = [t].[Id]
ORDER BY [t].[Id]");
        }

        public override async Task Include_after_multiple_SelectMany_and_reference_navigation(bool isAsync)
        {
            await base.Include_after_multiple_SelectMany_and_reference_navigation(isAsync);

            AssertSql(
                @"SELECT [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[Level3_Required_Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[Name], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Required1] ON [l1].[Id] = [l1.OneToMany_Required1].[OneToMany_Required_Inverse2Id]
INNER JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToMany_Optional2] ON [l1.OneToMany_Required1].[Id] = [l1.OneToMany_Required1.OneToMany_Optional2].[OneToMany_Optional_Inverse3Id]
LEFT JOIN [LevelFour] AS [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3] ON [l1.OneToMany_Required1.OneToMany_Optional2].[Id] = [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[Level3_Required_Id]
ORDER BY [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3].[Id]",
                //
                @"SELECT [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[Level3_Required_Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[Name], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[OneToOne_Optional_Self4Id]
FROM [LevelFour] AS [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4]
INNER JOIN (
    SELECT DISTINCT [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK30].[Id]
    FROM [LevelOne] AS [l10]
    INNER JOIN [LevelTwo] AS [l1.OneToMany_Required10] ON [l10].[Id] = [l1.OneToMany_Required10].[OneToMany_Required_Inverse2Id]
    INNER JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToMany_Optional20] ON [l1.OneToMany_Required10].[Id] = [l1.OneToMany_Required1.OneToMany_Optional20].[OneToMany_Optional_Inverse3Id]
    LEFT JOIN [LevelFour] AS [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK30] ON [l1.OneToMany_Required1.OneToMany_Optional20].[Id] = [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK30].[Level3_Required_Id]
) AS [t] ON [l1.OneToMany_Required1.OneToMany_Optional2.OneToOne_Required_FK3.OneToMany_Required_Self4].[OneToMany_Required_Self_Inverse4Id] = [t].[Id]
ORDER BY [t].[Id]");
        }

        public override async Task Include_after_SelectMany_and_multiple_reference_navigations(bool isAsync)
        {
            await base.Include_after_SelectMany_and_multiple_reference_navigations(isAsync);

            AssertSql(
                @"SELECT [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[Level3_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Required1] ON [l1].[Id] = [l1.OneToMany_Required1].[OneToMany_Required_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2] ON [l1.OneToMany_Required1].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3] ON [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[Level3_Required_Id]
ORDER BY [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3].[Id]",
                //
                @"SELECT [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[Level3_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[OneToOne_Optional_Self4Id]
FROM [LevelFour] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4]
INNER JOIN (
    SELECT DISTINCT [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK30].[Id]
    FROM [LevelOne] AS [l10]
    INNER JOIN [LevelTwo] AS [l1.OneToMany_Required10] ON [l10].[Id] = [l1.OneToMany_Required10].[OneToMany_Required_Inverse2Id]
    LEFT JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK20] ON [l1.OneToMany_Required10].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK20].[Level2_Optional_Id]
    LEFT JOIN [LevelFour] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK30] ON [l1.OneToMany_Required1.OneToOne_Optional_FK20].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK30].[Level3_Required_Id]
) AS [t] ON [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToOne_Required_FK3.OneToMany_Optional_Self4].[OneToMany_Optional_Self_Inverse4Id] = [t].[Id]
ORDER BY [t].[Id]");
        }

        public override async Task Include_after_SelectMany_and_reference_navigation_with_another_SelectMany_with_Distinct(bool isAsync)
        {
            await base.Include_after_SelectMany_and_reference_navigation_with_another_SelectMany_with_Distinct(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Name], [l1].[OneToMany_Optional_Self_Inverse1Id], [l1].[OneToMany_Required_Self_Inverse1Id], [l1].[OneToOne_Optional_Self1Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Level2_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Level2_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Optional_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Optional_Self_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Required_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToMany_Required_Self_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToOne_Optional_PK_Inverse3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2].[OneToOne_Optional_Self3Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Level3_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l1]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Required1] ON [l1].[Id] = [l1.OneToMany_Required1].[OneToMany_Required_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2] ON [l1.OneToMany_Required1].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Level2_Optional_Id]
INNER JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK20] ON [l1.OneToMany_Required1].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK20].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3] ON [l1.OneToMany_Required1.OneToOne_Optional_FK20].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional3].[OneToMany_Optional_Inverse4Id]
ORDER BY [l1.OneToMany_Required1.OneToOne_Optional_FK2].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK20].[Id]",
                //
                @"SELECT [l11].[Id], [l11].[Date], [l11].[Name], [l11].[OneToMany_Optional_Self_Inverse1Id], [l11].[OneToMany_Required_Self_Inverse1Id], [l11].[OneToOne_Optional_Self1Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[Level3_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[OneToOne_Optional_Self4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK23].[Id]
FROM [LevelOne] AS [l11]
INNER JOIN [LevelTwo] AS [l1.OneToMany_Required11] ON [l11].[Id] = [l1.OneToMany_Required11].[OneToMany_Required_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK23] ON [l1.OneToMany_Required11].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK23].[Level2_Optional_Id]
INNER JOIN [LevelThree] AS [l1.OneToMany_Required1.OneToOne_Optional_FK24] ON [l1.OneToMany_Required11].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK24].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32] ON [l1.OneToMany_Required1.OneToOne_Optional_FK24].[Id] = [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional32].[OneToMany_Optional_Inverse4Id]
ORDER BY [l1.OneToMany_Required1.OneToOne_Optional_FK24].[Id]",
                //
                @"SELECT [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[Level3_Optional_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[Level3_Required_Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[Name], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[OneToMany_Optional_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[OneToMany_Optional_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[OneToMany_Required_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[OneToMany_Required_Self_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[OneToOne_Optional_PK_Inverse4Id], [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30].[OneToOne_Optional_Self4Id]
FROM [LevelFour] AS [l1.OneToMany_Required1.OneToOne_Optional_FK2.OneToMany_Optional30]");
        }

        public override async Task Null_check_in_anonymous_type_projection_should_not_be_removed(bool isAsync)
        {
            await base.Null_check_in_anonymous_type_projection_should_not_be_removed(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [t].[c], [t].[Name], [t].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT CASE
        WHEN [l1].[Id] IS NULL THEN CAST(1 AS bit)
        ELSE CAST(0 AS bit)
    END AS [c], [l1].[Name], [l0].[Id], [l0].[OneToMany_Optional_Inverse2Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id]");
        }

        public override async Task Null_check_in_Dto_projection_should_not_be_removed(bool isAsync)
        {
            await base.Null_check_in_Dto_projection_should_not_be_removed(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [t].[c], [t].[Name], [t].[Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT CASE
        WHEN [l1].[Id] IS NULL THEN CAST(1 AS bit)
        ELSE CAST(0 AS bit)
    END AS [c], [l1].[Name], [l0].[Id], [l0].[OneToMany_Optional_Inverse2Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Required_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id]");
        }

        public override async Task SelectMany_navigation_property_followed_by_select_collection_navigation(bool isAsync)
        {
            await base.SelectMany_navigation_property_followed_by_select_collection_navigation(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l0].[Id], [l1].[Id]");
        }

        public override async Task Multiple_SelectMany_navigation_property_followed_by_select_collection_navigation(bool isAsync)
        {
            await base.Multiple_SelectMany_navigation_property_followed_by_select_collection_navigation(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l].[Id], [l0].[Id], [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
INNER JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[OneToMany_Optional_Inverse4Id]
ORDER BY [l].[Id], [l0].[Id], [l1].[Id], [l2].[Id]");
        }

        public override async Task SelectMany_navigation_property_with_include_and_followed_by_select_collection_navigation(bool isAsync)
        {
            await base.SelectMany_navigation_property_with_include_and_followed_by_select_collection_navigation(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l].[Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Required_Inverse3Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l0].[Id], [l1].[Id], [l2].[Id]");
        }

        public override async Task Include1(bool isAsync)
        {
            await base.Include1(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Include2(bool isAsync)
        {
            await base.Include2(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Include3(bool isAsync)
        {
            await base.Include3(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[OneToOne_Optional_PK_Inverse2Id]");
        }

        public override async Task Include4(bool isAsync)
        {
            await base.Include4(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]");
        }

        public override async Task Include5(bool isAsync)
        {
            await base.Include5(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]");
        }

        public override async Task Include6(bool isAsync)
        {
            await base.Include6(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]");
        }

        public override async Task Include7(bool isAsync)
        {
            await base.Include7(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToOne_Optional_PK_Inverse2Id]");
        }

        public override async Task Include8(bool isAsync)
        {
            await base.Include8(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelOne] AS [l0] ON [l].[Level1_Optional_Id] = [l0].[Id]
WHERE ([l0].[Name] <> N'Fubar') OR [l0].[Name] IS NULL");
        }

        public override async Task Include9(bool isAsync)
        {
            await base.Include9(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Date], [l0].[Name], [l0].[OneToMany_Optional_Self_Inverse1Id], [l0].[OneToMany_Required_Self_Inverse1Id], [l0].[OneToOne_Optional_Self1Id]
FROM [LevelTwo] AS [l]
LEFT JOIN [LevelOne] AS [l0] ON [l].[Level1_Optional_Id] = [l0].[Id]
WHERE ([l0].[Name] <> N'Fubar') OR [l0].[Name] IS NULL");
        }

        public override async Task Include10(bool isAsync)
        {
            await base.Include10(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse3Id], [l3].[OneToMany_Optional_Self_Inverse3Id], [l3].[OneToMany_Required_Inverse3Id], [l3].[OneToMany_Required_Self_Inverse3Id], [l3].[OneToOne_Optional_PK_Inverse3Id], [l3].[OneToOne_Optional_Self3Id], [l4].[Id], [l4].[Level3_Optional_Id], [l4].[Level3_Required_Id], [l4].[Name], [l4].[OneToMany_Optional_Inverse4Id], [l4].[OneToMany_Optional_Self_Inverse4Id], [l4].[OneToMany_Required_Inverse4Id], [l4].[OneToMany_Required_Self_Inverse4Id], [l4].[OneToOne_Optional_PK_Inverse4Id], [l4].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
LEFT JOIN [LevelTwo] AS [l2] ON [l].[Id] = [l2].[OneToOne_Optional_PK_Inverse2Id]
LEFT JOIN [LevelThree] AS [l3] ON [l2].[Id] = [l3].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l4] ON [l3].[Id] = [l4].[OneToOne_Optional_PK_Inverse4Id]");
        }

        public override async Task Include11(bool isAsync)
        {
            await base.Include11(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id], [l3].[Id], [l3].[Date], [l3].[Level1_Optional_Id], [l3].[Level1_Required_Id], [l3].[Name], [l3].[OneToMany_Optional_Inverse2Id], [l3].[OneToMany_Optional_Self_Inverse2Id], [l3].[OneToMany_Required_Inverse2Id], [l3].[OneToMany_Required_Self_Inverse2Id], [l3].[OneToOne_Optional_PK_Inverse2Id], [l3].[OneToOne_Optional_Self2Id], [l4].[Id], [l4].[Level2_Optional_Id], [l4].[Level2_Required_Id], [l4].[Name], [l4].[OneToMany_Optional_Inverse3Id], [l4].[OneToMany_Optional_Self_Inverse3Id], [l4].[OneToMany_Required_Inverse3Id], [l4].[OneToMany_Required_Self_Inverse3Id], [l4].[OneToOne_Optional_PK_Inverse3Id], [l4].[OneToOne_Optional_Self3Id], [l5].[Id], [l5].[Level3_Optional_Id], [l5].[Level3_Required_Id], [l5].[Name], [l5].[OneToMany_Optional_Inverse4Id], [l5].[OneToMany_Optional_Self_Inverse4Id], [l5].[OneToMany_Required_Inverse4Id], [l5].[OneToMany_Required_Self_Inverse4Id], [l5].[OneToOne_Optional_PK_Inverse4Id], [l5].[OneToOne_Optional_Self4Id], [l6].[Id], [l6].[Level3_Optional_Id], [l6].[Level3_Required_Id], [l6].[Name], [l6].[OneToMany_Optional_Inverse4Id], [l6].[OneToMany_Optional_Self_Inverse4Id], [l6].[OneToMany_Required_Inverse4Id], [l6].[OneToMany_Required_Self_Inverse4Id], [l6].[OneToOne_Optional_PK_Inverse4Id], [l6].[OneToOne_Optional_Self4Id], [l7].[Id], [l7].[Level2_Optional_Id], [l7].[Level2_Required_Id], [l7].[Name], [l7].[OneToMany_Optional_Inverse3Id], [l7].[OneToMany_Optional_Self_Inverse3Id], [l7].[OneToMany_Required_Inverse3Id], [l7].[OneToMany_Required_Self_Inverse3Id], [l7].[OneToOne_Optional_PK_Inverse3Id], [l7].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[OneToOne_Optional_PK_Inverse3Id]
LEFT JOIN [LevelTwo] AS [l3] ON [l].[Id] = [l3].[OneToOne_Optional_PK_Inverse2Id]
LEFT JOIN [LevelThree] AS [l4] ON [l3].[Id] = [l4].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l5] ON [l4].[Id] = [l5].[Level3_Optional_Id]
LEFT JOIN [LevelFour] AS [l6] ON [l4].[Id] = [l6].[OneToOne_Optional_PK_Inverse4Id]
LEFT JOIN [LevelThree] AS [l7] ON [l3].[Id] = [l7].[OneToOne_Optional_PK_Inverse3Id]");
        }

        public override async Task Include12(bool isAsync)
        {
            await base.Include12(isAsync);

            AssertSql(
                @"SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]");
        }

        public override async Task Include13(bool isAsync)
        {
            await base.Include13(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Include14(bool isAsync)
        {
            await base.Include14(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[OneToOne_Optional_PK_Inverse2Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[Level2_Optional_Id]");
        }

        public override void Include15()
        {
            base.Include15();

            AssertSql(
                @"");
        }

        public override void Include16()
        {
            base.Include16();

            AssertSql(
                @"");
        }

        public override void Include17()
        {
            base.Include17();

            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id], [t].[Id0], [t].[Date0], [t].[Level1_Optional_Id0], [t].[Level1_Required_Id0], [t].[Name0], [t].[OneToMany_Optional_Inverse2Id0], [t].[OneToMany_Optional_Self_Inverse2Id0], [t].[OneToMany_Required_Inverse2Id0], [t].[OneToMany_Required_Self_Inverse2Id0], [t].[OneToOne_Optional_PK_Inverse2Id0], [t].[OneToOne_Optional_Self2Id0]
FROM (
    SELECT DISTINCT [l].[Id], [l].[Date], [l].[Level1_Optional_Id], [l].[Level1_Required_Id], [l].[Name], [l].[OneToMany_Optional_Inverse2Id], [l].[OneToMany_Optional_Self_Inverse2Id], [l].[OneToMany_Required_Inverse2Id], [l].[OneToMany_Required_Self_Inverse2Id], [l].[OneToOne_Optional_PK_Inverse2Id], [l].[OneToOne_Optional_Self2Id], [l0].[Id] AS [Id0], [l0].[Date] AS [Date0], [l0].[Level1_Optional_Id] AS [Level1_Optional_Id0], [l0].[Level1_Required_Id] AS [Level1_Required_Id0], [l0].[Name] AS [Name0], [l0].[OneToMany_Optional_Inverse2Id] AS [OneToMany_Optional_Inverse2Id0], [l0].[OneToMany_Optional_Self_Inverse2Id] AS [OneToMany_Optional_Self_Inverse2Id0], [l0].[OneToMany_Required_Inverse2Id] AS [OneToMany_Required_Inverse2Id0], [l0].[OneToMany_Required_Self_Inverse2Id] AS [OneToMany_Required_Self_Inverse2Id0], [l0].[OneToOne_Optional_PK_Inverse2Id] AS [OneToOne_Optional_PK_Inverse2Id0], [l0].[OneToOne_Optional_Self2Id] AS [OneToOne_Optional_Self2Id0]
    FROM [LevelOne] AS [l1]
    LEFT JOIN [LevelTwo] AS [l] ON [l1].[Id] = [l].[Level1_Optional_Id]
    LEFT JOIN [LevelTwo] AS [l0] ON [l1].[Id] = [l0].[OneToOne_Optional_PK_Inverse2Id]
) AS [t]
LEFT JOIN [LevelThree] AS [l2] ON [t].[Id] = [l2].[Level2_Optional_Id]");
        }

        public override async Task Include18_1(bool isAsync)
        {
            await base.Include18_1(isAsync);

            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM (
    SELECT DISTINCT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
) AS [t]
LEFT JOIN [LevelTwo] AS [l0] ON [t].[Id] = [l0].[Level1_Optional_Id]");
        }

        public override async Task Include18_1_1(bool isAsync)
        {
            await base.Include18_1_1(isAsync);

            AssertSql(
                @"@__p_0='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
FROM (
    SELECT TOP(@__p_0) [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Name] AS [Name0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
    ORDER BY [l0].[Name]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
ORDER BY [t].[Name0]");
        }

        public override async Task Include18_2(bool isAsync)
        {
            await base.Include18_2(isAsync);

            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
FROM (
    SELECT DISTINCT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
    WHERE ([l0].[Name] <> N'Foo') OR [l0].[Name] IS NULL
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]");
        }

        public override void Include18_3()
        {
            base.Include18_3();

            // issue #15783
            AssertSql(
                @"@__p_0='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
FROM (
    SELECT TOP(@__p_0) [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Name] AS [Name0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
    ORDER BY [l0].[Name]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l2] ON [t].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [t].[Name0]");
        }

        public override void Include18_3_1()
        {
            base.Include18_3_1();

            // issue #15783
            AssertSql(
                @"@__p_0='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
FROM (
    SELECT TOP(@__p_0) [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Name] AS [Name0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
    ORDER BY [l0].[Name]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l2] ON [t].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [t].[Name0]");
        }

        public override void Include18_3_2()
        {
            base.Include18_3_2();

            // issue #15783
            AssertSql(
                @"@__p_0='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id]
FROM (
    SELECT TOP(@__p_0) [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Name] AS [Name0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
    ORDER BY [l0].[Name]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l2] ON [t].[Id] = [l2].[Level1_Optional_Id]
ORDER BY [t].[Name0]");
        }

        public override async Task Include18_3_3(bool isAsync)
        {
            await base.Include18_3_3(isAsync);

            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM (
    SELECT DISTINCT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
) AS [t]
LEFT JOIN [LevelThree] AS [l1] ON [t].[Id] = [l1].[Level2_Optional_Id]");
        }

        public override void Include18_4()
        {
            base.Include18_4();

            // issue #15783
            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
FROM (
    SELECT DISTINCT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
    FROM [LevelOne] AS [l]
) AS [t]
LEFT JOIN [LevelTwo] AS [l0] ON [t].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]");
        }

        public override void Include18()
        {
            base.Include18();

            AssertSql(
                @"@__p_0='10'

SELECT [t].[Id], [t].[Date], [t].[Name], [t].[OneToMany_Optional_Self_Inverse1Id], [t].[OneToMany_Required_Self_Inverse1Id], [t].[OneToOne_Optional_Self1Id], [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Date0], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id]
FROM (
    SELECT TOP(@__p_0) [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id] AS [Id0], [l0].[Date] AS [Date0], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name] AS [Name0], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToOne_Optional_PK_Inverse2Id]
    ORDER BY [l].[Id]
) AS [t]
LEFT JOIN [LevelTwo] AS [l1] ON [t].[Id] = [l1].[Level1_Optional_Id]
ORDER BY [t].[Id]");
        }

        public override void Include19()
        {
            base.Include19();

            AssertSql(
                @"SELECT [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Date0], [t].[Level1_Optional_Id0], [t].[Level1_Required_Id0], [t].[Name0], [t].[OneToMany_Optional_Inverse2Id0], [t].[OneToMany_Optional_Self_Inverse2Id0], [t].[OneToMany_Required_Inverse2Id0], [t].[OneToMany_Required_Self_Inverse2Id0], [t].[OneToOne_Optional_PK_Inverse2Id0], [t].[OneToOne_Optional_Self2Id0]
FROM (
    SELECT DISTINCT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id] AS [Level1_Optional_Id0], [l1].[Level1_Required_Id] AS [Level1_Required_Id0], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id] AS [OneToMany_Optional_Inverse2Id0], [l1].[OneToMany_Optional_Self_Inverse2Id] AS [OneToMany_Optional_Self_Inverse2Id0], [l1].[OneToMany_Required_Inverse2Id] AS [OneToMany_Required_Inverse2Id0], [l1].[OneToMany_Required_Self_Inverse2Id] AS [OneToMany_Required_Self_Inverse2Id0], [l1].[OneToOne_Optional_PK_Inverse2Id] AS [OneToOne_Optional_PK_Inverse2Id0], [l1].[OneToOne_Optional_Self2Id] AS [OneToOne_Optional_Self2Id0]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[OneToOne_Optional_PK_Inverse2Id]
) AS [t]");
        }

        public override void IncludeCollection1()
        {
            base.IncludeCollection1();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l0].[Id]");
        }

        public override void IncludeCollection2()
        {
            base.IncludeCollection2();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id]");
        }

        public override void IncludeCollection3()
        {
            base.IncludeCollection3();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l1].[Id]");
        }

        public override void IncludeCollection4()
        {
            base.IncludeCollection4();

            AssertSql(
                @"SELECT [l].[Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [l0].[Id]");
        }

        public override void IncludeCollection5()
        {
            base.IncludeCollection5();

            AssertSql(
                @"SELECT [l].[Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id]");
        }

        public override void IncludeCollection6()
        {
            base.IncludeCollection6();

            AssertSql(
                @"SELECT [l].[Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id1], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name1], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
    LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id]");
        }

        public override void IncludeCollection6_1()
        {
            base.IncludeCollection6_1();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id1], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name1], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
    LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id]");
        }

        public override void IncludeCollection6_2()
        {
            base.IncludeCollection6_2();

            AssertSql(
                @"SELECT [l].[Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id1], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name1], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id], [t].[Id2], [t].[Level2_Optional_Id0], [t].[Level2_Required_Id0], [t].[Name2], [t].[OneToMany_Optional_Inverse3Id0], [t].[OneToMany_Optional_Self_Inverse3Id0], [t].[OneToMany_Required_Inverse3Id0], [t].[OneToMany_Required_Self_Inverse3Id0], [t].[OneToOne_Optional_PK_Inverse3Id0], [t].[OneToOne_Optional_Self3Id0], [t].[Id3], [t].[Level3_Optional_Id0], [t].[Level3_Required_Id0], [t].[Name3], [t].[OneToMany_Optional_Inverse4Id0], [t].[OneToMany_Optional_Self_Inverse4Id0], [t].[OneToMany_Required_Inverse4Id0], [t].[OneToMany_Required_Self_Inverse4Id0], [t].[OneToOne_Optional_PK_Inverse4Id0], [t].[OneToOne_Optional_Self4Id0]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [l3].[Id] AS [Id2], [l3].[Level2_Optional_Id] AS [Level2_Optional_Id0], [l3].[Level2_Required_Id] AS [Level2_Required_Id0], [l3].[Name] AS [Name2], [l3].[OneToMany_Optional_Inverse3Id] AS [OneToMany_Optional_Inverse3Id0], [l3].[OneToMany_Optional_Self_Inverse3Id] AS [OneToMany_Optional_Self_Inverse3Id0], [l3].[OneToMany_Required_Inverse3Id] AS [OneToMany_Required_Inverse3Id0], [l3].[OneToMany_Required_Self_Inverse3Id] AS [OneToMany_Required_Self_Inverse3Id0], [l3].[OneToOne_Optional_PK_Inverse3Id] AS [OneToOne_Optional_PK_Inverse3Id0], [l3].[OneToOne_Optional_Self3Id] AS [OneToOne_Optional_Self3Id0], [l4].[Id] AS [Id3], [l4].[Level3_Optional_Id] AS [Level3_Optional_Id0], [l4].[Level3_Required_Id] AS [Level3_Required_Id0], [l4].[Name] AS [Name3], [l4].[OneToMany_Optional_Inverse4Id] AS [OneToMany_Optional_Inverse4Id0], [l4].[OneToMany_Optional_Self_Inverse4Id] AS [OneToMany_Optional_Self_Inverse4Id0], [l4].[OneToMany_Required_Inverse4Id] AS [OneToMany_Required_Inverse4Id0], [l4].[OneToMany_Required_Self_Inverse4Id] AS [OneToMany_Required_Self_Inverse4Id0], [l4].[OneToOne_Optional_PK_Inverse4Id] AS [OneToOne_Optional_PK_Inverse4Id0], [l4].[OneToOne_Optional_Self4Id] AS [OneToOne_Optional_Self4Id0]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
    LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Optional_Id]
    LEFT JOIN [LevelThree] AS [l3] ON [l0].[Id] = [l3].[Level2_Optional_Id]
    LEFT JOIN [LevelFour] AS [l4] ON [l3].[Id] = [l4].[OneToMany_Optional_Inverse4Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id], [t].[Id3]");
        }

        public override void IncludeCollection6_3()
        {
            base.IncludeCollection6_3();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id1], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name1], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id], [t].[Id2], [t].[Level2_Optional_Id0], [t].[Level2_Required_Id0], [t].[Name2], [t].[OneToMany_Optional_Inverse3Id0], [t].[OneToMany_Optional_Self_Inverse3Id0], [t].[OneToMany_Required_Inverse3Id0], [t].[OneToMany_Required_Self_Inverse3Id0], [t].[OneToOne_Optional_PK_Inverse3Id0], [t].[OneToOne_Optional_Self3Id0], [t].[Id3], [t].[Level3_Optional_Id0], [t].[Level3_Required_Id0], [t].[Name3], [t].[OneToMany_Optional_Inverse4Id0], [t].[OneToMany_Optional_Self_Inverse4Id0], [t].[OneToMany_Required_Inverse4Id0], [t].[OneToMany_Required_Self_Inverse4Id0], [t].[OneToOne_Optional_PK_Inverse4Id0], [t].[OneToOne_Optional_Self4Id0]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [l3].[Id] AS [Id2], [l3].[Level2_Optional_Id] AS [Level2_Optional_Id0], [l3].[Level2_Required_Id] AS [Level2_Required_Id0], [l3].[Name] AS [Name2], [l3].[OneToMany_Optional_Inverse3Id] AS [OneToMany_Optional_Inverse3Id0], [l3].[OneToMany_Optional_Self_Inverse3Id] AS [OneToMany_Optional_Self_Inverse3Id0], [l3].[OneToMany_Required_Inverse3Id] AS [OneToMany_Required_Inverse3Id0], [l3].[OneToMany_Required_Self_Inverse3Id] AS [OneToMany_Required_Self_Inverse3Id0], [l3].[OneToOne_Optional_PK_Inverse3Id] AS [OneToOne_Optional_PK_Inverse3Id0], [l3].[OneToOne_Optional_Self3Id] AS [OneToOne_Optional_Self3Id0], [l4].[Id] AS [Id3], [l4].[Level3_Optional_Id] AS [Level3_Optional_Id0], [l4].[Level3_Required_Id] AS [Level3_Required_Id0], [l4].[Name] AS [Name3], [l4].[OneToMany_Optional_Inverse4Id] AS [OneToMany_Optional_Inverse4Id0], [l4].[OneToMany_Optional_Self_Inverse4Id] AS [OneToMany_Optional_Self_Inverse4Id0], [l4].[OneToMany_Required_Inverse4Id] AS [OneToMany_Required_Inverse4Id0], [l4].[OneToMany_Required_Self_Inverse4Id] AS [OneToMany_Required_Self_Inverse4Id0], [l4].[OneToOne_Optional_PK_Inverse4Id] AS [OneToOne_Optional_PK_Inverse4Id0], [l4].[OneToOne_Optional_Self4Id] AS [OneToOne_Optional_Self4Id0]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
    LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Optional_Id]
    LEFT JOIN [LevelThree] AS [l3] ON [l0].[Id] = [l3].[Level2_Optional_Id]
    LEFT JOIN [LevelFour] AS [l4] ON [l3].[Id] = [l4].[OneToMany_Optional_Inverse4Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id], [t].[Id3]");
        }

        public override void IncludeCollection6_4()
        {
            base.IncludeCollection6_4();

            AssertSql(
                @"SELECT [l].[Id], [t].[Id], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id0], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id], [t].[Id1]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l1].[Id], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id0], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name] AS [Name0], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], [l0].[Id] AS [Id1], [l0].[OneToMany_Optional_Inverse2Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
    LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id1]");
        }

        public override void IncludeCollection7()
        {
            base.IncludeCollection7();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t0].[Id], [t0].[Date], [t0].[Level1_Optional_Id], [t0].[Level1_Required_Id], [t0].[Name], [t0].[OneToMany_Optional_Inverse2Id], [t0].[OneToMany_Optional_Self_Inverse2Id], [t0].[OneToMany_Required_Inverse2Id], [t0].[OneToMany_Required_Self_Inverse2Id], [t0].[OneToOne_Optional_PK_Inverse2Id], [t0].[OneToOne_Optional_Self2Id], [t0].[Id0], [t0].[Level2_Optional_Id], [t0].[Level2_Required_Id], [t0].[Name0], [t0].[OneToMany_Optional_Inverse3Id], [t0].[OneToMany_Optional_Self_Inverse3Id], [t0].[OneToMany_Required_Inverse3Id], [t0].[OneToMany_Required_Self_Inverse3Id], [t0].[OneToOne_Optional_PK_Inverse3Id], [t0].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
LEFT JOIN (
    SELECT [l2].[Id], [l2].[Date], [l2].[Level1_Optional_Id], [l2].[Level1_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse2Id], [l2].[OneToMany_Optional_Self_Inverse2Id], [l2].[OneToMany_Required_Inverse2Id], [l2].[OneToMany_Required_Self_Inverse2Id], [l2].[OneToOne_Optional_PK_Inverse2Id], [l2].[OneToOne_Optional_Self2Id], [l3].[Id] AS [Id0], [l3].[Level2_Optional_Id], [l3].[Level2_Required_Id], [l3].[Name] AS [Name0], [l3].[OneToMany_Optional_Inverse3Id], [l3].[OneToMany_Optional_Self_Inverse3Id], [l3].[OneToMany_Required_Inverse3Id], [l3].[OneToMany_Required_Self_Inverse3Id], [l3].[OneToOne_Optional_PK_Inverse3Id], [l3].[OneToOne_Optional_Self3Id]
    FROM [LevelTwo] AS [l2]
    LEFT JOIN [LevelThree] AS [l3] ON [l2].[Id] = [l3].[OneToOne_Optional_PK_Inverse3Id]
) AS [t0] ON [l].[Id] = [t0].[OneToMany_Optional_Inverse2Id]
ORDER BY [l].[Id], [t].[Id], [t0].[Id]");
        }

        public override async Task IncludeCollection8(bool isAsync)
        {
            await base.IncludeCollection8(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [t].[Id], [t].[Date], [t].[Level1_Optional_Id], [t].[Level1_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse2Id], [t].[OneToMany_Optional_Self_Inverse2Id], [t].[OneToMany_Required_Inverse2Id], [t].[OneToMany_Required_Self_Inverse2Id], [t].[OneToOne_Optional_PK_Inverse2Id], [t].[OneToOne_Optional_Self2Id], [t].[Id0], [t].[Level2_Optional_Id], [t].[Level2_Required_Id], [t].[Name0], [t].[OneToMany_Optional_Inverse3Id], [t].[OneToMany_Optional_Self_Inverse3Id], [t].[OneToMany_Required_Inverse3Id], [t].[OneToMany_Required_Self_Inverse3Id], [t].[OneToOne_Optional_PK_Inverse3Id], [t].[OneToOne_Optional_Self3Id], [t].[Id1], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name1], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id], [l1].[Id] AS [Id0], [l1].[Level2_Optional_Id], [l1].[Level2_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse3Id], [l1].[OneToMany_Optional_Self_Inverse3Id], [l1].[OneToMany_Required_Inverse3Id], [l1].[OneToMany_Required_Self_Inverse3Id], [l1].[OneToOne_Optional_PK_Inverse3Id], [l1].[OneToOne_Optional_Self3Id], [l2].[Id] AS [Id1], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name] AS [Name1], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id]
    FROM [LevelTwo] AS [l0]
    LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[OneToOne_Optional_PK_Inverse3Id]
    LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Optional_Id]
) AS [t] ON [l].[Id] = [t].[OneToMany_Optional_Inverse2Id]
WHERE (
    SELECT COUNT(*)
    FROM [LevelTwo] AS [l3]
    LEFT JOIN [LevelThree] AS [l4] ON [l3].[Id] = [l4].[OneToOne_Optional_PK_Inverse3Id]
    WHERE (([l].[Id] = [l3].[OneToMany_Optional_Inverse2Id]) AND [l3].[OneToMany_Optional_Inverse2Id] IS NOT NULL) AND (([l4].[Name] <> N'Foo') OR [l4].[Name] IS NULL)) > 0
ORDER BY [l].[Id], [t].[Id]");
        }

        public override async Task Include_with_all_method_include_gets_ignored(bool isAsnc)
        {
            await base.Include_with_all_method_include_gets_ignored(isAsnc);

            AssertSql(
                @"SELECT CASE
    WHEN NOT EXISTS (
        SELECT 1
        FROM [LevelOne] AS [l]
        WHERE ([l].[Name] = N'Foo') AND [l].[Name] IS NOT NULL) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END");
        }

        public override async Task Join_with_navigations_in_the_result_selector1(bool isAsync)
        {
            await base.Join_with_navigations_in_the_result_selector1(isAsync);

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l0].[Id], [l0].[Date], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[Level1_Optional_Id]");
        }

        public override void Join_with_navigations_in_the_result_selector2()
        {
            base.Join_with_navigations_in_the_result_selector2();

            AssertSql(
                @"SELECT [l1].[Id], [l1].[Date], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id], [l].[Id], [l0].[Id], [l2].[Id], [l2].[Level2_Optional_Id], [l2].[Level2_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse3Id], [l2].[OneToMany_Optional_Self_Inverse3Id], [l2].[OneToMany_Required_Inverse3Id], [l2].[OneToMany_Required_Self_Inverse3Id], [l2].[OneToOne_Optional_PK_Inverse3Id], [l2].[OneToOne_Optional_Self3Id]
FROM [LevelOne] AS [l]
INNER JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Required_Id]
LEFT JOIN [LevelTwo] AS [l1] ON [l].[Id] = [l1].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l2] ON [l0].[Id] = [l2].[OneToMany_Optional_Inverse3Id]
ORDER BY [l].[Id], [l0].[Id], [l2].[Id]");
        }

        public override void GroupJoin_with_navigations_in_the_result_selector()
        {
            base.GroupJoin_with_navigations_in_the_result_selector();

            AssertSql(
                @"");
        }

        public override void Member_pushdown_chain_3_levels_deep()
        {
            base.Member_pushdown_chain_3_levels_deep();

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
WHERE ((
    SELECT TOP(1) (
        SELECT TOP(1) (
            SELECT TOP(1) [l0].[Name]
            FROM [LevelFour] AS [l0]
            WHERE [l0].[Level3_Required_Id] = [l1].[Id]
            ORDER BY [l0].[Id])
        FROM [LevelThree] AS [l1]
        WHERE [l1].[Level2_Required_Id] = [l2].[Id]
        ORDER BY [l1].[Id])
    FROM [LevelTwo] AS [l2]
    WHERE ([l2].[Level1_Optional_Id] = [l].[Id]) AND [l2].[Level1_Optional_Id] IS NOT NULL
    ORDER BY [l2].[Id]) <> N'Foo') OR (
    SELECT TOP(1) (
        SELECT TOP(1) (
            SELECT TOP(1) [l0].[Name]
            FROM [LevelFour] AS [l0]
            WHERE [l0].[Level3_Required_Id] = [l1].[Id]
            ORDER BY [l0].[Id])
        FROM [LevelThree] AS [l1]
        WHERE [l1].[Level2_Required_Id] = [l2].[Id]
        ORDER BY [l1].[Id])
    FROM [LevelTwo] AS [l2]
    WHERE ([l2].[Level1_Optional_Id] = [l].[Id]) AND [l2].[Level1_Optional_Id] IS NOT NULL
    ORDER BY [l2].[Id]) IS NULL
ORDER BY [l].[Id]");
        }

        public override void Member_pushdown_chain_3_levels_deep_entity()
        {
            base.Member_pushdown_chain_3_levels_deep_entity();

            AssertSql(
                @"SELECT [t4].[Id], [t4].[Level3_Optional_Id], [t4].[Level3_Required_Id], [t4].[Name], [t4].[OneToMany_Optional_Inverse4Id], [t4].[OneToMany_Optional_Self_Inverse4Id], [t4].[OneToMany_Required_Inverse4Id], [t4].[OneToMany_Required_Self_Inverse4Id], [t4].[OneToOne_Optional_PK_Inverse4Id], [t4].[OneToOne_Optional_Self4Id]
FROM [LevelOne] AS [l]
LEFT JOIN (
    SELECT [t3].[Id], [t3].[Level3_Optional_Id], [t3].[Level3_Required_Id], [t3].[Name], [t3].[OneToMany_Optional_Inverse4Id], [t3].[OneToMany_Optional_Self_Inverse4Id], [t3].[OneToMany_Required_Inverse4Id], [t3].[OneToMany_Required_Self_Inverse4Id], [t3].[OneToOne_Optional_PK_Inverse4Id], [t3].[OneToOne_Optional_Self4Id], [t3].[Id0], [t3].[Level1_Optional_Id]
    FROM (
        SELECT [t2].[Id], [t2].[Level3_Optional_Id], [t2].[Level3_Required_Id], [t2].[Name], [t2].[OneToMany_Optional_Inverse4Id], [t2].[OneToMany_Optional_Self_Inverse4Id], [t2].[OneToMany_Required_Inverse4Id], [t2].[OneToMany_Required_Self_Inverse4Id], [t2].[OneToOne_Optional_PK_Inverse4Id], [t2].[OneToOne_Optional_Self4Id], [l0].[Id] AS [Id0], [l0].[Level1_Optional_Id], ROW_NUMBER() OVER(PARTITION BY [l0].[Level1_Optional_Id] ORDER BY [l0].[Id]) AS [row]
        FROM [LevelTwo] AS [l0]
        LEFT JOIN (
            SELECT [t1].[Id], [t1].[Level3_Optional_Id], [t1].[Level3_Required_Id], [t1].[Name], [t1].[OneToMany_Optional_Inverse4Id], [t1].[OneToMany_Optional_Self_Inverse4Id], [t1].[OneToMany_Required_Inverse4Id], [t1].[OneToMany_Required_Self_Inverse4Id], [t1].[OneToOne_Optional_PK_Inverse4Id], [t1].[OneToOne_Optional_Self4Id], [t1].[Id0], [t1].[Level2_Required_Id]
            FROM (
                SELECT [t0].[Id], [t0].[Level3_Optional_Id], [t0].[Level3_Required_Id], [t0].[Name], [t0].[OneToMany_Optional_Inverse4Id], [t0].[OneToMany_Optional_Self_Inverse4Id], [t0].[OneToMany_Required_Inverse4Id], [t0].[OneToMany_Required_Self_Inverse4Id], [t0].[OneToOne_Optional_PK_Inverse4Id], [t0].[OneToOne_Optional_Self4Id], [l1].[Id] AS [Id0], [l1].[Level2_Required_Id], ROW_NUMBER() OVER(PARTITION BY [l1].[Level2_Required_Id] ORDER BY [l1].[Id]) AS [row]
                FROM [LevelThree] AS [l1]
                LEFT JOIN (
                    SELECT [t].[Id], [t].[Level3_Optional_Id], [t].[Level3_Required_Id], [t].[Name], [t].[OneToMany_Optional_Inverse4Id], [t].[OneToMany_Optional_Self_Inverse4Id], [t].[OneToMany_Required_Inverse4Id], [t].[OneToMany_Required_Self_Inverse4Id], [t].[OneToOne_Optional_PK_Inverse4Id], [t].[OneToOne_Optional_Self4Id]
                    FROM (
                        SELECT [l2].[Id], [l2].[Level3_Optional_Id], [l2].[Level3_Required_Id], [l2].[Name], [l2].[OneToMany_Optional_Inverse4Id], [l2].[OneToMany_Optional_Self_Inverse4Id], [l2].[OneToMany_Required_Inverse4Id], [l2].[OneToMany_Required_Self_Inverse4Id], [l2].[OneToOne_Optional_PK_Inverse4Id], [l2].[OneToOne_Optional_Self4Id], ROW_NUMBER() OVER(PARTITION BY [l2].[Level3_Required_Id] ORDER BY [l2].[Id]) AS [row]
                        FROM [LevelFour] AS [l2]
                    ) AS [t]
                    WHERE [t].[row] <= 1
                ) AS [t0] ON [l1].[Id] = [t0].[Level3_Required_Id]
            ) AS [t1]
            WHERE [t1].[row] <= 1
        ) AS [t2] ON [l0].[Id] = [t2].[Level2_Required_Id]
    ) AS [t3]
    WHERE [t3].[row] <= 1
) AS [t4] ON [l].[Id] = [t4].[Level1_Optional_Id]
ORDER BY [l].[Id]");
        }

        public override void Member_pushdown_with_collection_navigation_in_the_middle()
        {
            base.Member_pushdown_with_collection_navigation_in_the_middle();

            AssertSql(
                @"SELECT (
    SELECT TOP(1) (
        SELECT TOP(1) (
            SELECT TOP(1) [l].[Name]
            FROM [LevelFour] AS [l]
            WHERE [l].[Level3_Required_Id] = [l0].[Id]
            ORDER BY [l].[Id])
        FROM [LevelThree] AS [l0]
        WHERE ([l1].[Id] = [l0].[OneToMany_Optional_Inverse3Id]) AND [l0].[OneToMany_Optional_Inverse3Id] IS NOT NULL)
    FROM [LevelTwo] AS [l1]
    WHERE [l1].[Level1_Required_Id] = [l2].[Id]
    ORDER BY [l1].[Id])
FROM [LevelOne] AS [l2]
ORDER BY [l2].[Id]");
        }

        public override async Task Member_pushdown_with_multiple_collections(bool isAsync)
        {
            await base.Member_pushdown_with_multiple_collections(isAsync);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [l].[Name]
    FROM [LevelThree] AS [l]
    WHERE (
        SELECT TOP(1) [l0].[Id]
        FROM [LevelTwo] AS [l0]
        WHERE ([l2].[Id] = [l0].[OneToMany_Optional_Inverse2Id]) AND [l0].[OneToMany_Optional_Inverse2Id] IS NOT NULL
        ORDER BY [l0].[Id]) IS NOT NULL AND ((((
        SELECT TOP(1) [l1].[Id]
        FROM [LevelTwo] AS [l1]
        WHERE ([l2].[Id] = [l1].[OneToMany_Optional_Inverse2Id]) AND [l1].[OneToMany_Optional_Inverse2Id] IS NOT NULL
        ORDER BY [l1].[Id]) = [l].[OneToMany_Optional_Inverse3Id]) AND ((
        SELECT TOP(1) [l1].[Id]
        FROM [LevelTwo] AS [l1]
        WHERE ([l2].[Id] = [l1].[OneToMany_Optional_Inverse2Id]) AND [l1].[OneToMany_Optional_Inverse2Id] IS NOT NULL
        ORDER BY [l1].[Id]) IS NOT NULL AND [l].[OneToMany_Optional_Inverse3Id] IS NOT NULL)) OR ((
        SELECT TOP(1) [l1].[Id]
        FROM [LevelTwo] AS [l1]
        WHERE ([l2].[Id] = [l1].[OneToMany_Optional_Inverse2Id]) AND [l1].[OneToMany_Optional_Inverse2Id] IS NOT NULL
        ORDER BY [l1].[Id]) IS NULL AND [l].[OneToMany_Optional_Inverse3Id] IS NULL))
    ORDER BY [l].[Id])
FROM [LevelOne] AS [l2]");
        }

        public override async Task Null_check_removal_applied_recursively(bool isAsync)
        {
            await base.Null_check_removal_applied_recursively(isAsync);

            AssertSql(" ");
        }

        public override async Task Null_check_different_structure_does_not_remove_null_checks(bool isAsync)
        {
            await base.Null_check_different_structure_does_not_remove_null_checks(isAsync);

            AssertSql(
                @"SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id]
FROM [LevelOne] AS [l]
LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
LEFT JOIN [LevelThree] AS [l1] ON [l0].[Id] = [l1].[Level2_Optional_Id]
LEFT JOIN [LevelFour] AS [l2] ON [l1].[Id] = [l2].[Level3_Optional_Id]
WHERE (CASE
    WHEN [l0].[Id] IS NULL THEN NULL
    ELSE CASE
        WHEN [l1].[Id] IS NULL THEN NULL
        ELSE [l2].[Name]
    END
END = N'L4 01') AND CASE
    WHEN [l0].[Id] IS NULL THEN NULL
    ELSE CASE
        WHEN [l1].[Id] IS NULL THEN NULL
        ELSE [l2].[Name]
    END
END IS NOT NULL");
        }

        public override void Union_over_entities_with_different_nullability()
        {
            base.Union_over_entities_with_different_nullability();

            AssertSql(
                @"SELECT [t].[Id]
FROM (
    SELECT [l].[Id], [l].[Date], [l].[Name], [l].[OneToMany_Optional_Self_Inverse1Id], [l].[OneToMany_Required_Self_Inverse1Id], [l].[OneToOne_Optional_Self1Id], [l0].[Id] AS [Id0], [l0].[Date] AS [Date0], [l0].[Level1_Optional_Id], [l0].[Level1_Required_Id], [l0].[Name] AS [Name0], [l0].[OneToMany_Optional_Inverse2Id], [l0].[OneToMany_Optional_Self_Inverse2Id], [l0].[OneToMany_Required_Inverse2Id], [l0].[OneToMany_Required_Self_Inverse2Id], [l0].[OneToOne_Optional_PK_Inverse2Id], [l0].[OneToOne_Optional_Self2Id]
    FROM [LevelOne] AS [l]
    LEFT JOIN [LevelTwo] AS [l0] ON [l].[Id] = [l0].[Level1_Optional_Id]
    UNION ALL
    SELECT [l2].[Id], [l2].[Date], [l2].[Name], [l2].[OneToMany_Optional_Self_Inverse1Id], [l2].[OneToMany_Required_Self_Inverse1Id], [l2].[OneToOne_Optional_Self1Id], [l1].[Id] AS [Id0], [l1].[Date] AS [Date0], [l1].[Level1_Optional_Id], [l1].[Level1_Required_Id], [l1].[Name] AS [Name0], [l1].[OneToMany_Optional_Inverse2Id], [l1].[OneToMany_Optional_Self_Inverse2Id], [l1].[OneToMany_Required_Inverse2Id], [l1].[OneToMany_Required_Self_Inverse2Id], [l1].[OneToOne_Optional_PK_Inverse2Id], [l1].[OneToOne_Optional_Self2Id]
    FROM [LevelTwo] AS [l1]
    LEFT JOIN [LevelOne] AS [l2] ON [l1].[Level1_Optional_Id] = [l2].[Id]
    WHERE [l2].[Id] IS NULL
) AS [t]");
        }

        private void AssertSql(params string[] expected)
        {
            Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
        }
    }
}
