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
            this.comboBoxAnalysisType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.plotViewEquivalenceLog = new OxyPlot.WindowsForms.PlotView();
            this.plotViewDifferenceLog = new OxyPlot.WindowsForms.PlotView();
            this.plotViewEquivalenceRepetitions = new OxyPlot.WindowsForms.PlotView();
            this.plotViewDifferenceRepetitions = new OxyPlot.WindowsForms.PlotView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            this.panelResultPlots.SuspendLayout();
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
            this.panelResultPlots.Controls.Add(this.comboBoxAnalysisType);
            this.panelResultPlots.Controls.Add(this.tableLayoutPanel);
            this.panelResultPlots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResultPlots.Location = new System.Drawing.Point(0, 0);
            this.panelResultPlots.Name = "panelResultPlots";
            this.panelResultPlots.Size = new System.Drawing.Size(561, 477);
            this.panelResultPlots.TabIndex = 0;
            // 
            // comboBoxAnalysisType
            // 
            this.comboBoxAnalysisType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAnalysisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnalysisType.FormattingEnabled = true;
            this.comboBoxAnalysisType.Location = new System.Drawing.Point(379, 0);
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
            this.tableLayoutPanel.Controls.Add(this.plotViewEquivalenceLog, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.plotViewDifferenceLog, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.plotViewEquivalenceRepetitions, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.plotViewDifferenceRepetitions, 0, 0);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 21);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(561, 456);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // plotViewEquivalenceLog
            // 
            this.plotViewEquivalenceLog.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalenceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalenceLog.Location = new System.Drawing.Point(283, 231);
            this.plotViewEquivalenceLog.Name = "plotViewEquivalenceLog";
            this.plotViewEquivalenceLog.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalenceLog.Size = new System.Drawing.Size(275, 222);
            this.plotViewEquivalenceLog.TabIndex = 4;
            this.plotViewEquivalenceLog.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewEquivalenceLog.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewEquivalenceLog.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewDifferenceLog
            // 
            this.plotViewDifferenceLog.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewDifferenceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewDifferenceLog.Location = new System.Drawing.Point(283, 3);
            this.plotViewDifferenceLog.Name = "plotViewDifferenceLog";
            this.plotViewDifferenceLog.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewDifferenceLog.Size = new System.Drawing.Size(275, 222);
            this.plotViewDifferenceLog.TabIndex = 2;
            this.plotViewDifferenceLog.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifferenceLog.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifferenceLog.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewEquivalenceRepetitions
            // 
            this.plotViewEquivalenceRepetitions.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalenceRepetitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalenceRepetitions.Location = new System.Drawing.Point(3, 231);
            this.plotViewEquivalenceRepetitions.Name = "plotViewEquivalenceRepetitions";
            this.plotViewEquivalenceRepetitions.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalenceRepetitions.Size = new System.Drawing.Size(274, 222);
            this.plotViewEquivalenceRepetitions.TabIndex = 3;
            this.plotViewEquivalenceRepetitions.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewEquivalenceRepetitions.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewEquivalenceRepetitions.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewDifferenceRepetitions
            // 
            this.plotViewDifferenceRepetitions.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewDifferenceRepetitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewDifferenceRepetitions.Location = new System.Drawing.Point(3, 3);
            this.plotViewDifferenceRepetitions.Name = "plotViewDifferenceRepetitions";
            this.plotViewDifferenceRepetitions.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewDifferenceRepetitions.Size = new System.Drawing.Size(274, 222);
            this.plotViewDifferenceRepetitions.TabIndex = 0;
            this.plotViewDifferenceRepetitions.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifferenceRepetitions.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifferenceRepetitions.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // AnalysisResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AnalysisResultsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.panelResultPlots.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
        private System.Windows.Forms.Panel panelResultPlots;
        private OxyPlot.WindowsForms.PlotView plotViewDifferenceRepetitions;
        private OxyPlot.WindowsForms.PlotView plotViewEquivalenceRepetitions;
        private OxyPlot.WindowsForms.PlotView plotViewDifferenceLog;
        private OxyPlot.WindowsForms.PlotView plotViewEquivalenceLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ComboBox comboBoxAnalysisType;
    }
}
