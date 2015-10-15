﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.GUI.Wrappers;
using AmigaPowerAnalysis.Properties;

namespace AmigaPowerAnalysis.GUI {
    public partial class MainWindow : Form {

        #region Private properties

        private Project _project;
        private string _currentProjectFilename;

        private List<ISelectionForm> _selectionForms;

        #endregion

        public MainWindow() {
            InitializeComponent();
            initialize();
        }

        #region Initialization

        private void initialize() {
            _selectionForms = new List<ISelectionForm>();
            this.closeProject();
        }

        #endregion

        #region Events

        private void MainWindow_Load(object sender, EventArgs e) {
            if (Settings.Default.WindowLocation != null && isOnScreen(Settings.Default.WindowLocation)) {
                this.Location = Settings.Default.WindowLocation;
            }
            if (Settings.Default.WindowSize != null) {
                this.Size = Settings.Default.WindowSize;
            }
            EndpointTypeProvider.LoadMyEndpointTypes();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e) {
            if (_project != null) {
                var confirmationBox = MessageBox.Show(@"Do you really want to quit? All unsaved changes will be lost.", @"Quit Amiga Power Analysis", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmationBox != DialogResult.Yes) {
                    e.Cancel = true;
                }
            }
            Settings.Default.WindowLocation = this.Location;
            if (this.WindowState == FormWindowState.Normal) {
                Settings.Default.WindowSize = this.Size;
            } else {
                Settings.Default.WindowSize = this.RestoreBounds.Size;
            }
            Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            newProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openProjectDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
            closeProject();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            saveAsDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveProject();
        }

        private void toolstripEndpointTypes_Click(object sender, EventArgs e) {
            var endpointGroupsForm = new SelectionPanelForm(new EndpointTypesPanel(_project));
            endpointGroupsForm.ShowDialog();
            if (endpointGroupsForm.DialogResult == System.Windows.Forms.DialogResult.OK) {
                EndpointTypeProvider.StoreMyEndpointTypes();
            }
            var endpointsPanel = _selectionForms.Where(s => s is EndpointsPanel).FirstOrDefault() as EndpointsPanel;
            if (endpointsPanel != null) {
                endpointsPanel.UpdateForm();
            }
        }

        private void toolstripAbout_Click(object sender, EventArgs e) {
            var aboutDialog = new AboutForm();
            aboutDialog.ShowDialog();
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(Path.Combine("Manual", "User Manual.pdf"));
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e) {
            runPowerAnalysis();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            openSettingsDialog();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            if (tabControl.SelectedTab != null) {
                var selectedForm = tabControl.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is ISelectionForm) as ISelectionForm;
                selectedForm.Activate();
            }
        }

        private void onVisibilitySettingsChanged(object sender, EventArgs e) {
            updateTabs();
            if (e != null && e is TabVisibilityChangedEventArgs && ((TabVisibilityChangedEventArgs)e).NavigateToResults) {
                navigateToResults();
            }
        }

