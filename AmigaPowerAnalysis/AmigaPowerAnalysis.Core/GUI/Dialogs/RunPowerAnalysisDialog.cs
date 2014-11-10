using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;

namespace AmigaPowerAnalysis.GUI {
    public partial class RunPowerAnalysisDialog : Form {

        private BackgroundWorker _powerAnalysisBackgroundWorker;

        private Project _project;
        private string _projectFilename;

        public RunPowerAnalysisDialog(Project project, string projectFilename) {
            _project = project;
            _projectFilename = projectFilename;
            InitializeComponent();
        }

        private void RunSimulationDialog_Load(object sender, EventArgs e) {
            _powerAnalysisBackgroundWorker = new BackgroundWorker();
            _powerAnalysisBackgroundWorker.WorkerReportsProgress = true;
            _powerAnalysisBackgroundWorker.DoWork += new DoWorkEventHandler(doWork);
            _powerAnalysisBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(runWorkerCompleted);
            _powerAnalysisBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(progressChanged);
            _powerAnalysisBackgroundWorker.RunWorkerAsync();
        }

        private void doWork(object sender, DoWorkEventArgs e) {
            var comparisons = _project.GetComparisons();
            var projectPath = Path.GetDirectoryName(_projectFilename);
            var projectName = Path.GetFileNameWithoutExtension(_projectFilename);

            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var scriptsDirectory = string.Format("{0}\\Resources", applicationDirectory);
            var scriptFilename = string.Format("{0}\\AmigaPowerAnalysis.gen", scriptsDirectory);
            var lylesScriptFilename = string.Format("{0}\\Lyles.pro", scriptsDirectory);

            var genstatPath = Properties.Settings.Default.GenstatPath;
            if (string.IsNullOrEmpty(genstatPath) || !File.Exists(genstatPath)) {
                showError("Cannot find GenStat path", "The GenStat executable GenBatch.exe cannot be found. Please go to options -> settings to specify this path.");
                return;
            }

            var filesPath = Path.Combine(projectPath, projectName);
            if (!Directory.Exists(filesPath)) {
                Directory.CreateDirectory(filesPath);
            } else {
                try {
                    Directory.Delete(filesPath, true);
                    Thread.Sleep(100);
                    Directory.CreateDirectory(filesPath);
                } catch (Exception ex) {
                    var msg = ex.Message;
                }
            }

            var inputGenerator = new PowerAnalysisInputGenerator();
            //var powerAnalysisExecuter = new GenstatPowerAnalysisExecuter(filesPath);
            var powerAnalysisExecuter = new RPowerAnalysisExecuter(filesPath);

            var numberOfComparisons = comparisons.Count();
            var progressStep = 100D / numberOfComparisons;

            for (int i = 0; i < comparisons.Count(); ++i) {
                try {
                    _powerAnalysisBackgroundWorker.ReportProgress((int)(i * progressStep), string.Format("Running power analysis for comparison {0} of {1}...", i + 1, comparisons.Count()));
                    var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparisons.ElementAt(i), _project.DesignSettings, _project.PowerCalculationSettings, i);
                    comparisons.ElementAt(i).OutputPowerAnalysis = powerAnalysisExecuter.RunAnalysis(inputPowerAnalysis);
                } catch (Exception ex) {
                    showError("Power analysis error", string.Format("An error occurred while executing the power analysis simulation. Message: {0}", ex.Message));
                    return;
                    // TODO: Log error.
                }
            }
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e) {
            if (e.ProgressPercentage <= 100) {
                progressBarCurrentProgress.Value = e.ProgressPercentage;
            } else {
                progressBarCurrentProgress.Value = 100;
            }
            if (e.UserState != null) {
                labelCurrentActivity.Text = "Current activity: " + e.UserState.ToString();
            } else {
                labelCurrentActivity.Text = "Current activity: working...";
            }
        }

        private void runWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.Close();
            //MessageBox.Show("Done");
        }

        private void showError(string title, string message) {
            MessageBox.Show(
                    message,
                    title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
        }
    }
}
