using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class SettingsForm : Form {

        private string _genstatPath;

        public SettingsForm() {
            InitializeComponent();
            _genstatPath = Properties.Settings.Default.GenstatPath;
            textBoxGenstatPath.Text = _genstatPath;
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
                    textBoxGenstatPath.Text = _genstatPath;
                } else {
                    showError("Invalid path", "The provided path is not valid.");
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
                Properties.Settings.Default.Save();
            }
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
