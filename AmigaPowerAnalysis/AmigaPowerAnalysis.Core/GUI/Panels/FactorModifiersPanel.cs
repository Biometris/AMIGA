﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI {
    public partial class FactorModifiersPanel : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<ModifierFactorLevelCombination> _currentFactorModifiers;

        public FactorModifiersPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Factor modifiers";
            Description = "The power of tests will be lower if data are uninformative or less informative, e.g. if counts are very low (<5), or fractions are close to 0 or 1. In principle, the already specified Comparator Means and CVs are sufficient to perform the power analysis. However, it should be specified if other factors in the design are expected to make part of the data less informative.\r\nFor fixed factors, provide multiplication factors for factor levels where data may become less informative (e.g. counts less than 5, or all binomial results positive or all negative).";
            checkBoxUseFactorModifiers.Checked = _project.UseFactorModifiers;
            createDataGridEndpoints();
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridEndpoints();
            updateDataGridFactorModifiers();
            updateVisibilities();
        }

        public bool IsVisible() {
            return _project != null && _project.Endpoints.Any(ep => ep.NonInteractionFactors.Count() > 0);
        }

        private void updateVisibilities() {
            dataGridViewEndpoints.Visible = _project.UseFactorModifiers;
            dataGridViewFactorModifiers.Visible = _project.UseFactorModifiers;
            groupBoxFactorModifiers.Visible = _project.Endpoints.Any(ep => ep.NonInteractionFactors.Count() > 0);
        }

        private void createDataGridEndpoints() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Endpoint";
            column.ReadOnly = true;
            dataGridViewEndpoints.Columns.Add(column);
        }

        private void updateDataGridEndpoints() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = endpointsBindingSouce;
        }

        private void updateDataGridFactorModifiers() {
            dataGridViewFactorModifiers.DataSource = null;
            if (_currentFactorModifiers != null) {
                var dataTable = new DataTable();
                var modifierFactors = _currentEndpoint.NonInteractionFactors.ToList();
                foreach (var modifierFactor in modifierFactors) {
                    dataTable.Columns.Add(modifierFactor.Name, typeof(string));
                }
                dataTable.Columns.Add("Modifier factor", typeof(double));
                foreach (var factorLevelCombination in _currentFactorModifiers) {
                    DataRow row = dataTable.NewRow();
                    foreach (var factorLevel in factorLevelCombination.Levels) {
                        row[factorLevel.Parent.Name] = factorLevel.Label;
                    }
                    row["Modifier factor"] = factorLevelCombination.ModifierFactor;
                    dataTable.Rows.Add(row);
                }
                dataGridViewFactorModifiers.Columns.Clear();
                dataGridViewFactorModifiers.DataSource = dataTable;
                for (int i = 0; i < modifierFactors.Count; ++i) {
                    dataGridViewFactorModifiers.Columns[i].ReadOnly = true;
                }
            }
            dataGridViewFactorModifiers.Refresh();
        }

        private void dataGridViewEndpoints_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewEndpoints.CurrentRow.Index);
            var factorFactorLevelTuples = _currentEndpoint.InteractionFactors.SelectMany(f => f.FactorLevels, (ifc, fl) => new Tuple<IFactor, FactorLevel>(ifc, fl)).ToList();
            _currentFactorModifiers = _currentEndpoint.Modifiers;
            updateDataGridFactorModifiers();
        }

        private void checkBoxUseFactorModifiers_CheckedChanged(object sender, EventArgs e) {
            _project.SetUseFactorModifiers(checkBoxUseFactorModifiers.Checked);
            updateVisibilities();
        }

        private void dataGridViewFactorModifiers_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            var editedCell = dataGridViewFactorModifiers.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var newValue = editedCell.Value;
            if (_currentFactorModifiers != null) {
                if (editedCell.ColumnIndex == dataGridViewFactorModifiers.Columns["Modifier factor"].Index) {
                    _currentFactorModifiers[e.RowIndex].ModifierFactor = (double)newValue;
                }
            }
        }

        private void dataGridViewFactorModifiers_DataError(object sender, DataGridViewDataErrorEventArgs e) {
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

        public event EventHandler TabVisibilitiesChanged;

        private void fireTabVisibilitiesChanged() {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
        }
    }
}