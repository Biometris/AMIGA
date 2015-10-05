using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using Biometris.ExtensionMethods;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisResultsPerComparisonPanel : UserControl, ISelectionForm {

        #region Private properties

        private Project _project;
        private string _currentOutputName; 
        private ResultPowerAnalysis _resultPowerAnalysis;
        private OutputPowerAnalysis _currentComparisonAnalysisResult;
        private AnalysisPlotType _currentPlotType;

        #endregion

        public AnalysisResultsPerComparisonPanel(Project project) {
            InitializeComponent();
            Name = "Results per endpoint";
            Description = "Choose endpoint in table. Choose method of analysis if more have been investigated. Power is shown for difference tests or equivalence tests, and as a function of the number of replicates or the Ratio Test/Comp (on a ln scale).\r\nNote: Number of plots in design is Number of replicates times Number of plots per block";
            _project = project;
            _currentPlotType = AnalysisPlotType.Ratio;
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridViewComparisons_EditingControlShowing);
        }

        public event EventHandler TabVisibilitiesChanged;

        public string Description { get; private set; }

        public string CurrentProjectFilesPath { get; set; }

        public string CurrentOutputFilesPath {
            get {
                if (_resultPowerAnalysis.GetPrimaryComparisons().Count() > 0) {
                    return Path.Combine(CurrentProjectFilesPath, _project.PrimaryOutput);
                }
                return null;
            }
        }

        public void Activate() {
            updatePlotTypeRadioButtons();
            updateDataGridComparisons();
            if (_resultPowerAnalysis.GetPrimaryComparisons().Count() > 0) {
                splitContainerComparisons.Visible = true;
            } else {
                splitContainerComparisons.Visible = false;
            }
        }

        public bool IsVisible() {
            return _project != null && CurrentProjectFilesPath != null && _project.PrimaryOutputExists(CurrentProjectFilesPath);
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            _currentOutputName = _project.PrimaryOutput;
            _resultPowerAnalysis = _project.GetPrimaryOutput(CurrentProjectFilesPath);
            labelOutputName.Text = _currentOutputName;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Endpoint";
            column.Name = "Endpoint";
            column.HeaderText = "Endpoint";
            column.ValueType = typeof(string);
            column.ReadOnly = true;
            dataGridViewComparisons.Columns.Add(column);

            var _availableAnalysisMethodTypesDifferenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults
                .SelectMany(r => r.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags()).Distinct().ToList();

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableAnalysisMethodTypesDifferenceTests;
            combo.DataPropertyName = "AnalysisMethodDifferenceTest";
            combo.ValueType = typeof(AnalysisMethodType);
            combo.HeaderText = "Difference test";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            var _availableAnalysisMethodTypesEquivalenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults
                .SelectMany(r => r.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags()).Distinct().ToList();

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableAnalysisMethodTypesEquivalenceTests;
            combo.DataPropertyName = "AnalysisMethodEquivalenceTest";
            combo.ValueType = typeof(AnalysisMethodType);
            combo.HeaderText = "Equivalence test";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            if (_resultPowerAnalysis.ComparisonPowerAnalysisResults.Count > 0) {
                var comparisonsBindingSouce = new BindingSource(_resultPowerAnalysis.ComparisonPowerAnalysisResults, null);
                dataGridViewComparisons.DataSource = comparisonsBindingSouce;
                dataGridViewComparisons.Update();
            } else {
                dataGridViewComparisons.DataSource = null;
                dataGridViewComparisons.Update();
            }
        }

        private ComboBox _currentComboBox;

        void dataGridViewComparisons_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            if (e.Control is ComboBox) {
                _currentComboBox = (ComboBox)e.Control;
                if (_currentComboBox != null) {
                    _currentComboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
                }
            }
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e) {
            this.BeginInvoke(new MethodInvoker(EndEdit));
        }

        void EndEdit() {
            if (_currentComboBox != null) {
                this.dataGridViewComparisons.EndEdit();
            }
        }

        private void updateAnalysisOutputPanel() {
            if (_currentComparisonAnalysisResult != null) {
                if (tabControlEndpointResult.SelectedIndex < 2) {
                    updateAnalysisCharts();
                } else if (tabControlEndpointResult.SelectedIndex == 2) {
                    updateSettingsReport();
                } else if (tabControlEndpointResult.SelectedIndex == 3) {
                    updateFullReport();
                }
            }
        }

        private void updateAnalysisCharts() {
            var plotType = _currentPlotType;
            if (plotType == AnalysisPlotType.Replicates) {
                plotViewDifference.Model = PowerVersusReplicatesRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, TestType.Difference, _currentComparisonAnalysisResult.AnalysisMethodDifferenceTest);
                plotViewEquivalence.Model = PowerVersusReplicatesRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, TestType.Equivalence, _currentComparisonAnalysisResult.AnalysisMethodEquivalenceTest);
            } else if (plotType == AnalysisPlotType.Ratio) {
                plotViewDifference.Model = PowerVersusRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, TestType.Difference, _currentComparisonAnalysisResult.AnalysisMethodDifferenceTest, _currentComparisonAnalysisResult.InputPowerAnalysis.MeasurementType, _currentComparisonAnalysisResult.InputPowerAnalysis.NumberOfReplications);
                plotViewEquivalence.Model = PowerVersusRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, TestType.Equivalence, _currentComparisonAnalysisResult.AnalysisMethodEquivalenceTest, _currentComparisonAnalysisResult.InputPowerAnalysis.MeasurementType, _currentComparisonAnalysisResult.InputPowerAnalysis.NumberOfReplications);
            }
        }

        private void updateSettingsReport() {
            if (_currentComparisonAnalysisResult != null && _currentComparisonAnalysisResult != null) {
                if (webBrowserSettingsReport.Document == null) {
                    webBrowserSettingsReport.Navigate("about:blank");
                }
                var doc = webBrowserSettingsReport.Document.OpenNew(true);
                var reportGenerator = new ComparisonSettingsGenerator(_currentComparisonAnalysisResult, CurrentOutputFilesPath);
                var html = reportGenerator.Generate(true);
                doc.Write(html);
                doc.Title = "Settings report";
            }
        }

        private void updateFullReport() {
            if (_currentComparisonAnalysisResult != null && _currentComparisonAnalysisResult != null) {
                if (webBrowserFullReport.Document == null) {
                    webBrowserFullReport.Navigate("about:blank");
                }
                var doc = webBrowserFullReport.Document.OpenNew(true);
                var reportGenerator = new SingleComparisonReportGenerator(_resultPowerAnalysis, _currentComparisonAnalysisResult, _currentOutputName, CurrentOutputFilesPath);
                var html = reportGenerator.Generate(true);
                doc.Write(html);
                doc.Title = "Full report";
            }
        }

        private void updatePlotTypeRadioButtons() {
            radioButtonRatioDifference.Checked = _currentPlotType == AnalysisPlotType.Ratio;
            radioButtonRatioEquivalence.Checked = _currentPlotType == AnalysisPlotType.Ratio;
            radioButtonReplicatesDifference.Checked = _currentPlotType == AnalysisPlotType.Replicates;
            radioButtonReplicatesEquivalence.Checked = _currentPlotType == AnalysisPlotType.Replicates;
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewComparisons.CurrentRow.Index < _resultPowerAnalysis.ComparisonPowerAnalysisResults.Count()) {
                _currentComparisonAnalysisResult = _resultPowerAnalysis.ComparisonPowerAnalysisResults.ElementAt(dataGridViewComparisons.CurrentRow.Index);
            }
            else {
                _currentComparisonAnalysisResult = null;
            }
            updateAnalysisOutputPanel();
        }

        private void dataGridViewComparisons_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void radioButtonRatioDifference_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonRatioDifference.Checked ? AnalysisPlotType.Ratio : AnalysisPlotType.Replicates;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void radioButtonReplicatesDifference_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonReplicatesDifference.Checked ? AnalysisPlotType.Replicates : AnalysisPlotType.Ratio;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void radioButtonRatioEquivalence_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonRatioEquivalence.Checked ? AnalysisPlotType.Ratio : AnalysisPlotType.Replicates;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void radioButtonReplicatesEquivalence_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonReplicatesEquivalence.Checked ? AnalysisPlotType.Replicates : AnalysisPlotType.Ratio;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void tabControlEndpointResult_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void toolStripButtonExportPdf_Click(object sender, EventArgs e) {
            var title = "Report " + _currentComparisonAnalysisResult.InputPowerAnalysis.Endpoint;
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".pdf";
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = title.ReplaceInvalidChars("_");
            saveFileDialog.InitialDirectory = CurrentOutputFilesPath;
            if (saveFileDialog.FileName.Length == 0) {
                saveFileDialog.FileName = "unknown";
            }
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var filenamePdf = saveFileDialog.FileName;
                var reportGenerator = new SingleComparisonReportGenerator(_resultPowerAnalysis, _currentComparisonAnalysisResult, _currentOutputName, CurrentOutputFilesPath);
                reportGenerator.SaveAsPdf(filenamePdf);
                Process.Start(filenamePdf);
            }
        }
    }
}
