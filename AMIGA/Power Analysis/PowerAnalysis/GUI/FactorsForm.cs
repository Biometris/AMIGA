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
    public partial class FactorsForm : UserControl, ISelectionForm {

        private Project _project;

        private Factor _currentFactor;

        public FactorsForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Factors";
            Description = "The main factor in the experiment is variety, with levels 1 (labelled GMO) and 2 (labelled Comparator). If the design contains more varieies enter additional rows. If numbers of plots per variety are not equal, change the (relative) frequencies. If the design contains more factors (e.g. spraying treatments), add additional rows in the factor table, and specify the levels in the Levels table.";
            this.textBoxTabTitle.Text = Name;
            this.textBoxTabDescription.Text = Description;
            createDataGridFactors();
            createDataGridFactorLevels();
        }

        public string Description { get; private set; }

        public void Activate() {
            dataGridFactors.Rows[0].ReadOnly = true;
            dataGridFactors.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridFactors.Rows[0].Cells["ExperimentUnitType"].ReadOnly = false;
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void createDataGridFactors() {
            var factorsBindingSouce = new BindingSource(_project.Factors, null);
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
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            combo.Visible = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            dataGridFactors.Columns.Add(combo);

            factorsBindingSouce.AddingNew += new AddingNewEventHandler(factorsBindingSouceSource_AddingNew);
        }

        private void createDataGridFactorLevels() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Label";
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Level";
            column.HeaderText = "Level";
            column.ValueType = typeof(double);
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Frequency";
            column.Name = "Frequency";
            column.HeaderText = "Frequency";
            column.ValueType = typeof(int);
            dataGridViewFactorLevels.Columns.Add(column);

            updateDataGridFactorLevels();
        }

        private void updateDataGridFactorLevels() {
            if (_currentFactor != null) {
                var factorLevels = _currentFactor.FactorLevels;
                var factorLevelsBindingSouce = new BindingSource(factorLevels, null);
                dataGridViewFactorLevels.AutoGenerateColumns = false;
                dataGridViewFactorLevels.DataSource = factorLevelsBindingSouce;
                factorLevelsBindingSouce.AddingNew += new AddingNewEventHandler(factorLevelsBindingSouce_AddingNew);
            }
        }

        private void factorLevelsBindingSouce_AddingNew(object sender, AddingNewEventArgs e) {
            e.NewObject = new FactorLevel() {
                Parent = _currentFactor,
                Level = _currentFactor.GetUniqueFactorLevel(),
            };
        }

        private void factorsBindingSouceSource_AddingNew(object sender, AddingNewEventArgs e) {
            e.NewObject = new Factor("New factor", 2);
        }

        private void dataGridFactors_SelectionChanged(object sender, EventArgs e) {
            _currentFactor = _project.Factors.ElementAt(dataGridFactors.CurrentRow.Index);
            updateDataGridFactorLevels();
        }

        private void dataGridFactors_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {
            _project.UpdateEndpointFactors();
        }

        private void dataGridFactors_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            _project.UpdateEndpointFactors();
        }

        private void dataGridFactors_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (dataGridFactors.Columns[e.ColumnIndex].Name == "Name") {
                var newValue = e.FormattedValue.ToString();
                if (string.IsNullOrEmpty(newValue)) {
                    dataGridFactors.Rows[e.RowIndex].ErrorText = "Factor name cannot not be empty.";
                    showError("Invalid data", dataGridFactors.Rows[e.RowIndex].ErrorText);
                    e.Cancel = true;
                } else {
                    var newFactorNames = _project.Factors.Select(f => f.Name).ToList();
                    newFactorNames[e.RowIndex] = newValue;
                    if (newFactorNames.Distinct().Count() < newFactorNames.Count) {
                        dataGridFactors.Rows[e.RowIndex].ErrorText = "Duplicate factor names are not allowed.";
                        showError("Invalid data", dataGridFactors.Rows[e.RowIndex].ErrorText);
                        e.Cancel = true;
                    }
                }
            }
        }

        private void dataGridViewFactorLevels_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (dataGridViewFactorLevels.Columns[e.ColumnIndex].Name == "Label") {
                var newValue = e.FormattedValue.ToString();
                if (string.IsNullOrEmpty(newValue)) {
                    dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText = "Factor label cannot not be empty.";
                    showError("Invalid data", dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText);
                    e.Cancel = true;
                } else {
                    var newFactorLabelNames = _project.Factors.SelectMany(f => f.FactorLevels).Select(fl => fl.Label).ToList();
                    newFactorLabelNames[e.RowIndex] = newValue;
                    if (newFactorLabelNames.Distinct().Count() < newFactorLabelNames.Count) {
                        dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText = "Duplicate factor label names are not allowed.";
                        showError("Invalid data", dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText);
                        e.Cancel = true;
                    }
                }
            }
            if (dataGridViewFactorLevels.Columns[e.ColumnIndex].Name == "Level") {
                var newValue = Convert.ToDouble(e.FormattedValue);
                var newFactorLevels = _project.Factors.SelectMany(f => f.FactorLevels).Select(fl => fl.Level).ToList();
                newFactorLevels[e.RowIndex] = newValue;
                if (newFactorLevels.Distinct().Count() < newFactorLevels.Count) {
                    dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText = "Duplicate factor levels are not allowed.";
                    showError("Invalid data", dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText);
                    e.Cancel = true;
                }
            }
        }

        private void dataGridFactors_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            showError("Invalid data", e.Exception.Message);
        }

        private void dataGridViewFactorLevels_DataError(object sender, DataGridViewDataErrorEventArgs e) {
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
    }
}
