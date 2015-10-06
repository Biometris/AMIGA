using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.GUI.Wrappers;

namespace AmigaPowerAnalysis.GUI {
    public partial class InteractionsPanel : UserControl, ISelectionForm {

        private Project _project;
        private List<InteractionsWrapper> _defaultInteractionLevels;

        public InteractionsPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Define comparison";
            Description = "The Test-CMP comparison may be restricted to a subset of levels of additional factors for the Test and/or for the CMP. Indicate any factors for which this is relevant, and uncheck the levels to be excluded.";
            createDataGridFactors();
            createDataGridViewInteractionFactorLevelCombinations();
            updateVisibilities();
        }

        public string Description { get; private set; }

        public bool IsVisible() {
            return _project != null && _project.NonVarietyFactors.Count() > 0;
        }

        public void Activate() {
            checkBoxUseInteractions.Checked = _project.DesignSettings.UseInteractions;
            checkBoxUseDefaultInteractions.Checked = _project.DesignSettings.UseDefaultInteractions;
            updateDataGridViewFactors();
            updateDataGridViewInteractionFactorLevelCombinations();
            updateVisibilities();
        }

        private void updateVisibilities() {
            if (_project.NonVarietyFactors.Count() == 0) {
                checkBoxUseDefaultInteractions.Visible = false;
                checkBoxUseInteractions.Visible = false;
                dataGridViewFactors.Visible = false;
            } else {
                checkBoxUseInteractions.Visible = true;
                checkBoxUseDefaultInteractions.Visible = _project.DesignSettings.UseInteractions && _project.Endpoints.Count > 1;
                dataGridViewFactors.Visible = _project.DesignSettings.UseInteractions;
                dataGridViewInteractionFactorLevelCombinations.Visible = _project.DesignSettings.UseInteractions;

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
            checkbox.HeaderText = "Include in comparison";
            dataGridViewFactors.Columns.Add(checkbox);

            updateDataGridViewFactors();
        }

        private void createDataGridViewInteractionFactorLevelCombinations() {
            updateDataGridViewInteractionFactorLevelCombinations();
        }

        private void updateDataGridViewFactors() {
            var factorsBindingSouce = new BindingSource(_project.NonVarietyFactors, null);
            dataGridViewFactors.AutoGenerateColumns = false;
            dataGridViewFactors.DataSource = factorsBindingSouce;
        }

        private void updateDataGridViewInteractionFactorLevelCombinations() {
            dataGridViewInteractionFactorLevelCombinations.DataSource = null;
            var dataTable = new DataTable();

            // Create columns
            var interactionFactors = _project.Factors.Where(f => f.IsInteractionWithVariety && !f.IsVarietyFactor).ToList();
            foreach (var interactionFactor in interactionFactors) {
                dataTable.Columns.Add(interactionFactor.Name, typeof(string));
            }
            dataTable.Columns.Add("Comparison level Test", typeof(bool));
            dataTable.Columns.Add("Comparison level Comparator", typeof(bool));

            // Create interaction wrappers
            _defaultInteractionLevels = _project.DefaultInteractionFactorLevelCombinations
                .OrderBy(r => r)
                .GroupBy(ifl => ifl.NonVarietyFactorLevelCombination)
                .Select(g => new InteractionsWrapper(g.ToList()))
                .Where(i => i.Levels.Count() > 0)
                .ToList();

            foreach (var factorLevelCombination in _defaultInteractionLevels) {
                DataRow row = dataTable.NewRow();
                foreach (var factorLevel in factorLevelCombination.Levels) {
                    row[factorLevel.Parent.Name] = factorLevel.Label;
                }
                row["Comparison level Test"] = factorLevelCombination.IsComparisonLevelTest;
                row["Comparison level Comparator"] = factorLevelCombination.IsComparisonLevelComparator;
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
            var factor = _project.NonVarietyFactors.ElementAt(editedCell.RowIndex) as Factor;
            _project.SetFactorType(factor, factor.IsInteractionWithVariety);
            updateDataGridViewInteractionFactorLevelCombinations();
            fireTabVisibilitiesChanged();
        }

        private void dataGridViewFactors_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            var cell = this.dataGridViewFactors.CurrentCell;
            if (cell.ColumnIndex == dataGridViewFactors.Columns["IsInteractionWithVariety"].Index) {
                if (dataGridViewFactors.IsCurrentCellDirty) {
                    dataGridViewFactors.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void checkBoxUseDefaultInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.SetDefaultInteractions(checkBoxUseDefaultInteractions.Checked);
            updateVisibilities();
            fireTabVisibilitiesChanged();
        }

        private void dataGridViewInteractionFactorLevelCombinations_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < _defaultInteractionLevels.Count) {
                var factorLevelCombination = _defaultInteractionLevels[e.RowIndex];
                if (e.ColumnIndex == dataGridViewInteractionFactorLevelCombinations.Columns["Comparison level Test"].Index) {
                    var isChecked = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    factorLevelCombination.IsComparisonLevelTest = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                } else if (e.ColumnIndex == dataGridViewInteractionFactorLevelCombinations.Columns["Comparison level Comparator"].Index) {
                    var isChecked = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    factorLevelCombination.IsComparisonLevelComparator = (bool)dataGridViewInteractionFactorLevelCombinations.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                _project.UpdateEndpointFactorLevels();
                fireTabVisibilitiesChanged();
            }
        }

        private void dataGridViewInteractionFactorLevelCombinations_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (dataGridViewInteractionFactorLevelCombinations.IsCurrentCellDirty) {
                dataGridViewInteractionFactorLevelCombinations.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
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
