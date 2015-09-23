using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using Biometris.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisResultsPerComparisonPanel : UserControl, ISelectionForm {

        public event EventHandler TabVisibilitiesChanged;

        private Project _project;
        private ResultPowerAnalysis _resultPowerAnalysis;
        private OutputPowerAnalysis _currentComparisonAnalysisResult;
        private string _currentProjectFilesPath;

        public AnalysisResultsPerComparisonPanel(Project project) {
            InitializeComponent();
            Name = "Results per endpoint";
            Description = "Choose endpoint in table. Choose method of analysis if more have been investigated. Power is shown for difference tests or equivalence tests, and as a function of the number of replicates or the Ratio Test/Comp (on a ln scale).\r\nNote: Number of plots in design is Number of replicates times Number of plots per block";
            var testTypes = Enum.GetValues(typeof(TestType));
            this.comboBoxTestType.DataSource = testTypes;
            this.comboBoxTestType.SelectedIndex = 0;
            var plotTypes = new List<AnalysisPlotType>() { AnalysisPlotType.Replicates, AnalysisPlotType.Ratio };
            this.comboBoxAnalysisPlotTypes.DataSource = plotTypes;
            this.comboBoxAnalysisPlotTypes.SelectedIndex = 0;
            _project = project;
        }

        public string Description { get; private set; }

        public string CurrentProjectFilesPath {
            get { return _currentProjectFilesPath; }
            set { _currentProjectFilesPath = value; }
        }

        public void Activate() {
            updateDataGridComparisons();
            if (_project.HasOutput) {
                splitContainerComparisons.Visible = true;
            } else {
                splitContainerComparisons.Visible = false;
            }
        }

        public bool IsVisible() {
            if (_project.HasOutput) {
                return true;
            }
            return false;
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            _resultPowerAnalysis = _project.AnalysisResults.First();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Endpoint";
            column.Name = "Endpoint";
            column.HeaderText = "Endpoint";
            column.ValueType = typeof(string);
            column.ReadOnly = true;
            dataGridViewComparisons.Columns.Add(column);

            var _availableAnalysisMethodTypesDifferenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults.First().InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().ToArray();

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableAnalysisMethodTypesDifferenceTests;
            combo.DataPropertyName = "AnalysisMethodDifferenceTest";
            combo.ValueType = typeof(AnalysisMethodType);
            combo.HeaderText = "Difference test";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            var _availableAnalysisMethodTypesEquivalenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults.First().InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().ToArray();

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableAnalysisMethodTypesEquivalenceTests;
            combo.DataPropertyName = "AnalysisMethodEquivalenceTest";
            combo.ValueType = typeof(AnalysisMethodType);
            combo.HeaderText = "Equivalence test";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            var comparisonsBindingSouce = new BindingSource(_resultPowerAnalysis.ComparisonPowerAnalysisResults, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;

            this.dataGridViewComparisons.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridViewComparisons_EditingControlShowing);
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
                var plotType = (AnalysisPlotType)comboBoxAnalysisPlotTypes.SelectedValue;
                var testType = (TestType)comboBoxTestType.SelectedValue;
                var analysisMethodType = (testType == TestType.Difference) ? _currentComparisonAnalysisResult.AnalysisMethodDifferenceTest : _currentComparisonAnalysisResult.AnalysisMethodEquivalenceTest;
                if (plotType == AnalysisPlotType.Replicates) {
                    plotView.Model = PowerVersusReplicatesRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, testType, analysisMethodType);
                } else if (plotType == AnalysisPlotType.Ratio) {
                    plotView.Model = PowerVersusRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, testType, analysisMethodType, _currentComparisonAnalysisResult.InputPowerAnalysis.NumberOfReplications);
                }
                labelPlotsPerBlock.Text = string.Format("{0} plots per block", _currentComparisonAnalysisResult.InputPowerAnalysis.InputRecords.Sum(ir => ir.Frequency));
                labelLocLowerValue.Text = string.Format("{0:0.###}", _currentComparisonAnalysisResult.InputPowerAnalysis.LocLower);
                labelLocUpperValue.Text = string.Format("{0:0.###}", _currentComparisonAnalysisResult.InputPowerAnalysis.LocUpper);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparisonAnalysisResult = _resultPowerAnalysis.ComparisonPowerAnalysisResults.ElementAt(dataGridViewComparisons.CurrentRow.Index);
            updateAnalysisOutputPanel();
        }

        private void buttonShowSettings_Click(object sender, EventArgs e) {
            var title = Path.GetFileNameWithoutExtension(_currentProjectFilesPath) + "_" + _currentComparisonAnalysisResult.Endpoint + "_Settings";
            var reportGenerator = new ComparisonSettingsGenerator(_currentComparisonAnalysisResult, _currentProjectFilesPath);
            var htmlReportForm = new HtmlReportForm(reportGenerator, title, _currentProjectFilesPath);
            htmlReportForm.ShowDialog();
        }

        private void buttonShowInputData_Click(object sender, EventArgs e) {
            if (_currentComparisonAnalysisResult != null && _currentComparisonAnalysisResult != null) {
                var title = Path.GetFileNameWithoutExtension(_currentProjectFilesPath) + "_" + _currentComparisonAnalysisResult.Endpoint;
                var reportGenerator = new SingleComparisonReportGenerator(_currentComparisonAnalysisResult, _currentProjectFilesPath);
                var htmlReportForm = new HtmlReportForm(reportGenerator, title, _currentProjectFilesPath);
                htmlReportForm.ShowDialog();
            }
        }

        private void comboBoxTestType_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void comboBoxAnalysisPlotTypes_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void dataGridViewComparisons_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            updateAnalysisOutputPanel();
        }
    }
}
