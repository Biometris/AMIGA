using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using AmigaPowerAnalysis.GUI;

namespace AmigaPowerAnalysis {
    static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args == null || args.Length == 0) {
                Application.Run(new MainWindow());
            } else {
                Application.Run(new MainWindow(args[0]));
            }
        }
    }
}
