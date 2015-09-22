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
        private List<OutputPowerAnalysis> _comparisonAnalysisResults;
        private OutputPowerAnalysis _currentComparisonAnalysisResult;
        private AnalysisMethodType _currentAnalysisType = AnalysisMethodType.OverdispersedPoisson;
        private string _currentProjectFilesPath;

        public AnalysisResultsPerComparisonPanel(Project project) {
            InitializeComponent();
            Name = "Results per endpoint";
            Description = "Choose endpoint in table. Choose method of analysis if more have been investigated. Power is shown for difference tests or equivalence tests, and as a function of the number of replicates or the Ratio Test/Comp (on a ln scale).\r\nNote: Number of plots in design is Number of replicates times Number of plots per block";
            this.comboBoxAnalysisType.Visible = false;
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

            _comparisonAnalysisResults = _project.AnalysisResults.First().ComparisonPowerAnalysisResults;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Endpoint";
            column.Name = "Endpoint";
            column.HeaderText = "Endpoint";
            column.ValueType = typeof(string);
            dataGridViewComparisons.Columns.Add(column);

            var comparisonsBindingSouce = new BindingSource(_comparisonAnalysisResults, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
        }

        private void updateAnalysisOutputPanel() {
            if (_currentComparisonAnalysisResult != null) {
                var plotType = (AnalysisPlotType)comboBoxAnalysisPlotTypes.SelectedValue;
                var testType = (TestType)comboBoxTestType.SelectedValue;
                if (plotType == AnalysisPlotType.Replicates) {
                    plotView.Model = PowerVersusReplicatesRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, testType, _currentAnalysisType);
                } else if (plotType == AnalysisPlotType.Ratio) {
                    plotView.Model = PowerVersusRatioChartCreator.Create(_currentComparisonAnalysisResult.OutputRecords, testType, _currentAnalysisType, _currentComparisonAnalysisResult.InputPowerAnalysis.NumberOfReplications);
                }
                labelPlotsPerBlock.Text = string.Format("{0} plots per block", _currentComparisonAnalysisResult.InputPowerAnalysis.InputRecords.Sum(ir => ir.Frequency));
                labelLocLowerValue.Text = string.Format("{0:0.###}", _currentComparisonAnalysisResult.InputPowerAnalysis.LocLower);
                labelLocUpperValue.Text = string.Format("{0:0.###}", _currentComparisonAnalysisResult.InputPowerAnalysis.LocUpper);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparisonAnalysisResult = _comparisonAnalysisResults.ElementAt(dataGridViewComparisons.CurrentRow.Index);
            updateComboBoxAnalysisMethodTypes();
            updateAnalysisOutputPanel();
        }

        private void updateComboBoxAnalysisMethodTypes() {
            if (_currentComparisonAnalysisResult != null) {
                this.comboBoxAnalysisType.Visible = true;
                var testType = (TestType)comboBoxTestType.SelectedValue;
                Enum[] selectedAnalysisMethodTypes;
                if (testType == TestType.Difference) {
                    selectedAnalysisMethodTypes = _currentComparisonAnalysisResult.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().ToArray();
                } else {
                    selectedAnalysisMethodTypes = _currentComparisonAnalysisResult.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().ToArray();
                }
                this.comboBoxAnalysisType.Visible = selectedAnalysisMethodTypes.Count() > 1;
                this.comboBoxAnalysisType.DataSource = selectedAnalysisMethodTypes;
                if (selectedAnalysisMethodTypes.Count() > 0) {
                    this.comboBoxAnalysisType.SelectedIndex = 0;
                }
            } else {
                this.comboBoxAnalysisType.Visible = false;
            }
        }

        private void comboBoxAnalysisType_SelectedIndexChanged(object sender, EventArgs e) {
            AnalysisMethodType analysisMethodType;
            Enum.TryParse<AnalysisMethodType>(comboBoxAnalysisType.SelectedValue.ToString(), out analysisMethodType);
            _currentAnalysisType = analysisMethodType;
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
            updateComboBoxAnalysisMethodTypes();
        }

        private void comboBoxAnalysisPlotTypes_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }
    }
}
