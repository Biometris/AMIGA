using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

// TODO The Variety factor should not have an Interaction checkbox

namespace AmigaPowerAnalysis.GUI {
    public partial class FactorsPanel : UserControl, ISelectionForm {

        private Project _project;

        private Factor _currentFactor;

        public FactorsPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Factors";
            Description = "The main factor in the experiment is variety, with levels 1 (labelled GMO) and 2 (labelled Comparator). If the design contains more varieies enter additional rows. If numbers of plots per variety are not equal, change the (relative) frequencies. If the design contains more factors (e.g. spraying treatments), add additional rows in the factor table, and specify the levels in the Levels table.";
            createDataGridFactors();
            createDataGridFactorLevels();
        }

        public string Description { get; private set; }

        public void Activate() {
            dataGridViewFactors.Rows[0].ReadOnly = true;
            dataGridViewFactors.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewFactors.Rows[0].Cells["ExperimentUnitType"].ReadOnly = false;
            updateDataGridFactorLevels();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void createDataGridFactors() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Factor name";
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

            updateDataGridFactors();
        }

        private void updateDataGridFactors() {
            var factorsBindingSouce = new BindingSource(_project.Factors, null);
            dataGridViewFactors.AutoGenerateColumns = false;
            dataGridViewFactors.DataSource = factorsBindingSouce;

            dataGridViewFactors.Rows[0].ReadOnly = true;
            dataGridViewFactors.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewFactors.Rows[0].Cells["ExperimentUnitType"].ReadOnly = false;
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

                dataGridViewFactorLevels.Refresh();
                if (_currentFactor.Name == "Variety" && dataGridViewFactorLevels.Rows.Count >= 2) {
                    dataGridViewFactorLevels.Rows[0].ReadOnly = true;
                    dataGridViewFactorLevels.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridViewFactorLevels.Rows[1].ReadOnly = true;
                    dataGridViewFactorLevels.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void buttonAddFactor_Click(object sender, EventArgs e) {
            var factorNames = _project.Factors.Select(ep => ep.Name).ToList();
            var newFactorName = string.Format("New factor");
            var i = 0;
            while (factorNames.Contains(newFactorName)) {
                newFactorName = string.Format("New factor {0}", i++);
            }
            var newFactor = new Factor(newFactorName, 2);
            _project.AddFactor(newFactor);
            _project.UpdateEndpointFactors();
            updateDataGridFactors();
        }

        private void buttonRemoveFactor_Click(object sender, EventArgs e) {
            var currentRow = dataGridViewFactors.CurrentRow.Index;
            if (dataGridViewFactors.SelectedRows.Count == 1) {
                if (currentRow == 0) {
                    showError("Invalid operation", "Cannot delete variety.");
                } else {
                    _project.RemoveFactor(_project.Factors[currentRow]);
                    _project.UpdateEndpointFactors();
                    updateDataGridFactors();
                }
            } else {
                showError("Invalid selection", "Please select one entire row in order to remove its corresponding factor.");
            }
        }

        private void addFactorLevelButton_Click(object sender, EventArgs e) {
            if (_currentFactor != null) {
                _currentFactor.FactorLevels.Add(new FactorLevel() {
                    Label = string.Format("Label {0}", _currentFactor.GetUniqueFactorLevel()),
                    Parent = _currentFactor,
                    Level = _currentFactor.GetUniqueFactorLevel(),
                });
                _project.UpdateEndpointFactorLevels();
                updateDataGridFactorLevels();
            }
        }

        private void buttonRemoveFactorLevel_Click(object sender, EventArgs e) {
            if (dataGridViewFactorLevels.SelectedRows.Count == 1) {
                var currentRow = dataGridViewFactorLevels.CurrentRow.Index;
                if (_currentFactor.FactorLevels.Count <= 2) {
                    showError("Invalid operation", "A factor should have at least two levels.");
                } else if (_currentFactor.Name == "Variety" && currentRow < 2) {
                    showError("Invalid operation", "Cannot delete GMO or comparator level of the variety.");
                } else {
                    _currentFactor.FactorLevels.Remove(_currentFactor.FactorLevels[currentRow]);
                    _project.UpdateEndpointFactorLevels();
                    updateDataGridFactorLevels();
                }
            } else {
                showError("Invalid selection", "Please select one entire row in order to remove its corresponding factor level.");
            }
        }

        private void dataGridFactors_SelectionChanged(object sender, EventArgs e) {
            dataGridViewFactorLevels.DataSource = null;
            dataGridViewFactorLevels.Refresh();
            if (dataGridViewFactors.CurrentRow.Index < _project.Factors.Count) {
                _currentFactor = _project.Factors.ElementAt(dataGridViewFactors.CurrentRow.Index);
            } else {
                _currentFactor = null;
            }
            updateDataGridFactorLevels();
        }

        private void dataGridFactors_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (dataGridViewFactors.Columns[e.ColumnIndex].Name == "Name") {
                var newValue = e.FormattedValue.ToString();
                if (string.IsNullOrEmpty(newValue)) {
                    dataGridViewFactors.Rows[e.RowIndex].ErrorText = "Factor name cannot not be empty.";
                    e.Cancel = true;
                    showError("Invalid data", dataGridViewFactors.Rows[e.RowIndex].ErrorText);
                } else {
                    var newFactorNames = _project.Factors.Select(f => f.Name).ToList();
                    newFactorNames[e.RowIndex] = newValue;
                    if (newFactorNames.Distinct().Count() < newFactorNames.Count) {
                        dataGridViewFactors.Rows[e.RowIndex].ErrorText = "Duplicate factor names are not allowed.";
                        e.Cancel = true;
                        showError("Invalid data", dataGridViewFactors.Rows[e.RowIndex].ErrorText);
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
                    var newFactorLabelNames = _currentFactor.FactorLevels.Select(fl => fl.Label).ToList();
                    newFactorLabelNames[e.RowIndex] = newValue;
                    if (newFactorLabelNames.Distinct().Count() < newFactorLabelNames.Count) {
                        dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText = "Duplicate factor label names are not allowed.";
                        e.Cancel = true;
                        showError("Invalid data", dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText);
                    }
                }
            }
            if (dataGridViewFactorLevels.Columns[e.ColumnIndex].Name == "Level") {
                double newValue;
                if (double.TryParse(e.FormattedValue.ToString(), out newValue)) {
                    var newFactorLevels = _currentFactor.FactorLevels.Select(fl => fl.Level).ToList();
                    newFactorLevels[e.RowIndex] = newValue;
                    if (newFactorLevels.Distinct().Count() < newFactorLevels.Count) {
                        dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText = "Duplicate factor levels are not allowed.";
                        e.Cancel = true;
                        showError("Invalid data", dataGridViewFactorLevels.Rows[e.RowIndex].ErrorText);
                    }
                }
            }
            if (dataGridViewFactorLevels.Columns[e.ColumnIndex].Name == "Frequency") {
                double newValue;
                if (double.TryParse(e.FormattedValue.ToString(), out newValue)) {
                    if (newValue <= 0) {
                        e.Cancel = true;
                        showError("Invalid data", "Frequency of factor levels must be postitive.");
                    }
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
