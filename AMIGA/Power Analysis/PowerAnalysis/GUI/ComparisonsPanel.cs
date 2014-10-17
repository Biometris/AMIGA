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

namespace AmigaPowerAnalysis.GUI {
    public partial class ComparisonsPanel : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<InteractionFactorLevelCombination> _currentEndpointFactorLevels;

        public ComparisonsPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Comparisons";
            Description = "The comparison GMO to CMP is made separately for levels of other factors. Per endpoint, indicate which levels of these other factors should be included for the GMO and for the CMP.\r\nFor unselected levels, you can optionally specify expected mean values. These levels are not used in the comparison, but the data may give additional information for the pooled variation.";
            createDataGridComparisons();
            createDataGridFactorLevels();
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridComparisons();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void createDataGridComparisons() {
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpoints;
            combo.DataPropertyName = "Name";
            combo.DisplayMember = "Name";
            combo.HeaderText = "Endpoint";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            var comparisonsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
            updateDataGridFactorLevels();
        }

        private void createDataGridFactorLevels() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Factor level combination";
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsComparisonLevelGMO";
            checkbox.Name = "IsComparisonLevelGMO";
            checkbox.HeaderText = "Comparison level GMO";
            checkbox.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(checkbox);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MeanGMO";
            column.Name = "MeanGMO";
            column.HeaderText = "Mean GMO";
            dataGridViewFactorLevels.Columns.Add(column);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsComparisonLevelComparator";
            checkbox.Name = "IsComparisonLevelComparator";
            checkbox.HeaderText = "Comparison level comparator";
            checkbox.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(checkbox);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MeanComparator";
            column.Name = "MeanComparator";
            column.HeaderText = "Mean comparator";
            dataGridViewFactorLevels.Columns.Add(column);
        }

        private void updateDataGridFactorLevels() {
            if (_currentEndpointFactorLevels != null) {
                var factorLevelsBindingSouce = new BindingSource(_currentEndpointFactorLevels, null);
                dataGridViewFactorLevels.AutoGenerateColumns = false;
                dataGridViewFactorLevels.DataSource = factorLevelsBindingSouce;
            }
            dataGridViewFactorLevels.Columns["IsComparisonLevelGMO"].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewFactorLevels.Columns["IsComparisonLevelComparator"].DefaultCellStyle.BackColor = Color.LightGray;
            for (int i = 0; i < dataGridViewFactorLevels.Rows.Count; i++) {
                if ((bool)dataGridViewFactorLevels.Rows[i].Cells["IsComparisonLevelGMO"].Value) {
                    dataGridViewFactorLevels.Rows[i].Cells["MeanGMO"].Style.BackColor = Color.LightGray;
                    dataGridViewFactorLevels.Rows[i].Cells["MeanGMO"].Style.ForeColor = Color.LightGray;
                    dataGridViewFactorLevels.Rows[i].Cells["MeanGMO"].ReadOnly = true;
                }
                if ((bool)dataGridViewFactorLevels.Rows[i].Cells["IsComparisonLevelComparator"].Value) {
                    dataGridViewFactorLevels.Rows[i].Cells["MeanComparator"].Style.BackColor = Color.LightGray;
                    dataGridViewFactorLevels.Rows[i].Cells["MeanComparator"].Style.ForeColor = Color.LightGray;
                    dataGridViewFactorLevels.Rows[i].Cells["MeanComparator"].ReadOnly = true;
                }
            }
            dataGridViewFactorLevels.Refresh();
        }

        private void dataGridComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewComparisons.CurrentRow.Index);
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
