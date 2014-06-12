using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisResultsForm : UserControl, ISelectionForm {

        private Project _project;
        private List<Comparison> _comparisons;
        private Comparison _currentComparison;
        private AnalysisMethodType _currentAnalysisType = AnalysisMethodType.OverdispersedPoisson;

        public AnalysisResultsForm(Project project) {
            InitializeComponent();
            Name = "Results";
            Description = "Description here";
            this.textBoxTabTitle.Text = Name;
            this.textBoxTabDescription.Text = Description;
            this.comboBoxAnalysisType.DataSource = Enum.GetValues(typeof(AnalysisMethodType));
            this.comboBoxAnalysisType.SelectedIndex = 1;
            _project = project;
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridComparisons();
            if (_project.GetComparisons().Any(c => c.OutputPowerAnalysis != null)) {
                splitContainerComparisons.Visible = true;
            } else {
                splitContainerComparisons.Visible = false;
            }
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
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
                plotViewDifferenceRepetitions.Model = AnalysisResultsChartGenerator.CreatePlotViewReplicates(_currentComparison.OutputPowerAnalysis, TestType.Difference, _currentAnalysisType);
                plotViewEquivalenceRepetitions.Model = AnalysisResultsChartGenerator.CreatePlotViewReplicates(_currentComparison.OutputPowerAnalysis, TestType.Equivalence, _currentAnalysisType);
                plotViewDifferenceLog.Model = AnalysisResultsChartGenerator.CreatePlotViewLog(_currentComparison.OutputPowerAnalysis, TestType.Difference, _currentAnalysisType);
                plotViewEquivalenceLog.Model = AnalysisResultsChartGenerator.CreatePlotViewLog(_currentComparison.OutputPowerAnalysis, TestType.Equivalence, _currentAnalysisType);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.GetComparisons().ElementAt(dataGridViewComparisons.CurrentRow.Index);
            updateAnalysisOutputPanel();
        }

        private void comboBoxAnalysisType_SelectedIndexChanged(object sender, EventArgs e) {
            AnalysisMethodType analysisType;
            Enum.TryParse<AnalysisMethodType>(comboBoxAnalysisType.SelectedValue.ToString(), out analysisType);
            _currentAnalysisType = analysisType;
            updateAnalysisOutputPanel();
        }
    }
}
