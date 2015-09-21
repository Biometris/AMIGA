namespace AmigaPowerAnalysis.GUI {
    partial class AnalysisTemplatePanel {
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
            this.panelModelInfo = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxAnalysisMethodTypeDifferenceTests = new System.Windows.Forms.ComboBox();
            this.textBoxGeneratedAnalysisScript = new System.Windows.Forms.TextBox();
            this.flowLayoutPanelReport = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonGenerateDataTemplate = new System.Windows.Forms.Button();
            this.textBoxNumberOfReplicates = new System.Windows.Forms.TextBox();
            this.labelNumberOfReplicates = new System.Windows.Forms.Label();
            this.dataGridViewComparisons = new System.Windows.Forms.DataGridView();
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            this.comboBoxAnalysisMethodTypeEquivalenceTests = new System.Windows.Forms.ComboBox();
            this.panelModelInfo.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanelReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelModelInfo
            // 
            this.panelModelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModelInfo.Controls.Add(this.flowLayoutPanel1);
            this.panelModelInfo.Controls.Add(this.textBoxGeneratedAnalysisScript);
            this.panelModelInfo.Controls.Add(this.flowLayoutPanelReport);
            this.panelModelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelModelInfo.Name = "panelModelInfo";
            this.panelModelInfo.Padding = new System.Windows.Forms.Padding(5);
            this.panelModelInfo.Size = new System.Drawing.Size(610, 477);
            this.panelModelInfo.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.comboBoxAnalysisMethodTypeDifferenceTests);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxAnalysisMethodTypeEquivalenceTests);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 412);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(598, 29);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // comboBoxAnalysisMethodTypeDifferenceTests
            // 
            this.comboBoxAnalysisMethodTypeDifferenceTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnalysisMethodTypeDifferenceTests.FormattingEnabled = true;
            this.comboBoxAnalysisMethodTypeDifferenceTests.Location = new System.Drawing.Point(413, 3);
            this.comboBoxAnalysisMethodTypeDifferenceTests.Name = "comboBoxAnalysisMethodTypeDifferenceTests";
            this.comboBoxAnalysisMethodTypeDifferenceTests.Size = new System.Drawing.Size(182, 21);
            this.comboBoxAnalysisMethodTypeDifferenceTests.TabIndex = 12;
            this.comboBoxAnalysisMethodTypeDifferenceTests.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnalysisMethodType_SelectedIndexChanged);
            // 
            // textBoxGeneratedAnalysisScript
            // 
            this.textBoxGeneratedAnalysisScript.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxGeneratedAnalysisScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxGeneratedAnalysisScript.Location = new System.Drawing.Point(5, 5);
            this.textBoxGeneratedAnalysisScript.Multiline = true;
            this.textBoxGeneratedAnalysisScript.Name = "textBoxGeneratedAnalysisScript";
            this.textBoxGeneratedAnalysisScript.ReadOnly = true;
            this.textBoxGeneratedAnalysisScript.Size = new System.Drawing.Size(598, 436);
            this.textBoxGeneratedAnalysisScript.TabIndex = 19;
            // 
            // flowLayoutPanelReport
            // 
            this.flowLayoutPanelReport.Controls.Add(this.buttonGenerateDataTemplate);
            this.flowLayoutPanelReport.Controls.Add(this.textBoxNumberOfReplicates);
            this.flowLayoutPanelReport.Controls.Add(this.labelNumberOfReplicates);
            this.flowLayoutPanelReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelReport.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelReport.Location = new System.Drawing.Point(5, 441);
            this.flowLayoutPanelReport.Name = "flowLayoutPanelReport";
            this.flowLayoutPanelReport.Size = new System.Drawing.Size(598, 29);
            this.flowLayoutPanelReport.TabIndex = 18;
            // 
            // buttonGenerateDataTemplate
            // 
            this.buttonGenerateDataTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateDataTemplate.Location = new System.Drawing.Point(452, 3);
            this.buttonGenerateDataTemplate.Name = "buttonGenerateDataTemplate";
            this.buttonGenerateDataTemplate.Size = new System.Drawing.Size(143, 23);
            this.buttonGenerateDataTemplate.TabIndex = 0;
            this.buttonGenerateDataTemplate.Text = "Export data template";
            this.buttonGenerateDataTemplate.UseVisualStyleBackColor = true;
            this.buttonGenerateDataTemplate.Click += new System.EventHandler(this.buttonGenerateDataTemplate_Click);
            // 
            // textBoxNumberOfReplicates
            // 
            this.textBoxNumberOfReplicates.Location = new System.Drawing.Point(404, 3);
            this.textBoxNumberOfReplicates.Name = "textBoxNumberOfReplicates";
            this.textBoxNumberOfReplicates.Size = new System.Drawing.Size(42, 20);
            this.textBoxNumberOfReplicates.TabIndex = 11;
            this.textBoxNumberOfReplicates.Text = "2";
            this.textBoxNumberOfReplicates.TextChanged += new System.EventHandler(this.textBoxNumberOfReplicates_TextChanged);
            // 
            // labelNumberOfReplicates
            // 
            this.labelNumberOfReplicates.AutoSize = true;
            this.labelNumberOfReplicates.Location = new System.Drawing.Point(291, 8);
            this.labelNumberOfReplicates.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.labelNumberOfReplicates.Name = "labelNumberOfReplicates";
            this.labelNumberOfReplicates.Size = new System.Drawing.Size(107, 13);
            this.labelNumberOfReplicates.TabIndex = 10;
            this.labelNumberOfReplicates.Text = "Number of replicates:";
            // 
            // dataGridViewComparisons
            // 
            this.dataGridViewComparisons.AllowUserToAddRows = false;
            this.dataGridViewComparisons.AllowUserToDeleteRows = false;
            this.dataGridViewComparisons.AllowUserToResizeRows = false;
            this.dataGridViewComparisons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewComparisons.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewComparisons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewComparisons.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewComparisons.MultiSelect = false;
            this.dataGridViewComparisons.Name = "dataGridViewComparisons";
            this.dataGridViewComparisons.ReadOnly = true;
            this.dataGridViewComparisons.RowHeadersVisible = false;
            this.dataGridViewComparisons.RowHeadersWidth = 24;
            this.dataGridViewComparisons.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewComparisons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComparisons.Size = new System.Drawing.Size(283, 477);
            this.dataGridViewComparisons.TabIndex = 3;
            this.dataGridViewComparisons.SelectionChanged += new System.EventHandler(this.dataGridViewComparisons_SelectionChanged);
            // 
            // splitContainerComparisons
            // 
            this.splitContainerComparisons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerComparisons.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerComparisons.Location = new System.Drawing.Point(13, 13);
            this.splitContainerComparisons.Name = "splitContainerComparisons";
            // 
            // splitContainerComparisons.Panel1
            // 
            this.splitContainerComparisons.Panel1.Controls.Add(this.dataGridViewComparisons);
            // 
            // splitContainerComparisons.Panel2
            // 
            this.splitContainerComparisons.Panel2.Controls.Add(this.panelModelInfo);
            this.splitContainerComparisons.Size = new System.Drawing.Size(897, 477);
            this.splitContainerComparisons.SplitterDistance = 283;
            this.splitContainerComparisons.TabIndex = 9;
            // 
            // comboBoxAnalysisMethodTypeEquivalenceTests
            // 
            this.comboBoxAnalysisMethodTypeEquivalenceTests.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnalysisMethodTypeEquivalenceTests.FormattingEnabled = true;
            this.comboBoxAnalysisMethodTypeEquivalenceTests.Location = new System.Drawing.Point(225, 3);
            this.comboBoxAnalysisMethodTypeEquivalenceTests.Name = "comboBoxAnalysisMethodTypeEquivalenceTests";
            this.comboBoxAnalysisMethodTypeEquivalenceTests.Size = new System.Drawing.Size(182, 21);
            this.comboBoxAnalysisMethodTypeEquivalenceTests.TabIndex = 13;
            // 
            // AnalysisTemplatePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AnalysisTemplatePanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(923, 503);
            this.panelModelInfo.ResumeLayout(false);
            this.panelModelInfo.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanelReport.ResumeLayout(false);
            this.flowLayoutPanelReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelModelInfo;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.Button buttonGenerateDataTemplate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelReport;
        private System.Windows.Forms.TextBox textBoxGeneratedAnalysisScript;
        private System.Windows.Forms.TextBox textBoxNumberOfReplicates;
        private System.Windows.Forms.Label labelNumberOfReplicates;
        private System.Windows.Forms.ComboBox comboBoxAnalysisMethodTypeDifferenceTests;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxAnalysisMethodTypeEquivalenceTests;

    }
}
