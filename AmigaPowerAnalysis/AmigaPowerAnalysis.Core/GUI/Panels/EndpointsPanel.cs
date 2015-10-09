using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using Biometris.Statistics.Measurements;

namespace AmigaPowerAnalysis.GUI {
    public partial class EndpointsPanel : UserControl, ISelectionForm {

        private Project _project;
        private ComboBox _currentComboBox;

        public EndpointsPanel(Project project) {
            InitializeComponent();
            Name = "Endpoints";
            Description = "Enter a list of endpoints. For each endpoint indicate its group (retrieves default settings), and if needed adapt the measurement type and limits of concern (LoC). Endpoint groups can be edited under the Options menu. Note: measurement type can be Count, Nonnegative or Continuous.\r\n\r\nWithin Limits of Concern (LoCs) there is no concern about safety. There is not necessarily a safety concern outside these limits (no assumption is made).\r\nLoCs are specified as ratios of the mean values for Test and Comparator.\r\nProvide a lower LoC, an upper LoC, or both. Unspecified (NaN) means no concern for changes in that direction.";
            _project = project;
            dataGridViewEndpoints.AutoGenerateColumns = false;
            updateDataGridViewEndpoints();
        }

        public string Description { get; private set; }

        public event EventHandler TabVisibilitiesChanged;

        public void Activate() {
            updateDataGridViewEndpoints();
        }

        public bool IsVisible() {
            return true;
        }

        public void UpdateForm() {
            updateDataGridViewEndpoints();
        }

        private void updateDataGridViewEndpoints() {
            dataGridViewEndpoints.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewEndpoints.Columns.Add(column);

            var _availableEndpointTypes = _project.EndpointTypes.Select(h => new { Name = h.Name, EndpointType = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpointTypes;
            combo.DataPropertyName = "EndpointType";
            combo.DisplayMember = "Name";
            combo.ValueMember = "EndpointType";
            combo.HeaderText = "Endpoint group";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpoints.Columns.Add(combo);

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = new MeasurementType[] { MeasurementType.Count, MeasurementType.Nonnegative, MeasurementType.Continuous };
            combo.DataPropertyName = "Measurement";
            combo.ValueType = typeof(MeasurementType);
            combo.HeaderText = "Measurement type";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpoints.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocLower";
            column.Name = "LocLower";
            column.ValueType = typeof(double);
            dataGridViewEndpoints.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocUpper";
            column.Name = "LocUpper";
            column.ValueType = typeof(double);
            dataGridViewEndpoints.Columns.Add(column); 

            if (_project.Endpoints.Count > 0) {
                var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
                dataGridViewEndpoints.DataSource = endpointsBindingSouce;
                dataGridViewEndpoints.Update();
            } else {
                dataGridViewEndpoints.DataSource = null;
                dataGridViewEndpoints.Update();
            }

            this.dataGridViewEndpoints.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridViewEndpoints_EditingControlShowing);
        }

        void dataGridViewEndpoints_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
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
                this.dataGridViewEndpoints.EndEdit();
            }
        }

        private void addEndpointButton_Click(object sender, EventArgs e) {
            var endpointNames = _project.Endpoints.Select(ep => ep.Name).ToList();
            var newEndpointName = string.Format("Endpoint 1");
            var i = 2;
            while (endpointNames.Contains(newEndpointName)) {
                newEndpointName = string.Format("Endpoint {0}", i++);
            }
            _project.AddEndpoint(new Endpoint(newEndpointName, _project.EndpointTypes.First()));
            updateDataGridViewEndpoints();
            dataGridViewEndpoints.CurrentCell = dataGridViewEndpoints.Rows[dataGridViewEndpoints.RowCount - 1].Cells[0];
            fireTabVisibilitiesChanged();
        }

        private void buttonDeleteEndpoint_Click(object sender, EventArgs e) {
            if (dataGridViewEndpoints.SelectedRows.Count == 1) {
                _project.RemoveEndpoint(_project.Endpoints[dataGridViewEndpoints.CurrentRow.Index]);
                updateDataGridViewEndpoints();
                fireTabVisibilitiesChanged();
            } else {
                showError("Invalid selection", "Please select one entire row in order to remove its corresponding endpoint.");
            }
        }

