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
    public partial class InteractionsPerEndpointPanel : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<InteractionFactorLevelCombination> _currentEndpointInteractionFactorLevels;
        private DataTable _endpointInteractionFactorsDataTable;

        public InteractionsPerEndpointPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Exclude data per endpoint";
            Description = "The GMO-CMP comparison may be restricted to a subset of levels of additional factors for the GMO and/or for the CMP. Indicate per endpoint any factors for which this is relevant, and uncheck the levels to be excluded.";
            createDataGridInteractions();
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridInteractions();
        }

        public bool IsVisible() {
            return !_project.DesignSettings.UseDefaultInteractions;
        }

        private void createDataGridInteractions() {
            updateDataGridInteractions();
        }

        private void updateDataGridInteractions() {
            _endpointInteractionFactorsDataTable = new DataTable();
            dataGridViewEndpointInteractionFactors.DataSource = _endpointInteractionFactorsDataTable;
            _endpointInteractionFactorsDataTable.Columns.Clear();
            _endpointInteractionFactorsDataTable.Columns.Add("Endpoint");
            dataGridViewEndpointInteractionFactors.Columns[0].ReadOnly = true;
            for (int i = 1; i < _project.Factors.Count; ++i) {
                _endpointInteractionFactorsDataTable.Columns.Add(_project.Factors.ElementAt(i).Name, typeof(bool));
            }
            for (int i = 0; i < _project.Endpoints.Count; ++i) {
                DataRow row = _endpointInteractionFactorsDataTable.NewRow();
                row["Endpoint"] = _project.Endpoints.ElementAt(i).Name;
                var endpointInteractions = _project.Endpoints.ElementAt(i).InteractionFactors;
                for (int j = 0; j < endpointInteractions.Count(); ++j) {
                    if (_endpointInteractionFactorsDataTable.Columns.Contains(endpointInteractions.ElementAt(j).Name)) {
                        row[endpointInteractions.ElementAt(j).Name] = true;
                    }
                }
                _endpointInteractionFactorsDataTable.Rows.Add(row);
            }
        }

        private void updateDataGridFactorLevels() {
            if (_currentEndpoint != null) {
                dataGridViewFactorLevels.DataSource = null;
                var dataTable = new DataTable();
                var interactionFactors = _currentEndpoint.InteractionFactors.ToList();
                foreach (var interactionFactor in interactionFactors) {
                    dataTable.Columns.Add(interactionFactor.Name, typeof(string));
                }
                dataTable.Columns.Add("Interaction", typeof(bool));
                foreach (var factorLevelCombination in _currentEndpoint.Interactions) {
                    DataRow row = dataTable.NewRow();
                    foreach (var factorLevel in factorLevelCombination.Levels) {
                        row[factorLevel.Parent.Name] = factorLevel.Label;
                    }
                    row["Interaction"] = factorLevelCombination.IsComparisonLevel;
                    dataTable.Rows.Add(row);
                }
                dataGridViewFactorLevels.Columns.Clear();
                dataGridViewFactorLevels.DataSource = dataTable;
                for (int i = 0; i < interactionFactors.Count; ++i) {
                    dataGridViewFactorLevels.Columns[i].ReadOnly = true;
                }
            }
        }

        private void dataGridViewFactorLevels_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            showError("Invalid data", e.Exception.Message);
        }

        private void dataGridViewEndpointInteractionFactors_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewEndpointInteractionFactors.CurrentRow.Index);
            _currentEndpointInteractionFactorLevels = _currentEndpoint.Interactions;
            updateDataGridFactorLevels();
            fireTabVisibilitiesChanged();
        }

        private void dataGridViewEndpointInteractionFactors_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (dataGridViewEndpointInteractionFactors.IsCurrentCellDirty) {
                dataGridViewEndpointInteractionFactors.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            var cell = this.dataGridViewEndpointInteractionFactors.CurrentCell;
            if (cell.ColumnIndex > 0 && cell.ColumnIndex - 1 < _project.Factors.Count) {
                var endpoint = _project.Endpoints.ElementAt(cell.RowIndex);
                var factor = _project.Factors.ElementAt(cell.ColumnIndex);
                var isChecked = (bool)_endpointInteractionFactorsDataTable.Rows[cell.RowIndex][cell.ColumnIndex];
                endpoint.SetFactorType(factor, isChecked);
                updateDataGridFactorLevels();
                fireTabVisibilitiesChanged();
            }
        }

        private void dataGridViewFactorLevels_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < _project.DefaultInteractionFactorLevelCombinations.Count) {
                var factorLevelCombination = _currentEndpoint.Interactions[e.RowIndex];
                if (e.ColumnIndex == dataGridViewFactorLevels.Columns["Interaction"].Index) {
                    var isChecked = (bool)dataGridViewFactorLevels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    factorLevelCombination.IsComparisonLevel = (bool)dataGridViewFactorLevels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                _project.UpdateEndpointFactorLevels();
                fireTabVisibilitiesChanged();
            }
        }

        private void dataGridViewFactorLevels_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (dataGridViewFactorLevels.IsCurrentCellDirty) {
                dataGridViewFactorLevels.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void showError(string title, string message) {
            MessageBox.Show(
                    message,
                    title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
        }

        public event EventHandler TabVisibilitiesChanged;

        private void fireTabVisibilitiesChanged() {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
        }
    }
}
