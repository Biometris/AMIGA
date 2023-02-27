using RDotNet.NativeLibrary;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class SettingsForm : Form {

        private string _genstatPath;
        private string _pathR;
        private string _pathRHome;

        public SettingsForm() {
            InitializeComponent();
            _genstatPath = Properties.Settings.Default.GenstatPath;
            _pathR = Properties.Settings.Default.RPath;
            _pathRHome = Properties.Settings.Default.RHome;
            if (string.IsNullOrEmpty(_pathRHome)) {
                var nativeUtils = new NativeUtility();
                var rDll = nativeUtils.FindRPath();
                if (!string.IsNullOrEmpty(rDll)) {
                    _pathRHome = new DirectoryInfo(rDll)
                        .Parent.Parent
                        .FullName;
                    _pathR = Path.Combine(_pathRHome, @"bin\RScript.exe");
                }
            }
            textBoxPathGenstat.Text = _genstatPath;
            textBoxPathR.Text = _pathR;
            textBoxPathRHome.Text = _pathRHome;
        }

        private void buttonBrowseGenstatExecutable_Click(object sender, EventArgs e) {
            var openFileDialog = new OpenFileDialog();
            var currentGenstatPath = Properties.Settings.Default.GenstatPath;
            if (string.IsNullOrEmpty(currentGenstatPath)) {
                var defaultGenstatDirective = @"C:\Program Files\Gen16ed\Bin\GenBatch.exe";
                var defaultGenstatDirectory = Path.GetDirectoryName(defaultGenstatDirective);
                openFileDialog.InitialDirectory = Directory.Exists(defaultGenstatDirectory) ? defaultGenstatDirectory : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            } else {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(currentGenstatPath);
            }
            openFileDialog.Filter = "exe files (*.exe)| *.exe";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                var newGenstatPath = openFileDialog.FileName;
                if (File.Exists(newGenstatPath)) {
                    _genstatPath = newGenstatPath;
                    textBoxPathGenstat.Text = _genstatPath;
                } else {
                    showError("Invalid path", "The provided path is not valid.");
                }
            }
        }

        private void buttonBrowseExecutableR_Click(object sender, EventArgs e) {
            var openFileDialog = new OpenFileDialog();
            var currentPathR = Properties.Settings.Default.RPath;
            if (string.IsNullOrEmpty(currentPathR)) {
                var defaultPathR = @"C:\Program Files\R";
                openFileDialog.InitialDirectory = Directory.Exists(defaultPathR) ? defaultPathR : Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            } else {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(currentPathR);
            }
            openFileDialog.Filter = "exe files (*.exe)| *.exe";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                var newPathR = openFileDialog.FileName;
                if (!newPathR.EndsWith("Rscript.exe", StringComparison.InvariantCultureIgnoreCase)) {
                    showError("Invalid path", "Please specify the full path to the RScript executable.");
                } else if (!File.Exists(newPathR)) {
                    showError("Invalid path", "The provided path is not valid.");
                } else {
                    _pathR = newPathR;
                    _pathRHome = new DirectoryInfo(newPathR)
                        .Parent.Parent
                        .FullName;
                    textBoxPathR.Text = _pathR;
                    textBoxPathRHome.Text = _pathRHome;
                }
            }
        }

        private void showError(string title, string message) {
            MessageBox.Show(
                    message,
                    title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            if (File.Exists(_genstatPath)) {
                Properties.Settings.Default.GenstatPath = _genstatPath;
            }
            if (File.Exists(_pathR)) {
                Properties.Settings.Default.RPath = _pathR;
            }
            if (Directory.Exists(_pathRHome)) {
                Properties.Settings.Default.RHome = _pathRHome;
            }
            Properties.Settings.Default.Save();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
