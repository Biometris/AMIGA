using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Biometris.ApplicationUtilities {

    public static class ApplicationUtils {

        /// <summary>
        /// Determines the application's temp path, creates it if it doesn't exist
        /// and returns it.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationTempPath() {
            var tempdir = Path.Combine(Path.GetTempPath(), Application.ProductName);
            if (!Directory.Exists(tempdir)) {
                Directory.CreateDirectory(tempdir);
            }
            return tempdir;
        }

        /// <summary>
        /// Determines the application's temp path, creates it if it doesn't exist
        /// and returns it.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationDataPath() {
            var appDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName);
            if (!Directory.Exists(appDir)) {
                Directory.CreateDirectory(appDir);
            }
            return appDir;
        }

        /// <summary>
        /// Clears the application's temp path.
        /// </summary>
        public static void ClearApplicationTempPath() {
            var tempPathDirectoryInfo = new DirectoryInfo(GetApplicationTempPath());
            foreach (FileInfo file in tempPathDirectoryInfo.GetFiles()) {
                file.Delete();
            }
            foreach (DirectoryInfo dir in tempPathDirectoryInfo.GetDirectories()) {
                dir.Delete(true);
            }
        }

        /// <summary>
        /// Gets the entry assembly version number.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationVersion() {
            var assembly = Assembly.GetEntryAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return versionInfo.FileVersion;
        }
    }
}
