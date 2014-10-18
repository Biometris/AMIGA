﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI {
    public partial class AdditionalMeansPanel : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<InteractionFactorLevelCombination> _currentEndpointFactorLevels;

        public AdditionalMeansPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Additional Means";
            Description = "There are data which are not directly involved in the comparison GMO to CMP. Such data may be useful for pooling variance estimates, but the usefulness may depend on the expected means. Indicate if you expect less informative data due to low means. If so, specify expected mean values.";
        }

        public string Description { get; private set; }

        public void Activate() {
            createDataGridEndpoints();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

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
                dataTable.Columns.Add("Mean GMO", typeof(double));
                dataTable.Columns.Add("Mean Comparator", typeof(double));
                foreach (var factorLevelCombination in _currentEndpointFactorLevels) {
                    DataRow row = dataTable.NewRow();
                    foreach (var factorLevel in factorLevelCombination.Items) {
                        row[factorLevel.Parent.Name] = factorLevel.Label;
                    }
                    row["Mean GMO"] = factorLevelCombination.MeanGMO;
                    row["Mean Comparator"] = factorLevelCombination.MeanComparator;
                    dataTable.Rows.Add(row);
                }
                dataGridViewFactorLevels.Columns.Clear();
                dataGridViewFactorLevels.DataSource = dataTable;
                for (int i = 0; i < interactionFactors.Count; ++i) {
                    dataGridViewFactorLevels.Columns[i].ReadOnly = true;
                }
                for (int i = 0; i < dataGridViewFactorLevels.Rows.Count; i++) {
                    if (_currentEndpointFactorLevels[i].IsComparisonLevelGMO) {
                        dataGridViewFactorLevels.Rows[i].Cells["Mean GMO"].Style.BackColor = Color.LightGray;
                        dataGridViewFactorLevels.Rows[i].Cells["Mean GMO"].ReadOnly = true;
                    }
                    if (_currentEndpointFactorLevels[i].IsComparisonLevelComparator) {
                        dataGridViewFactorLevels.Rows[i].Cells["Mean Comparator"].Style.BackColor = Color.LightGray;
                        dataGridViewFactorLevels.Rows[i].Cells["Mean Comparator"].ReadOnly = true;
                    }
                }
            }
            dataGridViewFactorLevels.Refresh();
        }

        private void dataGridComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewEndpoints.CurrentRow.Index);
            _currentEndpointFactorLevels = _currentEndpoint.Interactions;
            updateDataGridFactorLevels();
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
