namespace AmigaPowerAnalysis.GUI {
    partial class SimulationPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxUseWaldTest = new System.Windows.Forms.CheckBox();
            this.labelUseWaldTest = new System.Windows.Forms.Label();
            this.textBoxSeedForRandomNumbers = new System.Windows.Forms.TextBox();
            this.labelSeedForRandomNumbers = new System.Windows.Forms.Label();
            this.textBoxNumberSimulatedDatasets = new System.Windows.Forms.TextBox();
            this.labelNumberSimulatedDatasets = new System.Windows.Forms.Label();
            this.comboBoxMethodForPowerCalculation = new System.Windows.Forms.ComboBox();
            this.labelMethodForPowerCalculation = new System.Windows.Forms.Label();
            this.textBoxNumberOfReplications = new System.Windows.Forms.TextBox();
            this.labelNumberOfReplications = new System.Windows.Forms.Label();
            this.textBoxNumberOfEvaluationPoints = new System.Windows.Forms.TextBox();
            this.labelNumberOfRatios = new System.Windows.Forms.Label();
            this.textBoxSignificanceLevel = new System.Windows.Forms.TextBox();
            this.labelSignificanceLevel = new System.Windows.Forms.Label();
            this.checkBoxAnalysisMethodLNDifference = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodSQDifference = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodOPDifference = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodNBDifference = new System.Windows.Forms.CheckBox();
            this.groupBoxAnalysisMethodsCountsDifference = new System.Windows.Forms.GroupBox();
            this.buttonRunPowerAnalysis = new System.Windows.Forms.Button();
            this.groupBoxAnalysisFractionsMethodsDifference = new System.Windows.Forms.GroupBox();
            this.checkBoxAnalysisMethodELDifference = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodBBNDifference = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodOBNDifference = new System.Windows.Forms.CheckBox();
            this.groupBoxAnalysisMethodsNonNegativeDifference = new System.Windows.Forms.GroupBox();
            this.checkBoxAnalysisMethodLPMDifference = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodGDifference = new System.Windows.Forms.CheckBox();
            this.groupBoxAnalysisMethodsContinuousDifference = new System.Windows.Forms.GroupBox();
            this.checkBoxAnalysisMethodNormalDifference = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelAnalysisMethodsDifference = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxAnalysisMethodsCountsEquivalence = new System.Windows.Forms.GroupBox();
            this.checkBoxAnalysisMethodNBEquivalence = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodLNEquivalence = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodOPEquivalence = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodSQEquivalence = new System.Windows.Forms.CheckBox();
            this.groupBoxAnalysisMethodsNonNegativeEquivalence = new System.Windows.Forms.GroupBox();
            this.checkBoxAnalysisMethodLPMEquivalence = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodGEquivalence = new System.Windows.Forms.CheckBox();
            this.groupBoxAnalysisMethodsContinuousEquivalence = new System.Windows.Forms.GroupBox();
            this.checkBoxAnalysisMethodNormalEquivalence = new System.Windows.Forms.CheckBox();
            this.groupBoxAnalysisFractionsMethodsEquivalence = new System.Windows.Forms.GroupBox();
            this.checkBoxAnalysisMethodELEquivalence = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodBBNEquivalence = new System.Windows.Forms.CheckBox();
            this.checkBoxAnalysisMethodOBNEquivalence = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions.SuspendLayout();
            this.groupBoxAnalysisMethodsCountsDifference.SuspendLayout();
            this.groupBoxAnalysisFractionsMethodsDifference.SuspendLayout();
            this.groupBoxAnalysisMethodsNonNegativeDifference.SuspendLayout();
            this.groupBoxAnalysisMethodsContinuousDifference.SuspendLayout();
            this.flowLayoutPanelAnalysisMethodsDifference.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxAnalysisMethodsCountsEquivalence.SuspendLayout();
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.SuspendLayout();
            this.groupBoxAnalysisMethodsContinuousEquivalence.SuspendLayout();
            this.groupBoxAnalysisFractionsMethodsEquivalence.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.AutoSize = true;
            this.groupBoxOptions.Controls.Add(this.checkBoxUseWaldTest);
            this.groupBoxOptions.Controls.Add(this.labelUseWaldTest);
            this.groupBoxOptions.Controls.Add(this.textBoxSeedForRandomNumbers);
            this.groupBoxOptions.Controls.Add(this.labelSeedForRandomNumbers);
            this.groupBoxOptions.Controls.Add(this.textBoxNumberSimulatedDatasets);
            this.groupBoxOptions.Controls.Add(this.labelNumberSimulatedDatasets);
            this.groupBoxOptions.Controls.Add(this.comboBoxMethodForPowerCalculation);
            this.groupBoxOptions.Controls.Add(this.labelMethodForPowerCalculation);
            this.groupBoxOptions.Controls.Add(this.textBoxNumberOfReplications);
            this.groupBoxOptions.Controls.Add(this.labelNumberOfReplications);
            this.groupBoxOptions.Controls.Add(this.textBoxNumberOfEvaluationPoints);
            this.groupBoxOptions.Controls.Add(this.labelNumberOfRatios);
            this.groupBoxOptions.Controls.Add(this.textBoxSignificanceLevel);
            this.groupBoxOptions.Controls.Add(this.labelSignificanceLevel);
            this.groupBoxOptions.Location = new System.Drawing.Point(15, 56);
            this.groupBoxOptions.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(943, 229);
            this.groupBoxOptions.TabIndex = 9;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // checkBoxUseWaldTest
            // 
            this.checkBoxUseWaldTest.AutoSize = true;
            this.checkBoxUseWaldTest.Checked = true;
            this.checkBoxUseWaldTest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseWaldTest.Location = new System.Drawing.Point(553, 138);
            this.checkBoxUseWaldTest.Name = "checkBoxUseWaldTest";
            this.checkBoxUseWaldTest.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseWaldTest.TabIndex = 13;
            this.checkBoxUseWaldTest.UseVisualStyleBackColor = true;
            this.checkBoxUseWaldTest.CheckedChanged += new System.EventHandler(this.checkBoxUseWaldTest_CheckedChanged);
            // 
            // labelUseWaldTest
            // 
            this.labelUseWaldTest.AutoSize = true;
            this.labelUseWaldTest.Location = new System.Drawing.Point(20, 136);
            this.labelUseWaldTest.Name = "labelUseWaldTest";
            this.labelUseWaldTest.Size = new System.Drawing.Size(74, 13);
            this.labelUseWaldTest.TabIndex = 12;
            this.labelUseWaldTest.Text = "Use Wald test";
            // 
            // textBoxSeedForRandomNumbers
            // 
            this.textBoxSeedForRandomNumbers.Location = new System.Drawing.Point(553, 190);
            this.textBoxSeedForRandomNumbers.Name = "textBoxSeedForRandomNumbers";
            this.textBoxSeedForRandomNumbers.Size = new System.Drawing.Size(100, 20);
            this.textBoxSeedForRandomNumbers.TabIndex = 11;
            this.textBoxSeedForRandomNumbers.Text = "123456";
            this.textBoxSeedForRandomNumbers.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSeedForRandomNumbers_Validating);
            // 
            // labelSeedForRandomNumbers
            // 
            this.labelSeedForRandomNumbers.AutoSize = true;
            this.labelSeedForRandomNumbers.Location = new System.Drawing.Point(20, 192);
            this.labelSeedForRandomNumbers.Name = "labelSeedForRandomNumbers";
            this.labelSeedForRandomNumbers.Size = new System.Drawing.Size(365, 13);
            this.labelSeedForRandomNumbers.TabIndex = 10;
            this.labelSeedForRandomNumbers.Text = "Seed for random number generator (non-negative value uses computer time)";
            // 
            // textBoxNumberSimulatedDatasets
            // 
            this.textBoxNumberSimulatedDatasets.Location = new System.Drawing.Point(553, 161);
            this.textBoxNumberSimulatedDatasets.Name = "textBoxNumberSimulatedDatasets";
            this.textBoxNumberSimulatedDatasets.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumberSimulatedDatasets.TabIndex = 9;
            this.textBoxNumberSimulatedDatasets.Text = "100";
            this.textBoxNumberSimulatedDatasets.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberSimulatedDatasets_Validating);
            // 
            // labelNumberSimulatedDatasets
            // 
            this.labelNumberSimulatedDatasets.AutoSize = true;
            this.labelNumberSimulatedDatasets.Location = new System.Drawing.Point(20, 164);
            this.labelNumberSimulatedDatasets.Name = "labelNumberSimulatedDatasets";
            this.labelNumberSimulatedDatasets.Size = new System.Drawing.Size(246, 13);
            this.labelNumberSimulatedDatasets.TabIndex = 8;
            this.labelNumberSimulatedDatasets.Text = "Number of simulated datasets for Method=Simulate";
            // 
            // comboBoxMethodForPowerCalculation
            // 
            this.comboBoxMethodForPowerCalculation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethodForPowerCalculation.FormattingEnabled = true;
            this.comboBoxMethodForPowerCalculation.Location = new System.Drawing.Point(553, 108);
            this.comboBoxMethodForPowerCalculation.Name = "comboBoxMethodForPowerCalculation";
            this.comboBoxMethodForPowerCalculation.Size = new System.Drawing.Size(100, 21);
            this.comboBoxMethodForPowerCalculation.TabIndex = 7;
            this.comboBoxMethodForPowerCalculation.SelectionChangeCommitted += new System.EventHandler(this.comboBoxMethodForPowerCalculation_SelectionChangeCommitted);
            // 
            // labelMethodForPowerCalculation
            // 
            this.labelMethodForPowerCalculation.AutoSize = true;
            this.labelMethodForPowerCalculation.Location = new System.Drawing.Point(20, 108);
            this.labelMethodForPowerCalculation.Name = "labelMethodForPowerCalculation";
            this.labelMethodForPowerCalculation.Size = new System.Drawing.Size(332, 13);
            this.labelMethodForPowerCalculation.TabIndex = 6;
            this.labelMethodForPowerCalculation.Text = "Method for Power Calculation (currently only Simulate is implemented)";
            // 
            // textBoxNumberOfReplications
            // 
            this.textBoxNumberOfReplications.Location = new System.Drawing.Point(553, 79);
            this.textBoxNumberOfReplications.Name = "textBoxNumberOfReplications";
            this.textBoxNumberOfReplications.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumberOfReplications.TabIndex = 5;
            this.textBoxNumberOfReplications.Text = "2,4,8,16,32";
            this.textBoxNumberOfReplications.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberOfReplications_Validating);
            // 
            // labelNumberOfReplications
            // 
            this.labelNumberOfReplications.AutoSize = true;
            this.labelNumberOfReplications.Location = new System.Drawing.Point(20, 80);
            this.labelNumberOfReplications.Name = "labelNumberOfReplications";
            this.labelNumberOfReplications.Size = new System.Drawing.Size(425, 13);
            this.labelNumberOfReplications.TabIndex = 4;
            this.labelNumberOfReplications.Text = "Number of Replications for which to calculate the power (comma-separated list of " +
    "values)";
            // 
            // textBoxNumberOfEvaluationPoints
            // 
            this.textBoxNumberOfEvaluationPoints.Location = new System.Drawing.Point(553, 50);
            this.textBoxNumberOfEvaluationPoints.Name = "textBoxNumberOfEvaluationPoints";
            this.textBoxNumberOfEvaluationPoints.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumberOfEvaluationPoints.TabIndex = 3;
            this.textBoxNumberOfEvaluationPoints.Text = "5";
            this.textBoxNumberOfEvaluationPoints.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberOfRatios_Validating);
            // 
            // labelNumberOfRatios
            // 
            this.labelNumberOfRatios.AutoSize = true;
            this.labelNumberOfRatios.Location = new System.Drawing.Point(20, 52);
            this.labelNumberOfRatios.Name = "labelNumberOfRatios";
            this.labelNumberOfRatios.Size = new System.Drawing.Size(524, 13);
            this.labelNumberOfRatios.TabIndex = 2;
            this.labelNumberOfRatios.Text = "Number of evaluation points between no-difference and each limit of concern for w" +
    "hich to calculate the power";
            // 
            // textBoxSignificanceLevel
            // 
            this.textBoxSignificanceLevel.Location = new System.Drawing.Point(553, 21);
            this.textBoxSignificanceLevel.Name = "textBoxSignificanceLevel";
            this.textBoxSignificanceLevel.Size = new System.Drawing.Size(100, 20);
            this.textBoxSignificanceLevel.TabIndex = 1;
            this.textBoxSignificanceLevel.Text = "0.05";
            this.textBoxSignificanceLevel.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSignificanceLevel_Validating);
            // 
            // labelSignificanceLevel
            // 
            this.labelSignificanceLevel.AutoSize = true;
            this.labelSignificanceLevel.Location = new System.Drawing.Point(20, 24);
            this.labelSignificanceLevel.Name = "labelSignificanceLevel";
            this.labelSignificanceLevel.Size = new System.Drawing.Size(173, 13);
            this.labelSignificanceLevel.TabIndex = 0;
            this.labelSignificanceLevel.Text = "Significance level of statistical tests";
            // 
            // checkBoxAnalysisMethodLNDifference
            // 
            this.checkBoxAnalysisMethodLNDifference.AutoSize = true;
            this.checkBoxAnalysisMethodLNDifference.Checked = true;
            this.checkBoxAnalysisMethodLNDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodLNDifference.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodLNDifference.Name = "checkBoxAnalysisMethodLNDifference";
            this.checkBoxAnalysisMethodLNDifference.Size = new System.Drawing.Size(139, 17);
            this.checkBoxAnalysisMethodLNDifference.TabIndex = 11;
            this.checkBoxAnalysisMethodLNDifference.Text = "Log(N+1) transformation";
            this.checkBoxAnalysisMethodLNDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodLNDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodLNDifference_CheckedChanged);
            // 
            // checkBoxAnalysisMethodSQDifference
            // 
            this.checkBoxAnalysisMethodSQDifference.AutoSize = true;
            this.checkBoxAnalysisMethodSQDifference.Location = new System.Drawing.Point(20, 52);
            this.checkBoxAnalysisMethodSQDifference.Name = "checkBoxAnalysisMethodSQDifference";
            this.checkBoxAnalysisMethodSQDifference.Size = new System.Drawing.Size(155, 17);
            this.checkBoxAnalysisMethodSQDifference.TabIndex = 12;
            this.checkBoxAnalysisMethodSQDifference.Text = "Square Root transformation";
            this.checkBoxAnalysisMethodSQDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodSQDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodSQDifference_CheckedChanged);
            // 
            // checkBoxAnalysisMethodOPDifference
            // 
            this.checkBoxAnalysisMethodOPDifference.AutoSize = true;
            this.checkBoxAnalysisMethodOPDifference.Checked = true;
            this.checkBoxAnalysisMethodOPDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodOPDifference.Location = new System.Drawing.Point(20, 80);
            this.checkBoxAnalysisMethodOPDifference.Name = "checkBoxAnalysisMethodOPDifference";
            this.checkBoxAnalysisMethodOPDifference.Size = new System.Drawing.Size(196, 17);
            this.checkBoxAnalysisMethodOPDifference.TabIndex = 13;
            this.checkBoxAnalysisMethodOPDifference.Text = "Log-linear model with overdispersion";
            this.checkBoxAnalysisMethodOPDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodOPDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodOPDifference_CheckedChanged);
            // 
            // checkBoxAnalysisMethodNBDifference
            // 
            this.checkBoxAnalysisMethodNBDifference.AutoSize = true;
            this.checkBoxAnalysisMethodNBDifference.Location = new System.Drawing.Point(20, 108);
            this.checkBoxAnalysisMethodNBDifference.Name = "checkBoxAnalysisMethodNBDifference";
            this.checkBoxAnalysisMethodNBDifference.Size = new System.Drawing.Size(200, 17);
            this.checkBoxAnalysisMethodNBDifference.TabIndex = 14;
            this.checkBoxAnalysisMethodNBDifference.Text = "Negative Binomial model with log link";
            this.checkBoxAnalysisMethodNBDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodNBDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodNBDifference_CheckedChanged);
            // 
            // groupBoxAnalysisMethodsCountsDifference
            // 
            this.groupBoxAnalysisMethodsCountsDifference.AutoSize = true;
            this.groupBoxAnalysisMethodsCountsDifference.Controls.Add(this.checkBoxAnalysisMethodNBDifference);
            this.groupBoxAnalysisMethodsCountsDifference.Controls.Add(this.checkBoxAnalysisMethodLNDifference);
            this.groupBoxAnalysisMethodsCountsDifference.Controls.Add(this.checkBoxAnalysisMethodOPDifference);
            this.groupBoxAnalysisMethodsCountsDifference.Controls.Add(this.checkBoxAnalysisMethodSQDifference);
            this.groupBoxAnalysisMethodsCountsDifference.Location = new System.Drawing.Point(5, 5);
            this.groupBoxAnalysisMethodsCountsDifference.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisMethodsCountsDifference.Name = "groupBoxAnalysisMethodsCountsDifference";
            this.groupBoxAnalysisMethodsCountsDifference.Size = new System.Drawing.Size(226, 144);
            this.groupBoxAnalysisMethodsCountsDifference.TabIndex = 10;
            this.groupBoxAnalysisMethodsCountsDifference.TabStop = false;
            this.groupBoxAnalysisMethodsCountsDifference.Text = "Analysis difference tests counts";
            // 
            // buttonRunPowerAnalysis
            // 
            this.buttonRunPowerAnalysis.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRunPowerAnalysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRunPowerAnalysis.Location = new System.Drawing.Point(15, 13);
            this.buttonRunPowerAnalysis.Name = "buttonRunPowerAnalysis";
            this.buttonRunPowerAnalysis.Size = new System.Drawing.Size(102, 35);
            this.buttonRunPowerAnalysis.TabIndex = 11;
            this.buttonRunPowerAnalysis.Text = "Run";
            this.buttonRunPowerAnalysis.UseVisualStyleBackColor = false;
            this.buttonRunPowerAnalysis.Click += new System.EventHandler(this.buttonRunPowerAnalysis_Click);
            // 
            // groupBoxAnalysisFractionsMethodsDifference
            // 
            this.groupBoxAnalysisFractionsMethodsDifference.Controls.Add(this.checkBoxAnalysisMethodELDifference);
            this.groupBoxAnalysisFractionsMethodsDifference.Controls.Add(this.checkBoxAnalysisMethodBBNDifference);
            this.groupBoxAnalysisFractionsMethodsDifference.Controls.Add(this.checkBoxAnalysisMethodOBNDifference);
            this.groupBoxAnalysisFractionsMethodsDifference.Location = new System.Drawing.Point(667, 5);
            this.groupBoxAnalysisFractionsMethodsDifference.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisFractionsMethodsDifference.Name = "groupBoxAnalysisFractionsMethodsDifference";
            this.groupBoxAnalysisFractionsMethodsDifference.Size = new System.Drawing.Size(193, 144);
            this.groupBoxAnalysisFractionsMethodsDifference.TabIndex = 15;
            this.groupBoxAnalysisFractionsMethodsDifference.TabStop = false;
            this.groupBoxAnalysisFractionsMethodsDifference.Text = "Analysis difference tests fractions";
            // 
            // checkBoxAnalysisMethodELDifference
            // 
            this.checkBoxAnalysisMethodELDifference.AutoSize = true;
            this.checkBoxAnalysisMethodELDifference.Checked = true;
            this.checkBoxAnalysisMethodELDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodELDifference.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodELDifference.Name = "checkBoxAnalysisMethodELDifference";
            this.checkBoxAnalysisMethodELDifference.Size = new System.Drawing.Size(159, 17);
            this.checkBoxAnalysisMethodELDifference.TabIndex = 11;
            this.checkBoxAnalysisMethodELDifference.Text = "Empirical logit transformation";
            this.checkBoxAnalysisMethodELDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodELDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodELDifference_CheckedChanged);
            // 
            // checkBoxAnalysisMethodBBNDifference
            // 
            this.checkBoxAnalysisMethodBBNDifference.AutoSize = true;
            this.checkBoxAnalysisMethodBBNDifference.Checked = true;
            this.checkBoxAnalysisMethodBBNDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodBBNDifference.Location = new System.Drawing.Point(20, 80);
            this.checkBoxAnalysisMethodBBNDifference.Name = "checkBoxAnalysisMethodBBNDifference";
            this.checkBoxAnalysisMethodBBNDifference.Size = new System.Drawing.Size(149, 17);
            this.checkBoxAnalysisMethodBBNDifference.TabIndex = 13;
            this.checkBoxAnalysisMethodBBNDifference.Text = "Betabinomial with logit link";
            this.checkBoxAnalysisMethodBBNDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodBBNDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodBBNDifference_CheckedChanged);
            // 
            // checkBoxAnalysisMethodOBNDifference
            // 
            this.checkBoxAnalysisMethodOBNDifference.AutoSize = true;
            this.checkBoxAnalysisMethodOBNDifference.Location = new System.Drawing.Point(20, 52);
            this.checkBoxAnalysisMethodOBNDifference.Name = "checkBoxAnalysisMethodOBNDifference";
            this.checkBoxAnalysisMethodOBNDifference.Size = new System.Drawing.Size(142, 17);
            this.checkBoxAnalysisMethodOBNDifference.TabIndex = 12;
            this.checkBoxAnalysisMethodOBNDifference.Text = "Logit with overdispersion";
            this.checkBoxAnalysisMethodOBNDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodOBNDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodOBNDifference_CheckedChanged);
            // 
            // groupBoxAnalysisMethodsNonNegativeDifference
            // 
            this.groupBoxAnalysisMethodsNonNegativeDifference.Controls.Add(this.checkBoxAnalysisMethodLPMDifference);
            this.groupBoxAnalysisMethodsNonNegativeDifference.Controls.Add(this.checkBoxAnalysisMethodGDifference);
            this.groupBoxAnalysisMethodsNonNegativeDifference.Location = new System.Drawing.Point(241, 5);
            this.groupBoxAnalysisMethodsNonNegativeDifference.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisMethodsNonNegativeDifference.Name = "groupBoxAnalysisMethodsNonNegativeDifference";
            this.groupBoxAnalysisMethodsNonNegativeDifference.Size = new System.Drawing.Size(211, 144);
            this.groupBoxAnalysisMethodsNonNegativeDifference.TabIndex = 16;
            this.groupBoxAnalysisMethodsNonNegativeDifference.TabStop = false;
            this.groupBoxAnalysisMethodsNonNegativeDifference.Text = "Analysis difference tests non-negative";
            // 
            // checkBoxAnalysisMethodLPMDifference
            // 
            this.checkBoxAnalysisMethodLPMDifference.AutoSize = true;
            this.checkBoxAnalysisMethodLPMDifference.Checked = true;
            this.checkBoxAnalysisMethodLPMDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodLPMDifference.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodLPMDifference.Name = "checkBoxAnalysisMethodLPMDifference";
            this.checkBoxAnalysisMethodLPMDifference.Size = new System.Drawing.Size(138, 17);
            this.checkBoxAnalysisMethodLPMDifference.TabIndex = 11;
            this.checkBoxAnalysisMethodLPMDifference.Text = "Log(x+m) transformation";
            this.checkBoxAnalysisMethodLPMDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodLPMDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodLPMDifference_CheckedChanged);
            // 
            // checkBoxAnalysisMethodGDifference
            // 
            this.checkBoxAnalysisMethodGDifference.AutoSize = true;
            this.checkBoxAnalysisMethodGDifference.Location = new System.Drawing.Point(20, 52);
            this.checkBoxAnalysisMethodGDifference.Name = "checkBoxAnalysisMethodGDifference";
            this.checkBoxAnalysisMethodGDifference.Size = new System.Drawing.Size(120, 17);
            this.checkBoxAnalysisMethodGDifference.TabIndex = 12;
            this.checkBoxAnalysisMethodGDifference.Text = "Gamma with log link";
            this.checkBoxAnalysisMethodGDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodGDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodGDifference_CheckedChanged);
            // 
            // groupBoxAnalysisMethodsContinuousDifference
            // 
            this.groupBoxAnalysisMethodsContinuousDifference.Controls.Add(this.checkBoxAnalysisMethodNormalDifference);
            this.groupBoxAnalysisMethodsContinuousDifference.Location = new System.Drawing.Point(462, 5);
            this.groupBoxAnalysisMethodsContinuousDifference.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisMethodsContinuousDifference.Name = "groupBoxAnalysisMethodsContinuousDifference";
            this.groupBoxAnalysisMethodsContinuousDifference.Size = new System.Drawing.Size(195, 144);
            this.groupBoxAnalysisMethodsContinuousDifference.TabIndex = 17;
            this.groupBoxAnalysisMethodsContinuousDifference.TabStop = false;
            this.groupBoxAnalysisMethodsContinuousDifference.Text = "Analysis difference tests continuous";
            // 
            // checkBoxAnalysisMethodNormalDifference
            // 
            this.checkBoxAnalysisMethodNormalDifference.AutoSize = true;
            this.checkBoxAnalysisMethodNormalDifference.Checked = true;
            this.checkBoxAnalysisMethodNormalDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodNormalDifference.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodNormalDifference.Name = "checkBoxAnalysisMethodNormalDifference";
            this.checkBoxAnalysisMethodNormalDifference.Size = new System.Drawing.Size(90, 17);
            this.checkBoxAnalysisMethodNormalDifference.TabIndex = 11;
            this.checkBoxAnalysisMethodNormalDifference.Text = "Normal model";
            this.checkBoxAnalysisMethodNormalDifference.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodNormalDifference.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodNDifference_CheckedChanged);
            // 
            // flowLayoutPanelAnalysisMethodsDifference
            // 
            this.flowLayoutPanelAnalysisMethodsDifference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelAnalysisMethodsDifference.AutoSize = true;
            this.flowLayoutPanelAnalysisMethodsDifference.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelAnalysisMethodsDifference.Controls.Add(this.groupBoxAnalysisMethodsCountsDifference);
            this.flowLayoutPanelAnalysisMethodsDifference.Controls.Add(this.groupBoxAnalysisMethodsNonNegativeDifference);
            this.flowLayoutPanelAnalysisMethodsDifference.Controls.Add(this.groupBoxAnalysisMethodsContinuousDifference);
            this.flowLayoutPanelAnalysisMethodsDifference.Controls.Add(this.groupBoxAnalysisFractionsMethodsDifference);
            this.flowLayoutPanelAnalysisMethodsDifference.Location = new System.Drawing.Point(10, 288);
            this.flowLayoutPanelAnalysisMethodsDifference.Name = "flowLayoutPanelAnalysisMethodsDifference";
            this.flowLayoutPanelAnalysisMethodsDifference.Size = new System.Drawing.Size(865, 154);
            this.flowLayoutPanelAnalysisMethodsDifference.TabIndex = 18;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.groupBoxAnalysisMethodsCountsEquivalence);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxAnalysisMethodsNonNegativeEquivalence);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxAnalysisMethodsContinuousEquivalence);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxAnalysisFractionsMethodsEquivalence);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 448);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(865, 154);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // groupBoxAnalysisMethodsCountsEquivalence
            // 
            this.groupBoxAnalysisMethodsCountsEquivalence.AutoSize = true;
            this.groupBoxAnalysisMethodsCountsEquivalence.Controls.Add(this.checkBoxAnalysisMethodNBEquivalence);
            this.groupBoxAnalysisMethodsCountsEquivalence.Controls.Add(this.checkBoxAnalysisMethodLNEquivalence);
            this.groupBoxAnalysisMethodsCountsEquivalence.Controls.Add(this.checkBoxAnalysisMethodOPEquivalence);
            this.groupBoxAnalysisMethodsCountsEquivalence.Controls.Add(this.checkBoxAnalysisMethodSQEquivalence);
            this.groupBoxAnalysisMethodsCountsEquivalence.Location = new System.Drawing.Point(5, 5);
            this.groupBoxAnalysisMethodsCountsEquivalence.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisMethodsCountsEquivalence.Name = "groupBoxAnalysisMethodsCountsEquivalence";
            this.groupBoxAnalysisMethodsCountsEquivalence.Size = new System.Drawing.Size(226, 144);
            this.groupBoxAnalysisMethodsCountsEquivalence.TabIndex = 10;
            this.groupBoxAnalysisMethodsCountsEquivalence.TabStop = false;
            this.groupBoxAnalysisMethodsCountsEquivalence.Text = "Analysis equivalence tests counts";
            // 
            // checkBoxAnalysisMethodNBEquivalence
            // 
            this.checkBoxAnalysisMethodNBEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodNBEquivalence.Location = new System.Drawing.Point(20, 108);
            this.checkBoxAnalysisMethodNBEquivalence.Name = "checkBoxAnalysisMethodNBEquivalence";
            this.checkBoxAnalysisMethodNBEquivalence.Size = new System.Drawing.Size(200, 17);
            this.checkBoxAnalysisMethodNBEquivalence.TabIndex = 14;
            this.checkBoxAnalysisMethodNBEquivalence.Text = "Negative Binomial model with log link";
            this.checkBoxAnalysisMethodNBEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodNBEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodNBEquivalence_CheckedChanged);
            // 
            // checkBoxAnalysisMethodLNEquivalence
            // 
            this.checkBoxAnalysisMethodLNEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodLNEquivalence.Checked = true;
            this.checkBoxAnalysisMethodLNEquivalence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodLNEquivalence.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodLNEquivalence.Name = "checkBoxAnalysisMethodLNEquivalence";
            this.checkBoxAnalysisMethodLNEquivalence.Size = new System.Drawing.Size(139, 17);
            this.checkBoxAnalysisMethodLNEquivalence.TabIndex = 11;
            this.checkBoxAnalysisMethodLNEquivalence.Text = "Log(N+1) transformation";
            this.checkBoxAnalysisMethodLNEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodLNEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodLNEquivalence_CheckedChanged);
            // 
            // checkBoxAnalysisMethodOPEquivalence
            // 
            this.checkBoxAnalysisMethodOPEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodOPEquivalence.Checked = true;
            this.checkBoxAnalysisMethodOPEquivalence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodOPEquivalence.Location = new System.Drawing.Point(20, 80);
            this.checkBoxAnalysisMethodOPEquivalence.Name = "checkBoxAnalysisMethodOPEquivalence";
            this.checkBoxAnalysisMethodOPEquivalence.Size = new System.Drawing.Size(196, 17);
            this.checkBoxAnalysisMethodOPEquivalence.TabIndex = 13;
            this.checkBoxAnalysisMethodOPEquivalence.Text = "Log-linear model with overdispersion";
            this.checkBoxAnalysisMethodOPEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodOPEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodOPEquivalence_CheckedChanged);
            // 
            // checkBoxAnalysisMethodSQEquivalence
            // 
            this.checkBoxAnalysisMethodSQEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodSQEquivalence.Location = new System.Drawing.Point(20, 52);
            this.checkBoxAnalysisMethodSQEquivalence.Name = "checkBoxAnalysisMethodSQEquivalence";
            this.checkBoxAnalysisMethodSQEquivalence.Size = new System.Drawing.Size(155, 17);
            this.checkBoxAnalysisMethodSQEquivalence.TabIndex = 12;
            this.checkBoxAnalysisMethodSQEquivalence.Text = "Square Root transformation";
            this.checkBoxAnalysisMethodSQEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodSQEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodSQEquivalence_CheckedChanged);
            // 
            // groupBoxAnalysisMethodsNonNegativeEquivalence
            // 
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.Controls.Add(this.checkBoxAnalysisMethodLPMEquivalence);
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.Controls.Add(this.checkBoxAnalysisMethodGEquivalence);
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.Location = new System.Drawing.Point(241, 5);
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.Name = "groupBoxAnalysisMethodsNonNegativeEquivalence";
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.Size = new System.Drawing.Size(211, 144);
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.TabIndex = 16;
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.TabStop = false;
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.Text = "Analysis equivalence tests non-negative";
            // 
            // checkBoxAnalysisMethodLPMEquivalence
            // 
            this.checkBoxAnalysisMethodLPMEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodLPMEquivalence.Checked = true;
            this.checkBoxAnalysisMethodLPMEquivalence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodLPMEquivalence.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodLPMEquivalence.Name = "checkBoxAnalysisMethodLPMEquivalence";
            this.checkBoxAnalysisMethodLPMEquivalence.Size = new System.Drawing.Size(138, 17);
            this.checkBoxAnalysisMethodLPMEquivalence.TabIndex = 11;
            this.checkBoxAnalysisMethodLPMEquivalence.Text = "Log(x+m) transformation";
            this.checkBoxAnalysisMethodLPMEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodLPMEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodLPMEquivalence_CheckedChanged);
            // 
            // checkBoxAnalysisMethodGEquivalence
            // 
            this.checkBoxAnalysisMethodGEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodGEquivalence.Location = new System.Drawing.Point(20, 52);
            this.checkBoxAnalysisMethodGEquivalence.Name = "checkBoxAnalysisMethodGEquivalence";
            this.checkBoxAnalysisMethodGEquivalence.Size = new System.Drawing.Size(120, 17);
            this.checkBoxAnalysisMethodGEquivalence.TabIndex = 12;
            this.checkBoxAnalysisMethodGEquivalence.Text = "Gamma with log link";
            this.checkBoxAnalysisMethodGEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodGEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodGEquivalence_CheckedChanged);
            // 
            // groupBoxAnalysisMethodsContinuousEquivalence
            // 
            this.groupBoxAnalysisMethodsContinuousEquivalence.Controls.Add(this.checkBoxAnalysisMethodNormalEquivalence);
            this.groupBoxAnalysisMethodsContinuousEquivalence.Location = new System.Drawing.Point(462, 5);
            this.groupBoxAnalysisMethodsContinuousEquivalence.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisMethodsContinuousEquivalence.Name = "groupBoxAnalysisMethodsContinuousEquivalence";
            this.groupBoxAnalysisMethodsContinuousEquivalence.Size = new System.Drawing.Size(195, 144);
            this.groupBoxAnalysisMethodsContinuousEquivalence.TabIndex = 17;
            this.groupBoxAnalysisMethodsContinuousEquivalence.TabStop = false;
            this.groupBoxAnalysisMethodsContinuousEquivalence.Text = "Analysis equivalence tests continous";
            // 
            // checkBoxAnalysisMethodNormalEquivalence
            // 
            this.checkBoxAnalysisMethodNormalEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodNormalEquivalence.Checked = true;
            this.checkBoxAnalysisMethodNormalEquivalence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodNormalEquivalence.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodNormalEquivalence.Name = "checkBoxAnalysisMethodNormalEquivalence";
            this.checkBoxAnalysisMethodNormalEquivalence.Size = new System.Drawing.Size(90, 17);
            this.checkBoxAnalysisMethodNormalEquivalence.TabIndex = 11;
            this.checkBoxAnalysisMethodNormalEquivalence.Text = "Normal model";
            this.checkBoxAnalysisMethodNormalEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodNormalEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodNormalEquivalence_CheckedChanged);
            // 
            // groupBoxAnalysisFractionsMethodsEquivalence
            // 
            this.groupBoxAnalysisFractionsMethodsEquivalence.Controls.Add(this.checkBoxAnalysisMethodELEquivalence);
            this.groupBoxAnalysisFractionsMethodsEquivalence.Controls.Add(this.checkBoxAnalysisMethodBBNEquivalence);
            this.groupBoxAnalysisFractionsMethodsEquivalence.Controls.Add(this.checkBoxAnalysisMethodOBNEquivalence);
            this.groupBoxAnalysisFractionsMethodsEquivalence.Location = new System.Drawing.Point(667, 5);
            this.groupBoxAnalysisFractionsMethodsEquivalence.Margin = new System.Windows.Forms.Padding(5);
            this.groupBoxAnalysisFractionsMethodsEquivalence.Name = "groupBoxAnalysisFractionsMethodsEquivalence";
            this.groupBoxAnalysisFractionsMethodsEquivalence.Size = new System.Drawing.Size(193, 144);
            this.groupBoxAnalysisFractionsMethodsEquivalence.TabIndex = 15;
            this.groupBoxAnalysisFractionsMethodsEquivalence.TabStop = false;
            this.groupBoxAnalysisFractionsMethodsEquivalence.Text = "Analysis equivalence tests fractions";
            // 
            // checkBoxAnalysisMethodELEquivalence
            // 
            this.checkBoxAnalysisMethodELEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodELEquivalence.Checked = true;
            this.checkBoxAnalysisMethodELEquivalence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodELEquivalence.Location = new System.Drawing.Point(20, 24);
            this.checkBoxAnalysisMethodELEquivalence.Name = "checkBoxAnalysisMethodELEquivalence";
            this.checkBoxAnalysisMethodELEquivalence.Size = new System.Drawing.Size(159, 17);
            this.checkBoxAnalysisMethodELEquivalence.TabIndex = 11;
            this.checkBoxAnalysisMethodELEquivalence.Text = "Empirical logit transformation";
            this.checkBoxAnalysisMethodELEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodELEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodELEquivalence_CheckedChanged);
            // 
            // checkBoxAnalysisMethodBBNEquivalence
            // 
            this.checkBoxAnalysisMethodBBNEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodBBNEquivalence.Checked = true;
            this.checkBoxAnalysisMethodBBNEquivalence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAnalysisMethodBBNEquivalence.Location = new System.Drawing.Point(20, 80);
            this.checkBoxAnalysisMethodBBNEquivalence.Name = "checkBoxAnalysisMethodBBNEquivalence";
            this.checkBoxAnalysisMethodBBNEquivalence.Size = new System.Drawing.Size(149, 17);
            this.checkBoxAnalysisMethodBBNEquivalence.TabIndex = 13;
            this.checkBoxAnalysisMethodBBNEquivalence.Text = "Betabinomial with logit link";
            this.checkBoxAnalysisMethodBBNEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodBBNEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodBBNEquivalence_CheckedChanged);
            // 
            // checkBoxAnalysisMethodOBNEquivalence
            // 
            this.checkBoxAnalysisMethodOBNEquivalence.AutoSize = true;
            this.checkBoxAnalysisMethodOBNEquivalence.Location = new System.Drawing.Point(20, 52);
            this.checkBoxAnalysisMethodOBNEquivalence.Name = "checkBoxAnalysisMethodOBNEquivalence";
            this.checkBoxAnalysisMethodOBNEquivalence.Size = new System.Drawing.Size(142, 17);
            this.checkBoxAnalysisMethodOBNEquivalence.TabIndex = 12;
            this.checkBoxAnalysisMethodOBNEquivalence.Text = "Logit with overdispersion";
            this.checkBoxAnalysisMethodOBNEquivalence.UseVisualStyleBackColor = true;
            this.checkBoxAnalysisMethodOBNEquivalence.CheckedChanged += new System.EventHandler(this.checkBoxAnalysisMethodOBNEquivalence_CheckedChanged);
            // 
            // SimulationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.flowLayoutPanelAnalysisMethodsDifference);
            this.Controls.Add(this.buttonRunPowerAnalysis);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "SimulationPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(961, 615);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.groupBoxAnalysisMethodsCountsDifference.ResumeLayout(false);
            this.groupBoxAnalysisMethodsCountsDifference.PerformLayout();
            this.groupBoxAnalysisFractionsMethodsDifference.ResumeLayout(false);
            this.groupBoxAnalysisFractionsMethodsDifference.PerformLayout();
            this.groupBoxAnalysisMethodsNonNegativeDifference.ResumeLayout(false);
            this.groupBoxAnalysisMethodsNonNegativeDifference.PerformLayout();
            this.groupBoxAnalysisMethodsContinuousDifference.ResumeLayout(false);
            this.groupBoxAnalysisMethodsContinuousDifference.PerformLayout();
            this.flowLayoutPanelAnalysisMethodsDifference.ResumeLayout(false);
            this.flowLayoutPanelAnalysisMethodsDifference.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxAnalysisMethodsCountsEquivalence.ResumeLayout(false);
            this.groupBoxAnalysisMethodsCountsEquivalence.PerformLayout();
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.ResumeLayout(false);
            this.groupBoxAnalysisMethodsNonNegativeEquivalence.PerformLayout();
            this.groupBoxAnalysisMethodsContinuousEquivalence.ResumeLayout(false);
            this.groupBoxAnalysisMethodsContinuousEquivalence.PerformLayout();
            this.groupBoxAnalysisFractionsMethodsEquivalence.ResumeLayout(false);
            this.groupBoxAnalysisFractionsMethodsEquivalence.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Label labelNumberOfRatios;
        private System.Windows.Forms.TextBox textBoxSignificanceLevel;
        private System.Windows.Forms.Label labelSignificanceLevel;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodLNDifference;
        private System.Windows.Forms.TextBox textBoxNumberSimulatedDatasets;
        private System.Windows.Forms.Label labelNumberSimulatedDatasets;
        private System.Windows.Forms.ComboBox comboBoxMethodForPowerCalculation;
        private System.Windows.Forms.Label labelMethodForPowerCalculation;
        private System.Windows.Forms.TextBox textBoxNumberOfReplications;
        private System.Windows.Forms.Label labelNumberOfReplications;
        private System.Windows.Forms.TextBox textBoxNumberOfEvaluationPoints;
        private System.Windows.Forms.GroupBox groupBoxAnalysisMethodsCountsDifference;
        private System.Windows.Forms.GroupBox groupBoxAnalysisMethodsNonNegativeDifference;
        private System.Windows.Forms.GroupBox groupBoxAnalysisMethodsContinuousDifference;
        private System.Windows.Forms.GroupBox groupBoxAnalysisFractionsMethodsDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodNBDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodOPDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodSQDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodELDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodBBNDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodOBNDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodLPMDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodGDifference;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodNormalDifference;
        private System.Windows.Forms.TextBox textBoxSeedForRandomNumbers;
        private System.Windows.Forms.Label labelSeedForRandomNumbers;
        private System.Windows.Forms.Button buttonRunPowerAnalysis;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAnalysisMethodsDifference;
        private System.Windows.Forms.CheckBox checkBoxUseWaldTest;
        private System.Windows.Forms.Label labelUseWaldTest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxAnalysisMethodsCountsEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodNBEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodLNEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodOPEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodSQEquivalence;
        private System.Windows.Forms.GroupBox groupBoxAnalysisMethodsNonNegativeEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodLPMEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodGEquivalence;
        private System.Windows.Forms.GroupBox groupBoxAnalysisMethodsContinuousEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodNormalEquivalence;
        private System.Windows.Forms.GroupBox groupBoxAnalysisFractionsMethodsEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodELEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodBBNEquivalence;
        private System.Windows.Forms.CheckBox checkBoxAnalysisMethodOBNEquivalence;
    }
}
