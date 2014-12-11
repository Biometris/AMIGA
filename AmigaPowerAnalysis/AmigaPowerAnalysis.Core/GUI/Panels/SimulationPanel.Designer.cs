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
            this.checkBoxMethodForAnalysesLN = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesSQ = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesOP = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesNB = new System.Windows.Forms.CheckBox();
            this.groupBoxMethodsForAnalysisOfCounts = new System.Windows.Forms.GroupBox();
            this.buttonRunPowerAnalysis = new System.Windows.Forms.Button();
            this.groupBoxMethodsForAnalysisOfFractions = new System.Windows.Forms.GroupBox();
            this.checkBoxMethodForAnalysesEL = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesBBN = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesOBN = new System.Windows.Forms.CheckBox();
            this.groupBoxMethodsForAnalysisOfNonNegative = new System.Windows.Forms.GroupBox();
            this.checkBoxMethodForAnalysesLPM = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesG = new System.Windows.Forms.CheckBox();
            this.groupBoxMethodsForAnalysisOfContinuous = new System.Windows.Forms.GroupBox();
            this.checkBoxMethodForAnalysesN = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions.SuspendLayout();
            this.groupBoxMethodsForAnalysisOfCounts.SuspendLayout();
            this.groupBoxMethodsForAnalysisOfFractions.SuspendLayout();
            this.groupBoxMethodsForAnalysisOfNonNegative.SuspendLayout();
            this.groupBoxMethodsForAnalysisOfContinuous.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.AutoSize = true;
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
            this.groupBoxOptions.Size = new System.Drawing.Size(937, 200);
            this.groupBoxOptions.TabIndex = 9;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // textBoxSeedForRandomNumbers
            // 
            this.textBoxSeedForRandomNumbers.Location = new System.Drawing.Point(553, 161);
            this.textBoxSeedForRandomNumbers.Name = "textBoxSeedForRandomNumbers";
            this.textBoxSeedForRandomNumbers.Size = new System.Drawing.Size(100, 20);
            this.textBoxSeedForRandomNumbers.TabIndex = 11;
            this.textBoxSeedForRandomNumbers.Text = "123456";
            this.textBoxSeedForRandomNumbers.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSeedForRandomNumbers_Validating);
            // 
            // labelSeedForRandomNumbers
            // 
            this.labelSeedForRandomNumbers.AutoSize = true;
            this.labelSeedForRandomNumbers.Location = new System.Drawing.Point(20, 164);
            this.labelSeedForRandomNumbers.Name = "labelSeedForRandomNumbers";
            this.labelSeedForRandomNumbers.Size = new System.Drawing.Size(365, 13);
            this.labelSeedForRandomNumbers.TabIndex = 10;
            this.labelSeedForRandomNumbers.Text = "Seed for random number generator (non-negative value uses computer time)";
            // 
            // textBoxNumberSimulatedDatasets
            // 
            this.textBoxNumberSimulatedDatasets.Location = new System.Drawing.Point(553, 133);
            this.textBoxNumberSimulatedDatasets.Name = "textBoxNumberSimulatedDatasets";
            this.textBoxNumberSimulatedDatasets.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumberSimulatedDatasets.TabIndex = 9;
            this.textBoxNumberSimulatedDatasets.Text = "100";
            this.textBoxNumberSimulatedDatasets.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberSimulatedDatasets_Validating);
            // 
            // labelNumberSimulatedDatasets
            // 
            this.labelNumberSimulatedDatasets.AutoSize = true;
            this.labelNumberSimulatedDatasets.Location = new System.Drawing.Point(20, 136);
            this.labelNumberSimulatedDatasets.Name = "labelNumberSimulatedDatasets";
            this.labelNumberSimulatedDatasets.Size = new System.Drawing.Size(246, 13);
            this.labelNumberSimulatedDatasets.TabIndex = 8;
            this.labelNumberSimulatedDatasets.Text = "Number of simulated datasets for Method=Simulate";
            // 
            // comboBoxMethodForPowerCalculation
            // 
            this.comboBoxMethodForPowerCalculation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMethodForPowerCalculation.FormattingEnabled = true;
            this.comboBoxMethodForPowerCalculation.Location = new System.Drawing.Point(553, 105);
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
            this.labelMethodForPowerCalculation.Size = new System.Drawing.Size(350, 13);
            this.labelMethodForPowerCalculation.TabIndex = 6;
            this.labelMethodForPowerCalculation.Text = "Method for Power Calculation (currently only Approximate is implemented)";
            // 
            // textBoxNumberOfReplications
            // 
            this.textBoxNumberOfReplications.Location = new System.Drawing.Point(553, 77);
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
            this.labelNumberOfReplications.Size = new System.Drawing.Size(338, 13);
            this.labelNumberOfReplications.TabIndex = 4;
            this.labelNumberOfReplications.Text = "Number of Replications for which to calculate the power (list of values)";
            // 
            // textBoxNumberOfEvaluationPoints
            // 
            this.textBoxNumberOfEvaluationPoints.Location = new System.Drawing.Point(553, 49);
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
            // checkBoxMethodForAnalysesLN
            // 
            this.checkBoxMethodForAnalysesLN.AutoSize = true;
            this.checkBoxMethodForAnalysesLN.Checked = true;
            this.checkBoxMethodForAnalysesLN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMethodForAnalysesLN.Location = new System.Drawing.Point(20, 24);
            this.checkBoxMethodForAnalysesLN.Name = "checkBoxMethodForAnalysesLN";
            this.checkBoxMethodForAnalysesLN.Size = new System.Drawing.Size(139, 17);
            this.checkBoxMethodForAnalysesLN.TabIndex = 11;
            this.checkBoxMethodForAnalysesLN.Text = "Log(N+1) transformation";
            this.checkBoxMethodForAnalysesLN.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesLN.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesLN_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesSQ
            // 
            this.checkBoxMethodForAnalysesSQ.AutoSize = true;
            this.checkBoxMethodForAnalysesSQ.Location = new System.Drawing.Point(20, 52);
            this.checkBoxMethodForAnalysesSQ.Name = "checkBoxMethodForAnalysesSQ";
            this.checkBoxMethodForAnalysesSQ.Size = new System.Drawing.Size(155, 17);
            this.checkBoxMethodForAnalysesSQ.TabIndex = 12;
            this.checkBoxMethodForAnalysesSQ.Text = "Square Root transformation";
            this.checkBoxMethodForAnalysesSQ.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesSQ.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesSQ_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesOP
            // 
            this.checkBoxMethodForAnalysesOP.AutoSize = true;
            this.checkBoxMethodForAnalysesOP.Checked = true;
            this.checkBoxMethodForAnalysesOP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMethodForAnalysesOP.Location = new System.Drawing.Point(20, 80);
            this.checkBoxMethodForAnalysesOP.Name = "checkBoxMethodForAnalysesOP";
            this.checkBoxMethodForAnalysesOP.Size = new System.Drawing.Size(196, 17);
            this.checkBoxMethodForAnalysesOP.TabIndex = 13;
            this.checkBoxMethodForAnalysesOP.Text = "Log-linear model with overdispersion";
            this.checkBoxMethodForAnalysesOP.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesOP.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesOP_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesNB
            // 
            this.checkBoxMethodForAnalysesNB.AutoSize = true;
            this.checkBoxMethodForAnalysesNB.Location = new System.Drawing.Point(20, 108);
            this.checkBoxMethodForAnalysesNB.Name = "checkBoxMethodForAnalysesNB";
            this.checkBoxMethodForAnalysesNB.Size = new System.Drawing.Size(200, 17);
            this.checkBoxMethodForAnalysesNB.TabIndex = 14;
            this.checkBoxMethodForAnalysesNB.Text = "Negative Binomial model with log link";
            this.checkBoxMethodForAnalysesNB.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesNB.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesNB_CheckedChanged);
            // 
            // groupBoxMethodsForAnalysisOfCounts
            // 
            this.groupBoxMethodsForAnalysisOfCounts.AutoSize = true;
            this.groupBoxMethodsForAnalysisOfCounts.Controls.Add(this.checkBoxMethodForAnalysesNB);
            this.groupBoxMethodsForAnalysisOfCounts.Controls.Add(this.checkBoxMethodForAnalysesLN);
            this.groupBoxMethodsForAnalysisOfCounts.Controls.Add(this.checkBoxMethodForAnalysesOP);
            this.groupBoxMethodsForAnalysisOfCounts.Controls.Add(this.checkBoxMethodForAnalysesSQ);
            this.groupBoxMethodsForAnalysisOfCounts.Location = new System.Drawing.Point(15, 261);
            this.groupBoxMethodsForAnalysisOfCounts.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.groupBoxMethodsForAnalysisOfCounts.Name = "groupBoxMethodsForAnalysisOfCounts";
            this.groupBoxMethodsForAnalysisOfCounts.Size = new System.Drawing.Size(226, 152);
            this.groupBoxMethodsForAnalysisOfCounts.TabIndex = 10;
            this.groupBoxMethodsForAnalysisOfCounts.TabStop = false;
            this.groupBoxMethodsForAnalysisOfCounts.Text = "Methods for Analysis of counts";
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
            // groupBoxMethodsForAnalysisOfFractions
            // 
            this.groupBoxMethodsForAnalysisOfFractions.AutoSize = true;
            this.groupBoxMethodsForAnalysisOfFractions.Controls.Add(this.checkBoxMethodForAnalysesEL);
            this.groupBoxMethodsForAnalysisOfFractions.Controls.Add(this.checkBoxMethodForAnalysesBBN);
            this.groupBoxMethodsForAnalysisOfFractions.Controls.Add(this.checkBoxMethodForAnalysesOBN);
            this.groupBoxMethodsForAnalysisOfFractions.Location = new System.Drawing.Point(250, 261);
            this.groupBoxMethodsForAnalysisOfFractions.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.groupBoxMethodsForAnalysisOfFractions.Name = "groupBoxMethodsForAnalysisOfFractions";
            this.groupBoxMethodsForAnalysisOfFractions.Size = new System.Drawing.Size(216, 152);
            this.groupBoxMethodsForAnalysisOfFractions.TabIndex = 15;
            this.groupBoxMethodsForAnalysisOfFractions.TabStop = false;
            this.groupBoxMethodsForAnalysisOfFractions.Text = "Methods for Analysis of fractions";
            // 
            // checkBoxMethodForAnalysesEL
            // 
            this.checkBoxMethodForAnalysesEL.AutoSize = true;
            this.checkBoxMethodForAnalysesEL.Checked = true;
            this.checkBoxMethodForAnalysesEL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMethodForAnalysesEL.Location = new System.Drawing.Point(20, 24);
            this.checkBoxMethodForAnalysesEL.Name = "checkBoxMethodForAnalysesEL";
            this.checkBoxMethodForAnalysesEL.Size = new System.Drawing.Size(159, 17);
            this.checkBoxMethodForAnalysesEL.TabIndex = 11;
            this.checkBoxMethodForAnalysesEL.Text = "Empirical logit transformation";
            this.checkBoxMethodForAnalysesEL.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesEL.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesEL_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesBBN
            // 
            this.checkBoxMethodForAnalysesBBN.AutoSize = true;
            this.checkBoxMethodForAnalysesBBN.Checked = true;
            this.checkBoxMethodForAnalysesBBN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMethodForAnalysesBBN.Location = new System.Drawing.Point(20, 80);
            this.checkBoxMethodForAnalysesBBN.Name = "checkBoxMethodForAnalysesBBN";
            this.checkBoxMethodForAnalysesBBN.Size = new System.Drawing.Size(149, 17);
            this.checkBoxMethodForAnalysesBBN.TabIndex = 13;
            this.checkBoxMethodForAnalysesBBN.Text = "Betabinomial with logit link";
            this.checkBoxMethodForAnalysesBBN.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesBBN.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesBBN_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesOBN
            // 
            this.checkBoxMethodForAnalysesOBN.AutoSize = true;
            this.checkBoxMethodForAnalysesOBN.Location = new System.Drawing.Point(20, 52);
            this.checkBoxMethodForAnalysesOBN.Name = "checkBoxMethodForAnalysesOBN";
            this.checkBoxMethodForAnalysesOBN.Size = new System.Drawing.Size(142, 17);
            this.checkBoxMethodForAnalysesOBN.TabIndex = 12;
            this.checkBoxMethodForAnalysesOBN.Text = "Logit with overdispersion";
            this.checkBoxMethodForAnalysesOBN.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesOBN.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesOBN_CheckedChanged);
            // 
            // groupBoxMethodsForAnalysisOfNonNegative
            // 
            this.groupBoxMethodsForAnalysisOfNonNegative.AutoSize = true;
            this.groupBoxMethodsForAnalysisOfNonNegative.Controls.Add(this.checkBoxMethodForAnalysesLPM);
            this.groupBoxMethodsForAnalysisOfNonNegative.Controls.Add(this.checkBoxMethodForAnalysesG);
            this.groupBoxMethodsForAnalysisOfNonNegative.Location = new System.Drawing.Point(476, 261);
            this.groupBoxMethodsForAnalysisOfNonNegative.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.groupBoxMethodsForAnalysisOfNonNegative.Name = "groupBoxMethodsForAnalysisOfNonNegative";
            this.groupBoxMethodsForAnalysisOfNonNegative.Size = new System.Drawing.Size(214, 152);
            this.groupBoxMethodsForAnalysisOfNonNegative.TabIndex = 16;
            this.groupBoxMethodsForAnalysisOfNonNegative.TabStop = false;
            this.groupBoxMethodsForAnalysisOfNonNegative.Text = "Methods for Analysis of non-negative";
            // 
            // checkBoxMethodForAnalysesLPM
            // 
            this.checkBoxMethodForAnalysesLPM.AutoSize = true;
            this.checkBoxMethodForAnalysesLPM.Checked = true;
            this.checkBoxMethodForAnalysesLPM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMethodForAnalysesLPM.Location = new System.Drawing.Point(20, 24);
            this.checkBoxMethodForAnalysesLPM.Name = "checkBoxMethodForAnalysesLPM";
            this.checkBoxMethodForAnalysesLPM.Size = new System.Drawing.Size(138, 17);
            this.checkBoxMethodForAnalysesLPM.TabIndex = 11;
            this.checkBoxMethodForAnalysesLPM.Text = "Log(x+m) transformation";
            this.checkBoxMethodForAnalysesLPM.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesLPM.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesLPM_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesG
            // 
            this.checkBoxMethodForAnalysesG.AutoSize = true;
            this.checkBoxMethodForAnalysesG.Location = new System.Drawing.Point(20, 52);
            this.checkBoxMethodForAnalysesG.Name = "checkBoxMethodForAnalysesG";
            this.checkBoxMethodForAnalysesG.Size = new System.Drawing.Size(120, 17);
            this.checkBoxMethodForAnalysesG.TabIndex = 12;
            this.checkBoxMethodForAnalysesG.Text = "Gamma with log link";
            this.checkBoxMethodForAnalysesG.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesG.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesG_CheckedChanged);
            // 
            // groupBoxMethodsForAnalysisOfContinuous
            // 
            this.groupBoxMethodsForAnalysisOfContinuous.AutoSize = true;
            this.groupBoxMethodsForAnalysisOfContinuous.Controls.Add(this.checkBoxMethodForAnalysesN);
            this.groupBoxMethodsForAnalysisOfContinuous.Location = new System.Drawing.Point(700, 261);
            this.groupBoxMethodsForAnalysisOfContinuous.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.groupBoxMethodsForAnalysisOfContinuous.Name = "groupBoxMethodsForAnalysisOfContinuous";
            this.groupBoxMethodsForAnalysisOfContinuous.Size = new System.Drawing.Size(214, 152);
            this.groupBoxMethodsForAnalysisOfContinuous.TabIndex = 17;
            this.groupBoxMethodsForAnalysisOfContinuous.TabStop = false;
            this.groupBoxMethodsForAnalysisOfContinuous.Text = "Methods for Analysis of continuous";
            // 
            // checkBoxMethodForAnalysesN
            // 
            this.checkBoxMethodForAnalysesN.AutoSize = true;
            this.checkBoxMethodForAnalysesN.Checked = true;
            this.checkBoxMethodForAnalysesN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMethodForAnalysesN.Location = new System.Drawing.Point(20, 24);
            this.checkBoxMethodForAnalysesN.Name = "checkBoxMethodForAnalysesN";
            this.checkBoxMethodForAnalysesN.Size = new System.Drawing.Size(90, 17);
            this.checkBoxMethodForAnalysesN.TabIndex = 11;
            this.checkBoxMethodForAnalysesN.Text = "Normal model";
            this.checkBoxMethodForAnalysesN.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesN.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesN_CheckedChanged);
            // 
            // SimulationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxMethodsForAnalysisOfContinuous);
            this.Controls.Add(this.groupBoxMethodsForAnalysisOfNonNegative);
            this.Controls.Add(this.groupBoxMethodsForAnalysisOfFractions);
            this.Controls.Add(this.buttonRunPowerAnalysis);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.groupBoxMethodsForAnalysisOfCounts);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "SimulationPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(958, 423);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.groupBoxMethodsForAnalysisOfCounts.ResumeLayout(false);
            this.groupBoxMethodsForAnalysisOfCounts.PerformLayout();
            this.groupBoxMethodsForAnalysisOfFractions.ResumeLayout(false);
            this.groupBoxMethodsForAnalysisOfFractions.PerformLayout();
            this.groupBoxMethodsForAnalysisOfNonNegative.ResumeLayout(false);
            this.groupBoxMethodsForAnalysisOfNonNegative.PerformLayout();
            this.groupBoxMethodsForAnalysisOfContinuous.ResumeLayout(false);
            this.groupBoxMethodsForAnalysisOfContinuous.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.Label labelNumberOfRatios;
        private System.Windows.Forms.TextBox textBoxSignificanceLevel;
        private System.Windows.Forms.Label labelSignificanceLevel;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesLN;
        private System.Windows.Forms.TextBox textBoxNumberSimulatedDatasets;
        private System.Windows.Forms.Label labelNumberSimulatedDatasets;
        private System.Windows.Forms.ComboBox comboBoxMethodForPowerCalculation;
        private System.Windows.Forms.Label labelMethodForPowerCalculation;
        private System.Windows.Forms.TextBox textBoxNumberOfReplications;
        private System.Windows.Forms.Label labelNumberOfReplications;
        private System.Windows.Forms.TextBox textBoxNumberOfEvaluationPoints;
        private System.Windows.Forms.GroupBox groupBoxMethodsForAnalysisOfCounts;
        private System.Windows.Forms.GroupBox groupBoxMethodsForAnalysisOfNonNegative;
        private System.Windows.Forms.GroupBox groupBoxMethodsForAnalysisOfContinuous;
        private System.Windows.Forms.GroupBox groupBoxMethodsForAnalysisOfFractions;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesNB;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesOP;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesSQ;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesEL;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesBBN;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesOBN;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesLPM;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesG;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesN;
        private System.Windows.Forms.TextBox textBoxSeedForRandomNumbers;
        private System.Windows.Forms.Label labelSeedForRandomNumbers;
        private System.Windows.Forms.Button buttonRunPowerAnalysis;
    }
}
