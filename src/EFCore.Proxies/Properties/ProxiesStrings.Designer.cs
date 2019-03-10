// <auto-generated />

using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Internal
{
    /// <summary>
    ///		This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class ProxiesStrings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Microsoft.EntityFrameworkCore.Properties.ProxiesStrings", typeof(ProxiesStrings).GetTypeInfo().Assembly);

        /// <summary>
        ///     UseLazyLoadingProxies requires AddEntityFrameworkProxies to be called on the internal service provider used.
        /// </summary>
        public static string ProxyServicesMissing
            => GetString("ProxyServicesMissing");

        /// <summary>
        ///     Entity type '{entityType}' is sealed. UseLazyLoadingProxies requires all entity types to be public, unsealed, have virtual navigation properties, and have a public or protected constructor.
        /// </summary>
        public static string ItsASeal([CanBeNull] object entityType)
            => string.Format(
                GetString("ItsASeal", nameof(entityType)),
                entityType);

        /// <summary>
        ///     Navigation property '{navigation}' on entity type '{entityType}' is not virtual. UseLazyLoadingProxies requires all entity types to be public, unsealed, have virtual navigation properties, and have a public or protected constructor.
        /// </summary>
        public static string NonVirtualNavigation([CanBeNull] object navigation, [CanBeNull] object entityType)
            => string.Format(
                GetString("NonVirtualNavigation", nameof(navigation), nameof(entityType)),
                navigation, entityType);

        /// <summary>
        ///     Navigation property '{navigation}' on entity type '{entityType}' is mapped without a CLR property. UseLazyLoadingProxies requires all entity types to be public, unsealed, have virtual navigation properties, and have a public or protected constructor.
        /// </summary>
        public static string FieldNavigation([CanBeNull] object navigation, [CanBeNull] object entityType)
            => string.Format(
                GetString("FieldNavigation", nameof(navigation), nameof(entityType)),
                navigation, entityType);

        /// <summary>
        ///     Unable to create proxy for '{entityType}' because proxies are not enabled. Call 'DbContextOptionsBuilder.UseLazyLoadingProxies' to enable lazy-loading proxies.
        /// </summary>
        public static string ProxiesNotEnabled([CanBeNull] object entityType)
            => string.Format(
                GetString("ProxiesNotEnabled", nameof(entityType)),
                entityType);

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);
            for (var i = 0; i < formatterNames.Length; i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }

            return value;
        }
    }
}
