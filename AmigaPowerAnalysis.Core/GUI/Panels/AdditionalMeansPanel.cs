using AmigaPowerAnalysis.Core;
using Biometris.Statistics.Measurements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class AdditionalMeansPanel : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<InteractionFactorLevelCombination> _currentEndpointFactorLevels;

        public AdditionalMeansPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Additional means";
            Description = "There are data which are not directly involved in the comparison Test to CMP. Such data may be useful for pooling variance estimates, but the usefulness may depend on the expected means. Indicate if you expect less informative data due to low means. If so, specify expected mean values.";
        }

        public event EventHandler TabVisibilitiesChanged;

        public string Description { get; private set; }

        public void Activate() {
            createDataGridEndpoints();
        }

        public bool IsVisible() {
            if (_project != null) {
                if (!_project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Count)) {
                    return false;
                } else {
                    return (_project.Endpoints.Any(ep => ep.Interactions.Any(epi => !epi.IsComparisonLevel)) || _project.VarietyFactor.FactorLevels.Count() > 2);
                }
            }
            return false;
        }

        private void createDataGridEndpoints() {
            dataGridViewEndpoints.Columns.Clear();

            var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpoints;
            combo.DataPropertyName = "Name";
            combo.DisplayMember = "Name";
            combo.HeaderText = "Endpoint";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewEndpoints.Columns.Add(combo);

            var comparisonsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = comparisonsBindingSouce;
            updateDataGridFactorLevels();
        }

        private void updateDataGridFactorLevels() {
            dataGridViewFactorLevels.DataSource = null;
            if (_currentEndpointFactorLevels != null) {
                var dataTable = new DataTable();

                var interactionFactors = _currentEndpoint.InteractionFactors.ToList();
                foreach (var interactionFactor in interactionFactors) {
                    dataTable.Columns.Add(interactionFactor.Name, typeof(string));
                }
                dataTable.Columns.Add("_index", typeof(int)); 
                dataTable.Columns.Add("Mean", typeof(double));
                for (int i = 0; i < _currentEndpointFactorLevels.Count; ++i) {
                    DataRow row = dataTable.NewRow();
                    row["_index"] = i;
                    foreach (var factorLevel in _currentEndpointFactorLevels[i].Levels) {
                        row[factorLevel.Parent.Name] = factorLevel.Label;
                    }
                    row["Mean"] = _currentEndpointFactorLevels[i].Mean;
                    dataTable.Rows.Add(row);
                }
                dataGridViewFactorLevels.Columns.Clear();
                dataGridViewFactorLevels.DataSource = dataTable;
                for (int i = 0; i < interactionFactors.Count; ++i) {
                    dataGridViewFactorLevels.Columns[i].ReadOnly = true;
                }
                for (int i = 0; i < dataGridViewFactorLevels.Rows.Count; i++) {
                    if (_currentEndpointFactorLevels[i].IsComparisonLevel) {
                        dataGridViewFactorLevels.Rows[i].Cells["Mean"].Style.BackColor = Color.LightGray;
                        dataGridViewFactorLevels.Rows[i].Cells["Mean"].ReadOnly = true;
                    }
                }
                dataGridViewFactorLevels.Columns["_index"].Visible = false;
            }
            dataGridViewFactorLevels.Refresh();
        }

        private void dataGridComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewEndpoints.CurrentRow.Index);
            _currentEndpointFactorLevels = _currentEndpoint.Interactions.Where(i => !i.IsComparisonLevel).ToList();
            updateDataGridFactorLevels();
        }

        private void dataGridViewFactorLevels_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var editedCell = dataGridViewFactorLevels.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var newValue = editedCell.Value;
            if (_currentEndpointFactorLevels != null) {
                if (editedCell.ColumnIndex == dataGridViewFactorLevels.Columns["Mean"].Index) {
                    var index = (int)dataGridViewFactorLevels.Rows[e.RowIndex].Cells["_index"].Value;
                    _currentEndpointFactorLevels[index].Mean = (double)newValue;
                }
            }
        }

        private void dataGridViewFactorLevels_DataError(object sender, DataGridViewDataErrorEventArgs e) {
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
