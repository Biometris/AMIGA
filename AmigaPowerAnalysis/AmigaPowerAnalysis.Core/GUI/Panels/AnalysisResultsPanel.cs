using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.DataAnalysis;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using Biometris.ExtensionMethods;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisResultsPanel : UserControl, ISelectionForm {

        #region Private properties

        private Project _project;
        private string _currentOutputName;
        private ResultPowerAnalysis _resultPowerAnalysis;
        private AnalysisPlotType _currentPlotType;
        private List<Enum> _availableAnalysisMethodTypesDifferenceTests;
        private List<Enum> _availableAnalysisMethodTypesEquivalenceTests;

        #endregion

        public AnalysisResultsPanel(Project project) {
            InitializeComponent();
            Name = "Combined results";
            Description = "The power analysis is based on the minimum or mean power across the primary comparisons, in terms of the concern quotient (CQ), (CQ equals 1 at the Limit of Concern).\r\nSelect primary comparisons. Choose method of analysis for difference and equivalence tests if more have been investigated.\r\n\r\nPower is shown for difference tests and equivalence tests, as a function of the CQ or the number of replicates.\r\nFinally, APA can export a data template and an R script for the analysis of the experiment.";
            _project = project;
            _currentPlotType = AnalysisPlotType.ConcernStandardizedDifference;
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridViewComparisons_EditingControlShowing);
        }

        public event EventHandler TabVisibilitiesChanged;

        public string Description { get; private set; }

        public string CurrentProjectFilesPath { get; set; }

        public string CurrentOutputFilesPath {
            get {
                if (_resultPowerAnalysis.GetPrimaryComparisons().Count() > 0) {
                    return Path.Combine(CurrentProjectFilesPath, _project.PrimaryOutputId);
                }
                return null;
            }
        }

        public void Activate() {
            updateDataGridComparisons();
            updateAnalysisOutputPanel();
            updatePlotTypeRadioButtons();
            if (_resultPowerAnalysis.GetPrimaryComparisons().Count() > 0) {
                splitContainerComparisons.Visible = true;
            } else {
                splitContainerComparisons.Visible = false;
            }
            if (_resultPowerAnalysis.PowerAggregationType == PowerAggregationType.AggregateMinimum) {
                radioButtonAggregateMin.Checked = true;
            } else {
                radioButtonAggregateMean.Checked = true;
            }
        }

        public bool IsVisible() {
            return _project != null && _project.HasOutput;
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            _currentOutputName = _project.PrimaryOutputId;
            _resultPowerAnalysis = _project.PrimaryOutput;
            labelOutputName.Text = _currentOutputName;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Endpoint";
            column.Name = "Endpoint";
            column.HeaderText = "Endpoint";
            column.ValueType = typeof(string);
            column.ReadOnly = true;
            dataGridViewComparisons.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsPrimary";
            checkbox.Name = "IsPrimary";
            checkbox.HeaderText = "Primary";
            dataGridViewComparisons.Columns.Add(checkbox);

            _availableAnalysisMethodTypesDifferenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults
                .SelectMany(r => r.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags()).Distinct().ToList();

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableAnalysisMethodTypesDifferenceTests;
            combo.Name = "AnalysisMethodDifferenceTest";
            combo.DataPropertyName = "AnalysisMethodDifferenceTest";
            combo.ValueType = typeof(AnalysisMethodType);
            combo.HeaderText = "Difference test";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            _availableAnalysisMethodTypesEquivalenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults
                .SelectMany(r => r.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags()).Distinct().ToList();

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableAnalysisMethodTypesEquivalenceTests;
            combo.Name = "AnalysisMethodEquivalenceTest";
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
            if (tabControlEndpointResult.SelectedIndex < 2) {
                updateAnalysisCharts();
            } else if (tabControlEndpointResult.SelectedIndex == 2) {
                updateFullReport();
            }
        }

        private void updateAnalysisCharts() {
            var plotType = _currentPlotType;
            var records = _resultPowerAnalysis.GetAggregateOutputRecords(_resultPowerAnalysis.PowerAggregationType).ToList();
            var primaryComparisons = _resultPowerAnalysis.GetPrimaryComparisons().ToList();
            if (primaryComparisons.Count > 0) {
                if (plotType == AnalysisPlotType.Replicates) {
                    plotViewDifference.Model = PowerVersusReplicatesCsdChartCreator.Create(records, TestType.Difference);
                    plotViewEquivalence.Model = PowerVersusReplicatesCsdChartCreator.Create(records, TestType.Equivalence);
                } else if (plotType == AnalysisPlotType.ConcernStandardizedDifference) {
                    plotViewDifference.Model = PowerVersusCsdChartCreator.Create(records, TestType.Difference, _resultPowerAnalysis.ComparisonPowerAnalysisResults.First().InputPowerAnalysis.NumberOfReplications);
                    plotViewEquivalence.Model = PowerVersusCsdChartCreator.Create(records, TestType.Equivalence, _resultPowerAnalysis.ComparisonPowerAnalysisResults.First().InputPowerAnalysis.NumberOfReplications);
                }
            } else {
                plotViewDifference.Model = null;
                plotViewEquivalence.Model = null;
                plotViewDifference.Model = null;
                plotViewEquivalence.Model = null;
            }
        }

        private void updateFullReport() {
            if (_resultPowerAnalysis != null) {
                if (webBrowserFullReport.Document == null) {
                    webBrowserFullReport.Navigate("about:blank");
                }
                var doc = webBrowserFullReport.Document.OpenNew(true);
                var reportGenerator = new MultiComparisonReportGenerator(_resultPowerAnalysis, _currentOutputName, CurrentOutputFilesPath);
                var html = reportGenerator.Generate(ChartCreationMethod.ExternalPng);
                doc.Write(html);
                doc.Title = "Full report";
            }
        }

        private void updatePlotTypeRadioButtons() {
            radioButtonCsdDifference.Checked = _currentPlotType == AnalysisPlotType.ConcernStandardizedDifference;
            radioButtonCsdEquivalence.Checked = _currentPlotType == AnalysisPlotType.ConcernStandardizedDifference;
            radioButtonReplicatesDifference.Checked = _currentPlotType == AnalysisPlotType.Replicates;
            radioButtonReplicatesEquivalence.Checked = _currentPlotType == AnalysisPlotType.Replicates;
        }


        private void radioButtonAggregateMin_CheckedChanged(object sender, EventArgs e) {
            _resultPowerAnalysis.PowerAggregationType = PowerAggregationType.AggregateMinimum;
            updateAnalysisOutputPanel();
        }

        private void radioButtonAggregateMean_CheckedChanged(object sender, EventArgs e) {
            _resultPowerAnalysis.PowerAggregationType = PowerAggregationType.AggregateMean;
            updateAnalysisOutputPanel();
        }

        private void dataGridViewComparisons_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (dataGridViewComparisons.CurrentCell != null) {
                updateAnalysisOutputPanel();
            }
        }

        private void dataGridViewComparisons_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (dataGridViewComparisons.IsCurrentCellDirty) {
                dataGridViewComparisons.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridViewComparisons_Leave(object sender, EventArgs e) {
            dataGridViewComparisons.CurrentCell = null;
        }

        private void radioButtonCsdDifference_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonCsdDifference.Checked ? AnalysisPlotType.ConcernStandardizedDifference : AnalysisPlotType.Replicates;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void radioButtonReplicatesDifference_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonReplicatesDifference.Checked ? AnalysisPlotType.Replicates : AnalysisPlotType.ConcernStandardizedDifference;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void radioButtonCsdEquivalence_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonCsdEquivalence.Checked ? AnalysisPlotType.ConcernStandardizedDifference : AnalysisPlotType.Replicates;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void radioButtonReplicatesEquivalence_CheckedChanged(object sender, EventArgs e) {
            _currentPlotType = radioButtonReplicatesEquivalence.Checked ? AnalysisPlotType.Replicates : AnalysisPlotType.ConcernStandardizedDifference;
            updatePlotTypeRadioButtons();
            updateAnalysisOutputPanel();
        }

        private void tabControlEndpointResult_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void toolStripButtonExportPdf_Click(object sender, EventArgs e) {
            var title = "Report " + _currentOutputName;
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
                var reportGenerator = new MultiComparisonReportGenerator(_resultPowerAnalysis, _currentOutputName, CurrentOutputFilesPath);
                reportGenerator.SaveAsPdf(filenamePdf);
                Process.Start(filenamePdf);
            }
        }

        private void buttonGenerateDataTemplate_Click(object sender, EventArgs e) {
            var saveFileDialog = new SaveFileDialog() {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FileName = "Template-" + _project.ProjectName + ".csv",
                InitialDirectory = CurrentOutputFilesPath,
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    int blocks;
                    if (!Int32.TryParse(this.textBoxNumberOfReplicates.Text, out blocks)) {
                        blocks = 2;
                    }
                    var dataTemplateGenerator = new AnalysisDataTemplateGenerator();
                    var template = dataTemplateGenerator.CreateAnalysisDataTemplate(_resultPowerAnalysis.GetPrimaryComparisons().Select(r => r.InputPowerAnalysis), blocks);
                    var filename = saveFileDialog.FileName;
                    AnalysisDataTemplateGenerator.AnalysisDataTemplateToCsv(template, filename);
                    var contrastsFileName = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + "Contrasts.csv");
                    AnalysisDataTemplateGenerator.AnalysisDataTemplateContrastsToCsv(template, contrastsFileName);

                    var scriptFileName = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + "Analysis.R");
                    var scriptGenerator = new AnalysisRScriptGenerator();
                    scriptGenerator.Generate(_resultPowerAnalysis.GetPrimaryComparisons(), scriptFileName, Path.GetFileName(filename), Path.GetFileNameWithoutExtension(filename) + "Contrasts.csv");

                    System.Diagnostics.Process.Start(Path.GetDirectoryName(filename));
                } catch (Exception ex) {
                    this.showError("Error while exporting data template", ex.Message);
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

        private void dataGridViewComparisons_CellClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void dataGridViewComparisons_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
            var currentComparisonAnalysisResult = _resultPowerAnalysis.ComparisonPowerAnalysisResults[e.RowIndex];
            if (dataGridViewComparisons.Columns[e.ColumnIndex].Name == "AnalysisMethodDifferenceTest") {
                var measurement = currentComparisonAnalysisResult.InputPowerAnalysis.MeasurementType;
                var combo = dataGridViewComparisons.Rows[e.RowIndex].Cells["AnalysisMethodDifferenceTest"] as DataGridViewComboBoxCell;
                var source = currentComparisonAnalysisResult.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests
                    .GetFlags().Cast<AnalysisMethodType>().ToList();
                combo.DataSource = source;
            }
            if (dataGridViewComparisons.Columns[e.ColumnIndex].Name == "AnalysisMethodEquivalenceTest") {
                var measurement = currentComparisonAnalysisResult.InputPowerAnalysis.MeasurementType;
                var combo = dataGridViewComparisons.Rows[e.RowIndex].Cells["AnalysisMethodEquivalenceTest"] as DataGridViewComboBoxCell;
                var source = currentComparisonAnalysisResult.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests
                    .GetFlags().Cast<AnalysisMethodType>().ToList();
                combo.DataSource = source;
            }
        }
    }
}
