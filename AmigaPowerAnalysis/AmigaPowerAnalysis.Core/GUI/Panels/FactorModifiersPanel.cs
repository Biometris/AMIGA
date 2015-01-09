using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Helpers.Statistics.Measurements;

namespace AmigaPowerAnalysis.GUI {
    public partial class FactorModifiersPanel : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        //private List<ModifierFactorLevelCombination> _currentFactorModifiers;

        public FactorModifiersPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Factor modifiers";
            Description = "The power of tests will be lower if data are uninformative or less informative, e.g. if counts are very low (<5), or fractions are close to 0 or 1. In principle, the already specified Comparator Means and CVs are sufficient to perform the power analysis. However, it should be specified if other factors in the design are expected to make part of the data less informative.\r\nFor fixed factors, provide multiplication factors for factor levels where data may become less informative (e.g. counts less than 5, or all binomial results positive or all negative).";
            checkBoxUseFactorModifiers.Checked = _project.UseFactorModifiers;
            createDataGridEndpoints();
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridEndpoints();
            updateDataGridFactorModifiers();
            updateVisibilities();
        }

        public bool IsVisible() {
            return _project != null && _project.Endpoints.Any(ep => ep.NonInteractionFactors.Count() > 0);
        }

        private void updateVisibilities() {
            dataGridViewEndpoints.Visible = _project.UseFactorModifiers;
            dataGridViewFactorModifiers.Visible = _project.UseFactorModifiers;
            groupBoxFactorModifiers.Visible = _project.Endpoints.Any(ep => ep.NonInteractionFactors.Count() > 0);
        }

        private void createDataGridEndpoints() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Endpoint";
            column.ReadOnly = true;
            dataGridViewEndpoints.Columns.Add(column);
        }

        private void updateDataGridEndpoints() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = endpointsBindingSouce;
        }

        private void updateDataGridFactorModifiers() {
            dataGridViewFactorModifiers.DataSource = null;
            if (_currentEndpoint != null) {
                var dataTable = new DataTable();
                var modifierFactors = _currentEndpoint.NonInteractionFactors.ToList();
                foreach (var modifierFactor in modifierFactors) {
                    dataTable.Columns.Add(modifierFactor.Name, typeof(string));
                }
                dataTable.Columns.Add("Modifier", typeof(double));
                dataTable.Columns.Add("Frequency", typeof(double));
                dataTable.Columns.Add("Modified mean", typeof(double));
                foreach (var factorLevelCombination in _currentEndpoint.Modifiers) {
                    DataRow row = dataTable.NewRow();
                    foreach (var factorLevel in factorLevelCombination.Levels) {
                        row[factorLevel.Parent.Name] = factorLevel.Label;
                    }
                    row["Modifier"] = Math.Round(factorLevelCombination.ModifierFactor, 2);
                    row["Frequency"] = Math.Round(factorLevelCombination.Frequency, 2);
                    row["Modified mean"] = Math.Round(MeasurementFactory.Modify(_currentEndpoint.MuComparator, factorLevelCombination.ModifierFactor, _currentEndpoint.Measurement), 2);
                    dataTable.Rows.Add(row);
                }
                dataGridViewFactorModifiers.Columns.Clear();
                dataGridViewFactorModifiers.DataSource = dataTable;
                for (int i = 0; i < modifierFactors.Count; ++i) {
                    dataGridViewFactorModifiers.Columns[i].ReadOnly = true;
                }
            }
            foreach (DataGridViewColumn column in dataGridViewFactorModifiers.Columns) {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewFactorModifiers.Columns["Frequency"].ReadOnly = true;
            dataGridViewFactorModifiers.Columns["Modified mean"].ReadOnly = true;
            dataGridViewFactorModifiers.Refresh();
        }

        private void dataGridViewEndpoints_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewEndpoints.CurrentRow.Index);
            var factorFactorLevelTuples = _currentEndpoint.InteractionFactors.SelectMany(f => f.FactorLevels, (ifc, fl) => new Tuple<IFactor, FactorLevel>(ifc, fl)).ToList();
            updateDataGridFactorModifiers();
        }

        private void checkBoxUseFactorModifiers_CheckedChanged(object sender, EventArgs e) {
            _project.SetUseFactorModifiers(checkBoxUseFactorModifiers.Checked);
            updateVisibilities();
        }

        private void dataGridViewFactorModifiers_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var editedCell = dataGridViewFactorModifiers.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (_currentEndpoint != null) {
                if (editedCell.ColumnIndex == dataGridViewFactorModifiers.Columns["Modifier"].Index) {
                    _currentEndpoint.SetModifier(e.RowIndex, (double)editedCell.Value);
                    updateDataGridFactorModifiers();
                }
            }
        }

        private void dataGridViewFactorModifiers_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            showError("Invalid data", e.Exception.Message);
        }

        private void showError(string title, string message) {
            MessageBox.Show(
                    message,
                    title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
        }

        public event EventHandler TabVisibilitiesChanged;

        private void fireTabVisibilitiesChanged() {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
        }
    }
}
