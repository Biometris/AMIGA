using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Helpers;
using AmigaPowerAnalysis.Helpers.Log;

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

        #region Initialization

        private void initialize() {
            _selectionForms = new List<ISelectionForm>();
            _logger = new SimpleLogger();
            this.closeProject();
        }

        #endregion

        #region Events

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
            if (!string.IsNullOrEmpty(_currentProjectFilename)) {
                ProjectManager.SaveProject(_project, _currentProjectFilename);
            } else {
                saveAsDialog();
            }
        }

        private void toolstripEndpointTypes_Click(object sender, EventArgs e) {
            var endpointGroupsForm = new SelectionPanelForm(new EndpointTypesPanel(_project));
            endpointGroupsForm.ShowDialog();
        }

        private void toolstripAbout_Click(object sender, EventArgs e) {
            var aboutDialog = new AboutForm();
            aboutDialog.ShowDialog();
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e) {
            runPowerAnalysis();
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
                _currentProjectFilename = null;
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
                    _currentProjectFilename = openFileDialog.FileName;
                    loadProject(project);
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
                    _currentProjectFilename = saveFileDialog.FileName;
                }
            } catch (Exception ex) {
                showErrorMessage(ex);
            }
        }

        private void loadProject(Project project) {
            try {
                closeProject();
                _project = project;

                _selectionForms.Add(new SelectionPanelContainer(new EndpointsPanel(_project)));
                _selectionForms.Add(new SelectionPanelContainer(new EndpointsDataPanel(_project)));
                _selectionForms.Add(new SelectionPanelContainer(new FactorsPanel(_project)));
                _selectionForms.Add(new SelectionPanelContainer(new DesignPanel(_project)));
                _selectionForms.Add(new SelectionPanelContainer(new InteractionsPanel(_project)));
                _selectionForms.Add(new SelectionPanelContainer(new ComparisonsPanel(_project)));
                _selectionForms.Add(new SelectionPanelContainer(new ModifiersPanel(_project)));
                var simulationPanel = new SimulationPanel(_project);
                _selectionForms.Add(new SelectionPanelContainer(simulationPanel));
                simulationPanel.RunButtonPressed += onRunButtonPressed;
                _selectionForms.Add(new SelectionPanelContainer(new AnalysisResultsPanel(_project)));

                _selectionForms.ForEach(s => s.TabVisibilitiesChanged += onVisibilitySettingsChanged);

                this.closeToolStripMenuItem.Enabled = true;
                this.saveAsToolStripMenuItem.Enabled = true;
                this.saveToolStripMenuItem.Enabled = true;

                updateTabs();
            } catch (Exception ex) {
                showErrorMessage(ex);
                closeProject();
                return;
            }
        }

        private void closeProject() {
            _selectionForms.Clear();
            _selectionForms.Add(new SelectionPanelContainer(new IntroductionPanel()));
            this.tabControl.TabPages.Clear();
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Enabled = false;
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
                    var form = (UserControl)selectionForm;
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
            this.updateTabs();
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
