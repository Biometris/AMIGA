using System;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class IntroductionPanel : UserControl, ISelectionForm {

        public string Description { get; private set; }

        public IntroductionPanel() {
            InitializeComponent();
            Name = "Introduction";
            Description = "Welcome to APA, the Amiga Power Analysis for environmental risk assessment (ERA) using field trials.\r\nYou can calculate the necessary replication for assessing differences and equivalences between a test and a comparator plant variety under different data models for count and continuous data.\r\nAPA allows to specify the experimental design, additional factors in the experiment, and the method of statistical analysis that will be used.\r\nStart by opening an existing file or creating a new file (File menu).\r\nNote: The current implementation requires that the statistical system R is available (http://www.r-project.org/).";
        }

        public event EventHandler TabVisibilitiesChanged;

        public void Activate() {
        }

        public bool IsVisible() {
            return true;
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
