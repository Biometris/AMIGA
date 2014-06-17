﻿using System;
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
using AmigaPowerAnalysis.Helpers;

namespace AmigaPowerAnalysis.GUI {
    public partial class ModifiersForm : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<ModifierFactorLevelCombination> _currentFactorModifiers;

        public ModifiersForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Modifiers";
            Description = "The power of tests will be lower if data are uninformative or less informative, e.g. if counts are very low (<5), or fractions are close to 0 or 1.\r\nIn principle, the specified means and CVs are sufficient ot perform the power analysis, but it should be specified if other factors in the design are expected to make part of the data less informative.\r\nPlease provide a CV if you expect a large variation between blocks or main plots in a split-plot design.\r\nFor fixed factors, indicate levels where data may become less informative (e.g. counts less than 5, or all binomial results positive or all negative).\r\n";
            this.textBoxTabTitle.Text = Name;
            this.textBoxTabDescription.Text = Description;
            checkBoxUseBlockModifier.Checked = _project.UseBlockModifier;
            checkBoxUseMainPlotModifier.Checked = _project.UseMainPlotModifier;
            checkBoxUseFactorModifiers.Checked = _project.UseFactorModifiers;
            createDataGridEndpoints();
            createDataGridFactorModifiers();
        }

        public string Description { get; private set; }

        public void Activate() {
            updateDataGridEndpoints();
            updateDataGridFactorModifiers();
            updateVisibilities();
            textBoxCVForBlocks.Text = _project.CVForBlocks.ToString();
            textBoxCVForMainPlots.Text = _project.CVForMainPlots.ToString();
        }

        public bool IsVisible() {
            return true;
        }

        private void updateVisibilities() {
            dataGridViewEndpoints.Visible = _project.UseFactorModifiers;
            labelCVForBlocks.Visible = _project.UseBlockModifier;
            textBoxCVForBlocks.Visible = _project.UseBlockModifier;
            if (dataGridViewEndpoints.Columns.Contains("CVForBlocks")) {
                dataGridViewEndpoints.Columns["CVForBlocks"].Visible = _project.UseBlockModifier;
            }
            checkBoxUseMainPlotModifier.Visible = _project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots;
            labelCVForMainPlots.Visible = (_project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots) && _project.UseMainPlotModifier;
            textBoxCVForMainPlots.Visible = (_project.DesignSettings.ExperimentalDesignType == ExperimentalDesignType.SplitPlots) && _project.UseMainPlotModifier;
            dataGridViewFactorModifiers.Visible = _project.UseFactorModifiers;
        }

        private void createDataGridEndpoints() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Endpoint";
            column.ReadOnly = true;
            dataGridViewEndpoints.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "CVForBlocks";
            column.Name = "CVForBlocks";
            column.HeaderText = "CV";
            column.ValueType = typeof(double);
            dataGridViewEndpoints.Columns.Add(column);
        }

        private void createDataGridFactorModifiers() {
            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "FactorLevelCombinationName";
            column.Name = "FactorLevelCombinationName";
            column.HeaderText = "Factor level combination";
            column.ReadOnly = true;
            dataGridViewFactorModifiers.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Modifier";
            column.Name = "Modifier";
            column.HeaderText = "Modifier";
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
            _currentFactorModifiers = _currentEndpoint.NonInteractionFactorLevelCombinations;
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
            _project.SetUseFactorModifiers(checkBoxUseFactorModifiers.Checked);
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
