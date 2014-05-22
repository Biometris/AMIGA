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
using System.Text.RegularExpressions;

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
            textBoxCVForBlocks.Text = _project.CVForBlocks.ToString();
            textBoxCVForMainPlots.Text = _project.CVForMainPlots.ToString();
        }

        private void updateVisibilities() {
            dataGridViewEndpoints.Visible = _project.UseFactorModifiers;
            labelCVForBlocks.Visible = _project.UseBlockModifier;
            textBoxCVForBlocks.Visible = _project.UseBlockModifier;
            checkBoxUseMainPlotModifier.Visible = _project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            labelCVForMainPlots.Visible = (_project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots) && _project.UseMainPlotModifier;
            textBoxCVForMainPlots.Visible = (_project.Design.ExperimentalDesignType == ExperimentalDesignType.SplitPlots) && _project.UseMainPlotModifier;
            dataGridViewFactorModifiers.Visible = _project.UseFactorModifiers;
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
            dataGridViewFactorModifiers.AutoGenerateColumns = false;
            dataGridViewFactorModifiers.DataSource = comparisonsBindingSouce;
        }

        private void updateDataGridModifiers() {
            if (_currentEndpoint != null) {
                dataGridViewFactorModifiers.Columns.Clear();

                var column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = "Name";
                column.Name = "Name";
                dataGridViewFactorModifiers.Columns.Add(column);

                var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
                var combo = new DataGridViewComboBoxColumn();
                combo.DataSource = _availableEndpoints;
                combo.DataPropertyName = "Endpoint";
                combo.DisplayMember = "Name";
                combo.ValueMember = "Endpoint";
                combo.HeaderText = "Endpoint";
                combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                dataGridViewFactorModifiers.Columns.Add(combo);
            } else {
                dataGridViewFactorModifiers.DataSource = null;
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

        private void textBoxCVForBlocks_Validating(object sender, CancelEventArgs e) {
            var textBox = sender as TextBox;
            double value;
            if (!Double.TryParse(textBox.Text, out value)) {
                textBox.Text = Regex.Replace(textBox.Text, "[^0-9.]", "");
                Double.TryParse(textBox.Text, out value);
            }
            _project.CVForBlocks = value;
            textBox.Text = value.ToString();
        }

        private void textBoxCVForMainPlots_Validating(object sender, CancelEventArgs e) {
            var textBox = sender as TextBox;
            double value;
            if (!Double.TryParse(textBox.Text, out value)) {
                textBox.Text = Regex.Replace(textBox.Text, "[^0-9.]", "");
                Double.TryParse(textBox.Text, out value);
            }
            _project.CVForMainPlots = value;
            textBox.Text = value.ToString();
        }
    }
}
