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

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class EndpointsForm : UserControl, ISelectionForm {

        private Project _project;

        public string Description { get; private set; }

        public EndpointsForm(Project project) {
            InitializeComponent();
            Name = "Endpoints";
            Description = "Enter a list of endpoints. For each endpoint indicate its group. The power analysis will be based on all primary endpoints. Results for other endpoints will be shown for information only. For each endpoint provide the measurement type and limits of concern (LoC). Provide a lower LoC, an upper LoC, or both.";
            _project = project;
            createDataGridEndpoints();
        }

        public void Activate() {
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void createDataGridEndpoints() {
            dataGridViewEndpoints.AutoGenerateColumns = false;

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

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "Primary";
            checkbox.Name = "Primary";
            dataGridViewEndpoints.Columns.Add(checkbox);

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

            updateDataGridViewEndpoints();
        }

        private void updateDataGridViewEndpoints() {
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
            var newEndpointName = string.Format("New endpoint");
            var i = 0;
            while (endpointNames.Contains(newEndpointName)) {
                newEndpointName = string.Format("New endpoint {0}", i++);
            }
            _project.AddEndpoint(new Endpoint(newEndpointName, _project.EndpointTypes.First()));
            updateDataGridViewEndpoints();
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
    }
}
