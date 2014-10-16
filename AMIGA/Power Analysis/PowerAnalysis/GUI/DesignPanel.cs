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
    public partial class DesignPanel : UserControl, ISelectionForm {

        private Project _project;

        private Factor _currentFactor;

        public DesignPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Design";
            Description = "Specify the type of experimental design. When other factors have been specified, the GMO-CMP Variety comparisons can be expected to be the same for all levels of such a factor (no interaction) or different (interaction). Indicate if such interactions are expected for one or more endpoints. Uncheck the box 'Use interactions for all endpoints' will allow you to specify specific endpoints in the next screen. Note: Interactions with Variety will lower the effective replication, because comparisons are now needed at the separate levels of the other factor. For specified interactions in a split-plot design, indicate the level where the factor is randomised. For specified interactions, indicate both for the GMO and the CMP the levels of the additional factor that have to be compared.";
            createDataGridFactors();
            this.radioButtonCompletelyRandomized.Checked = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.CompletelyRandomized;
            this.radioButtonRandomizedCompleteBlocks.Checked = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.RandomizedCompleteBlocks;
            this.radioButtonSplitPlot.Checked = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridViewFactors();
            updateVisibilities();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void onTabVisibilitiesChanged() {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
        }

        private void updateVisibilities() {
            if (_project.Factors.Count <= 1) {
                dataGridViewFactors.Visible = false;
                radioButtonSplitPlot.Visible = false;
            } else {
                radioButtonSplitPlot.Visible = true;
                dataGridViewFactors.Visible = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
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
            combo.Visible = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            dataGridViewFactors.Columns.Add(combo);
        }

        private void updateDataGridViewFactors() {
            var factorsBindingSouce = new BindingSource(_project.Factors, null);
            dataGridViewFactors.AutoGenerateColumns = false;
            dataGridViewFactors.DataSource = factorsBindingSouce;
            dataGridViewFactors.Rows[0].ReadOnly = true;
            dataGridViewFactors.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewFactors.Rows[0].Cells["ExperimentUnitType"].ReadOnly = false;
        }

        private void dataGridFactors_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewFactors.CurrentRow != null && dataGridViewFactors.CurrentRow.Index < _project.Factors.Count) {
                _currentFactor = _project.Factors.ElementAt(dataGridViewFactors.CurrentRow.Index);
            }
        }

        private void radioButtonTypeOfDesign_CheckedChanged(object sender, EventArgs e) {
            if (this.radioButtonCompletelyRandomized.Checked) {
                _project.DesignSettings.ExperimentalDesignType = ExperimentalDesignType.CompletelyRandomized;
            } else if (this.radioButtonRandomizedCompleteBlocks.Checked) {
                _project.DesignSettings.ExperimentalDesignType = ExperimentalDesignType.RandomizedCompleteBlocks;
            } else if (this.radioButtonSplitPlot.Checked) {
                _project.DesignSettings.ExperimentalDesignType = ExperimentalDesignType.SplitPlots;
            }
            if (dataGridViewFactors.ColumnCount > 0) {
                dataGridViewFactors.Columns["ExperimentUnitType"].Visible = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            }
            dataGridViewFactors.Visible = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
        }
    }
}
