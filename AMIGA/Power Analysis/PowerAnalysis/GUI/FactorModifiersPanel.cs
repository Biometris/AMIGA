using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            createDataGridFactorModifiers();
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridEndpoints();
            updateDataGridFactorModifiers();
            updateVisibilities();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void updateVisibilities() {
            dataGridViewEndpoints.Visible = _project.UseFactorModifiers;
            dataGridViewFactorModifiers.Visible = _project.UseFactorModifiers;
            groupBoxFactorModifiers.Enabled = _project.Endpoints.Any(ep => ep.NonInteractionFactors.Count() > 0);
        }

        private void createDataGridEndpoints() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Endpoint";
            column.ReadOnly = true;
            dataGridViewEndpoints.Columns.Add(column);
        }

        private void createDataGridFactorModifiers() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Factor level combination";
            column.ReadOnly = true;
            dataGridViewFactorModifiers.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ModifierFactor";
            column.Name = "ModifierFactor";
            column.HeaderText = "Multiplication factor for the Comparator Mean";
            column.ValueType = typeof(double);
            dataGridViewFactorModifiers.Columns.Add(column);
        }

        private void updateDataGridEndpoints() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = endpointsBindingSouce;
        }

        private void updateDataGridFactorModifiers() {
            if (_currentEndpoint != null) {
                var factorModifiersBindingSouce = new BindingSource(_currentFactorModifiers, null);
                dataGridViewFactorModifiers.AutoGenerateColumns = false;
                dataGridViewFactorModifiers.DataSource = factorModifiersBindingSouce;
            }
        }

        private void dataGridViewEndpoints_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewEndpoints.CurrentRow.Index);
            var factorFactorLevelTuples = _currentEndpoint.InteractionFactors.SelectMany(f => f.FactorLevels, (ifc, fl) => new Tuple<Factor, FactorLevel>(ifc, fl)).ToList();
            _currentFactorModifiers = _currentEndpoint.Modifiers;
            updateDataGridFactorModifiers();
        }

        private void checkBoxUseFactorModifiers_CheckedChanged(object sender, EventArgs e) {
            _project.SetUseFactorModifiers(checkBoxUseFactorModifiers.Checked);
            updateVisibilities();
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
    }
}
