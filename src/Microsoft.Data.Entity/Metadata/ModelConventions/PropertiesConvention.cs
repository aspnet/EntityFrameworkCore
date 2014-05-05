// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Utilities;

namespace Microsoft.Data.Entity.Metadata.ModelConventions
{
    public class PropertiesConvention : IModelConvention
    {
        private static readonly Type[] _propertyTypes = new[]
            {
                typeof(bool),
                typeof(byte),
                typeof(byte[]),
                typeof(char),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(Guid),
                typeof(int),
                typeof(long),
                typeof(sbyte),
                typeof(short),
                typeof(string),
                typeof(TimeSpan),
                typeof(uint),
                typeof(ulong),
                typeof(ushort)
            };

        public virtual void Apply([NotNull] EntityType entityType)
        {
            Check.NotNull(entityType, "entityType");

            // TODO: Honor [NotMapped]
            var primitiveProperties = entityType.Type.GetRuntimeProperties().Where(IsPrimitiveProperty);
            foreach (var propertyInfo in primitiveProperties)
            {
                entityType.AddProperty(propertyInfo);
            }
        }

        protected virtual bool IsValidProperty([NotNull] PropertyInfo propertyInfo)
        {
            Check.NotNull(propertyInfo, "propertyInfo");

            return !propertyInfo.IsStatic()
                   && propertyInfo.GetIndexParameters().Length == 0
                   && propertyInfo.CanRead
                   && propertyInfo.CanWrite;
        }

        protected virtual bool IsPrimitiveProperty([NotNull] PropertyInfo propertyInfo)
        {
            Check.NotNull(propertyInfo, "propertyInfo");

            if (!IsValidProperty(propertyInfo))
            {
                return false;
            }

            var propertyType = propertyInfo.PropertyType.UnwrapNullableType();

            return _propertyTypes.Contains(propertyType)
                   || propertyType.GetTypeInfo().IsEnum;
        }
    }
}
