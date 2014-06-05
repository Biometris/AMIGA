using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
            var filePath = Path.GetDirectoryName(_projectFilename);
            var projectName = Path.GetFileNameWithoutExtension(_projectFilename);

            var inputGenerator = new PowerAnalysisInputGenerator();

            // Create power analysis settings file
            var settingsFilename = Path.Combine(filePath, "PowerAnalysisSettings.csv");
            inputGenerator.PowerCalculationSettingsToCsv(_project.PowerCalculationSettings, settingsFilename);

            // Create input files for power analysis
            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress((int)(33 * i), string.Format("compiling analysis input for comparison {0} of {1}...", i + 1, comparisons.Count()));
                var comparison = comparisons.ElementAt(i);
                var comparisonRecords = inputGenerator.GetComparisonInputPowerAnalysisRecords(comparison);
                comparisonRecords.ForEach(r => r.ComparisonId = i);
                var comparisonFilename = Path.Combine(filePath, string.Format("{0}-{1}.csv", projectName, i));
                inputGenerator.PowerAnalysisInputToCsv(comparison.Endpoint, comparisonRecords, comparisonFilename);
            }

            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress((int)(33 + 33 * i / 3D), string.Format("running analysis for comparison {0} of {1}...", i + 1, comparisons.Count()));

                var comparisonInputFilename = Path.Combine(filePath, string.Format("{0}-{1}.csv", projectName, i));
                var comparisonOutputFilename = Path.Combine(filePath, string.Format("{0}-{1}-Output.csv", projectName, i));
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = @"C:\Program Files\Gen16ed\Bin\GenBatch.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = string.Format("in=\"..\\AmigaPowerAnalysis.gen\" in2=\"{0}\" out=\"{1}\" out2=\"{2}\" /D=\"{3}\"", comparisonInputFilename, settingsFilename, comparisonOutputFilename, filePath);
                try {
                    using (Process exeProcess = Process.Start(startInfo)) {
                        exeProcess.WaitForExit();
                    }
                } catch {
                    // TODO: Log error.
                }
            }

            // Create output files for power analysis
            var outputReader = new PowerAnalysisOutputReader();
            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress((int)(66 + 34 * i / 3D), string.Format("reading analysis output for comparison {0} of {1}...", i + 1, comparisons.Count()));
                var comparison = comparisons.ElementAt(i);
                var comparisonFilename = Path.Combine(filePath, string.Format("{0}-{1}-Output.csv", projectName, i));
                comparison.OutputPowerAnalysis = outputReader.ReadOutputPowerAnalysis(comparisonFilename);
            }
        }

        private void progressChanged(object sender, ProgressChangedEventArgs e) {
            progressBarCurrentProgress.Value = e.ProgressPercentage;
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
    }
}
