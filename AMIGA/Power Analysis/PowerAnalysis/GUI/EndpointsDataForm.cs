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
    public partial class EndpointsDataForm : UserControl, ISelectionForm {

        private Project _project;

        public EndpointsDataForm(Project project) {
            InitializeComponent();
            Name = "Endpoints data";
            Description = "For each endpoint, indicate the distribution type. For binomial data specify the binomial total. For Taylor's law enter the power. Specify expected values of mean and coefficient of variation (CV) for the comparator variety. Indicate if more there are observations in time series per plot (repeated measures). Indicate if more zeroes are expected than corresponds to the chosen distribution (Excess zeroes).";
            this.textBoxTabTitle.Text = Name;
            this.textBoxTabDescription.Text = Description;
            _project = project;
            createDataGridEndpoints();
        }

        public string Description { get; private set; }

        public void Activate() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = endpointsBindingSouce;
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
            combo.ValueType = typeof(MeasurementType);
            combo.HeaderText = "Measurement";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpoints.Columns.Add(combo);

            combo = new DataGridViewComboBoxColumn();
            combo.DataSource = Enum.GetValues(typeof(DistributionType));
            combo.DataPropertyName = "DistributionType";
            combo.ValueType = typeof(DistributionType);
            combo.HeaderText = "DistributionType";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpoints.Columns.Add(combo);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "BinomialTotal";
            column.Name = "BinomialTotal";
            column.HeaderText = "Binomial total";
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
