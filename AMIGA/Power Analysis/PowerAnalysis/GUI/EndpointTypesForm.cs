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
using AmigaPowerAnalysis.Core.Distributions;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class EndpointTypesForm : UserControl, ISelectionForm {

        private Project _project;

        private List<EndpointType> _endpointTypes;

        public string Description { get; private set; }

        public EndpointTypesForm(Project project) {
            InitializeComponent();
            var endpointTypeProvider = new EndpointTypeProvider();
            _endpointTypes = endpointTypeProvider.GetAvailableEndpointTypes();
            Name = "Endpoints";
            Description = "Enter a list of endpoints. For each endpoint indicate its group. The power analysis will be based on all primary endpoints. Results for other endpoints will be shown for information only. For each endpoint provide the measurement type and limits of concern (LoC). Provide a lower LoC, an upper LoC, or both.";
            _project = project;
            createDataGridEndpointTypes();

        }

        public void Activate() {
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void createDataGridEndpointTypes() {
            dataGridViewEndpointGroups.AutoGenerateColumns = false;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewEndpointGroups.Columns.Add(column);

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(MeasurementType));
            combo.DataPropertyName = "Measurement";
            combo.ValueType = typeof(MeasurementType);
            combo.HeaderText = "Measurement type";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpointGroups.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MuComparator";
            column.Name = "MuComparator";
            column.HeaderText = "Mean";
            column.ValueType = typeof(double);
            dataGridViewEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "CvComparator";
            column.Name = "CvComparator";
            column.HeaderText = "CV";
            column.ValueType = typeof(double);
            dataGridViewEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocLower";
            column.Name = "LocLower";
            column.ValueType = typeof(double);
            dataGridViewEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocUpper";
            column.Name = "LocUpper";
            column.ValueType = typeof(double);
            dataGridViewEndpointGroups.Columns.Add(column);

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(DistributionType));
            combo.DataPropertyName = "DistributionType";
            combo.ValueType = typeof(DistributionType);
            combo.HeaderText = "DistributionType";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpointGroups.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "BinomialTotal";
            column.Name = "BinomialTotal";
            column.HeaderText = "Binomial total";
            column.ValueType = typeof(int);
            dataGridViewEndpointGroups.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "PowerLawPower";
            column.Name = "PowerLawPower";
            column.HeaderText = "p (power law)";
            column.ValueType = typeof(double);
            dataGridViewEndpointGroups.Columns.Add(column);

            updateDataGridViewEndpointTypes();
        }

        private void updateDataGridViewEndpointTypes() {
            if (_endpointTypes.Count > 0) {
                var endpointsBindingSouce = new BindingSource(_endpointTypes, null);
                dataGridViewEndpointGroups.DataSource = endpointsBindingSouce;
                dataGridViewEndpointGroups.Update();
            } else {
                dataGridViewEndpointGroups.DataSource = null;
                dataGridViewEndpointGroups.Update();
            }
        }

        private void addEndpointTypeButton_Click(object sender, EventArgs e) {
        }

        private void buttonDeleteEndpointType_Click(object sender, EventArgs e) {

        }

        private void dataGridViewEndpointTypes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (dataGridViewEndpointGroups.Columns[e.ColumnIndex].Name == "Name") {

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
    }
}
