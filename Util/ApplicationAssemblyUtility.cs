using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Pdam.Common.Shared.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationAssemblyUtility
    {
        private static readonly Lazy<Assembly> LazyApplicationAssembly = new(GetApplicationAssembly, LazyThreadSafetyMode.ExecutionAndPublication);
        [Obsolete("Obsolete")] private static readonly Lazy<string> LazyApplicationBinFolder = new(GetApplicationBinFolder, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// 
        /// </summary>
        public static Assembly ApplicationAssembly => LazyApplicationAssembly.Value;

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("Obsolete")]
        public static string ApplicationBinFolder => LazyApplicationBinFolder.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationVersionNumber()
        {
            return ApplicationAssembly.GetName().Version!.ToString(3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAssemblyVersionFromType(Type type)
        {
            return type.Assembly.GetName().Version!.ToString(3);
        }

        /// <summary>
        /// Returns true if the current application assembly is built in Debug mode.
        /// </summary>
        public static bool ApplicationIsDebugBuild()
        {
            return AssemblyIsDebugBuild(ApplicationAssembly);
        }

        /// <summary>
        /// Checks for the DebuggableAttribute on the assembly provided to determine
        /// whether it has been built in Debug mode.
        /// </summary>
        private static bool AssemblyIsDebugBuild(Assembly assembly)
        {
            return assembly
                .GetCustomAttributes(false)
                .OfType<DebuggableAttribute>()
                .Select(attr => attr.IsJITTrackingEnabled)
                .FirstOrDefault();
        }

        [Obsolete("Obsolete")]
        private static string GetApplicationBinFolder()
        {
            var codeBase = ApplicationAssembly.CodeBase;
            var uri = new UriBuilder(codeBase ?? throw new InvalidOperationException());
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path) ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete("Obsolete")]
        public static DateTime GetBuildDateTime()
        {
            var codeBase = ApplicationAssembly.CodeBase;
            var uri = new UriBuilder(codeBase ?? throw new InvalidOperationException());
            var path = Uri.UnescapeDataString(uri.Path);
            return File.GetLastWriteTime(path);
        }

        static Assembly GetApplicationAssembly()
        {
            // Are we in a web application?
            //if (HttpContext != null)
            //{
            //    // Get the global application type
            //    var globalAsax = BuildManager.GetGlobalAsaxType();
            //    if (globalAsax != null && globalAsax.BaseType != null) return globalAsax.BaseType.Assembly;
            //}

            // Provide entry assembly and fallback to executing assembly
            return Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        }
    }
}
