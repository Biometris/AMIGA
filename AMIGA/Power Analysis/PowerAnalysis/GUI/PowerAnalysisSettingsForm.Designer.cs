namespace AmigaPowerAnalysis.GUI {
    partial class PowerAnalysisSettingsForm {
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
            this.textBoxNumberOfRatios = new System.Windows.Forms.TextBox();
            this.labelNumberOfRatios = new System.Windows.Forms.Label();
            this.textBoxSignificanceLevel = new System.Windows.Forms.TextBox();
            this.labelSignificanceLevel = new System.Windows.Forms.Label();
            this.checkBoxMethodForAnalysesLN = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesSQ = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesOP = new System.Windows.Forms.CheckBox();
            this.checkBoxMethodForAnalysesNB = new System.Windows.Forms.CheckBox();
            this.groupBoxMethodsForAnalysis = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxOptions.SuspendLayout();
            this.groupBoxMethodsForAnalysis.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.textBoxSeedForRandomNumbers);
            this.groupBoxOptions.Controls.Add(this.labelSeedForRandomNumbers);
            this.groupBoxOptions.Controls.Add(this.textBoxNumberSimulatedDatasets);
            this.groupBoxOptions.Controls.Add(this.labelNumberSimulatedDatasets);
            this.groupBoxOptions.Controls.Add(this.comboBoxMethodForPowerCalculation);
            this.groupBoxOptions.Controls.Add(this.labelMethodForPowerCalculation);
            this.groupBoxOptions.Controls.Add(this.textBoxNumberOfReplications);
            this.groupBoxOptions.Controls.Add(this.labelNumberOfReplications);
            this.groupBoxOptions.Controls.Add(this.textBoxNumberOfRatios);
            this.groupBoxOptions.Controls.Add(this.labelNumberOfRatios);
            this.groupBoxOptions.Controls.Add(this.textBoxSignificanceLevel);
            this.groupBoxOptions.Controls.Add(this.labelSignificanceLevel);
            this.groupBoxOptions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxOptions.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(851, 162);
            this.groupBoxOptions.TabIndex = 9;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // textBoxSeedForRandomNumbers
            // 
            this.textBoxSeedForRandomNumbers.Location = new System.Drawing.Point(447, 161);
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
            this.textBoxNumberSimulatedDatasets.Location = new System.Drawing.Point(447, 133);
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
            this.comboBoxMethodForPowerCalculation.Location = new System.Drawing.Point(447, 105);
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
            this.labelMethodForPowerCalculation.Size = new System.Drawing.Size(146, 13);
            this.labelMethodForPowerCalculation.TabIndex = 6;
            this.labelMethodForPowerCalculation.Text = "Method for Power Calculation";
            // 
            // textBoxNumberOfReplications
            // 
            this.textBoxNumberOfReplications.Location = new System.Drawing.Point(447, 77);
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
            // textBoxNumberOfRatios
            // 
            this.textBoxNumberOfRatios.Location = new System.Drawing.Point(447, 49);
            this.textBoxNumberOfRatios.Name = "textBoxNumberOfRatios";
            this.textBoxNumberOfRatios.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumberOfRatios.TabIndex = 3;
            this.textBoxNumberOfRatios.Text = "5";
            this.textBoxNumberOfRatios.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberOfRatios_Validating);
            // 
            // labelNumberOfRatios
            // 
            this.labelNumberOfRatios.AutoSize = true;
            this.labelNumberOfRatios.Location = new System.Drawing.Point(20, 52);
            this.labelNumberOfRatios.Name = "labelNumberOfRatios";
            this.labelNumberOfRatios.Size = new System.Drawing.Size(395, 13);
            this.labelNumberOfRatios.TabIndex = 2;
            this.labelNumberOfRatios.Text = "Number of Ratios in between the limits of concern for which to calculate the powe" +
    "r";
            // 
            // textBoxSignificanceLevel
            // 
            this.textBoxSignificanceLevel.Location = new System.Drawing.Point(447, 21);
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
            this.checkBoxMethodForAnalysesLN.Size = new System.Drawing.Size(142, 17);
            this.checkBoxMethodForAnalysesLN.TabIndex = 11;
            this.checkBoxMethodForAnalysesLN.Text = "Log(y + 1) transformation";
            this.checkBoxMethodForAnalysesLN.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesLN.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesLN_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesSQ
            // 
            this.checkBoxMethodForAnalysesSQ.AutoSize = true;
            this.checkBoxMethodForAnalysesSQ.Location = new System.Drawing.Point(20, 52);
            this.checkBoxMethodForAnalysesSQ.Name = "checkBoxMethodForAnalysesSQ";
            this.checkBoxMethodForAnalysesSQ.Size = new System.Drawing.Size(156, 17);
            this.checkBoxMethodForAnalysesSQ.TabIndex = 12;
            this.checkBoxMethodForAnalysesSQ.Text = "Squared root transformation";
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
            this.checkBoxMethodForAnalysesOP.Text = "Log linear model with overdispersion";
            this.checkBoxMethodForAnalysesOP.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesOP.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesOP_CheckedChanged);
            // 
            // checkBoxMethodForAnalysesNB
            // 
            this.checkBoxMethodForAnalysesNB.AutoSize = true;
            this.checkBoxMethodForAnalysesNB.Location = new System.Drawing.Point(20, 108);
            this.checkBoxMethodForAnalysesNB.Name = "checkBoxMethodForAnalysesNB";
            this.checkBoxMethodForAnalysesNB.Size = new System.Drawing.Size(199, 17);
            this.checkBoxMethodForAnalysesNB.TabIndex = 14;
            this.checkBoxMethodForAnalysesNB.Text = "Negative binomial model with log link";
            this.checkBoxMethodForAnalysesNB.UseVisualStyleBackColor = true;
            this.checkBoxMethodForAnalysesNB.CheckedChanged += new System.EventHandler(this.checkBoxMethodForAnalysesNB_CheckedChanged);
            // 
            // groupBoxMethodsForAnalysis
            // 
            this.groupBoxMethodsForAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMethodsForAnalysis.Controls.Add(this.checkBoxMethodForAnalysesNB);
            this.groupBoxMethodsForAnalysis.Controls.Add(this.checkBoxMethodForAnalysesLN);
            this.groupBoxMethodsForAnalysis.Controls.Add(this.checkBoxMethodForAnalysesOP);
            this.groupBoxMethodsForAnalysis.Controls.Add(this.checkBoxMethodForAnalysesSQ);
            this.groupBoxMethodsForAnalysis.Location = new System.Drawing.Point(0, 167);
            this.groupBoxMethodsForAnalysis.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.groupBoxMethodsForAnalysis.Name = "groupBoxMethodsForAnalysis";
            this.groupBoxMethodsForAnalysis.Size = new System.Drawing.Size(851, 138);
            this.groupBoxMethodsForAnalysis.TabIndex = 10;
            this.groupBoxMethodsForAnalysis.TabStop = false;
            this.groupBoxMethodsForAnalysis.Text = "Methods for Analysis";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.Controls.Add(this.groupBoxOptions);
            this.flowLayoutPanel.Controls.Add(this.groupBoxMethodsForAnalysis);
            this.flowLayoutPanel.Location = new System.Drawing.Point(13, 13);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(848, 477);
            this.flowLayoutPanel.TabIndex = 11;
            // 
            // PowerAnalysisSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "PowerAnalysisSettingsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.groupBoxMethodsForAnalysis.ResumeLayout(false);
            this.groupBoxMethodsForAnalysis.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox textBoxNumberOfRatios;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesNB;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesOP;
        private System.Windows.Forms.CheckBox checkBoxMethodForAnalysesSQ;
        private System.Windows.Forms.GroupBox groupBoxMethodsForAnalysis;
        private System.Windows.Forms.TextBox textBoxSeedForRandomNumbers;
        private System.Windows.Forms.Label labelSeedForRandomNumbers;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}
