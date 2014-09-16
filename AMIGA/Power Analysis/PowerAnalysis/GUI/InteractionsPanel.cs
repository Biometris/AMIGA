using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI {
    public partial class InteractionsPanel : UserControl, ISelectionForm {

        private Project _project;

        private Factor _currentFactor;

        private DataTable _interactionsDataTable;

        public InteractionsPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Interactions";
            Description = "Indicate per endpoint which factors are expected to have an interaction with the GMO-CMP Variety comparison. The appropraite levels of these factors can then be chosen in the Comparison tab.";
            createDataGridFactors();
            createDataGridFactorLevels();
            createDataGridInteractions();
            updateVisibilities();
        }

        public string Description { get; private set; }

        public void Activate() {
            var factorsBindingSouce = new BindingSource(_project.Factors, null);
            checkBoxUseInteractions.Checked = _project.DesignSettings.UseInteractions;
            checkBoxUseDefaultInteractions.Checked = _project.DesignSettings.UseDefaultInteractions;
            updateDataGridInteractions();
            updateDataGridViewFactors();
            updateDataGridFactorLevels();
            updateVisibilities();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void updateVisibilities() {
            if (_project.Factors.Count <= 1) {
                checkBoxUseDefaultInteractions.Visible = false;
                checkBoxUseInteractions.Visible = false;
                dataGridViewFactors.Visible = false;
                dataGridViewFactorLevels.Visible = false;
                dataGridInteractions.Visible = false;
            } else {
                checkBoxUseInteractions.Visible = true;
                checkBoxUseDefaultInteractions.Visible = _project.DesignSettings.UseInteractions;
                dataGridViewFactors.Visible = _project.DesignSettings.UseInteractions;
                dataGridViewFactorLevels.Visible = _project.DesignSettings.UseInteractions;
                dataGridInteractions.Visible = !_project.DesignSettings.UseDefaultInteractions;
            }
        }

        private void createDataGridFactors() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Factor name";
            column.ReadOnly = true;
            dataGridViewFactors.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionWithVariety";
            checkbox.Name = "IsInteractionWithVariety";
            checkbox.HeaderText = "Interaction";
            dataGridViewFactors.Columns.Add(checkbox);
        }

        private void createDataGridFactorLevels() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Label";
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Level";
            column.HeaderText = "Level";
            column.ValueType = typeof(double);
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsComparisonLevelGMO";
            checkbox.Name = "IsComparisonLevelGMO";
            dataGridViewFactorLevels.Columns.Add(checkbox);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsComparisonLevelComparator";
            checkbox.Name = "IsComparisonLevelComparator";
            dataGridViewFactorLevels.Columns.Add(checkbox);

            updateDataGridFactorLevels();
        }

        private void createDataGridInteractions() {
            updateDataGridInteractions();
        }

        private void updateDataGridViewFactors() {
            var factorsBindingSouce = new BindingSource(_project.Factors, null);
            dataGridViewFactors.AutoGenerateColumns = false;
            dataGridViewFactors.DataSource = factorsBindingSouce;
            dataGridViewFactors.Rows[0].ReadOnly = true;
            dataGridViewFactors.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void updateDataGridFactorLevels() {
            if (_currentFactor != null) {
                var factorLevels = _currentFactor.FactorLevels;
                var factorLevelsBindingSouce = new BindingSource(factorLevels, null);
                dataGridViewFactorLevels.AutoGenerateColumns = false;
                dataGridViewFactorLevels.DataSource = factorLevelsBindingSouce;
                if (dataGridViewFactorLevels.Columns.Count > 0) {
                    dataGridViewFactorLevels.Columns["IsComparisonLevelGMO"].Visible = _currentFactor.IsInteractionWithVariety;
                    dataGridViewFactorLevels.Columns["IsComparisonLevelComparator"].Visible = _currentFactor.IsInteractionWithVariety;
                }
            }
        }

        private void updateDataGridInteractions() {
            _interactionsDataTable = new DataTable();
            dataGridInteractions.DataSource = _interactionsDataTable;
            _interactionsDataTable.Columns.Clear();
            _interactionsDataTable.Columns.Add("Endpoint");
            for (int i = 1; i < _project.Factors.Count; ++i) {
                _interactionsDataTable.Columns.Add(_project.Factors.ElementAt(i).Name, typeof(bool));
                if (!_project.Factors.ElementAt(i).IsInteractionWithVariety) {
                    dataGridInteractions.Columns[i].ReadOnly = true;
                    dataGridInteractions.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
            for (int i = 0; i < _project.Endpoints.Count; ++i) {
                DataRow row = _interactionsDataTable.NewRow();
                row["Endpoint"] = _project.Endpoints.ElementAt(i).Name;
                var endpointInteractions = _project.Endpoints.ElementAt(i).InteractionFactors;
                for (int j = 0; j < endpointInteractions.Count(); ++j) {
                    if (_interactionsDataTable.Columns.Contains(endpointInteractions.ElementAt(j).Name)) {
                        row[endpointInteractions.ElementAt(j).Name] = true;
                    }
                }
                _interactionsDataTable.Rows.Add(row);
            }
        }

        private void checkBoxUseInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.SetUseInteractions(checkBoxUseInteractions.Checked);
            updateVisibilities();
        }

        private void dataGridViewFactors_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var editedCell = this.dataGridViewFactors.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var newValue = editedCell.Value;
            if (e.ColumnIndex == dataGridViewFactors.Columns["IsInteractionWithVariety"].Index) {
                if (dataGridViewFactorLevels.Columns.Count > 0) {
                    dataGridViewFactorLevels.Columns["IsComparisonLevelGMO"].Visible = _currentFactor.IsInteractionWithVariety;
                    dataGridViewFactorLevels.Columns["IsComparisonLevelComparator"].Visible = _currentFactor.IsInteractionWithVariety;
                }
            }
            updateDataGridInteractions();
        }

        private void dataGridViewFactors_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            var cell = this.dataGridViewFactors.CurrentCell;
            if (cell.ColumnIndex == dataGridViewFactors.Columns["IsInteractionWithVariety"].Index) {
                if (dataGridViewFactors.IsCurrentCellDirty) {
                    dataGridViewFactors.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    var factor = _project.Factors.ElementAt(cell.RowIndex);
                    foreach (var endpoint in _project.Endpoints) {
                        endpoint.SetFactorType(factor, factor.IsInteractionWithVariety);
                    }
                }
            }
            updateDataGridInteractions();
        }

        private void dataGridViewFactors_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewFactors.CurrentRow.Index < _project.Factors.Count) {
                _currentFactor = _project.Factors.ElementAt(dataGridViewFactors.CurrentRow.Index);
            }
            updateDataGridFactorLevels();
        }

        private void dataGridViewFactorLevels_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            updateDataGridFactorLevels();
        }

        private void checkBoxUseDefaultInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.SetDefaultInteractions(checkBoxUseDefaultInteractions.Checked);
            updateVisibilities();
        }

        private void dataGridInteractions_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex > 0 && e.ColumnIndex - 1 < _project.Factors.Count) {
                var endpoint = _project.Endpoints.ElementAt(e.RowIndex);
                var factor = _project.Factors.ElementAt(e.ColumnIndex);
                var isChecked = (bool)_interactionsDataTable.Rows[e.RowIndex][e.ColumnIndex];
                endpoint.SetFactorType(factor, isChecked);
            }
        }
    }
}
