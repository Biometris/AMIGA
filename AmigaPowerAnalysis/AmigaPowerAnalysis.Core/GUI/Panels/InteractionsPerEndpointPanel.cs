using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.GUI.Wrappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class InteractionsPerEndpointPanel : UserControl, ISelectionForm {

        private Project _project;
        private List<InteractionsWrapper> _currentEndpointInteractionLevels;
        private Endpoint _currentEndpoint;
        private DataTable _endpointInteractionFactorsDataTable;

        public InteractionsPerEndpointPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Define comparison per endpoint";
            Description = "The Test-CMP comparison may be restricted to a subset of levels of additional factors for the Test and/or for the CMP. Indicate per endpoint any factors for which this is relevant, and uncheck the levels to be excluded.";
            createDataGridInteractions();
        }

        public event EventHandler TabVisibilitiesChanged;

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
            for (int i = 0; i < _project.NonVarietyFactors.Count(); ++i) {
                _endpointInteractionFactorsDataTable.Columns.Add(_project.NonVarietyFactors.ElementAt(i).Name, typeof(bool));
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

                // Create columns
                var interactionFactors = _currentEndpoint.InteractionFactors.Where(f => !f.IsVarietyFactor).ToList();
                foreach (var interactionFactor in interactionFactors) {
                    dataTable.Columns.Add(interactionFactor.Name, typeof(string));
                }
                dataTable.Columns.Add("Comparison level Test", typeof(bool));
                dataTable.Columns.Add("Comparison level Comparator", typeof(bool));

                // Create interaction wrappers
                _currentEndpointInteractionLevels = _currentEndpoint.Interactions
                    .OrderBy(r => r)
                    .GroupBy(ifl => ifl.NonVarietyFactorLevelCombination)
                    .Select(g => new InteractionsWrapper(g.ToList()))
                    .Where(i => i.Levels.Count() > 0)
                    .ToList();

                foreach (var factorLevelCombination in _currentEndpointInteractionLevels) {
                    DataRow row = dataTable.NewRow();
                    foreach (var factorLevel in factorLevelCombination.Levels) {
                        row[factorLevel.Parent.Name] = factorLevel.Label;
                    }
                    row["Comparison level Test"] = factorLevelCombination.IsComparisonLevelTest;
                    row["Comparison level Comparator"] = factorLevelCombination.IsComparisonLevelComparator;
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
            updateDataGridFactorLevels();
            fireTabVisibilitiesChanged();
        }

        private void dataGridViewEndpointInteractionFactors_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            if (dataGridViewEndpointInteractionFactors.IsCurrentCellDirty) {
                dataGridViewEndpointInteractionFactors.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            var cell = dataGridViewEndpointInteractionFactors.CurrentCell;
            if (cell.ColumnIndex > 0 && cell.ColumnIndex - 1 < _project.NonVarietyFactors.Count()) {
                var endpoint = _project.Endpoints.ElementAt(cell.RowIndex);
                var factor = _project.NonVarietyFactors.ElementAt(cell.ColumnIndex - 1);
                var isChecked = (bool)_endpointInteractionFactorsDataTable.Rows[cell.RowIndex][cell.ColumnIndex];
                endpoint.SetFactorType(factor, isChecked);
                updateDataGridFactorLevels();
                fireTabVisibilitiesChanged();
            }
        }

        private void dataGridViewFactorLevels_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (_currentEndpointInteractionLevels != null && e.RowIndex < _currentEndpointInteractionLevels.Count) {
                var factorLevelCombination = _currentEndpointInteractionLevels[e.RowIndex];
                if (e.ColumnIndex == dataGridViewFactorLevels.Columns["Comparison level Test"].Index) {
                    var isChecked = (bool)dataGridViewFactorLevels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    factorLevelCombination.IsComparisonLevelTest = (bool)dataGridViewFactorLevels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                } else if (e.ColumnIndex == dataGridViewFactorLevels.Columns["Comparison level Comparator"].Index) {
                    var isChecked = (bool)dataGridViewFactorLevels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    factorLevelCombination.IsComparisonLevelComparator = (bool)dataGridViewFactorLevels.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
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

        private void fireTabVisibilitiesChanged() {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
        }
    }
}
