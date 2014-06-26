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
            
            var filesPath = Path.Combine(projectPath, projectName);
            if (!Directory.Exists(filesPath)) {
                Directory.CreateDirectory(filesPath);
            }

            var inputGenerator = new PowerAnalysisInputGenerator();
            var outputReader = new PowerAnalysisOutputReader();

            var numberOfComparisons = comparisons.Count();
            var progressStep = 100D / numberOfComparisons;


            for (int i = 0; i < comparisons.Count(); ++i) {
                try {
                    var comparisonInputFilename = Path.Combine(filesPath, string.Format("{0}-{1}.csv", projectName, i));
                    var comparisonOutputFilename = Path.Combine(filesPath, string.Format("{0}-{1}-Output.csv", projectName, i));
                    var comparisonLogFilename = Path.Combine(filesPath, string.Format("{0}-{1}.log", projectName, i));

                    // Create input file for power analysis
                    _powerAnalysisBackgroundWorker.ReportProgress((int)(i * progressStep), string.Format("compiling analysis input for comparison {0} of {1}...", i + 1, comparisons.Count()));
                    var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparisons.ElementAt(i), _project.DesignSettings, _project.PowerCalculationSettings, i);
                    inputGenerator.PowerAnalysisInputToCsv(inputPowerAnalysis, comparisonInputFilename);

                    // Run power analysis
                    _powerAnalysisBackgroundWorker.ReportProgress((int)(i * progressStep), string.Format("running analysis for comparison {0} of {1}...", i + 1, comparisons.Count()));
                    var startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = @"C:\Program Files\Gen16ed\Bin\GenBatch.exe";
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = string.Format("in=\"{0}\" in2=\"{1}\" in3=\"{2}\" out=\"{3}\" out2=\"{4}\"", scriptFilename, lylesScriptFilename, comparisonInputFilename, comparisonLogFilename, comparisonOutputFilename);
                    using (Process exeProcess = Process.Start(startInfo)) {
                        exeProcess.WaitForExit();
                    }

                    // Read output file of power analysis
                    _powerAnalysisBackgroundWorker.ReportProgress((int)(i * progressStep), string.Format("reading analysis output for comparison {0} of {1}...", i + 1, comparisons.Count()));
                    var comparison = comparisons.ElementAt(i);
                    comparison.OutputPowerAnalysis = outputReader.ReadOutputPowerAnalysis(comparisonOutputFilename);
                    comparison.OutputPowerAnalysis.InputPowerAnalysis = inputPowerAnalysis;

                } catch {
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
    }
}
