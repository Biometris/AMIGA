using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class SimulationPanel : UserControl, ISelectionForm {

        private Project _project;

        public SimulationPanel(Project project) {
            InitializeComponent();
            Name = "Simulation";
            Description = "Specify how to perform the power analysis and which methods of analysis are to be compared. It is advised first to use the Approximate method (Lyles) because it is much faster.";
            _project = project;
        }

        public string Description { get; private set; }

        public void Activate() {
            var selectedAnalysisMethodType = _project.PowerCalculationSettings.SelectedAnalysisMethodTypes;
            checkBoxMethodForAnalysesLN.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.LogNormal);
            checkBoxMethodForAnalysesSQ.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.SquareRoot);
            checkBoxMethodForAnalysesOP.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.OverdispersedPoisson);
            checkBoxMethodForAnalysesNB.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.NegativeBinomial);
            checkBoxMethodForAnalysesEL.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.EmpiricalLogit);
            checkBoxMethodForAnalysesBBN.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.Betabinomial);
            checkBoxMethodForAnalysesOBN.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.OverdispersedBinomial);
            checkBoxMethodForAnalysesLPM.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.LogPlusM);
            checkBoxMethodForAnalysesG.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.Gamma);
            checkBoxMethodForAnalysesN.Checked = selectedAnalysisMethodType.Has(AnalysisMethodType.Normal);

            textBoxSignificanceLevel.Text = _project.PowerCalculationSettings.SignificanceLevel.ToString();
            textBoxNumberOfEvaluationPoints.Text = _project.PowerCalculationSettings.NumberOfRatios.ToString();
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

        public event EventHandler RunButtonPressed;

        private void onRunButtonPressed() {
            var runButtonPressed = RunButtonPressed;
            if (runButtonPressed != null) {
                runButtonPressed(this, null);
            }
        }

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

        private void setAnalysisMethodType(AnalysisMethodType analysisMethodType, bool selected) {
            if (selected) {
                _project.PowerCalculationSettings.SelectedAnalysisMethodTypes |= analysisMethodType;
            } else {
                _project.PowerCalculationSettings.SelectedAnalysisMethodTypes &= ~analysisMethodType;
            }
        }

        private void checkBoxMethodForAnalysesLN_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.LogNormal, checkBoxMethodForAnalysesLN.Checked);
        }

        private void checkBoxMethodForAnalysesSQ_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.SquareRoot, checkBoxMethodForAnalysesSQ.Checked);
        }

        private void checkBoxMethodForAnalysesOP_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.OverdispersedPoisson, checkBoxMethodForAnalysesOP.Checked);
        }

        private void checkBoxMethodForAnalysesNB_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.NegativeBinomial, checkBoxMethodForAnalysesNB.Checked);
        }

        private void checkBoxMethodForAnalysesEL_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.EmpiricalLogit, checkBoxMethodForAnalysesEL.Checked);
        }

        private void checkBoxMethodForAnalysesOBN_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.OverdispersedBinomial, checkBoxMethodForAnalysesOBN.Checked);
        }

        private void checkBoxMethodForAnalysesBBN_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.Betabinomial, checkBoxMethodForAnalysesBBN.Checked);
        }

        private void checkBoxMethodForAnalysesLPM_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.LogPlusM, checkBoxMethodForAnalysesLPM.Checked);
        }

        private void checkBoxMethodForAnalysesG_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.Gamma, checkBoxMethodForAnalysesG.Checked);
        }

        private void checkBoxMethodForAnalysesN_CheckedChanged(object sender, EventArgs e) {
            setAnalysisMethodType(AnalysisMethodType.Normal, checkBoxMethodForAnalysesN.Checked);
        }

        private void comboBoxMethodForPowerCalculation_SelectionChangeCommitted(object sender, EventArgs e) {
            PowerCalculationMethod powerCalculationMethod;
            Enum.TryParse<PowerCalculationMethod>(comboBoxMethodForPowerCalculation.SelectedValue.ToString(), out powerCalculationMethod);
            _project.PowerCalculationSettings.PowerCalculationMethod = powerCalculationMethod;
        }

        private void buttonRunPowerAnalysis_Click(object sender, EventArgs e) {
            onRunButtonPressed();
        }
    }
}
