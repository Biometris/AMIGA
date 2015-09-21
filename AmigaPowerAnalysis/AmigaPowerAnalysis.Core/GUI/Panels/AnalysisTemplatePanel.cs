using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Biometris.ExtensionMethods;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.Reporting;
using OxyPlot.WindowsForms;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisTemplatePanel : UserControl, ISelectionForm {

        public event EventHandler TabVisibilitiesChanged;

        private Project _project;
        private Comparison _currentComparison;
        private List<Comparison> _comparisons;
        private string _currentProjectFilePath;
        private int _numberOfReplicates;
        private AnalysisMethodType _currentAnalysisMethodType;

        public AnalysisTemplatePanel(Project project) {
            InitializeComponent();
            Name = "Analysis template";
            Description = "Creates analysis data template plus analysis scripts";
            _project = project;
            _numberOfReplicates = 3;
            var analysisMethodType = Enum.GetValues(typeof(AnalysisMethodType));
            this.comboBoxAnalysisMethodTypeDifferenceTests.DataSource = analysisMethodType;
            this.comboBoxAnalysisMethodTypeDifferenceTests.SelectedIndex = 0;
        }

        public string Description { get; private set; }

        public string CurrentProjectFilesPath {
            get { return _currentProjectFilePath; }
            set { _currentProjectFilePath = value; }
        }

        public void Activate() {
            updateDataGridComparisons();
            updateVisibilities();
        }

        private void updateVisibilities() {
            if (_currentComparison != null) {

            }
        }

        public bool IsVisible() {
            if (_project.GetComparisons().Any(c => c.OutputPowerAnalysis != null)) {
                return true;
            }
            return false;
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            var _availableEndpoints = _project.Endpoints.Select(h => new { Name = h.Name, Endpoint = h }).ToList();
            var combo = new DataGridViewComboBoxColumn();
            combo.DataSource = _availableEndpoints;
            combo.DataPropertyName = "Endpoint";
            combo.DisplayMember = "Name";
            combo.ValueMember = "Endpoint";
            combo.HeaderText = "Endpoint";
            combo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            dataGridViewComparisons.Columns.Add(combo);

            _comparisons = _project.GetComparisons().Where(c => c.OutputPowerAnalysis != null).ToList();
            var comparisonsBindingSouce = new BindingSource(_comparisons, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
        }

        private void updatePanelModelInfo() {
            if (_currentComparison != null) {
                var AnalysisRScriptGenerator = new AnalysisRScriptGenerator();
                textBoxGeneratedAnalysisScript.Text = AnalysisRScriptGenerator.Generate(_currentComparison.OutputPowerAnalysis.InputPowerAnalysis, _currentAnalysisMethodType);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.GetComparisons().ElementAt(dataGridViewComparisons.CurrentRow.Index);
            var selectedAnalysisMethodTypesDifferenceTests = _currentComparison.OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().ToArray();
            this.comboBoxAnalysisMethodTypeDifferenceTests.DataSource = selectedAnalysisMethodTypesDifferenceTests;
            if (selectedAnalysisMethodTypesDifferenceTests.Count() > 0) {
                this.comboBoxAnalysisMethodTypeDifferenceTests.SelectedIndex = 0;
            }
            var selectedAnalysisMethodTypesEquivalenceTests = _currentComparison.OutputPowerAnalysis.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().ToArray();
            this.comboBoxAnalysisMethodTypeEquivalenceTests.DataSource = selectedAnalysisMethodTypesEquivalenceTests;
            if (selectedAnalysisMethodTypesEquivalenceTests.Count() > 0) {
                this.comboBoxAnalysisMethodTypeEquivalenceTests.SelectedIndex = 0;
            }
            updatePanelModelInfo();
        }

        private void comboBoxAnalysisMethodType_SelectedIndexChanged(object sender, EventArgs e) {
            AnalysisMethodType analysisType;
            Enum.TryParse<AnalysisMethodType>(comboBoxAnalysisMethodTypeDifferenceTests.SelectedValue.ToString(), out analysisType);
            _currentAnalysisMethodType = analysisType;
            updatePanelModelInfo();
        }

        private void textBoxNumberOfReplicates_TextChanged(object sender, EventArgs e) {
            bool result = Int32.TryParse(this.textBoxNumberOfReplicates.Text, out _numberOfReplicates);
        }

        private void buttonGenerateDataTemplate_Click(object sender, EventArgs e) {
            var saveFileDialog = new SaveFileDialog() {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                try {
                    var generator = new AnalysisDataTemplateGenerator();
                    var template = generator.CreateAnalysisDataTemplate(_project, _numberOfReplicates);
                    generator.AnalysisDataTemplateToCsv(template, saveFileDialog.FileName);
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                } catch (Exception ex) {
                    this.showError("Error while exporting data template", ex.Message);
                }
            }
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
