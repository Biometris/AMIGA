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
using AmigaPowerAnalysis.GUI.Wrappers;

namespace AmigaPowerAnalysis.GUI {
    public partial class ModifiersForm : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<FactorModifierWrapper> _currentFactorModifiers;

        public ModifiersForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Modifiers";
            checkBoxUseBlockModifier .Checked = _project.UseBlockModifier;
            checkBoxUseMainPlotModifier.Checked = _project.UseMainPlotModifier;
            checkBoxUseFactorModifiers.Checked = _project.UseFactorModifiers;
            createDataGridEndpoints();
            createDataGridFactorModifiers();
        }

        public void Activate() {
            updateDataGridEndpoints();
            updateDataGridFactorModifiers();
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
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewEndpoints.Columns.Add(column);
        }

        private void createDataGridFactorModifiers() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "FactorName";
            column.Name = "FactorName";
            column.HeaderText = "Factor name";
            column.ReadOnly = true;
            dataGridViewFactorModifiers.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Label";
            column.HeaderText = "Label";
            column.ReadOnly = true;
            dataGridViewFactorModifiers.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Level";
            column.HeaderText = "Level";
            column.ValueType = typeof(double);
            column.ReadOnly = true;
            dataGridViewFactorModifiers.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Frequency";
            column.Name = "Frequency";
            column.HeaderText = "Frequency";
            column.ValueType = typeof(int);
            column.ReadOnly = true;
            dataGridViewFactorModifiers.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "MuComparator";
            column.Name = "MuComparator";
            column.HeaderText = "Mu comparator";
            column.ValueType = typeof(double);
            column.ReadOnly = true;
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
            _currentFactorModifiers = _currentEndpoint.InteractionFactors
                .SelectMany(f => f.FactorLevels, (ifc, fl) => new FactorModifierWrapper(_currentEndpoint, ifc, fl)).ToList();
            updateDataGridFactorModifiers();
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
