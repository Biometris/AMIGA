using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Helpers;

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
            Description = "Specify how to perform the power analysis and which methods of analysis are to be compared. It is advised first to use the Approximate method (Lyles) because it is much faster.";
            this.textBoxTabTitle.Text = Name;
            this.textBoxTabDescription.Text = Description;
            _project = project;
        }

        public string Description { get; private set; }

        public void Activate() {
            checkBoxMethodForAnalysesLN.Checked = _project.PowerCalculationSettings.IsLogNormal;
            checkBoxMethodForAnalysesSQ.Checked = _project.PowerCalculationSettings.IsSquareRoot;
            checkBoxMethodForAnalysesOP.Checked = _project.PowerCalculationSettings.IsOverdispersedPoisson;
            checkBoxMethodForAnalysesNB.Checked = _project.PowerCalculationSettings.IsNegativeBinomial;
            textBoxSignificanceLevel.Text = _project.PowerCalculationSettings.SignificanceLevel.ToString();
            textBoxNumberOfRatios.Text = _project.PowerCalculationSettings.NumberOfRatios.ToString();
            textBoxNumberOfReplications.Text = string.Join(", ", _project.PowerCalculationSettings.NumberOfReplications.Select(r => r.ToString()).ToList());
            comboBoxMethodForPowerCalculation.DataSource = Enum.GetValues(typeof(PowerCalculationMethod));
            comboBoxMethodForPowerCalculation.SelectedIndex = (int)_project.PowerCalculationSettings.PowerCalculationMethod;
            textBoxNumberSimulatedDatasets.Text = _project.PowerCalculationSettings.NumberOfSimulatedDataSets.ToString();
            textBoxSeedForRandomNumbers.Text = _project.PowerCalculationSettings.Seed.ToString();
        }

        public bool IsVisible() {
            return true;
        }

        public event EventHandler TabVisibilitiesChanged;

        private void textBoxSignificanceLevel_Validating(object sender, CancelEventArgs e) {
            var textBox = sender as TextBox;
            double value;
            if (Double.TryParse(textBox.Text, out value)) {
                _project.PowerCalculationSettings.SignificanceLevel = value;
            }
            textBox.Text = _project.PowerCalculationSettings.SignificanceLevel.ToString();
        }

        private void textBoxNumberOfRatios_Validating(object sender, CancelEventArgs e) {
            var textBox = sender as TextBox;
            int value;
            if (Int32.TryParse(textBox.Text, out value)) {
                _project.PowerCalculationSettings.NumberOfRatios = value;
            }
            textBox.Text = _project.PowerCalculationSettings.NumberOfRatios.ToString();
        }

        private void textBoxNumberOfReplications_Validating(object sender, CancelEventArgs e) {
            var textBox = sender as TextBox;
            var numberOfReplications = textBox.Text.ParseRange(1, 64);
            if (numberOfReplications.Count() > 0) {
                _project.PowerCalculationSettings.NumberOfReplications = numberOfReplications.ToList();
            }
            textBox.Text = string.Join(", ", _project.PowerCalculationSettings.NumberOfReplications.Select(r => r.ToString()).ToList());
        }

        private void textBoxNumberSimulatedDatasets_Validating(object sender, CancelEventArgs e) {
            var textBox = sender as TextBox;
            int value;
            if (Int32.TryParse(textBox.Text, out value)) {
                _project.PowerCalculationSettings.NumberOfSimulatedDataSets = value;
            }
            textBox.Text = _project.PowerCalculationSettings.NumberOfSimulatedDataSets.ToString();
        }

        private void textBoxSeedForRandomNumbers_Validating(object sender, CancelEventArgs e) {
            var textBox = sender as TextBox;
            int value;
            if (Int32.TryParse(textBox.Text, out value)) {
                _project.PowerCalculationSettings.Seed = value;
            }
            textBox.Text = _project.PowerCalculationSettings.Seed.ToString();
        }

        private void checkBoxMethodForAnalysesLN_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.IsLogNormal = checkBoxMethodForAnalysesLN.Checked;
        }

        private void checkBoxMethodForAnalysesSQ_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.IsSquareRoot = checkBoxMethodForAnalysesSQ.Checked;
        }

        private void checkBoxMethodForAnalysesOP_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.IsOverdispersedPoisson = checkBoxMethodForAnalysesOP.Checked;
        }

        private void checkBoxMethodForAnalysesNB_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.IsNegativeBinomial = checkBoxMethodForAnalysesNB.Checked;
        }

        private void comboBoxMethodForPowerCalculation_SelectionChangeCommitted(object sender, EventArgs e) {
            PowerCalculationMethod powerCalculationMethod;
            Enum.TryParse<PowerCalculationMethod>(comboBoxMethodForPowerCalculation.SelectedValue.ToString(), out powerCalculationMethod);
            _project.PowerCalculationSettings.PowerCalculationMethod = powerCalculationMethod;
        }
    }
}
