﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;
using AmigaPowerAnalysis.Core.DataAnalysis.AnalysisModels;
using Biometris.ExtensionMethods;
using Biometris.Statistics.Measurements;

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
            var selectedAnalysisMethodTypesDifferenceTests = _project.PowerCalculationSettings.SelectedAnalysisMethodTypesDifferenceTests;
            checkBoxAnalysisMethodLNDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.LogNormal);
            checkBoxAnalysisMethodSQDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.SquareRoot);
            checkBoxAnalysisMethodOPDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.OverdispersedPoisson);
            checkBoxAnalysisMethodNBDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.NegativeBinomial);
            checkBoxAnalysisMethodELDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.EmpiricalLogit);
            checkBoxAnalysisMethodBBNDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.Betabinomial);
            checkBoxAnalysisMethodOBNDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.OverdispersedBinomial);
            checkBoxAnalysisMethodLPMDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.LogPlusM);
            checkBoxAnalysisMethodGDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.Gamma);
            checkBoxAnalysisMethodNormalDifference.Checked = selectedAnalysisMethodTypesDifferenceTests.Has(AnalysisMethodType.Normal);

            var selectedAnalysisMethodTypesEquivalenceTests = _project.PowerCalculationSettings.SelectedAnalysisMethodTypesEquivalenceTests;
            checkBoxAnalysisMethodLNEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.LogNormal);
            checkBoxAnalysisMethodSQEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.SquareRoot);
            checkBoxAnalysisMethodOPEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.OverdispersedPoisson);
            checkBoxAnalysisMethodNBEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.NegativeBinomial);
            checkBoxAnalysisMethodELEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.EmpiricalLogit);
            checkBoxAnalysisMethodBBNEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.Betabinomial);
            checkBoxAnalysisMethodOBNEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.OverdispersedBinomial);
            checkBoxAnalysisMethodLPMEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.LogPlusM);
            checkBoxAnalysisMethodGEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.Gamma);
            checkBoxAnalysisMethodNormalEquivalence.Checked = selectedAnalysisMethodTypesEquivalenceTests.Has(AnalysisMethodType.Normal);

            textBoxSignificanceLevel.Text = _project.PowerCalculationSettings.SignificanceLevel.ToString();
            textBoxNumberOfEvaluationPoints.Text = _project.PowerCalculationSettings.NumberOfRatios.ToString();
            textBoxNumberOfReplications.Text = string.Join(", ", _project.PowerCalculationSettings.NumberOfReplications.Select(r => r.ToString()).ToList());
            comboBoxMethodForPowerCalculation.DataSource = Enum.GetValues(typeof(PowerCalculationMethod));
            comboBoxMethodForPowerCalculation.SelectedIndex = (int)_project.PowerCalculationSettings.PowerCalculationMethod;
            checkBoxUseWaldTest.Checked = _project.PowerCalculationSettings.UseWaldTest;
            textBoxNumberSimulatedDatasets.Text = _project.PowerCalculationSettings.NumberOfSimulatedDataSets.ToString();
            textBoxSeedForRandomNumbers.Text = _project.PowerCalculationSettings.Seed.ToString();

            groupBoxAnalysisMethodsContinuousDifference.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Continuous);
            groupBoxAnalysisMethodsCountsDifference.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Count);
            groupBoxAnalysisFractionsMethodsDifference.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Fraction);
            groupBoxAnalysisMethodsNonNegativeDifference.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Nonnegative);

            groupBoxAnalysisMethodsContinuousEquivalence.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Continuous);
            groupBoxAnalysisMethodsCountsEquivalence.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Count);
            groupBoxAnalysisFractionsMethodsEquivalence.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Fraction);
            groupBoxAnalysisMethodsNonNegativeEquivalence.Visible = _project.Endpoints.Any(ep => ep.Measurement == MeasurementType.Nonnegative);

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

        private void checkBoxUseWaldTest_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.UseWaldTest = checkBoxUseWaldTest.Checked;
        }

        private void checkBoxAnalysisMethodLNDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.LogNormal, checkBoxAnalysisMethodLNDifference.Checked);
        }

        private void checkBoxAnalysisMethodSQDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.SquareRoot, checkBoxAnalysisMethodSQDifference.Checked);
        }

        private void checkBoxAnalysisMethodOPDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.OverdispersedPoisson, checkBoxAnalysisMethodOPDifference.Checked);
        }

        private void checkBoxAnalysisMethodNBDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.NegativeBinomial, checkBoxAnalysisMethodNBDifference.Checked);
        }

        private void checkBoxAnalysisMethodELDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.EmpiricalLogit, checkBoxAnalysisMethodELDifference.Checked);
        }

        private void checkBoxAnalysisMethodOBNDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.OverdispersedBinomial, checkBoxAnalysisMethodOBNDifference.Checked);
        }

        private void checkBoxAnalysisMethodBBNDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.Betabinomial, checkBoxAnalysisMethodBBNDifference.Checked);
        }

        private void checkBoxAnalysisMethodLPMDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.LogPlusM, checkBoxAnalysisMethodLPMDifference.Checked);
        }

        private void checkBoxAnalysisMethodGDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.Gamma, checkBoxAnalysisMethodGDifference.Checked);
        }

        private void checkBoxAnalysisMethodNDifference_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeDifferenceTests(AnalysisMethodType.Normal, checkBoxAnalysisMethodNormalDifference.Checked);
        }

        private void checkBoxAnalysisMethodLNEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.LogNormal, checkBoxAnalysisMethodLNEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodSQEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.SquareRoot, checkBoxAnalysisMethodSQEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodOPEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.OverdispersedPoisson, checkBoxAnalysisMethodOPEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodNBEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.NegativeBinomial, checkBoxAnalysisMethodNBEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodLPMEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.LogPlusM, checkBoxAnalysisMethodLPMEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodGEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.Gamma, checkBoxAnalysisMethodGEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodNormalEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.Normal, checkBoxAnalysisMethodNormalEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodELEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.EmpiricalLogit, checkBoxAnalysisMethodELEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodOBNEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.OverdispersedBinomial, checkBoxAnalysisMethodOBNEquivalence.Checked);
        }

        private void checkBoxAnalysisMethodBBNEquivalence_CheckedChanged(object sender, EventArgs e) {
            _project.PowerCalculationSettings.SetAnalysisMethodTypeEquivalenceTests(AnalysisMethodType.Betabinomial, checkBoxAnalysisMethodBBNEquivalence.Checked);
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
