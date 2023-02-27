using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ApplicationUtilities;
using Biometris.ExtensionMethods;
using Biometris.ProgressReporting;
using Biometris.R.REngines;

namespace AmigaPowerAnalysis.GUI {
    public partial class RunPowerAnalysisDialog : Form {

        private Project _project;
        private string _projectFilename;
        private bool _runComplete;
        private CancellationTokenSource _cancellationTokenSource;

        public RunPowerAnalysisDialog(Project project, string projectFilename) {
            _project = project;
            _projectFilename = projectFilename;
            InitializeComponent();
        }

        public bool RunComplete {
            get {
                return _runComplete;
            }
        }

        private async void RunSimulationDialog_Load(object sender, EventArgs e) {
            try {
                _cancellationTokenSource = new CancellationTokenSource();
                var progressReporter = new ProgressReporter(p => {
                    labelCurrentActivity.Text = p.CurrentActivity;
                    progressBarCurrentProgress.Value = (int)p.Progress;
                    labelElapsedValue.Text = string.Format("{0:hh\\:mm\\:ss}", p.Elapsed);
                    labelRemainingValue.Text = (p.Remaining == TimeSpan.MaxValue) ? "--:--:--" : string.Format("{0:hh\\:mm\\:ss}", p.Remaining);
                }, _cancellationTokenSource.Token);
                await runSimulation(progressReporter.ProgressReport);
            } catch (OperationCanceledException) {
            } catch (RNotFoundException) {
                showError("Cannot find R on this computer", string.Format("Cannot find R on this computer. Consult the user manual and follow the instructions on how to install R."));
            } catch (RLoadLibraryException ex) {
                showError("Failed to load R package", string.Format("Failed to load R package. {0} Consult the user manual and follow the instructions on how to install these packages manually.", ex.Message));
            } catch (Exception ex) {
                showError("Power analysis error", string.Format("An error occurred while executing the power analysis simulation. Message: {0}", ex.Message));
            } finally {
                Close();
            }
        }

        private async Task runSimulation(CompositeProgressState progressReport) {
            _project.ValidateAnalysisSettings();
            _project.ClearProjectOutput();
            var comparisons = _project.Endpoints.ToList();
            var projectPath = Path.GetDirectoryName(_projectFilename);
            var projectName = Path.GetFileNameWithoutExtension(_projectFilename);
            var runId = string.Format("{0} - {1:yyyy-MM-dd-HH-mm-ss}", projectName, DateTime.Now);
            var filesPath = Path.Combine(projectPath, projectName, runId);
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
            var resultPowerAnalysis = new ResultPowerAnalysis();
            var inputGenerator = new PowerAnalysisInputGenerator();
            var powerAnalysisExecuter = new RDotNetPowerAnalysisExecuter(filesPath);
            var numberOfComparisons = comparisons.Count();
            var progressStep = 100D / numberOfComparisons;
            for (int i = 0; i < comparisons.Count; ++i) {
                var localProgress = progressReport.NewProgressState(100D / comparisons.Count());
                localProgress.Update(string.Format("Running power analysis for comparison {0} of {1}...", i + 1, comparisons.Count()));
                var inputPowerAnalysis = inputGenerator.CreateInputPowerAnalysis(comparisons.ElementAt(i), _project.DesignSettings, _project.PowerCalculationSettings, i, numberOfComparisons, _project.UseBlockModifier, _project.ProjectName);
                var output = await powerAnalysisExecuter.RunAsync(inputPowerAnalysis, localProgress);
                resultPowerAnalysis.ComparisonPowerAnalysisResults.Add(output);
                localProgress.Update(100);
            }
            if (resultPowerAnalysis.ComparisonPowerAnalysisResults.Any(r => !r.Success)) {
                showWarning("Warning", "Power Analysis completed with errors. Some results may be incomplete or non existent.");
            }
            resultPowerAnalysis.OuputTimeStamp = DateTime.Now;
            resultPowerAnalysis.Version = ApplicationUtils.GetApplicationVersion();
            resultPowerAnalysis.ToXmlFile(filesPath + ".xml");
            _project.PrimaryOutputId = runId;
            _project.LoadPrimaryOutput(Path.Combine(projectPath, projectName));
            _runComplete = true;
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
