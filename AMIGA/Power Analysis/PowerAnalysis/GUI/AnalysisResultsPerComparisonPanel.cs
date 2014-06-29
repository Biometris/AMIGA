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

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

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
            Name = "Results per comparison";
            Description = "Choose endpoint in table. Choose method of analysis if more have been investigated. Power is shown for difference tests (upper graphs) and equivalence tests (lower graphs), both as a function of the number of replicates (left) and the Ratio GMO/CMP (right).\r\nNote: Number of plots in design is Number of replicates times ....(number plots in a block)";
            this.comboBoxAnalysisType.Visible = false;
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
                plotViewDifferenceRepetitions.Model = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(_currentComparison.OutputPowerAnalysis.OutputRecords, TestType.Difference, _currentAnalysisType);
                plotViewEquivalenceRepetitions.Model = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLogRatio(_currentComparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, _currentAnalysisType);
                plotViewDifferenceLog.Model = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(_currentComparison.OutputPowerAnalysis.OutputRecords, TestType.Difference, _currentAnalysisType);
                plotViewEquivalenceLog.Model = AnalysisResultsChartGenerator.CreatePlotViewLogRatioReplicates(_currentComparison.OutputPowerAnalysis.OutputRecords, TestType.Equivalence, _currentAnalysisType);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.GetComparisons().ElementAt(dataGridViewComparisons.CurrentRow.Index);
            if (_currentComparison != null) {
                var selectedAnalysisMethodTypes = _currentComparison.OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags<AnalysisMethodType>().ToArray();
                this.comboBoxAnalysisType.DataSource = selectedAnalysisMethodTypes;
                if (selectedAnalysisMethodTypes.Count() > 0) {
                    this.comboBoxAnalysisType.SelectedIndex = 0;
                }
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

        private void buttonShowInputData_Click(object sender, EventArgs e) {
            if (_currentComparison != null && _currentComparison.OutputPowerAnalysis != null) {
                //var tempPath = Path.GetTempPath();
                //tempPath = @"D:\Projects\Amiga\Source\TestData\ssss";
                var htmlReportForm = new HtmlReportForm(ComparisonSummaryReportGenerator.GenerateComparisonReport(_currentComparison, _currentProjectFilesPath));
                htmlReportForm.ShowDialog();
            }
        }
    }
}
