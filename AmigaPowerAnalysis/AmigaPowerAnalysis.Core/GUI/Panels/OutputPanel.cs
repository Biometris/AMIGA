using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.GUI.Wrappers;
using Biometris.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class OutputPanel : UserControl, ISelectionForm {

        private Project _project;

        private List<OutputWrapper> _availableOutputs;

        public OutputPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Outputs";
            Description = "This panel shows the power analysis outputs that are produced within this project. Select an output and press load to set this output as the default output of the project and to view the results.";
            dataGridViewAvailableOutputs.AutoGenerateColumns = false;
        }

        public string Description { get; private set; }

        public string CurrentProjectFilesPath { get; set; }

        public void Activate() {
            updateDataGridViewAvailableOutputs();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void updateDataGridViewAvailableOutputs() {
            _availableOutputs = getAvailableOutputs(CurrentProjectFilesPath);

            dataGridViewAvailableOutputs.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridViewAvailableOutputs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "ExecutionDateTime";
            column.Name = "ExecutionDateTime";
            column.HeaderText = "Date/time";
            column.ReadOnly = true;
            dataGridViewAvailableOutputs.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "IsCurrentOutput";
            column.Name = "IsCurrentOutput";
            column.HeaderText = "Current output";
            column.ReadOnly = true;
            dataGridViewAvailableOutputs.Columns.Add(column);

            if (_availableOutputs.Count > 0) {
                var bindingSource = new BindingSource(_availableOutputs, null);
                dataGridViewAvailableOutputs.DataSource = bindingSource;
                dataGridViewAvailableOutputs.Update();
            } else {
                dataGridViewAvailableOutputs.DataSource = null;
                dataGridViewAvailableOutputs.Update();
            }
        }

        private void buttonLoadCurrentOutput_Click(object sender, EventArgs e) {
            if (dataGridViewAvailableOutputs.SelectedRows.Count == 1) {
                var currentRowFileName = dataGridViewAvailableOutputs.Rows[dataGridViewAvailableOutputs.CurrentRow.Index].Cells["Name"].Value.ToString();
                var output = _availableOutputs.First(r => r.Name == currentRowFileName);
                _project.PrimaryOutputId = output.Name;
                _project.LoadPrimaryOutput(CurrentProjectFilesPath);
                fireTabVisibilitiesChanged(true);
            }
        }

        private void buttonDeleteCurrentOutput_Click(object sender, EventArgs e) {
            if (dataGridViewAvailableOutputs.SelectedRows.Count == 1) {
                var currentRowOutputId = dataGridViewAvailableOutputs.Rows[dataGridViewAvailableOutputs.CurrentRow.Index].Cells["Name"].Value.ToString();
                var output = _availableOutputs.First(r => r.Name == currentRowOutputId);
                if (_project.PrimaryOutputId == currentRowOutputId) {
                    _project.PrimaryOutputId = null;
                }
                try {
                    File.Delete(output.FilePath);
                    if (Directory.Exists(output.FolderPath)) {
                        Directory.Delete(output.FolderPath, true);
                    }
                } catch (Exception ex) {
                    showError("Error deleting output", ex.Message);
                }
                _project.LoadPrimaryOutput(CurrentProjectFilesPath);
                updateDataGridViewAvailableOutputs();
                fireTabVisibilitiesChanged();
            } else {
                showError("Invalid selection", "Please select one entire row in order to remove its corresponding endpoint.");
            }
        }

        private List<OutputWrapper> getAvailableOutputs(string currentProjectFilePath) {
            if (!string.IsNullOrEmpty(currentProjectFilePath) || Directory.Exists(currentProjectFilePath)) {
                var availableOutputs = Directory.GetFiles(currentProjectFilePath, "*.xml", SearchOption.TopDirectoryOnly)
                    .Where(r => SerializationExtensions.CanReadFromXmlFile<ResultPowerAnalysis>(r))
                    .Select(r => {
                        var output = SerializationExtensions.FromXmlFile<ResultPowerAnalysis>(r);
                        var record = new OutputWrapper() {
                            FilePath = r,
                            ExecutionDateTime = output.OuputTimeStamp,
                        };
                        record.IsPrimary = (record.Name == _project.PrimaryOutputId);
                        return record;
                    })
                    .OrderByDescending(r => r.ExecutionDateTime)
                    .ToList();
                return availableOutputs;
            }
            return new List<OutputWrapper>();
        }

        private void fireTabVisibilitiesChanged(bool navigateToResultsPanel = false) {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, new TabVisibilityChangedEventArgs(navigateToResultsPanel));
            }
        }

        private void updateVisibilities() {
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
