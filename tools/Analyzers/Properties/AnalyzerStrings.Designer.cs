// <auto-generated />
namespace Microsoft.Data.Entity.Internal
{
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using JetBrains.Annotations;

    public static class AnalyzerStrings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("Analyzers.AnalyzerStrings", typeof(AnalyzerStrings).GetTypeInfo().Assembly);

        /// <summary>
        /// Add GenericMethodFactory attribute
        /// </summary>
        public static string AddAttribute
        {
            get { return GetString("AddAttribute"); }
        }

        /// <summary>
        /// Require GenericMethodFactoryAttribute when System.Reflection.MethodInfo.MakeGenericMethod is used.
        /// </summary>
        public static string AnalyzerTitle
        {
            get { return GetString("AnalyzerTitle"); }
        }

        /// <summary>
        /// disable {methodName} inspection
        /// </summary>
        public static string DisableInspectionComment([CanBeNull] object methodName)
        {
            return string.Format(CultureInfo.CurrentCulture, GetString("DisableInspectionComment", "methodName"), methodName);
        }

        /// <summary>
        /// Ignore inspection with comment
        /// </summary>
        public static string IgnoreInspection
        {
            get { return GetString("IgnoreInspection"); }
        }

        /// <summary>
        /// Unsafe usage of MethodInfo.MakeGenericMethod. Add GenericMethodFactoryAttribute to describe the usage of MakeGenericMethod
        /// </summary>
        public static string UnsafeMakeGenericMethod
        {
            get { return GetString("UnsafeMakeGenericMethod"); }
        }

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);

            Debug.Assert(value != null);

            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }

            return value;
        }
    }
}