        private void dataGridViewEndpoints_CellParsing(object sender, DataGridViewCellParsingEventArgs e) {
            if (dataGridViewEndpoints.Columns[e.ColumnIndex].Name == "LocLower" || dataGridViewEndpoints.Columns[e.ColumnIndex].Name == "LocUpper") {
                if (string.IsNullOrEmpty(e.Value.ToString())) {
                    e.Value = double.NaN;
                    e.ParsingApplied = true;
                }
            }
        }

        private void dataGridViewEndpoints_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (dataGridViewEndpoints.Columns[e.ColumnIndex].Name == "Name") {
                var newValue = e.FormattedValue.ToString();
                if (string.IsNullOrEmpty(newValue)) {
                    dataGridViewEndpoints.Rows[e.RowIndex].ErrorText = "Endpoint name cannot not be empty.";
                    showError("Invalid data", dataGridViewEndpoints.Rows[e.RowIndex].ErrorText);
                    e.Cancel = true;
                } else {
                    var newEndpointNames = _project.Endpoints.Select(ep => ep.Name).ToList();
                    newEndpointNames[e.RowIndex] = newValue;
                    if (newEndpointNames.Distinct().Count() < newEndpointNames.Count) {
                        dataGridViewEndpoints.Rows[e.RowIndex].ErrorText = "Duplicate endpoint names are not allowed.";
                        showError("Invalid data", dataGridViewEndpoints.Rows[e.RowIndex].ErrorText);
                        e.Cancel = true;
                    }
                }
            } else if (dataGridViewEndpoints.Columns[e.ColumnIndex].Name == "LocLower" || dataGridViewEndpoints.Columns[e.ColumnIndex].Name == "LocUpper") {
                if (string.IsNullOrEmpty(e.FormattedValue.ToString())) {
                    dataGridViewEndpoints.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = double.NaN;
                }
            }
        }

        private void dataGridViewEndpoints_DataError(object sender, DataGridViewDataErrorEventArgs e) {
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

        private void dataGridViewEndpoints_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Delete || e.KeyData == Keys.Back) {
                if (dataGridViewEndpoints.CurrentCell.OwningColumn == dataGridViewEndpoints.Columns["LocLower"]
                    || dataGridViewEndpoints.CurrentCell.OwningColumn == dataGridViewEndpoints.Columns["LocUpper"]) {
                    dataGridViewEndpoints.CurrentCell.Value = double.NaN;
                }
            }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e) {
            var selectedRows = getSelectedRowIndexes();
            if (selectedRows.Count == 1) {
                var currentIndex = selectedRows[0];
                var newIndex = _project.MoveEndpoint(currentIndex, 1);
                updateDataGridViewEndpoints();
                dataGridViewEndpoints.ClearSelection();
                dataGridViewEndpoints.Rows[newIndex].Selected = true;
            } else {
                showError("Invalid selection", "Please select one entire row in order to move an endpoint.");
            }
        }

        private void buttonMoveUp_Click(object sender, EventArgs e) {
            var selectedRows = getSelectedRowIndexes();
            if (selectedRows.Count == 1) {
                var currentIndex = selectedRows[0];
                var newIndex = _project.MoveEndpoint(currentIndex, -1);
                updateDataGridViewEndpoints();
                dataGridViewEndpoints.ClearSelection();
                dataGridViewEndpoints.Rows[newIndex].Selected = true;
            } else {
                showError("Invalid selection", "Please select one entire row in order to move an endpoint.");
            }
        }

        private List<int> getSelectedRowIndexes() {
            if (dataGridViewEndpoints.SelectedRows.Count > 0) {
                return dataGridViewEndpoints.SelectedRows.Cast<DataGridViewRow>()
                    .Select(row => row.Index)
                    .Distinct()
                    .ToList();
            } else {
                return dataGridViewEndpoints.SelectedCells.Cast<DataGridViewCell>()
                    .Select(cell => cell.RowIndex)
                    .Distinct()
                    .ToList();
            }
        }

        private void fireTabVisibilitiesChanged() {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
        }

        private void dataGridViewEndpoints_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            fireTabVisibilitiesChanged();
        }
    }
}
