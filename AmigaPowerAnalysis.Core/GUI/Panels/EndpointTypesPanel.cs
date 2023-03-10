using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;
using Biometris.ExtensionMethods;

namespace AmigaPowerAnalysis.GUI {
    public partial class EndpointTypesPanel : UserControl, ISelectionForm {

        private Project _project;

        private List<EndpointType> _endpointTypes;

        public string Description { get; private set; }

        public event EventHandler TabVisibilitiesChanged;

        public EndpointTypesPanel(Project project) {
            InitializeComponent();
            _endpointTypes = EndpointTypeProvider.MyEndpointTypes;
            Name = "Endpoint groups";
            Description = "This form allows you to define your endpoint groups.\r\n\r\nEndpoints belong to a group. Changing the endpoint group in the Endpoints tab will set defaults for the Measurement type, Distribution, Limits of concern, the Mean and the CV of endpoints (easy for new endpoints, but be aware of this if you change the group for existing endpoints!). These defaults are specified on this page under Current project endpoint groups.\r\nYou can also maintain a more general collection of Endpoint groups (My endpoint groups).\r\nReset to default groups will reset My endpoint groups to 'factory' settings.";
            _project = project;
            createDataGridDefaultEndpointGroups();
            createDataGridProjectEndpointGroups();
            updateVisibilities();
        }

        public void Activate() {
        }

        public bool IsVisible() {
            return true;
        }

        public void updateVisibilities() {
            if (_project != null && _project.EndpointTypes.Count > 0) {
                splitContainer.Panel2.Show();
            } else {
                splitContainer.Panel2.Hide();
            }
        }

        private void createDataGridDefaultEndpointGroups() {
            dataGridViewDefaultEndpointGroups.AutoGenerateColumns = false;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewDefaultEndpointGroups.Columns.Add(column);

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = new MeasurementType[] { MeasurementType.Count, MeasurementType.Nonnegative, MeasurementType.Continuous };
            combo.DataPropertyName = "Measurement";
            combo.ValueType = typeof(MeasurementType);
            combo.HeaderText = "Measurement type";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewDefaultEndpointGroups.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocLower";
            column.Name = "LocLower";
            column.ValueType = typeof(double);
            dataGridViewDefaultEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocUpper";
            column.Name = "LocUpper";
            column.ValueType = typeof(double);
            dataGridViewDefaultEndpointGroups.Columns.Add(column);

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(DistributionType)).Cast<DistributionType>().ToList();
            combo.Name = "DistributionType";
            combo.DataPropertyName = "DistributionType";
            combo.ValueType = typeof(DistributionType);
            combo.HeaderText = "Distribution";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewDefaultEndpointGroups.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MuComparator";
            column.Name = "MuComparator";
            column.HeaderText = "Mean";
            column.ValueType = typeof(double);
            dataGridViewDefaultEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "CvComparator";
            column.Name = "CvComparator";
            column.HeaderText = "CV";
            column.ValueType = typeof(double);
            dataGridViewDefaultEndpointGroups.Columns.Add(column);

            //column = new DataGridViewTextBoxColumn();
            //column.DataPropertyName = "BinomialTotal";
            //column.Name = "BinomialTotal";
            //column.HeaderText = "Binomial total";
            //column.ValueType = typeof(int);
            //dataGridViewDefaultEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "PowerLawPower";
            column.Name = "PowerLawPower";
            column.HeaderText = "p (power law)";
            column.ValueType = typeof(double);
            dataGridViewDefaultEndpointGroups.Columns.Add(column);

            updateDataGridViewDefaultEndpointGroups();
        }

        private void updateDataGridViewDefaultEndpointGroups() {
            if (_endpointTypes.Count > 0) {
                var endpointsBindingSouce = new BindingSource(_endpointTypes, null);
                dataGridViewDefaultEndpointGroups.DataSource = endpointsBindingSouce;
                dataGridViewDefaultEndpointGroups.Update();
            } else {
                dataGridViewDefaultEndpointGroups.DataSource = null;
                dataGridViewDefaultEndpointGroups.Update();
            }
        }

