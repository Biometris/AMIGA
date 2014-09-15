using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.Charting;
using AmigaPowerAnalysis.Core.DataAnalysis;
using AmigaPowerAnalysis.Core.Reporting;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;
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
        private List<Comparison> _comparisons;
        private AnalysisMethodType _currentAnalysisType = AnalysisMethodType.OverdispersedPoisson;
        private string _currentProjectFilePath;

        public AnalysisTemplatePanel(Project project) {
            InitializeComponent();
            Name = "Analysis template";
            Description = "Creates analysis data template plus analysis scripts";
            _project = project;
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
            var primaryComparisons = _comparisons.Where(c => c.OutputPowerAnalysis != null && c.IsPrimary).ToList();
        }

        public bool IsVisible() {
            if (_project.GetComparisons().Any(c => c.OutputPowerAnalysis != null)) {
                return true;
            }
            return false;
        }

        private void updateDataGridComparisons() {
            dataGridViewComparisons.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.HeaderText = "Comparison";
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

            _comparisons = _project.GetComparisons().Where(c => c.OutputPowerAnalysis != null).ToList();
            var comparisonsBindingSouce = new BindingSource(_comparisons, null);
            dataGridViewComparisons.AutoGenerateColumns = false;
            dataGridViewComparisons.DataSource = comparisonsBindingSouce;
            dataGridViewComparisons.Columns[0].ReadOnly = true;
            dataGridViewComparisons.Columns[1].ReadOnly = true;
        }

        private void updatePanelModelInfo() {
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            updatePanelModelInfo();
        }

        private void buttonGenerateDataTemplate_Click(object sender, EventArgs e) {
            var saveFileDialog = new SaveFileDialog() {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                OverwritePrompt = true
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                var generator = new AnalysisDataTemplateGenerator();
                var template = generator.CreateAnalysisDataTemplate(_project, 3);
                generator.AnalysisDataTemplateToCsv(template, saveFileDialog.FileName);
                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }
    }
}
