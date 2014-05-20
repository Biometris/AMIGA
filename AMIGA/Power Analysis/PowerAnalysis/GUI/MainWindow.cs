using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI {
    public partial class MainWindow : Form {

        private Project _project;
        private EndpointTypeProvider _endpointTypeProvider;

        private EndpointsForm _endpointsForm;
        private DesignForm _designForm;
        private InteractionsForm _interactionsForm;
        private ComparisonsForm _comparisonsForm;

        public MainWindow() {
            InitializeComponent();
            initialize();
        }

        #region Initialization

        private void initialize() {
            _project = new Project();
            _endpointTypeProvider = new EndpointTypeProvider();
            _project.Endpoints.Add(new Endpoint() {
                Name = "Beatle",
                EndpointType = _endpointTypeProvider.GetEndpointType("Predator")
            });
            _project.UpdateComparisons(_project.Endpoints.Last());
            _project.Endpoints.Add(new Endpoint() {
                Name = "Giraffe",
                EndpointType = _endpointTypeProvider.GetEndpointType("Herbivore")
            });
            _project.UpdateComparisons(_project.Endpoints.Last());
            _project.Design.Factors.Add(new Factor("Spraying", 3));

            _endpointsForm = new EndpointsForm(_project, _endpointTypeProvider);
            var tab = new TabPage(_endpointsForm.Name);
            this.tabControl.TabPages.Add(tab);
            _endpointsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(_endpointsForm);

            _designForm = new DesignForm(_project);
            tab = new TabPage(_designForm.Name);
            this.tabControl.TabPages.Add(tab);
            _designForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(_designForm);

            _interactionsForm = new InteractionsForm(_project);
            tab = new TabPage(_interactionsForm.Name);
            this.tabControl.TabPages.Add(tab);
            _interactionsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(_interactionsForm);

            _comparisonsForm = new ComparisonsForm(_project);
            tab = new TabPage(_comparisonsForm.Name);
            this.tabControl.TabPages.Add(tab);
            _comparisonsForm.Dock = System.Windows.Forms.DockStyle.Fill;
            tab.Controls.Add(_comparisonsForm);
        }

        #endregion

        #region EventHandling

        private void toolstripAbout_Click(object sender, EventArgs e) {
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedForm = tabControl.SelectedTab.Controls.Cast<Control>().FirstOrDefault(x => x is ISelectionForm) as ISelectionForm;
            selectedForm.Activate();
        }

        #endregion

    }
}
