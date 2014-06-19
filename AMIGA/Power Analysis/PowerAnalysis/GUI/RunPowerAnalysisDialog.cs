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
            var filesPath = Path.Combine(projectPath, projectName);
            if (!Directory.Exists(filesPath)) {
                Directory.CreateDirectory(filesPath);
            }

            var inputGenerator = new PowerAnalysisInputGenerator();

            var numberOfComparisons = comparisons.Count();
            var progressStep = 10D / numberOfComparisons;

            // Create input files for power analysis
            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress((int)(i * progressStep), string.Format("compiling analysis input for comparison {0} of {1}...", i + 1, comparisons.Count()));
                var comparison = comparisons.ElementAt(i);
                var comparisonRecords = inputGenerator.GetComparisonInputPowerAnalysisRecords(comparison);
                comparisonRecords.ForEach(r => r.ComparisonId = i);
                var comparisonFilename = Path.Combine(filesPath, string.Format("{0}-{1}.csv", projectName, i));
                inputGenerator.PowerAnalysisInputToCsv(comparison.Endpoint, _project.PowerCalculationSettings, comparisonRecords, comparisonFilename);
            }

            var applicationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            progressStep = 80D / numberOfComparisons;
            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress((int)(10 + i * progressStep), string.Format("running analysis for comparison {0} of {1}...", i + 1, comparisons.Count()));
                var comparisonInputFilename = Path.Combine(filesPath, string.Format("{0}-{1}.csv", projectName, i));
                var comparisonOutputFilename = Path.Combine(filesPath, string.Format("{0}-{1}-Output.csv", projectName, i));
                var logFilename = Path.Combine(filesPath, string.Format("{0}-{1}.log", projectName, i));
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = @"C:\Program Files\Gen16ed\Bin\GenBatch.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.Arguments = string.Format("in=\"{0}\\Resources\\AmigaPowerAnalysis.gen\" in2=\"{1}\" out=\"{2}\" out2=\"{3}\"", applicationDirectory, comparisonInputFilename, logFilename, comparisonOutputFilename);
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
            progressStep = 10D / numberOfComparisons; 
            for (int i = 0; i < comparisons.Count(); ++i) {
                _powerAnalysisBackgroundWorker.ReportProgress((int)(90 + i * progressStep), string.Format("reading analysis output for comparison {0} of {1}...", i + 1, comparisons.Count()));
                var comparison = comparisons.ElementAt(i);
                var comparisonFilename = Path.Combine(filesPath, string.Format("{0}-{1}-Output.csv", projectName, i));
                comparison.OutputPowerAnalysis = outputReader.ReadOutputPowerAnalysis(comparisonFilename);
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
    }
}
