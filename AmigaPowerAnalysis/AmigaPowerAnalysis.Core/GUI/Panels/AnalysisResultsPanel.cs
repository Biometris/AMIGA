using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.Charting.AnalysisResultsChartCreators;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using Biometris.ExtensionMethods;

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisResultsPanel : UserControl, ISelectionForm {

        public event EventHandler TabVisibilitiesChanged;

        private Project _project;
        private List<OutputPowerAnalysis> _outputRecords;
        private AnalysisMethodType _currentAnalysisType = AnalysisMethodType.OverdispersedPoisson;
        private string _currentProjectFilePath;

        public AnalysisResultsPanel(Project project) {
            InitializeComponent();
            Name = "Overall results";
            Description = "The power analysis is based on the minimum power across the primary comparisons, in terms of Concern Standardized Differences (CSD, equals 1 at the Limit of Concern ).\r\nSelect primary comparisons. Choose method of analysis if more have been investigated.\r\n\r\nPower is shown for difference tests (upper graphs) and equivalence tests (lower graphs), both as a function of the number of replicates (left) and the Concern Standardized Difference (right).\r\nNote: Number of plots in design is Number of replicates times Number of plots per block";
            _project = project;
            var testTypes = Enum.GetValues(typeof(TestType));
            this.comboBoxTestType.DataSource = testTypes;
            this.comboBoxTestType.SelectedIndex = 0;
            var plotTypes = new List<AnalysisPlotType>() { AnalysisPlotType.Replicates, AnalysisPlotType.ConcernStandardizedDifference };
            this.comboBoxAnalysisPlotTypes.DataSource = plotTypes;
            this.comboBoxAnalysisPlotTypes.SelectedIndex = 0;
        }

        public string Description { get; private set; }

        public string CurrentProjectFilesPath {
            get { return _currentProjectFilePath; }
            set { _currentProjectFilePath = value; }
        }

        public void Activate() {
            updateDataGridComparisons();
            updateVisibilities();
            var selectedAnalysisMethodTypesDifferenceTests = _outputRecords.First().InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().ToArray();
            var selectedAnalysisMethodTypesEquivalenceTests = _outputRecords.First().InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().ToArray();
            updateAnalysisOutputPanel();
            dataGridViewComparisons.CurrentCell = null;
        }

        private void updateVisibilities() {
            var primaryComparisons = _outputRecords.Where(c => c.IsPrimary).ToList();
            if (primaryComparisons.Count > 0) {
                splitContainerComparisons.Panel2.Show();
            } else {
                splitContainerComparisons.Panel2.Hide();
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

            _outputRecords = _project.AnalysisResults.First().ComparisonPowerAnalysisResults;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Endpoint";
            column.Name = "Endpoint";
            column.HeaderText = "Endpoint";
            column.ValueType = typeof(string);
            dataGridViewComparisons.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsPrimary";
            checkbox.Name = "IsPrimary";
            checkbox.HeaderText = "Primary";
            dataGridViewComparisons.Columns.Add(checkbox);

            var comparisonsBindingSouce = new BindingSource(_outputRecords, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
            dataGridViewComparisons.Columns[0].ReadOnly = true;
        }

        private void updateAnalysisOutputPanel() {
            if (_outputRecords != null) {
                var primaryComparisons = _outputRecords.Where(c => c.IsPrimary).ToList();
                if (primaryComparisons.Count > 0) {
                    var records = primaryComparisons.SelectMany(c => c.OutputRecords)
                        .GroupBy(r => new { LevelOfConcern = r.ConcernStandardizedDifference, NumberOfReplicates = r.NumberOfReplications })
                        .Select(g => new OutputPowerAnalysisRecord() {
                            ConcernStandardizedDifference = g.Key.LevelOfConcern,
                            NumberOfReplications = g.Key.NumberOfReplicates,
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
                    var plotType = (AnalysisPlotType)comboBoxAnalysisPlotTypes.SelectedValue;
                    var testType = (TestType)comboBoxTestType.SelectedValue;
                    if (plotType == AnalysisPlotType.Replicates) {
                        plotView.Model = PowerVersusReplicatesCsdChartCreator.Create(records, testType, _currentAnalysisType);
                    } else if (plotType == AnalysisPlotType.ConcernStandardizedDifference) {
                        plotView.Model = PowerVersusCsdChartCreator.Create(records, testType, _currentAnalysisType, primaryComparisons.First().InputPowerAnalysis.NumberOfReplications);
                    }
                    var plotsPerBlockCounts = primaryComparisons.Select(pc => pc.InputPowerAnalysis.InputRecords.Sum(ir => ir.Frequency));
                    var minPlotsPerBlockCount = plotsPerBlockCounts.Min();
                    var maxPlotsPerBlockCount = plotsPerBlockCounts.Max();
                    if (minPlotsPerBlockCount == maxPlotsPerBlockCount) {
                        labelPlotsPerBlock.Text = string.Format("{0} plots per block", minPlotsPerBlockCount);
                    } else {
                        labelPlotsPerBlock.Text = string.Format("between {0} and {1} plots per block", minPlotsPerBlockCount, maxPlotsPerBlockCount);
                    }
                }
                updateVisibilities();
            }
        }

        private void comboBoxTestType_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void comboBoxAnalysisPlotTypes_SelectedIndexChanged(object sender, EventArgs e) {
            updateAnalysisOutputPanel();
        }

        private void buttonShowReport_Click(object sender, EventArgs e) {
            var primaryComparisons = _outputRecords.Where(c => c.IsPrimary).ToList();
            if (primaryComparisons.Count > 0) {
                var tempPath = Path.GetTempPath();
                var title = Path.GetFileNameWithoutExtension(_currentProjectFilePath) + "_CSD";
                var multiComparisonReportGenerator = new MultiComparisonReportGenerator(_outputRecords, _currentProjectFilePath);
                var htmlReportForm = new HtmlReportForm(multiComparisonReportGenerator, title, _currentProjectFilePath);
                htmlReportForm.ShowDialog();
            }
        }

        private void dataGridViewComparisons_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (dataGridViewComparisons.CurrentCell != null) {
                updateAnalysisOutputPanel();
                updateVisibilities();
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
    }
}
