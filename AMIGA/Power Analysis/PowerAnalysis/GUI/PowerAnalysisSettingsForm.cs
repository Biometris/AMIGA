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
using System.Text.RegularExpressions;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class PowerAnalysisSettingsForm : UserControl, ISelectionForm {

        private Project _project;

        public PowerAnalysisSettingsForm(Project project) {
            InitializeComponent();
            Name = "Simulation settings";
            this.textBoxTabTitle.Text = Name;
            _project = project;
        }

        public void Activate() {
            textBoxSignificanceLevel.Text = "";
            textBoxNumberOfRatios.Text = "";
            textBoxNumberOfReplications.Text = "";
//            comboBoxMethodForPowerCalculation;
            textBoxNumberSimulatedDatasets.Text = "";
            textBoxSeedForRandomNumbers.Text = "";
        }

        private void textBoxSignificanceLevel_Validating(object sender, CancelEventArgs e) {
            _project.PowerCalculationSettings.SignificanceLevel = ValidateDouble(sender as TextBox);
        }

        private void textBoxNumberOfRatios_Validating(object sender, CancelEventArgs e) {

        }

        private void textBoxNumberOfReplications_Validating(object sender, CancelEventArgs e) {

        }

        private void comboBoxMethodForPowerCalculation_Validating(object sender, CancelEventArgs e) {

        }

        private void textBoxNumberSimulatedDatasets_Validating(object sender, CancelEventArgs e) {

        }

        private void textBoxSeedForRandomNumbers_Validating(object sender, CancelEventArgs e) {

        }

        private static double ValidateDouble(TextBox textBox) {
            double value;
            if (!Double.TryParse(textBox.Text, out value)) {
                textBox.Text = Regex.Replace(textBox.Text, "[^0-9.]", "");
                Double.TryParse(textBox.Text, out value);
            }
            textBox.Text = value.ToString();
            return value;
        }

        private void checkBoxMethodForAnalysesLN_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBoxMethodForAnalysesSQ_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBoxMethodForAnalysesOP_CheckedChanged(object sender, EventArgs e) {

        }

        private void checkBoxMethodForAnalysesNB_CheckedChanged(object sender, EventArgs e) {

        }
    }
}