        private void createDataGridProjectEndpointGroups() {
            dataGridViewDefaultEndpointGroups.AutoGenerateColumns = false;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewProjectEndpointGroups.Columns.Add(column);

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = new MeasurementType[] { MeasurementType.Count, MeasurementType.Nonnegative, MeasurementType.Continuous };
            combo.DataPropertyName = "Measurement";
            combo.ValueType = typeof(MeasurementType);
            combo.HeaderText = "Measurement type";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewProjectEndpointGroups.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocLower";
            column.Name = "LocLower";
            column.ValueType = typeof(double);
            dataGridViewProjectEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocUpper";
            column.Name = "LocUpper";
            column.ValueType = typeof(double);
            dataGridViewProjectEndpointGroups.Columns.Add(column);

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(DistributionType));
            combo.Name = "DistributionType";
            combo.DataPropertyName = "DistributionType";
            combo.ValueType = typeof(DistributionType);
            combo.HeaderText = "Distribution";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewProjectEndpointGroups.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MuComparator";
            column.Name = "MuComparator";
            column.HeaderText = "Mean";
            column.ValueType = typeof(double);
            dataGridViewProjectEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "CvComparator";
            column.Name = "CvComparator";
            column.HeaderText = "CV";
            column.ValueType = typeof(double);
            dataGridViewProjectEndpointGroups.Columns.Add(column);

            //column = new DataGridViewTextBoxColumn();
            //column.DataPropertyName = "BinomialTotal";
            //column.Name = "BinomialTotal";
            //column.HeaderText = "Binomial total";
            //column.ValueType = typeof(int);
            //dataGridViewProjectEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "PowerLawPower";
            column.Name = "PowerLawPower";
            column.HeaderText = "p (power law)";
            column.ValueType = typeof(double);
            dataGridViewProjectEndpointGroups.Columns.Add(column);

