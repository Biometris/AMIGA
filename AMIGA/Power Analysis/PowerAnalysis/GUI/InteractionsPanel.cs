using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI {
    public partial class InteractionsPanel : UserControl, ISelectionForm {

        private Project _project;

        public InteractionsPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Exclude Data";
            Description = "The GMO-CMP comparison may be restricted to a subset of levels of additional factors for the GMO and/or for the CMP. Indicate per endpoint any factors for which this is relevant, and uncheck the levels to be excluded.";
            createDataGridFactors();
            createDataGridViewInteractionFactorLevelCombinations();
            updateVisibilities();
        }

        public string Description { get; private set; }

        public bool IsVisible() {
            return true;
        }

        public void Activate() {
            checkBoxUseInteractions.Checked = _project.DesignSettings.UseInteractions;
            checkBoxUseDefaultInteractions.Checked = _project.DesignSettings.UseDefaultInteractions;
            updateDataGridViewFactors();
            updateDataGridViewInteractionFactorLevelCombinations();
            updateVisibilities();
        }

        private void updateVisibilities() {
            if (_project.Factors.Count <= 1) {
                checkBoxUseDefaultInteractions.Visible = false;
                checkBoxUseInteractions.Visible = false;
                dataGridViewFactors.Visible = false;
            } else {
                checkBoxUseInteractions.Visible = true;
                checkBoxUseDefaultInteractions.Visible = _project.DesignSettings.UseInteractions && _project.Endpoints.Count > 1;
                dataGridViewFactors.Visible = _project.DesignSettings.UseInteractions;
                dataGridViewInteractionFactorLevelCombinations.Visible = _project.DesignSettings.UseInteractions;
                if (_project.Factors.Count > 1 && dataGridViewFactors.Visible) {
                    var currencyManager = (CurrencyManager)BindingContext[dataGridViewFactors.DataSource];
                    currencyManager.SuspendBinding();
                    dataGridViewFactors.Rows[0].Visible = false;
                    currencyManager.ResumeBinding();
                }

                if (_project.DesignSettings.UseDefaultInteractions) {
                    dataGridViewFactors.Enabled = true;
                    dataGridViewFactors.ForeColor = SystemColors.ControlText;
                    dataGridViewFactors.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.Window;
                    dataGridViewFactors.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                    dataGridViewFactors.EnableHeadersVisualStyles = true;

                    dataGridViewInteractionFactorLevelCombinations.Enabled = true;
                    dataGridViewInteractionFactorLevelCombinations.ForeColor = SystemColors.ControlText;
                    dataGridViewInteractionFactorLevelCombinations.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
                    dataGridViewInteractionFactorLevelCombinations.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
                    dataGridViewInteractionFactorLevelCombinations.EnableHeadersVisualStyles = true;
                } else {
                    dataGridViewFactors.Enabled = false;
                    dataGridViewFactors.ForeColor = Color.Gray;
                    dataGridViewFactors.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                    dataGridViewFactors.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
                    dataGridViewFactors.EnableHeadersVisualStyles = false;
                    dataGridViewFactors.ClearSelection();

                    dataGridViewInteractionFactorLevelCombinations.Enabled = false;
                    dataGridViewInteractionFactorLevelCombinations.ForeColor = Color.Gray;
                    dataGridViewInteractionFactorLevelCombinations.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                    dataGridViewInteractionFactorLevelCombinations.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;
                    dataGridViewInteractionFactorLevelCombinations.EnableHeadersVisualStyles = false;
                    dataGridViewInteractionFactorLevelCombinations.CurrentCell = null;
                }
            }
        }

        public event EventHandler TabVisibilitiesChanged;

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

            updateDataGridViewFactors();
        }

        private void createDataGridViewInteractionFactorLevelCombinations() {
            updateDataGridViewInteractionFactorLevelCombinations();
        }

        private void updateDataGridViewFactors() {
            var factorsBindingSouce = new BindingSource(_project.Factors, null);
            dataGridViewFactors.AutoGenerateColumns = false;
            dataGridViewFactors.DataSource = factorsBindingSouce;
        }

        private void updateDataGridViewInteractionFactorLevelCombinations() {
            dataGridViewInteractionFactorLevelCombinations.DataSource = null;
            var dataTable = new DataTable();
            var interactionFactors = _project.Factors.Where(f => f.IsInteractionWithVariety).ToList();
            foreach (var interactionFactor in interactionFactors) {
                dataTable.Columns.Add(interactionFactor.Name, typeof(string));
            }
            dataTable.Columns.Add("Interaction GMO", typeof(bool));
            dataTable.Columns.Add("Interaction Comparator", typeof(bool));
            foreach (var factorLevelCombination in _project.DefaultInteractionFactorLevelCombinations) {
                DataRow row = dataTable.NewRow();
                foreach (var factorLevel in factorLevelCombination.Items) {
                    row[factorLevel.Parent.Name] = factorLevel.Label;
                }
                row["Interaction GMO"] = factorLevelCombination.IsComparisonLevelGMO;
                row["Interaction Comparator"] = factorLevelCombination.IsComparisonLevelComparator;
                dataTable.Rows.Add(row);
            }
            dataGridViewInteractionFactorLevelCombinations.Columns.Clear();
            dataGridViewInteractionFactorLevelCombinations.DataSource = dataTable;
            for (int i = 0; i < interactionFactors.Count; ++i) {
                dataGridViewInteractionFactorLevelCombinations.Columns[i].ReadOnly = true;
            }
        }

        private void checkBoxUseInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.SetUseInteractions(checkBoxUseInteractions.Checked);
            updateDataGridViewFactors();
            updateDataGridViewInteractionFactorLevelCombinations();
            updateVisibilities();
        }

        private void dataGridViewFactors_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var editedCell = this.dataGridViewFactors.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var newValue = editedCell.Value;
            updateDataGridViewInteractionFactorLevelCombinations();
        }

        private void dataGridViewFactors_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            var cell = this.dataGridViewFactors.CurrentCell;
            if (cell.ColumnIndex == dataGridViewFactors.Columns["IsInteractionWithVariety"].Index) {
                if (dataGridViewFactors.IsCurrentCellDirty) {
                    dataGridViewFactors.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    var factor = _project.Factors.ElementAt(cell.RowIndex);
                    _project.SetFactorType(factor, factor.IsInteractionWithVariety);
                    updateDataGridViewInteractionFactorLevelCombinations();
                }
            }
        }

        private void checkBoxUseDefaultInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.SetDefaultInteractions(checkBoxUseDefaultInteractions.Checked);
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
            updateVisibilities();
        }

        private void dataGridViewInteractionFactorLevelCombinations_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < _project.DefaultInteractionFactorLevelCombinations.Count) {
                var factorLevelCombination = _project.DefaultInteractionFactorLevelCombinations[e.RowIndex];
                if (e.ColumnIndex == dataGridViewInteractionFactorLevelCombinations.Columns["Interaction GMO"].Index) {
                    var isChecked = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    factorLevelCombination.IsComparisonLevelGMO = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                } else if (e.ColumnIndex == dataGridViewInteractionFactorLevelCombinations.Columns["Interaction Comparator"].Index) {
                    var isChecked = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    factorLevelCombination.IsComparisonLevelComparator = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                _project.UpdateEndpointFactorLevels();
            }
        }
    }
}
