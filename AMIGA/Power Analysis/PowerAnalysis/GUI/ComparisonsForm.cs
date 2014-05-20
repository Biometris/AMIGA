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
    public partial class ComparisonsForm : UserControl, ISelectionForm {

        private Project _project;

        private Comparison _currentComparison;
        private ComparisonInteractionFactor _currentComparisonInteractionFactor;

        public ComparisonsForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Comparisons";
            createDataGridComparisons();
            createDataGridFactors();
            createDataGridFactorLevels();
        }

        public void Activate() {
            updateDataGridComparisons();
            updateDataGridFactors();
        }

        private void createDataGridComparisons() {
            var comparisonsBindingSouce = new BindingSource(_project.Comparisons, null);
            dataGridComparisons.AutoGenerateColumns = false;
            dataGridComparisons.DataSource = comparisonsBindingSouce;
            updateDataGridComparisons();
        }

        private void createDataGridFactors() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Factor name";
            dataGridViewFactors.Columns.Add(column);
        }

        private void createDataGridFactorLevels() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Label";
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Level";
            column.HeaderText = "Level";
            column.ValueType = typeof(double);
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Frequency";
            column.Name = "Frequency";
            column.HeaderText = "Frequency";
            column.ValueType = typeof(int);
            dataGridViewFactorLevels.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionLevelGMO";
            checkbox.Name = "IsInteractionLevelGMO";
            dataGridViewFactorLevels.Columns.Add(checkbox);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsInteractionLevelComparator";
            checkbox.Name = "IsInteractionLevelComparator";
            dataGridViewFactorLevels.Columns.Add(checkbox);
        }

        private void updateDataGridComparisons() {
            dataGridComparisons.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridComparisons.Columns.Add(column);

            var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpoints;
            combo.DataPropertyName = "Endpoint";
            combo.DisplayMember = "Name";
            combo.ValueMember = "Endpoint";
            combo.HeaderText = "Endpoint";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridComparisons.Columns.Add(combo);
        }

        private void updateDataGridFactors() {
            if (_currentComparison != null) {
                var factors = _currentComparison.InteractionFactors.Select(ifc => ifc.Factor).ToList();
                var factorsBindingSouce = new BindingSource(factors, null);
                dataGridViewFactors.AutoGenerateColumns = false;
                dataGridViewFactors.DataSource = factorsBindingSouce;
                updateDataGridFactorLevels();
            }
        }

        private void updateDataGridFactorLevels() {
            if (_currentComparisonInteractionFactor != null) {
                var factorLevels = _currentComparisonInteractionFactor.InteractionFactorLevels;
                var factorLevelsBindingSouce = new BindingSource(factorLevels, null);
                dataGridViewFactorLevels.AutoGenerateColumns = false;
                dataGridViewFactorLevels.DataSource = factorLevelsBindingSouce;
            } else {
                dataGridViewFactorLevels.DataSource = null;
            }
        }

        private void dataGridComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.Comparisons.ElementAt(dataGridComparisons.CurrentRow.Index);
            _currentComparisonInteractionFactor = null;
            updateDataGridFactors();
        }

        private void dataGridViewFactors_SelectionChanged(object sender, EventArgs e) {
            if (_currentComparison != null) {
                var index = dataGridViewFactors.CurrentRow.Index;
                if (index < _currentComparison.InteractionFactors.Count) {
                    _currentComparisonInteractionFactor = _currentComparison.InteractionFactors.ElementAt(index);
                    return;
                }
            }
            _currentComparisonInteractionFactor = null;
            updateDataGridFactorLevels();
        }
    }
}
