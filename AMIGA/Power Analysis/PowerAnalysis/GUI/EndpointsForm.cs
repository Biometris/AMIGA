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
            Description = "Enter a list of endpoints.\r\nThe power analysis will be based on all primary endpoints. Results for other endpoints will be shown for information only. For each endpoint provide the measurement type and limits of concern (LoC).  Provide a lower LoC, an upper LoC, or both.";
            this.textBoxTabTitle.Text = Name;
            this.textBoxTabDescription.Text = Description;
            _project = project;
            createDataGridEndpoints();
        }

        public void Activate() {
        }

        private void createDataGridEndpoints() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            endpointsBindingSouce.AddingNew += new AddingNewEventHandler(endpointsBindingSouce_AddingNew);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = endpointsBindingSouce;

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
        }

        private void endpointsBindingSouce_AddingNew(object sender, AddingNewEventArgs e) {
            e.NewObject = new Endpoint("New endpoint", _project.EndpointTypes.First());
        }

        private void dataGridViewEndpoints_UserAddedRow(object sender, DataGridViewRowEventArgs e) {
            _project.UpdateEndpointFactors();
        }

        private void dataGridViewEndpoints_UserDeletedRow(object sender, DataGridViewRowEventArgs e) {
            _project.UpdateEndpointFactors();
        }

        private void textBoxTabDescription_TextChanged(object sender, EventArgs e) {

        }
    }
}
