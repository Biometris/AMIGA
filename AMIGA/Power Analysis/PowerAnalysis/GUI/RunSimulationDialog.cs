using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class RunSimulationDialog : Form {

        private BackgroundWorker _powerAnalysisBackgroundWorker;

        private Project _project;
        private string _projectFilename;

        public RunSimulationDialog(Project project, string projectFilename) {
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

            // Create input files for power analysis
            var inputGenerator = new PowerAnalysisInputGenerator();
            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress(i, string.Format("compiling analysis input for comparison {0} of {1}...", i+1, comparisons.Count()));
                var comparison = comparisons.ElementAt(i);
                var comparisonRecords = inputGenerator.GetComparisonInputPowerAnalysisRecords(comparison);
                comparisonRecords.ForEach(r => r.ComparisonId = i);
                var comparisonFilename = Path.Combine(filePath, string.Format("{0}-{1}.csv", projectName, i));
                inputGenerator.PowerAnalysisInputToCsv(comparison.Endpoint, comparisonRecords, comparisonFilename);
            }

            // TODO: power analysis (preferably per comparison)
            for (int i = 1; i <= 100; i++) {
                Thread.Sleep(10);
                _powerAnalysisBackgroundWorker.ReportProgress(i);
            }

            // Create output files for power analysis
            var outputReader = new PowerAnalysisOutputReader();
            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress(i, string.Format("reading analysis output for comparison {0} of {1}...", i + 1, comparisons.Count()));
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
            MessageBox.Show("Done");
        }
    }
}
