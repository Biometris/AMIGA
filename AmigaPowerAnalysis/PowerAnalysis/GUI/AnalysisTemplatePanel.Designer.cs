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
            this.buttonGenerateDataTemplate = new System.Windows.Forms.Button();
            this.dataGridViewComparisons = new System.Windows.Forms.DataGridView();
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelReport = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxGeneratedAnalysisScript = new System.Windows.Forms.TextBox();
            this.panelModelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            this.flowLayoutPanelReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelModelInfo
            // 
            this.panelModelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModelInfo.Controls.Add(this.textBoxGeneratedAnalysisScript);
            this.panelModelInfo.Controls.Add(this.flowLayoutPanelReport);
            this.panelModelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelModelInfo.Name = "panelModelInfo";
            this.panelModelInfo.Padding = new System.Windows.Forms.Padding(5);
            this.panelModelInfo.Size = new System.Drawing.Size(561, 477);
            this.panelModelInfo.TabIndex = 0;
            // 
            // buttonGenerateDataTemplate
            // 
            this.buttonGenerateDataTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateDataTemplate.Location = new System.Drawing.Point(403, 3);
            this.buttonGenerateDataTemplate.Name = "buttonGenerateDataTemplate";
            this.buttonGenerateDataTemplate.Size = new System.Drawing.Size(143, 23);
            this.buttonGenerateDataTemplate.TabIndex = 0;
            this.buttonGenerateDataTemplate.Text = "Generate data template";
            this.buttonGenerateDataTemplate.UseVisualStyleBackColor = true;
            this.buttonGenerateDataTemplate.Click += new System.EventHandler(this.buttonGenerateDataTemplate_Click);
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
            this.splitContainerComparisons.Size = new System.Drawing.Size(848, 477);
            this.splitContainerComparisons.SplitterDistance = 283;
            this.splitContainerComparisons.TabIndex = 9;
            // 
            // flowLayoutPanelReport
            // 
            this.flowLayoutPanelReport.Controls.Add(this.buttonGenerateDataTemplate);
            this.flowLayoutPanelReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelReport.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelReport.Location = new System.Drawing.Point(5, 441);
            this.flowLayoutPanelReport.Name = "flowLayoutPanelReport";
            this.flowLayoutPanelReport.Size = new System.Drawing.Size(549, 29);
            this.flowLayoutPanelReport.TabIndex = 18;
            // 
            // textBoxGeneratedAnalysisScript
            // 
            this.textBoxGeneratedAnalysisScript.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxGeneratedAnalysisScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxGeneratedAnalysisScript.Location = new System.Drawing.Point(5, 5);
            this.textBoxGeneratedAnalysisScript.Multiline = true;
            this.textBoxGeneratedAnalysisScript.Name = "textBoxGeneratedAnalysisScript";
            this.textBoxGeneratedAnalysisScript.ReadOnly = true;
            this.textBoxGeneratedAnalysisScript.Size = new System.Drawing.Size(549, 436);
            this.textBoxGeneratedAnalysisScript.TabIndex = 19;
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
            this.Size = new System.Drawing.Size(874, 503);
            this.panelModelInfo.ResumeLayout(false);
            this.panelModelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            this.flowLayoutPanelReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelModelInfo;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.Button buttonGenerateDataTemplate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelReport;
        private System.Windows.Forms.TextBox textBoxGeneratedAnalysisScript;

    }
}
