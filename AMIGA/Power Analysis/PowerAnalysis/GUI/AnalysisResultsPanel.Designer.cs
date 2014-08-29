namespace AmigaPowerAnalysis.GUI {
    partial class AnalysisResultsPanel {
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
            this.flowLayoutPanelComparisonInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.labelBlockSize = new System.Windows.Forms.Label();
            this.labelPlotsPerBlock = new System.Windows.Forms.Label();
            this.buttonShowReport = new System.Windows.Forms.Button();
            this.comboBoxAnalysisType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.plotViewEquivalenceLevelOfConcern = new OxyPlot.WindowsForms.PlotView();
            this.plotViewDifferenceLevelOfConcern = new OxyPlot.WindowsForms.PlotView();
            this.plotViewEquivalenceReplicates = new OxyPlot.WindowsForms.PlotView();
            this.plotViewDifferenceReplicates = new OxyPlot.WindowsForms.PlotView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            this.panelResultPlots.SuspendLayout();
            this.flowLayoutPanelComparisonInfo.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
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
            this.dataGridViewComparisons.RowHeadersVisible = false;
            this.dataGridViewComparisons.RowHeadersWidth = 24;
            this.dataGridViewComparisons.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewComparisons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComparisons.Size = new System.Drawing.Size(283, 477);
            this.dataGridViewComparisons.TabIndex = 3;
            this.dataGridViewComparisons.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComparisons_CellValueChanged);
            this.dataGridViewComparisons.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewComparisons_CurrentCellDirtyStateChanged);
            this.dataGridViewComparisons.SelectionChanged += new System.EventHandler(this.dataGridViewComparisons_SelectionChanged);
            // 
            // panelResultPlots
            // 
            this.panelResultPlots.Controls.Add(this.flowLayoutPanelComparisonInfo);
            this.panelResultPlots.Controls.Add(this.buttonShowReport);
            this.panelResultPlots.Controls.Add(this.comboBoxAnalysisType);
            this.panelResultPlots.Controls.Add(this.tableLayoutPanel);
            this.panelResultPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResultPlots.Location = new System.Drawing.Point(0, 0);
            this.panelResultPlots.Name = "panelResultPlots";
            this.panelResultPlots.Size = new System.Drawing.Size(561, 477);
            this.panelResultPlots.TabIndex = 0;
            // 
            // flowLayoutPanelComparisonInfo
            // 
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelBlockSize);
            this.flowLayoutPanelComparisonInfo.Controls.Add(this.labelPlotsPerBlock);
            this.flowLayoutPanelComparisonInfo.Location = new System.Drawing.Point(0, 27);
            this.flowLayoutPanelComparisonInfo.Name = "flowLayoutPanelComparisonInfo";
            this.flowLayoutPanelComparisonInfo.Size = new System.Drawing.Size(561, 18);
            this.flowLayoutPanelComparisonInfo.TabIndex = 13;
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
            // buttonShowReport
            // 
            this.buttonShowReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShowReport.Location = new System.Drawing.Point(486, 0);
            this.buttonShowReport.Name = "buttonShowReport";
            this.buttonShowReport.Size = new System.Drawing.Size(75, 23);
            this.buttonShowReport.TabIndex = 7;
            this.buttonShowReport.Text = "Report";
            this.buttonShowReport.UseVisualStyleBackColor = true;
            this.buttonShowReport.Click += new System.EventHandler(this.buttonShowReport_Click);
            // 
            // comboBoxAnalysisType
            // 
            this.comboBoxAnalysisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnalysisType.FormattingEnabled = true;
            this.comboBoxAnalysisType.Location = new System.Drawing.Point(0, 0);
            this.comboBoxAnalysisType.Name = "comboBoxAnalysisType";
            this.comboBoxAnalysisType.Size = new System.Drawing.Size(182, 21);
            this.comboBoxAnalysisType.TabIndex = 6;
            this.comboBoxAnalysisType.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnalysisType_SelectedIndexChanged);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.plotViewEquivalenceLevelOfConcern, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.plotViewDifferenceLevelOfConcern, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.plotViewEquivalenceReplicates, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.plotViewDifferenceReplicates, 0, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 44);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(561, 433);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // plotViewEquivalenceLevelOfConcern
            // 
            this.plotViewEquivalenceLevelOfConcern.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalenceLevelOfConcern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalenceLevelOfConcern.Location = new System.Drawing.Point(283, 219);
            this.plotViewEquivalenceLevelOfConcern.Name = "plotViewEquivalenceLevelOfConcern";
            this.plotViewEquivalenceLevelOfConcern.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalenceLevelOfConcern.Size = new System.Drawing.Size(275, 211);
            this.plotViewEquivalenceLevelOfConcern.TabIndex = 4;
            this.plotViewEquivalenceLevelOfConcern.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewEquivalenceLevelOfConcern.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewEquivalenceLevelOfConcern.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewDifferenceLevelOfConcern
            // 
            this.plotViewDifferenceLevelOfConcern.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewDifferenceLevelOfConcern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewDifferenceLevelOfConcern.Location = new System.Drawing.Point(283, 3);
            this.plotViewDifferenceLevelOfConcern.Name = "plotViewDifferenceLevelOfConcern";
            this.plotViewDifferenceLevelOfConcern.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewDifferenceLevelOfConcern.Size = new System.Drawing.Size(275, 210);
            this.plotViewDifferenceLevelOfConcern.TabIndex = 2;
            this.plotViewDifferenceLevelOfConcern.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifferenceLevelOfConcern.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifferenceLevelOfConcern.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewEquivalenceReplicates
            // 
            this.plotViewEquivalenceReplicates.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalenceReplicates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalenceReplicates.Location = new System.Drawing.Point(3, 219);
            this.plotViewEquivalenceReplicates.Name = "plotViewEquivalenceReplicates";
            this.plotViewEquivalenceReplicates.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalenceReplicates.Size = new System.Drawing.Size(274, 211);
            this.plotViewEquivalenceReplicates.TabIndex = 3;
            this.plotViewEquivalenceReplicates.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewEquivalenceReplicates.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewEquivalenceReplicates.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewDifferenceReplicates
            // 
            this.plotViewDifferenceReplicates.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewDifferenceReplicates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewDifferenceReplicates.Location = new System.Drawing.Point(3, 3);
            this.plotViewDifferenceReplicates.Name = "plotViewDifferenceReplicates";
            this.plotViewDifferenceReplicates.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewDifferenceReplicates.Size = new System.Drawing.Size(274, 210);
            this.plotViewDifferenceReplicates.TabIndex = 0;
            this.plotViewDifferenceReplicates.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifferenceReplicates.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifferenceReplicates.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // AnalysisResultsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AnalysisResultsPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.panelResultPlots.ResumeLayout(false);
            this.flowLayoutPanelComparisonInfo.ResumeLayout(false);
            this.flowLayoutPanelComparisonInfo.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
        private System.Windows.Forms.Panel panelResultPlots;
        private OxyPlot.WindowsForms.PlotView plotViewDifferenceReplicates;
        private OxyPlot.WindowsForms.PlotView plotViewEquivalenceReplicates;
        private OxyPlot.WindowsForms.PlotView plotViewDifferenceLevelOfConcern;
        private OxyPlot.WindowsForms.PlotView plotViewEquivalenceLevelOfConcern;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ComboBox comboBoxAnalysisType;
        private System.Windows.Forms.Button buttonShowReport;
        private System.Windows.Forms.Label labelBlockSize;
        private System.Windows.Forms.Label labelPlotsPerBlock;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelComparisonInfo;
    }
}
