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
        private ResultPowerAnalysis _resultPowerAnalysis;
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
            updateAnalysisOutputPanel();
            dataGridViewComparisons.CurrentCell = null;
        }

        private void updateVisibilities() {
            if (_resultPowerAnalysis.GetPrimaryComparisons().Count() > 0) {
                splitContainerComparisons.Panel2.Show();
            } else {
                splitContainerComparisons.Panel2.Hide();
            }
        }

        public bool IsVisible() {
            return _project != null && _project.HasOutput;
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            _resultPowerAnalysis = _project.AnalysisResults.First();

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

            var _availableAnalysisMethodTypesDifferenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults
                .SelectMany(r => r.AnalysisMethodDifferenceTest.GetFlags().ToArray()).Distinct().ToList();

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableAnalysisMethodTypesDifferenceTests;
            combo.DataPropertyName = "AnalysisMethodDifferenceTest";
            combo.ValueType = typeof(AnalysisMethodType);
            combo.HeaderText = "Difference test";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            var _availableAnalysisMethodTypesEquivalenceTests = _resultPowerAnalysis.ComparisonPowerAnalysisResults
                .SelectMany(r => r.AnalysisMethodEquivalenceTest.GetFlags().ToArray()).Distinct().ToList();

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
            dataGridViewComparisons.Columns[0].ReadOnly = true;

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
            if (_resultPowerAnalysis != null) {
                var primaryComparisons = _resultPowerAnalysis.GetPrimaryComparisons().ToList();
                if (primaryComparisons.Count > 0) {
                    var records = _resultPowerAnalysis.GetAggregateOutputRecords().ToList();
                    var plotType = (AnalysisPlotType)comboBoxAnalysisPlotTypes.SelectedValue;
                    var testType = (TestType)comboBoxTestType.SelectedValue;
                    if (plotType == AnalysisPlotType.Replicates) {
                        plotView.Model = PowerVersusReplicatesCsdChartCreator.Create(records, testType);
                    } else if (plotType == AnalysisPlotType.ConcernStandardizedDifference) {
                        plotView.Model = PowerVersusCsdChartCreator.Create(records, testType, primaryComparisons.First().InputPowerAnalysis.NumberOfReplications);
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
            if (_resultPowerAnalysis.GetPrimaryComparisons().Count() > 0) {
                var tempPath = Path.GetTempPath();
                var title = Path.GetFileNameWithoutExtension(_currentProjectFilePath) + "_CSD";
                var multiComparisonReportGenerator = new MultiComparisonReportGenerator(_resultPowerAnalysis, _currentProjectFilePath);
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
