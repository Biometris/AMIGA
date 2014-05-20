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

        public ComparisonsForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Comparisons";
            createDataGridComparisons();
        }

        public void Activate() {
            updateDataGridComparisons();
            updateComparisonForm();
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

        private void updateComparisonForm() {
        }

        private void createDataGridComparisons() {
            var comparisonsBindingSouce = new BindingSource(_project.Comparisons, null);
            dataGridComparisons.AutoGenerateColumns = false;
            dataGridComparisons.DataSource = comparisonsBindingSouce;
            updateDataGridComparisons();
        }

        private void dataGridComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.Comparisons.ElementAt(dataGridComparisons.CurrentRow.Index);
            updateComparisonForm();
        }
    }
}
