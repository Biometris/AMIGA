using System;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Helpers.Statistics.Measurements;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class EndpointsPanel : UserControl, ISelectionForm {

        private Project _project;

        public string Description { get; private set; }

        public EndpointsPanel(Project project) {
            InitializeComponent();
            Name = "Endpoints";
            Description = "Enter a list of endpoints. For each endpoint indicate its group (retrieves default settings), and if needed adapt the measurement type and limits of concern (LoC). Endpoint groups can be edited under the Options menu. Note: currently only methods for Measurement type Count have been implemented.\r\n\r\nLimits of Concern are ratios of the expected values for the GMO and the Comparator. Within these limits there is no concern about safety.\r\nProvide a lower LoC, an upper LoC, or both. Unspecified (NaN) means no concern for changes in that direction.";
            _project = project;
            createDataGridEndpoints();
        }

        public void Activate() {
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        public void UpdateForm() {
            updateDataGridViewEndpoints();
        }

        private void createDataGridEndpoints() {
            dataGridViewEndpoints.AutoGenerateColumns = false;
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
            combo.DataSource = Enum.GetValues(typeof(MeasurementType));
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
        }

        private void buttonDeleteEndpoint_Click(object sender, EventArgs e) {
            if (dataGridViewEndpoints.SelectedRows.Count == 1) {
                _project.RemoveEndpoint(_project.Endpoints[dataGridViewEndpoints.CurrentRow.Index]);
                updateDataGridViewEndpoints();
            } else {
                showError("Invalid selection", "Please select one entire row in order to remove its corresponding endpoint.");
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
    }
}
