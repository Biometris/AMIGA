﻿using System;
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
        private Comparison _currentComparison;
        private List<Comparison> _comparisons;
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
                textBoxGeneratedAnalysisScript.Text = AnalysisRScriptGenerator.Generate(_currentComparison.OutputPowerAnalysis.InputPowerAnalysis);
            }
        }

        private void dataGridViewComparisons_SelectionChanged(object sender, EventArgs e) {
            _currentComparison = _project.GetComparisons().ElementAt(dataGridViewComparisons.CurrentRow.Index);
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
                try {
                    var generator = new AnalysisDataTemplateGenerator();
                    var template = generator.CreateAnalysisDataTemplate(_project, 3);
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