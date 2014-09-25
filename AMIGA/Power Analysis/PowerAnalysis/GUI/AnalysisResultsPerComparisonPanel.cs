using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.Reporting;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;
using OxyPlot.WindowsForms;

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisResultsPerComparisonPanel : UserControl, ISelectionForm {

        public event EventHandler TabVisibilitiesChanged;

        private Project _project;
        private List<Comparison> _comparisons;
        private Comparison _currentComparison;
        private AnalysisMethodType _currentAnalysisType = AnalysisMethodType.OverdispersedPoisson;
        private string _currentProjectFilesPath;

        public AnalysisResultsPerComparisonPanel(Project project) {
            InitializeComponent();
            Name = "Results per Endpoint";
            Description = "Choose endpoint in table. Choose method of analysis if more have been investigated. Power is shown for difference tests or equivalence tests,and as a function of the number of replicates or the Ratio GMO/CMP (on a ln scale).\r\nNote: Number of plots in design is Number of replicates times Number of plots per block";
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
            if (_project.GetComparisons().Any(c => c.OutputPowerAnalysis != null)) {
                splitContainerComparisons.Visible = true;
                var selectedAnalysisMethodTypes = _comparisons.First().OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags<AnalysisMethodType>().ToArray();
                this.comboBoxAnalysisType.Visible = selectedAnalysisMethodTypes.Count() > 1;
                this.comboBoxAnalysisType.DataSource = selectedAnalysisMethodTypes;
                if (selectedAnalysisMethodTypes.Count() > 0) {
                    this.comboBoxAnalysisType.SelectedIndex = 0;
                }
            } else {
                splitContainerComparisons.Visible = false;
            }
        }

        public bool IsVisible() {
            if (_project.GetComparisons().Any(c => c.OutputPowerAnalysis != null)) {
                return true;
            }
            return false;
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Comparison";
            dataGridViewComparisons.Columns.Add(column);

            var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpoints;
            combo.DataPropertyName = "Endpoint";
            combo.DisplayMember = "Name";
            combo.ValueMember = "Endpoint";
            combo.HeaderText = "Endpoint";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            _comparisons = _project.GetComparisons().Where(c => c.OutputPowerAnalysis != null).ToList();
            var comparisonsBindingSouce = new BindingSource(_comparisons, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
        }

        private void updateAnalysisOutputPanel() {
            if (_currentComparison != null) {
                var plotType = (AnalysisPlotType)comboBoxAnalysisPlotTypes.SelectedValue;
                var testType = (TestType)comboBoxTestType.SelectedValue;
                if (plotType == AnalysisPlotType.Replicates) {
                    plotView.Model = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(_currentComparison.OutputPowerAnalysis.OutputRecords, testType, _currentAnalysisType);
                } else if (plotType == AnalysisPlotType.Ratio) {
                    plotView.Model = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(_currentComparison.OutputPowerAnalysis.OutputRecords, testType, _currentAnalysisType);
                }
                labelPlotsPerBlock.Text = string.Format("{0} plots per block", _currentComparison.OutputPowerAnalysis.InputPowerAnalysis.InputRecords.Sum(ir => ir.Frequency));
                labelLocLowerValue.Text = string.Format("{0:0.###}", _currentComparison.OutputPowerAnalysis.InputPowerAnalysis.LocLower);
                labelLocUpperValue.Text = string.Format("{0:0.###}", _currentComparison.OutputPowerAnalysis.InputPowerAnalysis.LocUpper);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.GetComparisons().ElementAt(dataGridViewComparisons.CurrentRow.Index);
            if (_currentComparison != null) {
                this.comboBoxAnalysisType.Visible = true;
            } else {
                this.comboBoxAnalysisType.Visible = false;
            }
            updateAnalysisOutputPanel();
        }

        private void comboBoxAnalysisType_SelectedIndexChanged(object sender, EventArgs e) {
            AnalysisMethodType analysisType;
            Enum.TryParse<AnalysisMethodType>(comboBoxAnalysisType.SelectedValue.ToString(), out analysisType);
            _currentAnalysisType = analysisType;
            updateAnalysisOutputPanel();
        }

        private void buttonShowSettings_Click(object sender, EventArgs e) {
            var htmlReportForm = new HtmlReportForm(ComparisonSummaryReportGenerator.GenerateComparisonSettingsReport(_currentComparison, _currentProjectFilesPath), Path.GetFileNameWithoutExtension(_currentProjectFilesPath), _currentProjectFilesPath);
            htmlReportForm.ShowDialog();
        }

        private void buttonShowInputData_Click(object sender, EventArgs e) {
            if (_currentComparison != null && _currentComparison.OutputPowerAnalysis != null) {
                //var tempPath = Path.GetTempPath();
                //tempPath = @"D:\Projects\Amiga\Source\TestData\ssss";
                var htmlReportForm = new HtmlReportForm(ComparisonSummaryReportGenerator.GenerateComparisonReport(_currentComparison, _currentProjectFilesPath), Path.GetFileNameWithoutExtension(_currentProjectFilesPath), _currentProjectFilesPath);
                htmlReportForm.ShowDialog();
            }
        }

        private void comboBoxTestType_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void comboBoxAnalysisPlotTypes_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }
    }
}
