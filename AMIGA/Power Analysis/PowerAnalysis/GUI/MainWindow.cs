using System;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.Gui {
    public partial class MainWindow : Form {

        private Project _project;

        private EndpointTypeProvider _endpointTypeProvider;

        public MainWindow() {
            InitializeComponent();
            initialize();
        }

        private void initialize() {
            _project = new Project();
            _endpointTypeProvider = new EndpointTypeProvider();
            _project.Endpoints.Add(new Endpoint() {
                Name = "Beatle",
                EndpointType = _endpointTypeProvider.GetEndpointType("Predator")
            });
            _project.Endpoints.Add(new Endpoint() {
                Name = "Giraffe",
                EndpointType = _endpointTypeProvider.GetEndpointType("Herbivore")
            });
            createEndpointTypesDataGrid();
        }

        private void createEndpointTypesDataGrid() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridEndPoints.AutoGenerateColumns = false;
            dataGridEndPoints.DataSource = endpointsBindingSouce;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridEndPoints.Columns.Add(column);

            var _availableEndpointTypes = _endpointTypeProvider.GetAvailableEndpointTypes().Select(h => new { Name = h.Name, EndpointType = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpointTypes;
            combo.DisplayMember = "Name";
            combo.ValueMember = "EndpointType";
            combo.DataPropertyName = "EndpointType";
            dataGridEndPoints.Columns.Add(combo);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "VarietyInteractions";
            checkbox.Name = "VarietyInteractions";
            dataGridEndPoints.Columns.Add(checkbox);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "RepeatedMeasures";
            checkbox.Name = "RepeatedMeasures";
            dataGridEndPoints.Columns.Add(checkbox);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "ExcessZeroes";
            checkbox.Name = "ExcessZeroes";
            dataGridEndPoints.Columns.Add(checkbox);
        }

        private void toolstripAbout_Click(object sender, EventArgs e) {
        }
    }
}
