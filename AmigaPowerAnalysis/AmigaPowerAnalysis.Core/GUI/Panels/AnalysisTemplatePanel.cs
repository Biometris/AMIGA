using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataAnalysis;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris.ExtensionMethods;

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisTemplatePanel : UserControl, ISelectionForm {

        public event EventHandler TabVisibilitiesChanged;

        private Project _project;
        private OutputPowerAnalysis _currentComparisonAnalysisResult;
        private List<OutputPowerAnalysis> _comparisonAnalysisResults;
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
            if (_currentComparisonAnalysisResult != null) {

            }
        }

        public bool IsVisible() {
            return _project.HasOutput;
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            _comparisonAnalysisResults = _project.AnalysisResults.First().ComparisonPowerAnalysisResults;

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Endpoint";
            column.Name = "Endpoint";
            column.HeaderText = "Endpoint";
            column.ValueType = typeof(string);
            dataGridViewComparisons.Columns.Add(column);

            var comparisonsBindingSouce = new BindingSource(_comparisonAnalysisResults, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
        }

        private void updatePanelModelInfo() {
            if (_currentComparisonAnalysisResult != null) {
                var AnalysisRScriptGenerator = new AnalysisRScriptGenerator();
                textBoxGeneratedAnalysisScript.Text = AnalysisRScriptGenerator.Generate(_currentComparisonAnalysisResult.InputPowerAnalysis, _currentAnalysisMethodType);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparisonAnalysisResult = _comparisonAnalysisResults.ElementAt(dataGridViewComparisons.CurrentRow.Index);
            var selectedAnalysisMethodTypesDifferenceTests = _currentComparisonAnalysisResult.InputPowerAnalysis.SelectedAnalysisMethodTypesDifferenceTests.GetFlags().ToArray();
            this.comboBoxAnalysisMethodTypeDifferenceTests.DataSource = selectedAnalysisMethodTypesDifferenceTests;
            if (selectedAnalysisMethodTypesDifferenceTests.Count() > 0) {
                this.comboBoxAnalysisMethodTypeDifferenceTests.SelectedIndex = 0;
            }
            var selectedAnalysisMethodTypesEquivalenceTests = _currentComparisonAnalysisResult.InputPowerAnalysis.SelectedAnalysisMethodTypesEquivalenceTests.GetFlags().ToArray();
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
                    var filename = saveFileDialog.FileName;
                    AnalysisDataTemplateGenerator.AnalysisDataTemplateToCsv(template, filename);
                    var contrastsFileName = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + "Contrasts.csv");
                    AnalysisDataTemplateGenerator.AnalysisDataTemplateContrastsToCsv(template, contrastsFileName);
                    System.Diagnostics.Process.Start(filename);
                    System.Diagnostics.Process.Start(contrastsFileName);
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
