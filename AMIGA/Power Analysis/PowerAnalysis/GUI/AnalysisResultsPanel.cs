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
    public partial class AnalysisResultsPanel : UserControl, ISelectionForm {

        public event EventHandler TabVisibilitiesChanged;

        private Project _project;
        private List<Comparison> _comparisons;
        private AnalysisMethodType _currentAnalysisType = AnalysisMethodType.OverdispersedPoisson;
        private string _currentProjectFilePath;

        public AnalysisResultsPanel(Project project) {
            InitializeComponent();
            Name = "Results";
            Description = "Choose primary endpoint in table. The power analysis is based on the mimum power across all primary endpoints. Results for other endpoints will be shown for information only in the next tab. Choose method of analysis if more have been investigated.\r\n\r\nPower is shown for difference tests (upper graphs) and equivalence tests (lower graphs), both as a function of the number of replicates (left) and the natural log of the Ratio GMO/CMP (right).";
            _project = project;
        }

        public string Description { get; private set; }

        public string CurrentProjectFilesPath {
            get { return _currentProjectFilePath; }
            set { _currentProjectFilePath = value; }
        }

        public void Activate() {
            updateDataGridComparisons();
            updateVisibilities();
        }

        private void updateVisibilities() {
            var primaryComparisons = _comparisons.Where(c => c.OutputPowerAnalysis != null && c.IsPrimary).ToList();
            if (primaryComparisons.Count > 0) {
                comboBoxAnalysisType.Visible = true;
                var selectedAnalysisMethodTypes = primaryComparisons.First().OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypes.GetFlags<AnalysisMethodType>().ToArray();
                this.comboBoxAnalysisType.DataSource = selectedAnalysisMethodTypes;
                if (selectedAnalysisMethodTypes.Count() > 0) {
                    this.comboBoxAnalysisType.SelectedIndex = 0;
                }
                updateAnalysisOutputPanel();
            } else {
                comboBoxAnalysisType.Visible = false;
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

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsPrimary";
            checkbox.Name = "IsPrimary";
            checkbox.HeaderText = "Primary";
            dataGridViewComparisons.Columns.Add(checkbox);

            _comparisons = _project.GetComparisons().Where(c => c.OutputPowerAnalysis != null).ToList();
            var comparisonsBindingSouce = new BindingSource(_comparisons, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
            dataGridViewComparisons.Columns[0].ReadOnly = true;
            dataGridViewComparisons.Columns[1].ReadOnly = true;
        }

        private void updateAnalysisOutputPanel() {
            var primaryComparisons = _comparisons.Where(c => c.OutputPowerAnalysis != null && c.IsPrimary).ToList();
            if (primaryComparisons.Count > 0) {
                var records = primaryComparisons.SelectMany(c => c.OutputPowerAnalysis.OutputRecords)
                    .GroupBy(r => new { r.LevelOfConcern, r.NumberOfReplicates })
                    .Select(g => new OutputPowerAnalysisRecord() {
                        LevelOfConcern = g.Key.LevelOfConcern,
                        NumberOfReplicates = g.Key.NumberOfReplicates,
                        Ratio = double.NaN,
                        LogRatio = double.NaN,
                        PowerDifferenceLogNormal = g.Min(r => r.PowerDifferenceLogNormal),
                        PowerDifferenceSquareRoot = g.Min(r => r.PowerDifferenceSquareRoot),
                        PowerDifferenceOverdispersedPoisson = g.Min(r => r.PowerDifferenceOverdispersedPoisson),
                        PowerDifferenceNegativeBinomial = g.Min(r => r.PowerDifferenceNegativeBinomial),
                        PowerEquivalenceLogNormal = g.Min(r => r.PowerEquivalenceLogNormal),
                        PowerEquivalenceSquareRoot = g.Min(r => r.PowerEquivalenceSquareRoot),
                        PowerEquivalenceOverdispersedPoisson = g.Min(r => r.PowerEquivalenceOverdispersedPoisson),
                        PowerEquivalenceNegativeBinomial = g.Min(r => r.PowerEquivalenceNegativeBinomial),
                    })
                    .ToList();
                plotViewDifferenceReplicates.Model = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLevelOfConcern(records, TestType.Difference, _currentAnalysisType);
                plotViewEquivalenceReplicates.Model = AnalysisResultsChartGenerator.CreatePlotViewReplicatesLevelOfConcern(records, TestType.Equivalence, _currentAnalysisType);
                plotViewDifferenceLevelOfConcern.Model = AnalysisResultsChartGenerator.CreatePlotViewLevelOfConcernReplicates(records, TestType.Difference, _currentAnalysisType);
                plotViewEquivalenceLevelOfConcern.Model = AnalysisResultsChartGenerator.CreatePlotViewLevelOfConcernReplicates(records, TestType.Equivalence, _currentAnalysisType);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void comboBoxAnalysisType_SelectedIndexChanged(object sender, EventArgs e) {
            AnalysisMethodType analysisType;
            Enum.TryParse<AnalysisMethodType>(comboBoxAnalysisType.SelectedValue.ToString(), out analysisType);
            _currentAnalysisType = analysisType;
            updateAnalysisOutputPanel();
        }

        private void buttonShowInputData_Click(object sender, EventArgs e) {
            var primaryComparisons = _comparisons.Where(c => c.OutputPowerAnalysis != null && c.IsPrimary).ToList();
            if (primaryComparisons.Count > 0) {
                var tempPath = Path.GetTempPath();
                tempPath = @"D:\Projects\Amiga\Source\TestData\ssss";
                var htmlReportForm = new HtmlReportForm(ComparisonSummaryReportGenerator.GenerateAnalysisReport(primaryComparisons, _currentProjectFilePath));
                htmlReportForm.ShowDialog();
            }
        }

        private void dataGridViewComparisons_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            updateAnalysisOutputPanel();
            updateVisibilities();
        }

        private void dataGridViewComparisons_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (dataGridViewComparisons.IsCurrentCellDirty) {
                dataGridViewComparisons.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
