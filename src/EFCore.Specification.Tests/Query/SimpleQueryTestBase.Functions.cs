﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities.Xunit;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable StringStartsWithIsCultureSpecific
// ReSharper disable StringEndsWithIsCultureSpecific
// ReSharper disable StringCompareIsCultureSpecific.1
// ReSharper disable StringCompareToIsCultureSpecific
// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable SpecifyACultureInStringConversionExplicitly
namespace Microsoft.EntityFrameworkCore.Query
{
    // ReSharper disable once UnusedTypeParameter
    public abstract partial class SimpleQueryTestBase<TFixture>
    {
        [ConditionalFact]
        public virtual Task String_StartsWith_Literal()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.StartsWith("M")),
                entryCount: 12);
        }

        [ConditionalFact]
        public virtual Task String_StartsWith_Identity()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.StartsWith(c.ContactName)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual Task String_StartsWith_Column()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.StartsWith(c.ContactName)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual Task String_StartsWith_MethodCall()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.StartsWith(LocalMethod1())),
                entryCount: 12);
        }

        [ConditionalFact]
        public virtual Task String_EndsWith_Literal()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.EndsWith("b")),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual Task String_EndsWith_Identity()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.EndsWith(c.ContactName)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual Task String_EndsWith_Column()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.EndsWith(c.ContactName)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual Task String_EndsWith_MethodCall()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.EndsWith(LocalMethod2())),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual Task String_Contains_Literal()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.Contains("M")),
                entryCount: 19);
        }

        [ConditionalFact]
        public virtual Task String_Contains_Identity()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.Contains(c.ContactName)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual Task String_Contains_Column()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.Contains(c.ContactName)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual Task String_Contains_MethodCall()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactName.Contains(LocalMethod1())),
                entryCount: 19);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_simple_zero()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") == 0),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 != string.Compare(c.CustomerID, "ALFKI")),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") > 0),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 >= string.Compare(c.CustomerID, "ALFKI")),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 < string.Compare(c.CustomerID, "ALFKI")),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") <= 0),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_simple_one()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") == 1),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 == string.Compare(c.CustomerID, "ALFKI")));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") < 1),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 1 > string.Compare(c.CustomerID, "ALFKI")),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") > -1),
                entryCount: 91);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 < string.Compare(c.CustomerID, "ALFKI")),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_compare_with_parameter()
        {
            Customer customer = null;
            using (var context = CreateContext())
            {
                customer = context.Customers.OrderBy(c => c.CustomerID).First();
            }

            ClearLog();

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, customer.CustomerID) == 1),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 == string.Compare(c.CustomerID, customer.CustomerID)));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, customer.CustomerID) < 1),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 1 > string.Compare(c.CustomerID, customer.CustomerID)),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, customer.CustomerID) > -1),
                entryCount: 91);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 < string.Compare(c.CustomerID, customer.CustomerID)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_simple_client()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") == 42));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") > 42));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 42 > string.Compare(c.CustomerID, "ALFKI")),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_nested()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "M" + c.CustomerID) == 0));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 != string.Compare(c.CustomerID, c.CustomerID.ToUpper())));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI".Replace("ALF".ToUpper(), c.CustomerID)) > 0));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 >= string.Compare(c.CustomerID, "M" + c.CustomerID)),
                entryCount: 51);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 1 == string.Compare(c.CustomerID, c.CustomerID.ToUpper())));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI".Replace("ALF".ToUpper(), c.CustomerID)) == -1),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_multi_predicate()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.CustomerID, "ALFKI") > -1).Where(c => string.Compare(c.CustomerID, "CACTU") == -1),
                entryCount: 11);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Compare(c.ContactTitle, "Owner") == 0).Where(c => string.Compare(c.Country, "USA") != 0),
                entryCount: 15);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_to_simple_zero()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") == 0),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 != c.CustomerID.CompareTo("ALFKI")),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") > 0),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 >= c.CustomerID.CompareTo("ALFKI")),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 < c.CustomerID.CompareTo("ALFKI")),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") <= 0),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_to_simple_one()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") == 1),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 == c.CustomerID.CompareTo("ALFKI")));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") < 1),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 1 > c.CustomerID.CompareTo("ALFKI")),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") > -1),
                entryCount: 91);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 < c.CustomerID.CompareTo("ALFKI")),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_compare_to_with_parameter()
        {
            Customer customer = null;
            using (var context = CreateContext())
            {
                customer = context.Customers.OrderBy(c => c.CustomerID).First();
            }

            ClearLog();

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo(customer.CustomerID) == 1),
                entryCount: 90);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 == c.CustomerID.CompareTo(customer.CustomerID)));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo(customer.CustomerID) < 1),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 1 > c.CustomerID.CompareTo(customer.CustomerID)),
                entryCount: 1);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo(customer.CustomerID) > -1),
                entryCount: 91);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => -1 < c.CustomerID.CompareTo(customer.CustomerID)),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_to_simple_client()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") == 42));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") > 42));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 42 > c.CustomerID.CompareTo("ALFKI")),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_to_nested()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("M" + c.CustomerID) == 0));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 != c.CustomerID.CompareTo(c.CustomerID.ToUpper())));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI".Replace("ALF".ToUpper(), c.CustomerID)) > 0));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 0 >= c.CustomerID.CompareTo("M" + c.CustomerID)),
                entryCount: 51);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => 1 == c.CustomerID.CompareTo(c.CustomerID.ToUpper())));

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI".Replace("ALF".ToUpper(), c.CustomerID)) == -1),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task String_Compare_to_multi_predicate()
        {
            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.CompareTo("ALFKI") > -1).Where(c => c.CustomerID.CompareTo("CACTU") == -1),
                entryCount: 11);

            await AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.CompareTo("Owner") == 0).Where(c => c.Country.CompareTo("USA") != 0),
                entryCount: 15);
        }

        protected static string LocalMethod1()
        {
            return "M";
        }

        protected static string LocalMethod2()
        {
            return "m";
        }

        [ConditionalFact]
        public virtual Task Where_math_abs1()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Abs(od.ProductID) > 10),
                entryCount: 1939);
        }

        [ConditionalFact]
        public virtual Task Where_math_abs2()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Abs(od.Quantity) > 10),
                entryCount: 1547);
        }

        [ConditionalFact]
        public virtual Task Where_math_abs3()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Abs(od.UnitPrice) > 10),
                entryCount: 1677);
        }

        [ConditionalFact]
        public virtual Task Where_math_abs_uncorrelated()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Abs(-10) < od.ProductID),
                entryCount: 1939);
        }

        [ConditionalFact]
        public virtual Task Where_math_ceiling1()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Ceiling(od.Discount) > 0),
                entryCount: 838);
        }

        [ConditionalFact]
        public virtual Task Where_math_ceiling2()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Ceiling(od.UnitPrice) > 10),
                entryCount: 1677);
        }

        [ConditionalFact]
        public virtual Task Where_math_floor()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Floor(od.UnitPrice) > 10),
                entryCount: 1658);
        }

        [ConditionalFact]
        public virtual Task Where_math_power()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Pow(od.Discount, 2) > 0.05f),
                entryCount: 154);
        }

        [ConditionalFact]
        public virtual Task Where_math_round()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Round(od.UnitPrice) > 10),
                entryCount: 1662);
        }

        [ConditionalFact]
        public virtual Task Select_math_round_int()
        {
            return AssertQueryAsync<Order>(
                os => os.Where(o => o.OrderID < 10250).Select(
                    o => new
                    {
                        A = Math.Round((double)o.OrderID)
                    }),
                e => e.A);
        }

        [ConditionalFact]
        public virtual Task Select_math_truncate_int()
        {
            return AssertQueryAsync<Order>(
                os => os.Where(o => o.OrderID < 10250).Select(
                    o => new
                    {
                        A = Math.Truncate((double)o.OrderID)
                    }),
                e => e.A);
        }

        [ConditionalFact]
        public virtual Task Where_math_round2()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Round(od.UnitPrice, 2) > 100),
                entryCount: 46);
        }

        [ConditionalFact]
        public virtual Task Where_math_truncate()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Math.Truncate(od.UnitPrice) > 10),
                entryCount: 1658);
        }

        [ConditionalFact]
        public virtual Task Where_math_exp()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Exp(od.Discount) > 1),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_log10()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077 && od.Discount > 0).Where(od => Math.Log10(od.Discount) < 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_log()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077 && od.Discount > 0).Where(od => Math.Log(od.Discount) < 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_log_new_base()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077 && od.Discount > 0).Where(od => Math.Log(od.Discount, 7) < 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_sqrt()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Sqrt(od.Discount) > 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_acos()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Acos(od.Discount) > 1),
                entryCount: 25);
        }

        [ConditionalFact]
        public virtual Task Where_math_asin()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Asin(od.Discount) > 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_atan()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Atan(od.Discount) > 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_atan2()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Atan2(od.Discount, 1) > 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_cos()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Cos(od.Discount) > 0),
                entryCount: 25);
        }

        [ConditionalFact]
        public virtual Task Where_math_sin()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Sin(od.Discount) > 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_tan()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Tan(od.Discount) > 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_sign()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Sign(od.Discount) > 0),
                entryCount: 13);
        }

        [ConditionalFact]
        public virtual Task Where_math_max()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Max(od.OrderID, od.ProductID) == od.OrderID),
                entryCount: 25);
        }

        [ConditionalFact]
        public virtual Task Where_math_min()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => od.OrderID == 11077).Where(od => Math.Min(od.OrderID, od.ProductID) == od.ProductID),
                entryCount: 25);
        }

        [ConditionalFact]
        public virtual Task Where_guid_newguid()
        {
            return AssertQueryAsync<OrderDetail>(
                ods => ods.Where(od => Guid.NewGuid() != default),
                entryCount: 2155);
        }

        [ConditionalFact]
        public virtual Task Where_string_to_upper()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.ToUpper() == "ALFKI"),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual Task Where_string_to_lower()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.CustomerID.ToLower() == "alfki"),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual Task Where_functions_nested()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => Math.Pow(c.CustomerID.Length, 2) == 25),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual async Task Convert_ToByte()
        {
            var convertMethods = new List<Expression<Func<Order, bool>>>
            {
                o => Convert.ToByte(Convert.ToByte(o.OrderID % 1)) >= 0,
                o => Convert.ToByte(Convert.ToDecimal(o.OrderID % 1)) >= 0,
                o => Convert.ToByte(Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToByte((float)Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToByte(Convert.ToInt16(o.OrderID % 1)) >= 0,
                o => Convert.ToByte(Convert.ToInt32(o.OrderID % 1)) >= 0,
                o => Convert.ToByte(Convert.ToInt64(o.OrderID % 1)) >= 0,
                o => Convert.ToByte(Convert.ToString(o.OrderID % 1)) >= 0
            };

            foreach (var convertMethod in convertMethods)
            {
                await AssertQueryAsync<Order>(
                    os => os.Where(o => o.CustomerID == "ALFKI")
                        .Where(convertMethod),
                    entryCount: 6);
            }
        }

        [ConditionalFact]
        public virtual async Task Convert_ToDecimal()
        {
            var convertMethods = new List<Expression<Func<Order, bool>>>
            {
                o => Convert.ToDecimal(Convert.ToByte(o.OrderID % 1)) >= 0,
                o => Convert.ToDecimal(Convert.ToDecimal(o.OrderID % 1)) >= 0,
                o => Convert.ToDecimal(Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToDecimal((float)Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToDecimal(Convert.ToInt16(o.OrderID % 1)) >= 0,
                o => Convert.ToDecimal(Convert.ToInt32(o.OrderID % 1)) >= 0,
                o => Convert.ToDecimal(Convert.ToInt64(o.OrderID % 1)) >= 0,
                o => Convert.ToDecimal(Convert.ToString(o.OrderID % 1)) >= 0
            };

            foreach (var convertMethod in convertMethods)
            {
                await AssertQueryAsync<Order>(
                    os => os.Where(o => o.CustomerID == "ALFKI")
                        .Where(convertMethod),
                    entryCount: 6);
            }
        }

        [ConditionalFact]
        public virtual async Task Convert_ToDouble()
        {
            var convertMethods = new List<Expression<Func<Order, bool>>>
            {
                o => Convert.ToDouble(Convert.ToByte(o.OrderID % 1)) >= 0,
                o => Convert.ToDouble(Convert.ToDecimal(o.OrderID % 1)) >= 0,
                o => Convert.ToDouble(Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToDouble((float)Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToDouble(Convert.ToInt16(o.OrderID % 1)) >= 0,
                o => Convert.ToDouble(Convert.ToInt32(o.OrderID % 1)) >= 0,
                o => Convert.ToDouble(Convert.ToInt64(o.OrderID % 1)) >= 0,
                o => Convert.ToDouble(Convert.ToString(o.OrderID % 1)) >= 0
            };

            foreach (var convertMethod in convertMethods)
            {
                await AssertQueryAsync<Order>(
                    os => os.Where(o => o.CustomerID == "ALFKI")
                        .Where(convertMethod),
                    entryCount: 6);
            }
        }

        [ConditionalFact]
        public virtual async Task Convert_ToInt16()
        {
            var convertMethods = new List<Expression<Func<Order, bool>>>
            {
                o => Convert.ToInt16(Convert.ToByte(o.OrderID % 1)) >= 0,
                o => Convert.ToInt16(Convert.ToDecimal(o.OrderID % 1)) >= 0,
                o => Convert.ToInt16(Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToInt16((float)Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToInt16(Convert.ToInt16(o.OrderID % 1)) >= 0,
                o => Convert.ToInt16(Convert.ToInt32(o.OrderID % 1)) >= 0,
                o => Convert.ToInt16(Convert.ToInt64(o.OrderID % 1)) >= 0,
                o => Convert.ToInt16(Convert.ToString(o.OrderID % 1)) >= 0
            };

            foreach (var convertMethod in convertMethods)
            {
                await AssertQueryAsync<Order>(
                    os => os.Where(o => o.CustomerID == "ALFKI")
                        .Where(convertMethod),
                    entryCount: 6);
            }
        }

        [ConditionalFact]
        public virtual async Task Convert_ToInt32()
        {
            var convertMethods = new List<Expression<Func<Order, bool>>>
            {
                o => Convert.ToInt32(Convert.ToByte(o.OrderID % 1)) >= 0,
                o => Convert.ToInt32(Convert.ToDecimal(o.OrderID % 1)) >= 0,
                o => Convert.ToInt32(Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToInt32((float)Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToInt32(Convert.ToInt16(o.OrderID % 1)) >= 0,
                o => Convert.ToInt32(Convert.ToInt32(o.OrderID % 1)) >= 0,
                o => Convert.ToInt32(Convert.ToInt64(o.OrderID % 1)) >= 0,
                o => Convert.ToInt32(Convert.ToString(o.OrderID % 1)) >= 0
            };

            foreach (var convertMethod in convertMethods)
            {
                await AssertQueryAsync<Order>(
                    os => os.Where(o => o.CustomerID == "ALFKI")
                        .Where(convertMethod),
                    entryCount: 6);
            }
        }

        [ConditionalFact]
        public virtual async Task Convert_ToInt64()
        {
            var convertMethods = new List<Expression<Func<Order, bool>>>
            {
                o => Convert.ToInt64(Convert.ToByte(o.OrderID % 1)) >= 0,
                o => Convert.ToInt64(Convert.ToDecimal(o.OrderID % 1)) >= 0,
                o => Convert.ToInt64(Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToInt64((float)Convert.ToDouble(o.OrderID % 1)) >= 0,
                o => Convert.ToInt64(Convert.ToInt16(o.OrderID % 1)) >= 0,
                o => Convert.ToInt64(Convert.ToInt32(o.OrderID % 1)) >= 0,
                o => Convert.ToInt64(Convert.ToInt64(o.OrderID % 1)) >= 0,
                o => Convert.ToInt64(Convert.ToString(o.OrderID % 1)) >= 0
            };

            foreach (var convertMethod in convertMethods)
            {
                await AssertQueryAsync<Order>(
                    os => os.Where(o => o.CustomerID == "ALFKI")
                        .Where(convertMethod),
                    entryCount: 6);
            }
        }

        [ConditionalFact]
        public virtual async Task Convert_ToString()
        {
            var convertMethods = new List<Expression<Func<Order, bool>>>
            {
                o => Convert.ToString(Convert.ToByte(o.OrderID % 1)) != "10",
                o => Convert.ToString(Convert.ToDecimal(o.OrderID % 1)) != "10",
                o => Convert.ToString(Convert.ToDouble(o.OrderID % 1)) != "10",
                o => Convert.ToString((float)Convert.ToDouble(o.OrderID % 1)) != "10",
                o => Convert.ToString(Convert.ToInt16(o.OrderID % 1)) != "10",
                o => Convert.ToString(Convert.ToInt32(o.OrderID % 1)) != "10",
                o => Convert.ToString(Convert.ToInt64(o.OrderID % 1)) != "10",
                o => Convert.ToString(Convert.ToString(o.OrderID % 1)) != "10"
            };

            foreach (var convertMethod in convertMethods)
            {
                await AssertQueryAsync<Order>(
                    os => os.Where(o => o.CustomerID == "ALFKI")
                        .Where(convertMethod),
                    entryCount: 6);
            }
        }

        [ConditionalFact]
        public virtual void Indexof_with_emptystring()
        {
            // ReSharper disable once StringIndexOfIsCultureSpecific.1
            AssertSingleResult<Customer>(
                cs => cs.Where(c => c.CustomerID == "ALFKI").Select(c => c.ContactName.IndexOf(string.Empty)));
        }

        [ConditionalFact]
        public virtual void Replace_with_emptystring()
        {
            AssertSingleResult<Customer>(
                cs => cs.Where(c => c.CustomerID == "ALFKI").Select(c => c.ContactName.Replace("ari", string.Empty)));
        }

        [ConditionalFact]
        public virtual void Substring_with_zero_startindex()
        {
            AssertSingleResult<Customer>(
                cs => cs.Where(c => c.CustomerID == "ALFKI").Select(c => c.ContactName.Substring(0, 3)));
        }

        [ConditionalFact]
        public virtual void Substring_with_zero_length()
        {
            AssertSingleResult<Customer>(
                cs => cs.Where(c => c.CustomerID == "ALFKI").Select(c => c.ContactName.Substring(2, 0)));
        }

        [ConditionalFact]
        public virtual void Substring_with_constant()
        {
            AssertSingleResult<Customer>(
                cs => cs.Where(c => c.CustomerID == "ALFKI").Select(c => c.ContactName.Substring(1, 3)));
        }

        [ConditionalFact]
        public virtual void Substring_with_closure()
        {
            // ReSharper disable once ConvertToConstant.Local
            var start = 2;

            AssertSingleResult<Customer>(
                cs => cs.Where(c => c.CustomerID == "ALFKI").Select(c => c.ContactName.Substring(start, 3)));
        }

        [ConditionalFact]
        public virtual void Substring_with_client_eval()
        {
            AssertSingleResult<Customer>(
                cs => cs.Where(c => c.CustomerID == "ALFKI").Select(c => c.ContactName.Substring(c.ContactName.IndexOf('a'), 3)));
        }

        [ConditionalFact]
        public virtual Task IsNullOrEmpty_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.IsNullOrEmpty(c.Region)),
                entryCount: 60);
        }

        [ConditionalFact]
        public virtual void IsNullOrEmpty_in_projection()
        {
            using (var context = CreateContext())
            {
                var query = context.Set<Customer>()
                    .Select(
                        c => new
                        {
                            Id = c.CustomerID,
                            Value = string.IsNullOrEmpty(c.Region)
                        })
                    .ToList();

                Assert.Equal(91, query.Count);
            }
        }

        [ConditionalFact]
        public virtual void IsNullOrEmpty_negated_in_projection()
        {
            using (var context = CreateContext())
            {
                var query = context.Set<Customer>()
                    .Select(
                        c => new
                        {
                            Id = c.CustomerID,
                            Value = !string.IsNullOrEmpty(c.Region)
                        })
                    .ToList();

                Assert.Equal(91, query.Count);
            }
        }

        [ConditionalFact]
        public virtual Task IsNullOrWhiteSpace_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.IsNullOrWhiteSpace(c.Region)),
                entryCount: 60);
        }

        [ConditionalFact]
        public virtual Task TrimStart_without_arguments_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.TrimStart() == "Owner"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task TrimStart_with_char_argument_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.TrimStart('O') == "wner"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task TrimStart_with_char_array_argument_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.TrimStart('O', 'w') == "ner"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task TrimEnd_without_arguments_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.TrimEnd() == "Owner"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task TrimEnd_with_char_argument_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.TrimEnd('r') == "Owne"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task TrimEnd_with_char_array_argument_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.TrimEnd('e', 'r') == "Own"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task Trim_without_argument_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.Trim() == "Owner"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task Trim_with_char_argument_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.Trim('O') == "wner"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task Trim_with_char_array_argument_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => c.ContactTitle.Trim('O', 'r') == "wne"),
                entryCount: 17);
        }

        [ConditionalFact]
        public virtual Task Order_by_length_twice()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.OrderBy(c => c.CustomerID.Length).ThenBy(c => c.CustomerID.Length).ThenBy(c => c.CustomerID),
                entryCount: 91);
        }

        [ConditionalFact]
        public virtual Task Static_string_equals_in_predicate()
        {
            return AssertQueryAsync<Customer>(
                cs => cs.Where(c => string.Equals(c.CustomerID, "ANATR")),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual Task Static_equals_nullable_datetime_compared_to_non_nullable()
        {
            var arg = new DateTime(1996, 7, 4);

            return AssertQueryAsync<Order>(
                os => os.Where(o => Equals(o.OrderDate, arg)),
                entryCount: 1);
        }

        [ConditionalFact]
        public virtual Task Static_equals_int_compared_to_long()
        {
            long arg = 10248;

            return AssertQueryAsync<Order>(
                os => os.Where(o => Equals(o.OrderID, arg)));
        }
    }
}
