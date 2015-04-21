using System.IO;
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
    }
}
