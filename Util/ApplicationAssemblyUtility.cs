using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Pdam.Common.Shared.Util
{
    public class ApplicationAssemblyUtility
    {
        static readonly Lazy<Assembly> LazyApplicationAssembly = new Lazy<Assembly>(GetApplicationAssembly, LazyThreadSafetyMode.ExecutionAndPublication);
        static readonly Lazy<string> LazyApplicationBinFolder = new Lazy<string>(GetApplicationBinFolder, LazyThreadSafetyMode.ExecutionAndPublication);

        public static Assembly ApplicationAssembly
        {
            get { return LazyApplicationAssembly.Value; }
        }

        public static string ApplicationBinFolder
        {
            get { return LazyApplicationBinFolder.Value; }
        }

        public static string GetApplicationVersionNumber()
        {
            return ApplicationAssembly.GetName().Version.ToString(3);
        }

        public static string GetAssemblyVersionFromType(Type type)
        {
            return type.Assembly.GetName().Version.ToString(3);
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
        public static bool AssemblyIsDebugBuild(Assembly assembly)
        {
            return assembly
                .GetCustomAttributes(false)
                .OfType<DebuggableAttribute>()
                .Select(attr => attr.IsJITTrackingEnabled)
                .FirstOrDefault();
        }

        static string GetApplicationBinFolder()
        {
            var codeBase = ApplicationAssembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public static DateTime GetBuildDateTime()
        {
            var codeBase = ApplicationAssembly.CodeBase;
            var uri = new UriBuilder(codeBase);
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
