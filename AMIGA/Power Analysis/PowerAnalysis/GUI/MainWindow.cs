using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.PowerAnalysis;
using AmigaPowerAnalysis.Helpers;

namespace AmigaPowerAnalysis.GUI {
    public partial class MainWindow : Form {

        private Project _project;
        private string _currentProjectFilename;

        public MainWindow() {
            InitializeComponent();
            initialize();
        }

        #region Initialization

        private void initialize() {
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

        private void toolstripAbout_Click(object sender, EventArgs e) {
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            if (tabControl.SelectedTab != null) {
                var selectedForm = tabControl.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is ISelectionForm) as ISelectionForm;
                selectedForm.Activate();
            }
        }

        #endregion

        #region Actions

        private void newProject() {
            closeProject();
            loadProject(ProjectManager.CreateNewProject());
            _currentProjectFilename = null;
        }

        private void openProjectDialog() {
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
        }

        private void saveAsDialog() {
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
        }

        private void loadProject(Project project) {
            closeProject();
            _project = project;

            var endpointsForm = new EndpointsForm(_project);
            var tab = new TabPage(endpointsForm.Name);
            this.tabControl.TabPages.Add(tab);
            endpointsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(endpointsForm);

            var endpointsDataForm = new EndpointsDataForm(_project);
            tab = new TabPage(endpointsDataForm.Name);
            this.tabControl.TabPages.Add(tab);
            endpointsDataForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(endpointsDataForm);

            var factorsForm = new FactorsForm(_project);
            tab = new TabPage(factorsForm.Name);
            this.tabControl.TabPages.Add(tab);
            factorsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(factorsForm);

            var designForm = new DesignForm(_project);
            tab = new TabPage(designForm.Name);
            this.tabControl.TabPages.Add(tab);
            designForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(designForm);

            var interactionsForm = new InteractionsForm(_project);
            tab = new TabPage(interactionsForm.Name);
            this.tabControl.TabPages.Add(tab);
            interactionsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(interactionsForm);

            var comparisonsForm = new ComparisonsForm(_project);
            tab = new TabPage(comparisonsForm.Name);
            this.tabControl.TabPages.Add(tab);
            comparisonsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(comparisonsForm);

            var modifiersForm = new ModifiersForm(_project);
            tab = new TabPage(modifiersForm.Name);
            this.tabControl.TabPages.Add(tab);
            modifiersForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(modifiersForm);

            var simulationSettingsForm = new SimulationSettingsForm(_project);
            tab = new TabPage(simulationSettingsForm.Name);
            this.tabControl.TabPages.Add(tab);
            simulationSettingsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(simulationSettingsForm);

            var analysisResultsForm = new AnalysisResultsForm(_project);
            tab = new TabPage(analysisResultsForm.Name);
            this.tabControl.TabPages.Add(tab);
            analysisResultsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(analysisResultsForm);

            this.saveAsToolStripMenuItem.Enabled = true;
            this.saveToolStripMenuItem.Enabled = true;
            this.closeToolStripMenuItem.Enabled = true;
        }

        private void closeProject() {
            this.tabControl.TabPages.Clear();
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Enabled = false;
            this.closeToolStripMenuItem.Enabled = false;
        }

        #endregion

        private void goToolStripMenuItem_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(_currentProjectFilename)) {
                MessageBox.Show("Please save the project first.",
                   "Save project first",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
                return;
            }
            var runSimulationDialog = new RunSimulationDialog(_project, _currentProjectFilename);
            runSimulationDialog.ShowDialog();
        }
    }
}
