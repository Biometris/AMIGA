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
    public partial class ModifiersForm : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;

        public ModifiersForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Modifiers";
            createDataGridEndpoints();
            createDataGridModifiers();
            checkBoxUseBlockModifier .Checked = _project.UseBlockModifier;
            checkBoxUseMainPlotModifier.Checked = _project.UseMainPlotModifier;
            checkBoxUseFactorModifiers.Checked = _project.UseFactorModifiers;
        }

        public void Activate() {
            updateDataGridModifiers();
            updateVisibilities();
        }

        private void updateVisibilities() {
            dataGridViewEndpoints.Visible = _project.UseFactorModifiers;
            dataGridViewModifiers.Visible = _project.UseFactorModifiers;
            checkBoxUseMainPlotModifier.Visible = _project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
        }

        private void createDataGridEndpoints() {
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridViewEndpoints.AutoGenerateColumns = false;
            dataGridViewEndpoints.DataSource = endpointsBindingSouce;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewEndpoints.Columns.Add(column);
        }

        private void createDataGridModifiers() {
            var comparisonsBindingSouce = new BindingSource(_project.Comparisons, null);
            dataGridViewModifiers.AutoGenerateColumns = false;
            dataGridViewModifiers.DataSource = comparisonsBindingSouce;
        }

        private void updateDataGridModifiers() {
            if (_currentEndpoint != null) {
                dataGridViewModifiers.Columns.Clear();

                var column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "Name";
                column.Name = "Name";
                dataGridViewModifiers.Columns.Add(column);

                var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
                var combo = new DataGridViewComboBoxColumn();
                combo.DataSource = _availableEndpoints;
                combo.DataPropertyName = "Endpoint";
                combo.DisplayMember = "Name";
                combo.ValueMember = "Endpoint";
                combo.HeaderText = "Endpoint";
                combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                dataGridViewModifiers.Columns.Add(combo);
            } else {
                dataGridViewModifiers.DataSource = null;
            }
        }

        private void checkBoxUseBlockModifier_CheckedChanged(object sender, EventArgs e) {
            _project.UseBlockModifier = checkBoxUseBlockModifier.Checked;
            updateVisibilities();
        }

        private void checkBoxUseMainPlotModifier_CheckedChanged(object sender, EventArgs e) {
            _project.UseMainPlotModifier = checkBoxUseMainPlotModifier.Checked;
            updateVisibilities();
        }

        private void checkBoxUseFactorModifiers_CheckedChanged(object sender, EventArgs e) {
            _project.UseFactorModifiers = checkBoxUseFactorModifiers.Checked;
            updateVisibilities();
        }

        private void dataGridViewEndpoints_SelectionChanged(object sender, EventArgs e) {
            _currentEndpoint = _project.Endpoints.ElementAt(dataGridViewEndpoints.CurrentRow.Index);
            updateDataGridModifiers();
        }
    }
}
