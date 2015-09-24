using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using Biometris;
using Biometris.Logger;
using AmigaPowerAnalysis.Properties;

namespace AmigaPowerAnalysis.GUI {
    public partial class MainWindow : Form {

        private Project _project;
        private string _currentProjectFilename;

        private List<ISelectionForm> _selectionForms;

        private ILogger _logger;

        public MainWindow() {
            InitializeComponent();
            initialize();
        }

        public string CurrentProjectFilename {
            get { return _currentProjectFilename; }
            set {
                _currentProjectFilename = value;
                if (!string.IsNullOrEmpty(_currentProjectFilename)) {
                    this.Text = "Amiga Power Analysis - " + Path.GetFileNameWithoutExtension(_currentProjectFilename);
                    var analysisResultsPanel = _selectionForms.Where(s => s is AnalysisResultsPanel).Single() as AnalysisResultsPanel;
                    var analysisResultsPerPanel = _selectionForms.Where(s => s is AnalysisResultsPerComparisonPanel).Single() as AnalysisResultsPerComparisonPanel;
                    var filesPath = getCurrentProjectFilesPath();
                    analysisResultsPanel.CurrentProjectFilesPath = filesPath;
                    analysisResultsPerPanel.CurrentProjectFilesPath = filesPath;
                } else {
                    this.Text = "Amiga Power Analysis";
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

        #region Initialization

        private void initialize() {
            _selectionForms = new List<ISelectionForm>();
            _logger = new FileLogger();
            this.closeProject();
        }

        #endregion

        #region Events

        private void MainWindow_Load(object sender, EventArgs e) {
            if (Settings.Default.WindowLocation != null) {
                this.Location = Settings.Default.WindowLocation;
            }
            if (Settings.Default.WindowSize != null) {
                this.Size = Settings.Default.WindowSize;
            }
            EndpointTypeProvider.LoadMyEndpointTypes();

            var currentGenstatPath = Properties.Settings.Default.GenstatPath;
            if (string.IsNullOrEmpty(currentGenstatPath)) {
                var defaultGenstatDirective = @"C:\Program Files\Gen16ed\Bin\GenBatch.exe";
                if (File.Exists(defaultGenstatDirective)) {
                    Properties.Settings.Default.GenstatPath = defaultGenstatDirective;
                    Properties.Settings.Default.Save();
                }
            }
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
            EndpointTypeProvider.StoreMyEndpointTypes();
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
            var endpointsPanel = _selectionForms.Where(s => s is EndpointsPanel).FirstOrDefault() as EndpointsPanel;
            if (endpointsPanel != null) {
                endpointsPanel.UpdateForm();
            }
        }

        private void toolstripAbout_Click(object sender, EventArgs e) {
            var aboutDialog = new AboutForm();
            aboutDialog.ShowDialog();
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
        }

        private void onRunButtonPressed(object sender, EventArgs e) {
            runPowerAnalysis();
        }

        #endregion

        #region Actions

        private void newProject() {
            try {
                closeProject();
                loadProject(ProjectManager.CreateNewProject());
                CurrentProjectFilename = string.Empty;
            } catch (Exception ex) {
                showErrorMessage(ex);
            }
        }

        private void openProjectDialog() {
            try {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Amiga Power Analysis files (*.apa)|*.apa|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.InitialDirectory = Properties.Settings.Default.LastOpenedDirectory;
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    Properties.Settings.Default.LastOpenedDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                    Properties.Settings.Default.Save();
                    var project = ProjectManager.LoadProject(openFileDialog.FileName);
                    loadProject(project);
                    CurrentProjectFilename = openFileDialog.FileName;
                }
            } catch (Exception ex) {
                showErrorMessage(ex);
            }
        }

        private void saveAsDialog() {
            try {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Amiga Power Analysis files (*.apa)|*.apa|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.InitialDirectory = Properties.Settings.Default.LastOpenedDirectory;
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    Properties.Settings.Default.LastOpenedDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                    Properties.Settings.Default.Save();
                    ProjectManager.SaveProject(_project, saveFileDialog.FileName);
                    CurrentProjectFilename = saveFileDialog.FileName;
                }
            } catch (Exception ex) {
                showErrorMessage(ex);
            }
        }

        private void loadProject(Project project) {
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
                _selectionForms.Add(new AnalysisResultsPerComparisonPanel(_project));
                _selectionForms.Add(new AnalysisResultsPanel(_project));
                _selectionForms.Add(new AnalysisTemplatePanel(_project));

                _selectionForms.ForEach(s => s.TabVisibilitiesChanged += onVisibilitySettingsChanged);

                this.closeToolStripMenuItem.Enabled = true;
                this.saveAsToolStripMenuItem.Enabled = true;
                this.saveToolStripMenuItem.Enabled = true;
                this.goToolStripMenuItem.Enabled = true;

                updateTabs();
            } catch (Exception ex) {
                showErrorMessage(ex);
                closeProject();
                return;
            }
        }

        private void saveProject() {
            if (!string.IsNullOrEmpty(CurrentProjectFilename)) {
                ProjectManager.SaveProject(_project, CurrentProjectFilename);
            } else {
                saveAsDialog();
            }
        }

        private void closeProject() {
            _selectionForms.Clear();
            _selectionForms.Add(new IntroductionPanel());
            this.tabControl.TabPages.Clear();
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Enabled = false;
            this.goToolStripMenuItem.Enabled = false;
            updateTabs();
        }

        private void updateTabs() {
            var currentTabName = this.tabControl.SelectedTab != null ? this.tabControl.SelectedTab.Name : null;
            //this.tabControl.TabPages.Clear();
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
            if (string.IsNullOrEmpty(CurrentProjectFilename)) {
                MessageBox.Show("Please save the project first.",
                   "Save project first",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
                return;
            }
            var runSimulationDialog = new RunPowerAnalysisDialog(_project, CurrentProjectFilename);
            runSimulationDialog.ShowDialog();
            this.saveProject();
            this.updateTabs();
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
            _logger.Log(ex.Message);
        }

        #endregion

    }
}
