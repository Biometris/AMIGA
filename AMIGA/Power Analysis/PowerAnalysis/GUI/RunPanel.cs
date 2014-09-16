using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

namespace AmigaPowerAnalysis.GUI {
    public partial class RunPanel : UserControl, ISelectionForm {

        private Project _project;

        private Endpoint _currentEndpoint;
        private List<Modifier> _currentFactorModifiers;

        public RunPanel(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Run";
            Description = "Press the run button to start the power analysis.";
        }

        public string Description { get; private set; }

        public void Activate() {
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        public event EventHandler RunButtonPressed;

        private void onRunButtonPressed() {
            var runButtonPressed = RunButtonPressed;
            if (runButtonPressed != null) {
                runButtonPressed(this, null);
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

        private void buttonRunPowerAnalysis_Click(object sender, EventArgs e) {
            onRunButtonPressed();
        }
    }
}
