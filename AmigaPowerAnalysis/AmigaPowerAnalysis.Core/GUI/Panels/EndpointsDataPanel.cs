using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using Biometris.ExtensionMethods;
using Biometris.Statistics.Distributions;
using Biometris.Statistics.Measurements;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class EndpointsDataPanel : UserControl, ISelectionForm {

        private Project _project;

        public EndpointsDataPanel(Project project) {
            InitializeComponent();
            Name = "Endpoints data";
            Description = "For each endpoint, if needed adapt its distribution type, the binomial total (for fractions), and the power (for Taylor's Power law distribution).\r\nIf needed adapt expected values of mean and coefficient of variation (CV) for the comparator variety. Note: CV will be increased if incompatible with distribution type and mean.\r\nIndicate if more there are observations in time series per plot (Repeated measures, not yet implemented).\r\nIndicate if more zeroes are expected than corresponds to the chosen distribution (Excess zeroes, not yet implemented).";
            _project = project;
            createDataGridEndpoints();
        }

        public string Description { get; private set; }

        public void Activate() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = endpointsBindingSouce;
            updateEditableColumns();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void createDataGridEndpoints() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewEndpoints.Columns.Add(column);

            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(MeasurementType));
            combo.DataPropertyName = "Measurement";
            combo.Name = "Measurement";
            combo.ValueType = typeof(MeasurementType);
            combo.HeaderText = "Measurement type";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpoints.Columns.Add(combo);

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(DistributionType));
            combo.DataPropertyName = "DistributionType";
            combo.Name = "DistributionType";
            combo.ValueType = typeof(DistributionType);
            combo.HeaderText = "Distribution";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpoints.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "BinomialTotal";
            column.Name = "BinomialTotal";
            column.HeaderText = "Binomial total (fractions)";
            column.ValueType = typeof(int);
            dataGridViewEndpoints.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "PowerLawPower";
            column.Name = "PowerLawPower";
            column.HeaderText = "p (power law)";
            column.ValueType = typeof(double);
            dataGridViewEndpoints.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MuComparator";
            column.Name = "MuComparator";
            column.HeaderText = "Mean";
            column.ValueType = typeof(double);
            dataGridViewEndpoints.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "CvComparator";
            column.Name = "CvComparator";
            column.HeaderText = "CV";
            column.ValueType = typeof(double);
            dataGridViewEndpoints.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "RepeatedMeasures";
            checkbox.Name = "RepeatedMeasures";
            checkbox.HeaderText = "Repeated measures";
            dataGridViewEndpoints.Columns.Add(checkbox);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "ExcessZeroes";
            checkbox.Name = "ExcessZeroes";
            checkbox.HeaderText = "Excess zeroes";
            dataGridViewEndpoints.Columns.Add(checkbox);

            dataGridViewEndpoints.Columns["Name"].ReadOnly = true;
            dataGridViewEndpoints.Columns["Measurement"].ReadOnly = true;
            dataGridViewEndpoints.Columns["ExcessZeroes"].ReadOnly = true;
            dataGridViewEndpoints.Columns["ExcessZeroes"].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewEndpoints.Columns["RepeatedMeasures"].ReadOnly = true;
            dataGridViewEndpoints.Columns["RepeatedMeasures"].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void updateEditableColumns() {
            for (int i = 0; i < _project.Endpoints.Count(); ++i) {
                if (_project.Endpoints[i].Measurement != MeasurementType.Fraction) {
                    dataGridViewEndpoints.Rows[i].Cells["BinomialTotal"].ReadOnly = true;
                    dataGridViewEndpoints.Rows[i].Cells["BinomialTotal"].Style.BackColor = Color.LightGray;
                }
                if (_project.Endpoints[i].DistributionType != DistributionType.PowerLaw) {
                    dataGridViewEndpoints.Rows[i].Cells["PowerLawPower"].ReadOnly = true;
                    dataGridViewEndpoints.Rows[i].Cells["PowerLawPower"].Style.BackColor = Color.LightGray;
                }
                var measurementType = _project.Endpoints[i].Measurement;
                var datagridCellComboBox = (DataGridViewComboBoxCell)dataGridViewEndpoints.Rows[i].Cells["DistributionType"];
                datagridCellComboBox.DataSource = DistributionFactory.AvailableDistributionTypes(measurementType).GetFlags().Cast<DistributionType>().ToArray();
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

        private void dataGridViewEndpoints_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (dataGridViewEndpoints.Columns[e.ColumnIndex].Name == "DistributionType") {
                var value = dataGridViewEndpoints.Rows[e.RowIndex].Cells["DistributionType"].Value.ToString();
                if (value == DistributionType.PowerLaw.ToString()) {
                    dataGridViewEndpoints.Rows[e.RowIndex].Cells["PowerLawPower"].Value = 1.7;
                } else {
                    dataGridViewEndpoints.Rows[e.RowIndex].Cells["PowerLawPower"].Value = 0;
                }
            }
            updateEditableColumns();
        }
    }
}
