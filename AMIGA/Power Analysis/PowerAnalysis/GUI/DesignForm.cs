using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

// TODO Factor name must be entered before Interaction can be checked (otherwise fault)
// TODO Factor names must be unique
// TODO Factor labels must be unique
// TODO Factor labels are compulsitory
// TODO A factor should have at least two labels; remove factors with 0/1 labels (present user with a message)
// TODO Not allowed to remove (and rename?) the Variety factor 
// TODO The Variety factor should not have an Interaction checkbox
// TODO The First level of the variety factor is considered to be the GMO; second the comparator. renaming of these levels is not allowed???
// TODO Frequency off factor levels must be postitive

namespace AmigaPowerAnalysis.GUI {
    public partial class DesignForm : UserControl, ISelectionForm {

        private Project _project;

        private Factor _currentFactor;

        public DesignForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Design";
            createDataGridFactors();
            createDataGridFactorLevels();
            checkBoxUseInteractions.Checked = _project.UseInteractions;
            checkBoxUseDefaultInteractions.Checked = _project.UseDefaultInteractions;
        }

        public void Activate() {
            var factorsBindingSouce = new BindingSource(_project.Design.Factors, null);
            dataGridViewFactors.AutoGenerateColumns = false;
            dataGridViewFactors.DataSource = factorsBindingSouce;

            dataGridViewFactors.Rows[0].ReadOnly = true;
            dataGridViewFactors.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewFactors.Rows[0].Cells["ExperimentUnitType"].ReadOnly = false;
            updateVisibilities();
        }

        private void updateVisibilities() {
            if (_project.Design.Factors.Count <= 1) {
                groupBoxInteractions.Visible = false;
                dataGridViewFactors.Visible = false;
                dataGridViewFactorLevels.Visible = false;
                radioButtonSplitPlot.Visible = false;
            } else {
                groupBoxInteractions.Visible = true;
                checkBoxUseDefaultInteractions.Visible = _project.UseInteractions;
                dataGridViewFactors.Visible = _project.UseInteractions;
                dataGridViewFactorLevels.Visible = _project.UseInteractions;
            }
        }

        private void createDataGridFactors() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Factor name";
            column.ReadOnly = true;
            dataGridViewFactors.Columns.Add(column);

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(ExperimentUnitType));
            combo.Name = "ExperimentUnitType";
            combo.DataPropertyName = "ExperimentUnitType";
            combo.ValueType = typeof(ExperimentUnitType);
            combo.HeaderText = "Plot level";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            combo.Visible = _project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            dataGridViewFactors.Columns.Add(combo);

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

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Frequency";
            column.Name = "Frequency";
            column.HeaderText = "Frequency";
            column.ValueType = typeof(int);
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionLevelGMO";
            checkbox.Name = "IsInteractionLevelGMO";
            dataGridViewFactorLevels.Columns.Add(checkbox);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionLevelComparator";
            checkbox.Name = "IsInteractionLevelComparator";
            dataGridViewFactorLevels.Columns.Add(checkbox);

            updateDataGridFactorLevels();
        }

        private void updateDataGridFactorLevels() {
            if (_currentFactor != null) {
                var factorLevels = _currentFactor.FactorLevels;
                var factorLevelsBindingSouce = new BindingSource(factorLevels, null);
                dataGridViewFactorLevels.AutoGenerateColumns = false;
                dataGridViewFactorLevels.DataSource = factorLevelsBindingSouce;
                if (dataGridViewFactorLevels.Columns.Count > 0) {
                    dataGridViewFactorLevels.Columns["IsInteractionLevelGMO"].Visible = _currentFactor.IsInteractionWithVariety;
                    dataGridViewFactorLevels.Columns["IsInteractionLevelComparator"].Visible = _currentFactor.IsInteractionWithVariety; 
                }
            }
        }

        private void dataGridFactors_SelectionChanged(object sender, EventArgs e) {
            _currentFactor = _project.Design.Factors.ElementAt(dataGridViewFactors.CurrentRow.Index);
            updateDataGridFactorLevels();
        }

        private void radioButtonTypeOfDesign_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonCompletelyRandomized.Checked) {
                _project.Design.ExperimentalDesignType = ExperimentalDesignType.CompletelyRandomized;
            } else if (this.radioButtonRandomizedCompleteBlocks.Checked) {
                _project.Design.ExperimentalDesignType = ExperimentalDesignType.RandomizedCompleteBlocks;
            } else if (this.radioButtonSplitPlot.Checked) {
                _project.Design.ExperimentalDesignType = ExperimentalDesignType.SplitPlots;
            }
            if (dataGridViewFactorLevels.ColumnCount > 0) {
                dataGridViewFactors.Columns["ExperimentUnitType"].Visible = _project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            }
        }

        private void checkBoxUseInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.UseInteractions = checkBoxUseInteractions.Checked;
            updateVisibilities();
        }

        private void checkBoxUseDefaultInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.UseDefaultInteractions = checkBoxUseDefaultInteractions.Checked;
        }

        private void dataGridFactors_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var editedCell = this.dataGridViewFactors.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var newValue = editedCell.Value;
            if (e.ColumnIndex == dataGridViewFactors.Columns["IsInteractionWithVariety"].Index) {
                if (dataGridViewFactorLevels.Columns.Count > 0) {
                    dataGridViewFactorLevels.Columns["IsInteractionLevelGMO"].Visible = _currentFactor.IsInteractionWithVariety;
                    dataGridViewFactorLevels.Columns["IsInteractionLevelComparator"].Visible = _currentFactor.IsInteractionWithVariety;
                }
            }
        }

        private void dataGridFactors_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            var cell = this.dataGridViewFactors.CurrentCell;
            if (cell.ColumnIndex == dataGridViewFactors.Columns["IsInteractionWithVariety"].Index) {
                // TODO: update interaction factors for the factor levels
                if (dataGridViewFactors.IsCurrentCellDirty) {
                    dataGridViewFactors.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    var factor = _project.Design.Factors.ElementAt(cell.RowIndex);
                    if (factor.IsInteractionWithVariety) {
                        foreach (var endpoint in _project.Endpoints) {
                            endpoint.AddInteractionFactor(factor);
                        }
                    } else {
                        foreach (var endpoint in _project.Endpoints) {
                            endpoint.RemoveInteractionFactor(factor);
                        }
                    }
                }
            }
        }
    }
}
