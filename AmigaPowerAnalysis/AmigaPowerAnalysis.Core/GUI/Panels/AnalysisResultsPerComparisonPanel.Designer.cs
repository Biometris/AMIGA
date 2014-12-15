namespace AmigaPowerAnalysis.GUI {
    partial class AnalysisResultsPerComparisonPanel {
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
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            this.dataGridViewComparisons = new System.Windows.Forms.DataGridView();
            this.panelResultPlots = new System.Windows.Forms.Panel();
            this.flowLayoutPanelReport = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonShowSettings = new System.Windows.Forms.Button();
            this.buttonShowInputData = new System.Windows.Forms.Button();
            this.plotView = new OxyPlot.WindowsForms.PlotView();
            this.plotTypePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxAnalysisType = new System.Windows.Forms.ComboBox();
            this.comboBoxTestType = new System.Windows.Forms.ComboBox();
            this.comboBoxAnalysisPlotTypes = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanelComparisonInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.labelBlockSize = new System.Windows.Forms.Label();
            this.labelPlotsPerBlock = new System.Windows.Forms.Label();
            this.labelLocLower = new System.Windows.Forms.Label();
            this.labelLocLowerValue = new System.Windows.Forms.Label();
            this.labelLocUpper = new System.Windows.Forms.Label();
            this.labelLocUpperValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            this.panelResultPlots.SuspendLayout();
            this.flowLayoutPanelReport.SuspendLayout();
            this.plotTypePanel.SuspendLayout();
            this.flowLayoutPanelComparisonInfo.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainerComparisons.Panel2.Controls.Add(this.panelResultPlots);
            this.splitContainerComparisons.Size = new System.Drawing.Size(848, 477);
            this.splitContainerComparisons.SplitterDistance = 283;
            this.splitContainerComparisons.TabIndex = 9;
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
            // panelResultPlots
            // 
            this.panelResultPlots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResultPlots.Controls.Add(this.plotView);
            this.panelResultPlots.Controls.Add(this.plotTypePanel);
            this.panelResultPlots.Controls.Add(this.flowLayoutPanelComparisonInfo);
            this.panelResultPlots.Controls.Add(this.flowLayoutPanelReport);
            this.panelResultPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResultPlots.Location = new System.Drawing.Point(0, 0);
            this.panelResultPlots.Name = "panelResultPlots";
            this.panelResultPlots.Padding = new System.Windows.Forms.Padding(5);
            this.panelResultPlots.Size = new System.Drawing.Size(561, 477);
            this.panelResultPlots.TabIndex = 0;
            // 
            // flowLayoutPanelReport
            // 
            this.flowLayoutPanelReport.Controls.Add(this.buttonShowSettings);
            this.flowLayoutPanelReport.Controls.Add(this.buttonShowInputData);
            this.flowLayoutPanelReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelReport.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelReport.Location = new System.Drawing.Point(5, 441);
            this.flowLayoutPanelReport.Name = "flowLayoutPanelReport";
            this.flowLayoutPanelReport.Size = new System.Drawing.Size(549, 29);
            this.flowLayoutPanelReport.TabIndex = 17;
            // 
            // buttonShowSettings
            // 
            this.buttonShowSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowSettings.Location = new System.Drawing.Point(471, 3);
            this.buttonShowSettings.Name = "buttonShowSettings";
            this.buttonShowSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonShowSettings.TabIndex = 15;
            this.buttonShowSettings.Text = "Settings";
            this.buttonShowSettings.UseVisualStyleBackColor = true;
            this.buttonShowSettings.Click += new System.EventHandler(this.buttonShowSettings_Click);
            // 
            // buttonShowInputData
            // 
            this.buttonShowInputData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowInputData.Location = new System.Drawing.Point(390, 3);
            this.buttonShowInputData.Name = "buttonShowInputData";
            this.buttonShowInputData.Size = new System.Drawing.Size(75, 23);
            this.buttonShowInputData.TabIndex = 7;
            this.buttonShowInputData.Text = "Report";
            this.buttonShowInputData.UseVisualStyleBackColor = true;
            this.buttonShowInputData.Click += new System.EventHandler(this.buttonShowInputData_Click);
            // 
            // plotView
            // 
            this.plotView.BackColor = System.Drawing.SystemColors.Window;
            this.plotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView.Location = new System.Drawing.Point(5, 52);
            this.plotView.Name = "plotView";
            this.plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView.Size = new System.Drawing.Size(549, 389);
            this.plotView.TabIndex = 2;
            this.plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotTypePanel
            // 
            this.plotTypePanel.Controls.Add(this.comboBoxAnalysisType);
            this.plotTypePanel.Controls.Add(this.comboBoxTestType);
            this.plotTypePanel.Controls.Add(this.comboBoxAnalysisPlotTypes);
            this.plotTypePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.plotTypePanel.Location = new System.Drawing.Point(5, 23);
            this.plotTypePanel.Name = "plotTypePanel";
            this.plotTypePanel.Size = new System.Drawing.Size(549, 29);
            this.plotTypePanel.TabIndex = 16;
            // 
            // comboBoxAnalysisType
            // 
            this.comboBoxAnalysisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnalysisType.FormattingEnabled = true;
            this.comboBoxAnalysisType.Location = new System.Drawing.Point(3, 3);
            this.comboBoxAnalysisType.Name = "comboBoxAnalysisType";
            this.comboBoxAnalysisType.Size = new System.Drawing.Size(182, 21);
            this.comboBoxAnalysisType.TabIndex = 6;
            this.comboBoxAnalysisType.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnalysisType_SelectedIndexChanged);
            // 
            // comboBoxTestType
            // 
            this.comboBoxTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTestType.FormattingEnabled = true;
            this.comboBoxTestType.Location = new System.Drawing.Point(191, 3);
            this.comboBoxTestType.Name = "comboBoxTestType";
            this.comboBoxTestType.Size = new System.Drawing.Size(182, 21);
            this.comboBoxTestType.TabIndex = 7;
            this.comboBoxTestType.SelectedIndexChanged += new System.EventHandler(this.comboBoxTestType_SelectedIndexChanged);
            // 
            // comboBoxAnalysisPlotTypes
            // 
            this.comboBoxAnalysisPlotTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnalysisPlotTypes.FormattingEnabled = true;
            this.comboBoxAnalysisPlotTypes.Location = new System.Drawing.Point(3, 30);
            this.comboBoxAnalysisPlotTypes.Name = "comboBoxAnalysisPlotTypes";
            this.comboBoxAnalysisPlotTypes.Size = new System.Drawing.Size(182, 21);
            this.comboBoxAnalysisPlotTypes.TabIndex = 8;
            this.comboBoxAnalysisPlotTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnalysisPlotTypes_SelectedIndexChanged);
            // 
            // flowLayoutPanelComparisonInfo
            // 
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelBlockSize);
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelPlotsPerBlock);
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelLocLower);
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelLocLowerValue);
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelLocUpper);
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelLocUpperValue);
            this.flowLayoutPanelComparisonInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanelComparisonInfo.Location = new System.Drawing.Point(5, 5);
            this.flowLayoutPanelComparisonInfo.Name = "flowLayoutPanelComparisonInfo";
            this.flowLayoutPanelComparisonInfo.Size = new System.Drawing.Size(549, 18);
            this.flowLayoutPanelComparisonInfo.TabIndex = 14;
            // 
            // labelBlockSize
            // 
            this.labelBlockSize.AutoSize = true;
            this.labelBlockSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBlockSize.Location = new System.Drawing.Point(3, 0);
            this.labelBlockSize.Name = "labelBlockSize";
            this.labelBlockSize.Size = new System.Drawing.Size(69, 13);
            this.labelBlockSize.TabIndex = 11;
            this.labelBlockSize.Text = "Block size:";
            // 
            // labelPlotsPerBlock
            // 
            this.labelPlotsPerBlock.AutoSize = true;
            this.labelPlotsPerBlock.Location = new System.Drawing.Point(78, 0);
            this.labelPlotsPerBlock.Name = "labelPlotsPerBlock";
            this.labelPlotsPerBlock.Size = new System.Drawing.Size(88, 13);
            this.labelPlotsPerBlock.TabIndex = 10;
            this.labelPlotsPerBlock.Text = "... plots per block";
            // 
            // labelLocLower
            // 
            this.labelLocLower.AutoSize = true;
            this.labelLocLower.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocLower.Location = new System.Drawing.Point(172, 0);
            this.labelLocLower.Name = "labelLocLower";
            this.labelLocLower.Size = new System.Drawing.Size(66, 13);
            this.labelLocLower.TabIndex = 13;
            this.labelLocLower.Text = "LocLower:";
            // 
            // labelLocLowerValue
            // 
            this.labelLocLowerValue.AutoSize = true;
            this.labelLocLowerValue.Location = new System.Drawing.Point(244, 0);
            this.labelLocLowerValue.Name = "labelLocLowerValue";
            this.labelLocLowerValue.Size = new System.Drawing.Size(16, 13);
            this.labelLocLowerValue.TabIndex = 12;
            this.labelLocLowerValue.Text = "...";
            // 
            // labelLocUpper
            // 
            this.labelLocUpper.AutoSize = true;
            this.labelLocUpper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocUpper.Location = new System.Drawing.Point(266, 0);
            this.labelLocUpper.Name = "labelLocUpper";
            this.labelLocUpper.Size = new System.Drawing.Size(68, 13);
            this.labelLocUpper.TabIndex = 15;
            this.labelLocUpper.Text = "Loc upper:";
            // 
            // labelLocUpperValue
            // 
            this.labelLocUpperValue.AutoSize = true;
            this.labelLocUpperValue.Location = new System.Drawing.Point(340, 0);
            this.labelLocUpperValue.Name = "labelLocUpperValue";
            this.labelLocUpperValue.Size = new System.Drawing.Size(16, 13);
            this.labelLocUpperValue.TabIndex = 14;
            this.labelLocUpperValue.Text = "...";
            // 
            // AnalysisResultsPerComparisonPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AnalysisResultsPerComparisonPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.panelResultPlots.ResumeLayout(false);
            this.flowLayoutPanelReport.ResumeLayout(false);
            this.plotTypePanel.ResumeLayout(false);
            this.flowLayoutPanelComparisonInfo.ResumeLayout(false);
            this.flowLayoutPanelComparisonInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
        private System.Windows.Forms.Panel panelResultPlots;
        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.ComboBox comboBoxAnalysisType;
        private System.Windows.Forms.Button buttonShowInputData;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelComparisonInfo;
        private System.Windows.Forms.Label labelBlockSize;
        private System.Windows.Forms.Label labelPlotsPerBlock;
        private System.Windows.Forms.Label labelLocLower;
        private System.Windows.Forms.Label labelLocLowerValue;
        private System.Windows.Forms.Label labelLocUpper;
        private System.Windows.Forms.Label labelLocUpperValue;
        private System.Windows.Forms.Button buttonShowSettings;
        private System.Windows.Forms.FlowLayoutPanel plotTypePanel;
        private System.Windows.Forms.ComboBox comboBoxTestType;
        private System.Windows.Forms.ComboBox comboBoxAnalysisPlotTypes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelReport;
    }
}
