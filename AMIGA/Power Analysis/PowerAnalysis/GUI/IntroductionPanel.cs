using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI {
    public partial class IntroductionPanel : UserControl, ISelectionForm {

        public string Description { get; private set; }

        public IntroductionPanel() {
            InitializeComponent();
            Name = "Introduction";
            Description = "Welcome to Amiga Power Analysis for environmental risk assessment (ERA) using field trials. With this tool you can calculate the necessary replication for assessing differences and equivalences between a genetically modified organism (GMO) and a comparator plant variety.\r\nThe tool allows to specify the experimental design, additional factors in the experiment, and the method of statistical analysis that will be used.\r\nStart by opening an existing file or creating a new file.\r\nNote: The current implementation requires that the statistical system GenStat is available (http://www.vsni.co.uk/software/genstat).";
        }

        public void Activate() {
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

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