            updateDataGridViewProjectEndpointGroups();
        }

        private void updateDataGridViewProjectEndpointGroups() {
            if (_project != null && _project.EndpointTypes.Count > 0) {
                var endpointsBindingSouce = new BindingSource(_project.EndpointTypes, null);
                dataGridViewProjectEndpointGroups.DataSource = endpointsBindingSouce;
                dataGridViewProjectEndpointGroups.Update();
            } else {
                dataGridViewProjectEndpointGroups.DataSource = null;
                dataGridViewProjectEndpointGroups.Update();
            }
        }

        private void buttonAddDefaultEndpointGroup_Click(object sender, EventArgs e) {
            var endpointTypeNames = _endpointTypes.Select(ep => ep.Name).ToList();
            var newEndpointTypeName = string.Format("New endpoint type");
            var i = 0;
            while (endpointTypeNames.Contains(newEndpointTypeName)) {
                newEndpointTypeName = string.Format("New endpoint type {0}", i++);
            }
            _endpointTypes.Add(new EndpointType() {
                Name = newEndpointTypeName,
            });
            updateDataGridViewDefaultEndpointGroups();
        }

        private void buttonDeleteDefaultEndpointGroup_Click(object sender, EventArgs e) {
            if (dataGridViewDefaultEndpointGroups.SelectedRows.Count == 1) {
                _endpointTypes.Remove(_endpointTypes[dataGridViewDefaultEndpointGroups.CurrentRow.Index]);
                updateDataGridViewDefaultEndpointGroups();
            } else {
                showError("Invalid selection", "Please select one entire row in order to remove its corresponding endpoint group.");
            }
        }

        private void buttonAddProjectEndpointGroup_Click(object sender, EventArgs e) {
            if (_project != null && _project.EndpointTypes.Count > 0) {
                var endpointTypeNames = _project.EndpointTypes.Select(ep => ep.Name).ToList();
                var newEndpointTypeName = string.Format("New endpoint type");
                var i = 0;
                while (endpointTypeNames.Contains(newEndpointTypeName)) {
                    newEndpointTypeName = string.Format("New endpoint type {0}", i++);
                }
                _project.EndpointTypes.Add(new EndpointType() {
                    Name = newEndpointTypeName,
                });
                updateDataGridViewProjectEndpointGroups();
            }
        }

        private void buttonDeleteProjectEndpointGroup_Click(object sender, EventArgs e) {
            if (_project != null && _project.EndpointTypes.Count > 0) {
                if (dataGridViewProjectEndpointGroups.SelectedRows.Count == 1) {
                    var selectedEndpointType = _project.EndpointTypes[dataGridViewProjectEndpointGroups.CurrentRow.Index];
                    if (!_project.Endpoints.Any(ep => ep.EndpointType == selectedEndpointType)) {
                        _project.EndpointTypes.Remove(selectedEndpointType);
                        updateDataGridViewProjectEndpointGroups();
                    } else {
                        showError("Invalid selection", "Cannot delete endpoint group because it is referenced by one of the endpoints of the project.");
                    }
                    updateDataGridViewDefaultEndpointGroups();
                } else {
                    showError("Invalid selection", "Please select one entire row in order to remove its corresponding endpoint group.");
                }
            }
        }

        private void buttonAddToProject_Click(object sender, EventArgs e) {
            if (_project != null && _project.EndpointTypes.Count > 0) {
                if (dataGridViewDefaultEndpointGroups.SelectedRows.Count == 1) {
                    var selectedEndpointType = _endpointTypes[dataGridViewDefaultEndpointGroups.CurrentRow.Index];
                    if (!_project.EndpointTypes.Any(ep => ep.Name == selectedEndpointType.Name)) {
                        _project.EndpointTypes.Add(selectedEndpointType);
                        updateDataGridViewProjectEndpointGroups();
                    } else {
                        showError("Invalid selection", "The project already contains an endpoint group with the same name.");
                    }
                } else {
                    showError("Invalid selection", "Please select one entire row in order to copy its corresponding endpoint group to the endpoint groups of the project.");
                }
            }
        }

        private void buttonAddToDefault_Click(object sender, EventArgs e) {
            if (_project != null && _project.EndpointTypes.Count > 0) {
                if (dataGridViewProjectEndpointGroups.SelectedRows.Count == 1) {
                    var selectedEndpointType = _project.EndpointTypes[dataGridViewProjectEndpointGroups.CurrentRow.Index];
                    if (!_endpointTypes.Any(ep => ep.Name == selectedEndpointType.Name)) {
                        _endpointTypes.Add(selectedEndpointType);
                        updateDataGridViewDefaultEndpointGroups();
                    } else {
                        showError("Invalid selection", "There already exists a default endpoint with the same name.");
                    }
                } else {
                    showError("Invalid selection", "Please select one entire row in order to copy its corresponding endpoint group to your default endpoint.");
                }
            }
        }

        private void dataGridViewEndpointTypes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (e.ColumnIndex >= 0 && dataGridViewDefaultEndpointGroups.Columns[e.ColumnIndex].Name == "Name") {

            }
        }

        private void dataGridViewDefaultEndpointGroups_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex >= 0 && dataGridViewDefaultEndpointGroups.Columns[e.ColumnIndex].Name == "DistributionType") {
                var measurement = _endpointTypes[dataGridViewDefaultEndpointGroups.CurrentRow.Index].Measurement;
                var combo = this.dataGridViewDefaultEndpointGroups.Rows[e.RowIndex].Cells["DistributionType"] as DataGridViewComboBoxCell;
                var source = DistributionFactory.AvailableDistributionTypes(measurement).GetFlags().Cast<DistributionType>().ToList();
                combo.DataSource = source;
            }
        }

        private void dataGridViewProjectEndpointGroups_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex >= 0 && dataGridViewProjectEndpointGroups.Columns[e.ColumnIndex].Name == "DistributionType") {
                var measurement = _endpointTypes[dataGridViewProjectEndpointGroups.CurrentRow.Index].Measurement;
                var combo = this.dataGridViewProjectEndpointGroups.Rows[e.RowIndex].Cells["DistributionType"] as DataGridViewComboBoxCell;
                var source = DistributionFactory.AvailableDistributionTypes(measurement).GetFlags().Cast<DistributionType>().ToList();
                combo.DataSource = source;
            }
        }

        private void dataGridViewEndpointTypes_DataError(object sender, DataGridViewDataErrorEventArgs e) {
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

        private void dataGridViewDefaultEndpointGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var combo = this.dataGridViewDefaultEndpointGroups.Rows[e.RowIndex].Cells["DistributionType"] as DataGridViewComboBoxCell;
            combo.DataSource = Enum.GetValues(typeof(DistributionType));
            dataGridViewDefaultEndpointGroups.Refresh();
        }

        private void dataGridViewProjectEndpointGroups_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var combo = this.dataGridViewProjectEndpointGroups.Rows[e.RowIndex].Cells["DistributionType"] as DataGridViewComboBoxCell;
            combo.DataSource = Enum.GetValues(typeof(DistributionType));
            dataGridViewProjectEndpointGroups.Refresh();
        }

        private void buttonResetToDefaults_Click(object sender, EventArgs e) {
            var confirmResult = MessageBox.Show("Are you sure to reset your endpoint groups to the default?", "Confirm reset endpoint groups!", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes) {
                EndpointTypeProvider.MyEndpointTypes = EndpointTypeProvider.DefaultEndpointTypes();
                _endpointTypes = EndpointTypeProvider.MyEndpointTypes;
                updateDataGridViewDefaultEndpointGroups();
            }
        }
    }
}
