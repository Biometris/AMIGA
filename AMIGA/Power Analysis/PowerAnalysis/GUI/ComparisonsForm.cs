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
        private List<ComparisonFactorLevelCombination> _currentComparisonFactorLevels;

        public ComparisonsForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Comparisons";
            this.textBoxTabTitle.Text = Name;
            createDataGridComparisons();
            createDataGridFactorLevels();
        }

        public void Activate() {
            updateDataGridComparisons();
        }

        private void createDataGridComparisons() {
        }

        private void createDataGridFactorLevels() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "FactorLevelCombinationName";
            column.Name = "FactorLevelCombinationName";
            column.HeaderText = "FactorLevelCombinationName";
            column.ReadOnly = true;
            dataGridViewFactorLevels.Columns.Add(column);

            var checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsComparisonLevelGMO";
            checkbox.Name = "IsComparisonLevelGMO";
            dataGridViewFactorLevels.Columns.Add(checkbox);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MeanGMO";
            column.Name = "MeanGMO";
            column.HeaderText = "MeanGMO";
            dataGridViewFactorLevels.Columns.Add(column);

            checkbox = new DataGridViewCheckBoxColumn();
            checkbox.DataPropertyName = "IsComparisonLevelComparator";
            checkbox.Name = "IsComparisonLevelComparator";
            dataGridViewFactorLevels.Columns.Add(checkbox);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MeanComparator";
            column.Name = "MeanComparator";
            column.HeaderText = "MeanComparator";
            dataGridViewFactorLevels.Columns.Add(column);
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewComparisons.Columns.Add(column);

            var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpoints;
            combo.DataPropertyName = "Endpoint";
            combo.DisplayMember = "Name";
            combo.ValueMember = "Endpoint";
            combo.HeaderText = "Endpoint";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            _comparisons = _project.GetComparisons().ToList();
            var comparisonsBindingSouce = new BindingSource(_comparisons, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
            updateDataGridFactorLevels();
        }

        private void updateDataGridFactorLevels() {
            if (_currentComparisonFactorLevels != null) {
                var factorLevelsBindingSouce = new BindingSource(_currentComparisonFactorLevels, null);
                dataGridViewFactorLevels.AutoGenerateColumns = false;
                dataGridViewFactorLevels.DataSource = factorLevelsBindingSouce;
            }
        }

        private void dataGridComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.GetComparisons().ElementAt(dataGridViewComparisons.CurrentRow.Index);
            _currentComparisonFactorLevels = _currentComparison.ComparisonFactorLevelCombinations;
            updateDataGridFactorLevels();
        }
    }
}
