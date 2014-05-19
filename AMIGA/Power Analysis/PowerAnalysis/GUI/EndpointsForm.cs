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

        private EndpointTypeProvider _endpointTypeProvider;

        public EndpointsForm(Project project, EndpointTypeProvider endpoointTypeProvider) {
            InitializeComponent();
            Name = "Endpoints";
            _project = project;
            _endpointTypeProvider = endpoointTypeProvider;
            createDataGridEndpoints();
        }

        public void Activate() {
        }

        public EndpointTypeProvider EndpointTypeProvider {
            get { return _endpointTypeProvider; }
            set { _endpointTypeProvider = value; }
        }

        private void createDataGridEndpoints() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridEndpoints.AutoGenerateColumns = false;
            dataGridEndpoints.DataSource = endpointsBindingSouce;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridEndpoints.Columns.Add(column);

            var _availableEndpointTypes = _endpointTypeProvider.GetAvailableEndpointTypes().Select(h => new { Name = h.Name, EndpointType = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpointTypes;
            combo.DataPropertyName = "EndpointType";
            combo.DisplayMember = "Name";
            combo.ValueMember = "EndpointType";
            dataGridEndpoints.Columns.Add(combo);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "Primary";
            checkbox.Name = "Primary";
            dataGridEndpoints.Columns.Add(checkbox);

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(MeasurementType));
            combo.DataPropertyName = "Measurement";
            combo.ValueType = typeof(MeasurementType);
            dataGridEndpoints.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "BinomialTotal";
            column.Name = "BinomialTotal";
            column.ValueType = typeof(int);
            dataGridEndpoints.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocLower";
            column.Name = "LocLower";
            column.ValueType = typeof(double);
            dataGridEndpoints.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "LocUpper";
            column.Name = "LocUpper";
            column.ValueType = typeof(double);
            dataGridEndpoints.Columns.Add(column);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "ExcessZeroes";
            checkbox.Name = "ExcessZeroes";
            dataGridEndpoints.Columns.Add(checkbox);


            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "RepeatedMeasures";
            checkbox.Name = "RepeatedMeasures";
            dataGridEndpoints.Columns.Add(checkbox);
        }

        private void dataGridEndpoints_UserAddedRow(object sender, DataGridViewRowEventArgs e) {
            _project.UpdateComparisons(_project.Endpoints.Last());
        }

        private void dataGridEndpoints_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {
        }
    }
}
