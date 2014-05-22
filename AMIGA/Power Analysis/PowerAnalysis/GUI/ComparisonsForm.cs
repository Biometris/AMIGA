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
using AmigaPowerAnalysis.GUI.Wrappers;

namespace AmigaPowerAnalysis.GUI {
    public partial class ComparisonsForm : UserControl, ISelectionForm {

        private Project _project;

        private Comparison _currentComparison;
        private List<Comparison> _comparisons;
        private List<ComparisonWrapper> _currentEndpointComparisons;

        public ComparisonsForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Comparisons";
            createDataGridComparisons();
            createDataGridFactorLevels();
        }

        public void Activate() {
            updateDataGridComparisons();
        }

        private void createDataGridComparisons() {
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

        private void createDataGridFactorLevels() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "FactorName";
            column.Name = "FactorName";
            column.HeaderText = "Factor name";
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Label";
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Level";
            column.HeaderText = "Level";
            column.ValueType = typeof(double);
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Frequency";
            column.Name = "Frequency";
            column.HeaderText = "Frequency";
            column.ValueType = typeof(int);
            column.ReadOnly = true;
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
            _comparisons = _project.Comparisons.ToList();
            var comparisonsBindingSouce = new BindingSource(_comparisons, null);
            dataGridComparisons.AutoGenerateColumns = false;
            dataGridComparisons.DataSource = comparisonsBindingSouce;
            updateDataGridFactorLevels();
        }

        private void updateDataGridFactorLevels() {
            if (_currentEndpointComparisons != null) {
                var factorLevelsBindingSouce = new BindingSource(_currentEndpointComparisons, null);
                dataGridViewFactorLevels.AutoGenerateColumns = false;
                dataGridViewFactorLevels.DataSource = factorLevelsBindingSouce;
            }
        }

        private void dataGridComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.Comparisons.ElementAt(dataGridComparisons.CurrentRow.Index);
            _currentEndpointComparisons = _currentComparison.InteractionFactors.SelectMany(ifc => ifc.InteractionFactorLevels, (f, l) => new ComparisonWrapper(f, l)).ToList();
            updateDataGridFactorLevels();
        }
    }
}
