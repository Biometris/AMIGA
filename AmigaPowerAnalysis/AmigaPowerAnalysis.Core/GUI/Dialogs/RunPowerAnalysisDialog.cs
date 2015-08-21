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
using Biometris.ProgressReporting;

namespace AmigaPowerAnalysis.GUI {
    public partial class RunPowerAnalysisDialog : Form {

        private Project _project;
        private string _projectFilename;
        private CancellationTokenSource _cancellationTokenSource;

        public RunPowerAnalysisDialog(Project project, string projectFilename) {
            _project = project;
            _projectFilename = projectFilename;
            InitializeComponent();
        }

        private async void RunSimulationDialog_Load(object sender, EventArgs e) {
            try {
                _cancellationTokenSource = new CancellationTokenSource();
                var progressReporter = new ProgressReporter(p => {
                    labelCurrentActivity.Text = p.CurrentActivity;
                    progressBarCurrentProgress.Value = (int)p.Progress;
                }, _cancellationTokenSource.Token);
                await runSimulation(progressReporter.ProgressReport);
            } catch (OperationCanceledException) {
            } catch (Exception ex) {
                showError("Power analysis error", string.Format("An error occurred while executing the power analysis simulation. Message: {0}", ex.Message));
            } finally {
                Close();
            }
        }

        private async Task runSimulation(CompositeProgressState progressReport) {
            _project.ClearProjectOutput();
            var comparisons = _project.GetComparisons();
            var projectPath = Path.GetDirectoryName(_projectFilename);
            var projectName = Path.GetFileNameWithoutExtension(_projectFilename);
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
            //var powerAnalysisExecuter = new RPowerAnalysisExecuter(filesPath);
            var powerAnalysisExecuter = new RDotNetPowerAnalysisExecuter(filesPath);

            var numberOfComparisons = comparisons.Count();
            var progressStep = 100D / numberOfComparisons;


            for (int i = 0; i < comparisons.Count(); ++i) {
                var localProgress = progressReport.NewProgressState(100D / comparisons.Count());
                localProgress.Update(string.Format("Running power analysis for comparison {0} of {1}...", i + 1, comparisons.Count()));
                var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparisons.ElementAt(i), _project.DesignSettings, _project.PowerCalculationSettings, i, numberOfComparisons);
                comparisons.ElementAt(i).OutputPowerAnalysis = await powerAnalysisExecuter.RunAsync(inputPowerAnalysis, localProgress);
                localProgress.Update(100);
            }
            if (comparisons.Any(r => !r.OutputPowerAnalysis.Success)) {
                showWarning("Warning", "Power Analysis completed with errors. Some results may be incomplete or non existent.");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            if (_cancellationTokenSource != null) {
                _cancellationTokenSource.Cancel();
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

        private void showWarning(string title, string message) {
            MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
        }
    }
}
