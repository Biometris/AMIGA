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
            checkBoxUseDefaultInteractions.Checked = _project.Design.UseDefaultInteractions;
        }

        public void Activate() {
        }

        private void createDataGridFactors() {
            var factorsBindingSouce = new BindingSource(_project.Design.Factors, null);
            dataGridFactors.AutoGenerateColumns = false;
            dataGridFactors.DataSource = factorsBindingSouce;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Factor name";
            dataGridFactors.Columns.Add(column);

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(ExperimentUnitType));
            combo.Name = "ExperimentUnitType";
            combo.DataPropertyName = "ExperimentUnitType";
            combo.ValueType = typeof(ExperimentUnitType);
            combo.HeaderText = "Plot level";
            combo.Visible = _project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            dataGridFactors.Columns.Add(combo);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionWithVariety";
            checkbox.Name = "IsInteractionWithVariety";
            checkbox.HeaderText = "Interaction";
            dataGridFactors.Columns.Add(checkbox);
        }

        private void createDataGridFactorLevels() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Label";
            dataGridFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Level";
            column.HeaderText = "Level";
            column.ValueType = typeof(double);
            dataGridFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Frequency";
            column.Name = "Frequency";
            column.HeaderText = "Frequency";
            column.ValueType = typeof(int);
            dataGridFactorLevels.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionLevelGMO";
            checkbox.Name = "IsInteractionLevelGMO";
            dataGridFactorLevels.Columns.Add(checkbox);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionLevelComparator";
            checkbox.Name = "IsInteractionLevelComparator";
            dataGridFactorLevels.Columns.Add(checkbox);

            updateDataGridFactorLevels();
        }

        private void updateDataGridFactorLevels() {
            if (_currentFactor != null) {
                var factorLevels = _currentFactor.FactorLevels;
                var factorLevelsBindingSouce = new BindingSource(factorLevels, null);
                dataGridFactorLevels.AutoGenerateColumns = false;
                dataGridFactorLevels.DataSource = factorLevelsBindingSouce;
                // TODO: check for leaks
                factorLevelsBindingSouce.AddingNew += new AddingNewEventHandler(bindingSource_AddingNew);
                dataGridFactorLevels.Columns["IsInteractionLevelGMO"].Visible = _currentFactor.IsInteractionWithVariety;
                dataGridFactorLevels.Columns["IsInteractionLevelComparator"].Visible = _currentFactor.IsInteractionWithVariety; 
            }
        }

        private void bindingSource_AddingNew(object sender, AddingNewEventArgs e) {
            e.NewObject = new FactorLevel() {
                Level = _currentFactor.GetUniqueFactorLevel(),
            };
        }

        private void dataGridFactors_SelectionChanged(object sender, EventArgs e) {
            _currentFactor = _project.Design.Factors.ElementAt(dataGridFactors.CurrentRow.Index);
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
            if (dataGridFactorLevels.ColumnCount > 0) {
                dataGridFactors.Columns["ExperimentUnitType"].Visible = _project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            }
        }

        private void checkBoxUseDefaultInteractions_CheckedChanged(object sender, EventArgs e) {
            _project.Design.UseDefaultInteractions = checkBoxUseDefaultInteractions.Checked;
        }

        private void dataGridFactors_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            checkBoxUseDefaultInteractions.Visible = _project.Design.Factors.Count > 1;
        }

        private void dataGridFactors_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            checkBoxUseDefaultInteractions.Visible = _project.Design.Factors.Count > 1;
        }

        private void dataGridFactors_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var editedCell = this.dataGridFactors.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var newValue = editedCell.Value;
            if (e.ColumnIndex == dataGridFactors.Columns["IsInteractionWithVariety"].Index) {
                _project.UpdateInteractionsOfFactor(_project.Design.Factors.ElementAt(e.RowIndex));
                dataGridFactorLevels.Columns["IsInteractionLevelGMO"].Visible = _currentFactor.IsInteractionWithVariety;
                dataGridFactorLevels.Columns["IsInteractionLevelComparator"].Visible = _currentFactor.IsInteractionWithVariety;
            }
        }
    }
}