        private void navigateToResults() {
            var analysisResultsPerEndpointPanel = _selectionForms.Where(s => s is AnalysisResultsPerComparisonPanel).First() as AnalysisResultsPerComparisonPanel;
            var selectedTab = this.tabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Name == analysisResultsPerEndpointPanel.Name);
            if (selectedTab != null) {
                this.tabControl.SelectTab(selectedTab);
            }
        }

        private void onRunButtonPressed(object sender, EventArgs e) {
            runPowerAnalysis();
        }

        #endregion

        #region Actions

        private void newProject() {
            try {
                closeProject();
                loadProject(ProjectManager.CreateNewProject(), string.Empty);
            } catch (Exception ex) {
                showErrorMessage(ex);
            }
        }

        private void openProjectDialog() {
            try {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Amiga Power Analysis settings file (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.InitialDirectory = Properties.Settings.Default.LastOpenedDirectory;
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    var filename = openFileDialog.FileName;
                    Properties.Settings.Default.LastOpenedDirectory = Path.GetDirectoryName(filename);
                    Properties.Settings.Default.Save();
                    Project project;
                    if (Path.GetExtension(filename) == ".apa") {
                        project = ProjectManager.LoadProject(filename);
                    } else {
                        project = ProjectManager.LoadProjectXml(filename);
                    }
                    loadProject(project, filename);
                }
            } catch (Exception ex) {
                showErrorMessage(ex);
            }
        }

        private void saveAsDialog() {
            try {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Amiga Power Analysis xml files (*.xml)|*.xml|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.InitialDirectory = Properties.Settings.Default.LastOpenedDirectory;
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    var filename = saveFileDialog.FileName;
                    Properties.Settings.Default.LastOpenedDirectory = Path.GetDirectoryName(filename);
                    Properties.Settings.Default.Save();
                    if (Path.GetExtension(filename) == ".apa") {
                        ProjectManager.SaveProject(_project, saveFileDialog.FileName);
                    } else {
                        ProjectManager.SaveProjectXml(_project, saveFileDialog.FileName);
                    }
                    CurrentProjectFilename = saveFileDialog.FileName;
                }
            } catch (Exception ex) {
                showErrorMessage(ex);
            }
        }

        private void loadProject(Project project, string currentProjectFilename) {
            try {
                closeProject();
                _project = project;

                _selectionForms.Add(new EndpointsPanel(_project));
                _selectionForms.Add(new EndpointsDataPanel(_project));
                _selectionForms.Add(new FactorsPanel(_project));
                _selectionForms.Add(new DesignPanel(_project));
                _selectionForms.Add(new InteractionsPanel(_project));
                _selectionForms.Add(new InteractionsPerEndpointPanel(_project));
                _selectionForms.Add(new AdditionalMeansPanel(_project));
                _selectionForms.Add(new FactorModifiersPanel(_project));
                _selectionForms.Add(new BlockModifiersPanel(_project));
                var simulationPanel = new SimulationPanel(_project);
                _selectionForms.Add(simulationPanel);
                simulationPanel.RunButtonPressed += onRunButtonPressed;
                _selectionForms.Add(new OutputPanel(_project));
                _selectionForms.Add(new AnalysisResultsPerComparisonPanel(_project));
                _selectionForms.Add(new AnalysisResultsPanel(_project));

                _selectionForms.ForEach(s => s.TabVisibilitiesChanged += onVisibilitySettingsChanged);

                this.closeToolStripMenuItem.Enabled = true;
                this.saveAsToolStripMenuItem.Enabled = true;
                this.saveToolStripMenuItem.Enabled = true;
                this.goToolStripMenuItem.Enabled = true;
                
                CurrentProjectFilename = currentProjectFilename;

                updateTabs();
            } catch (Exception ex) {
                showErrorMessage(ex);
                closeProject();
                return;
            }
        }

        private void saveProject() {
            if (!string.IsNullOrEmpty(_currentProjectFilename)) {
                if (Path.GetExtension(_currentProjectFilename) == ".apa") {
                    ProjectManager.SaveProject(_project, _currentProjectFilename);
                } else {
                    ProjectManager.SaveProjectXml(_project, _currentProjectFilename);
                }
            } else {
                saveAsDialog();
            }
        }

        private void closeProject() {
            _selectionForms.ForEach(s => s.TabVisibilitiesChanged -= onVisibilitySettingsChanged);
            var simulationPanel = _selectionForms.FirstOrDefault(r => r is SimulationPanel) as SimulationPanel;
            _selectionForms.ForEach(s => s.Dispose());
            this.tabControl.TabPages.Clear();
            if (simulationPanel != null) {
                simulationPanel.RunButtonPressed -= onRunButtonPressed;
            }
            _project = null;
            _selectionForms.Clear();
            _selectionForms.Add(new IntroductionPanel());
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Enabled = false;
            this.goToolStripMenuItem.Enabled = false;
            updateTabs();
        }

        private void updateTabs() {
            var currentTabName = this.tabControl.SelectedTab != null ? this.tabControl.SelectedTab.Name : null;
            var visibleForms = _selectionForms.Where(f => f.IsVisible()).ToList();
            var visibleTabCount = 0;
            foreach (var selectionForm in _selectionForms) {
                var currentTab = this.tabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Name == selectionForm.Name);
                if (currentTab == null && selectionForm.IsVisible()) {
                    var form = new SelectionPanelContainer((UserControl)selectionForm);
                    form.Dock = System.Windows.Forms.DockStyle.Fill;
                    var tab = new TabPage(selectionForm.Name);
                    tab.Name = selectionForm.Name;
                    tab.Controls.Add(form);
                    tab.AutoScroll = true;
                    this.tabControl.TabPages.Insert(visibleTabCount, tab);
                } else if (currentTab != null && !selectionForm.IsVisible()) {
                    this.tabControl.TabPages.Remove(currentTab);
                }
                if (selectionForm.IsVisible()) {
                    ++visibleTabCount;
                }
            }
            var selectedTab = this.tabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Name == currentTabName);
            if (selectedTab != null) {
                this.tabControl.SelectTab(selectedTab);
            }
        }

        private void runPowerAnalysis() {
            if (string.IsNullOrEmpty(_currentProjectFilename)) {
                MessageBox.Show("Please save the project first.",
                   "Save project first",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
                return;
            }
            var runSimulationDialog = new RunPowerAnalysisDialog(_project, _currentProjectFilename);
            runSimulationDialog.ShowDialog();
            if (runSimulationDialog.RunComplete) {
                this.saveProject();
                this.updateTabs();
                this.navigateToResults();
            }
        }

        private void openSettingsDialog() {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void showErrorMessage(Exception ex) {
            MessageBox.Show(
                "An error occurred while opening the project. An invalid project file may have been provided or the project file may be corrupted.",
                "Error opening project.",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
        }

        #endregion

        private string CurrentProjectFilename {
            get { return _currentProjectFilename; }
            set {
                _currentProjectFilename = value;
                if (!string.IsNullOrEmpty(_currentProjectFilename)) {
                    this.Text = "Amiga Power Analysis (Beta) - " + Path.GetFileNameWithoutExtension(_currentProjectFilename);
                    var analysisResultsPanel = _selectionForms.Where(s => s is AnalysisResultsPanel).First() as AnalysisResultsPanel;
                    var analysisResultsPerEndpointPanel = _selectionForms.Where(s => s is AnalysisResultsPerComparisonPanel).First() as AnalysisResultsPerComparisonPanel;
                    var outputPerPanel = _selectionForms.Where(s => s is OutputPanel).First() as OutputPanel;
                    var filesPath = getCurrentProjectFilesPath();
                    analysisResultsPanel.CurrentProjectFilesPath = filesPath;
                    analysisResultsPerEndpointPanel.CurrentProjectFilesPath = filesPath;
                    outputPerPanel.CurrentProjectFilesPath = filesPath;
                } else {
                    this.Text = "Amiga Power Analysis (Beta)";
                }
            }
        }

        private string getCurrentProjectFilesPath() {
            var projectPath = Path.GetDirectoryName(_currentProjectFilename);
            var projectName = Path.GetFileNameWithoutExtension(_currentProjectFilename);
            var filesPath = Path.Combine(projectPath, projectName);
            if (!Directory.Exists(filesPath)) {
                Directory.CreateDirectory(filesPath);
            }
            return filesPath;
        }

        private static bool isOnScreen(Point location) {
            foreach (var screen in Screen.AllScreens) {
                if (screen.WorkingArea.Contains(location)) {
                    return true;
                }
            }
            return false;
        }
    }
}
