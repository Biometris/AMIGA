namespace AmigaPowerAnalysis.GUI {
    partial class AnalysisResultsForm {
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
            this.panelTabDescription = new System.Windows.Forms.Panel();
            this.textBoxTabTitle = new System.Windows.Forms.TextBox();
            this.textBoxTabDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            this.panelResultPlots.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.panelTabDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerComparisons
            // 
            this.splitContainerComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerComparisons.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerComparisons.Location = new System.Drawing.Point(10, 107);
            this.splitContainerComparisons.Name = "splitContainerComparisons";
            // 
            // splitContainerComparisons.Panel1
            // 
            this.splitContainerComparisons.Panel1.Controls.Add(this.dataGridViewComparisons);
            // 
            // splitContainerComparisons.Panel2
            // 
            this.splitContainerComparisons.Panel2.Controls.Add(this.panelResultPlots);
            this.splitContainerComparisons.Size = new System.Drawing.Size(854, 386);
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
            this.dataGridViewComparisons.Size = new System.Drawing.Size(283, 386);
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
            this.panelResultPlots.Size = new System.Drawing.Size(567, 386);
            this.panelResultPlots.TabIndex = 0;
            // 
            // comboBoxAnalysisType
            // 
            this.comboBoxAnalysisType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAnalysisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnalysisType.FormattingEnabled = true;
            this.comboBoxAnalysisType.Location = new System.Drawing.Point(385, 0);
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
            this.tableLayoutPanel.Size = new System.Drawing.Size(567, 365);
            this.tableLayoutPanel.TabIndex = 5;
            // 
            // plotViewEquivalenceLog
            // 
            this.plotViewEquivalenceLog.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalenceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalenceLog.Location = new System.Drawing.Point(286, 185);
            this.plotViewEquivalenceLog.Name = "plotViewEquivalenceLog";
            this.plotViewEquivalenceLog.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalenceLog.Size = new System.Drawing.Size(278, 177);
            this.plotViewEquivalenceLog.TabIndex = 4;
            this.plotViewEquivalenceLog.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewEquivalenceLog.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewEquivalenceLog.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewDifferenceLog
            // 
            this.plotViewDifferenceLog.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewDifferenceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewDifferenceLog.Location = new System.Drawing.Point(286, 3);
            this.plotViewDifferenceLog.Name = "plotViewDifferenceLog";
            this.plotViewDifferenceLog.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewDifferenceLog.Size = new System.Drawing.Size(278, 176);
            this.plotViewDifferenceLog.TabIndex = 2;
            this.plotViewDifferenceLog.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifferenceLog.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifferenceLog.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotViewEquivalenceRepetitions
            // 
            this.plotViewEquivalenceRepetitions.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalenceRepetitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalenceRepetitions.Location = new System.Drawing.Point(3, 185);
            this.plotViewEquivalenceRepetitions.Name = "plotViewEquivalenceRepetitions";
            this.plotViewEquivalenceRepetitions.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalenceRepetitions.Size = new System.Drawing.Size(277, 177);
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
            this.plotViewDifferenceRepetitions.Size = new System.Drawing.Size(277, 176);
            this.plotViewDifferenceRepetitions.TabIndex = 0;
            this.plotViewDifferenceRepetitions.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifferenceRepetitions.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifferenceRepetitions.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // panelTabDescription
            // 
            this.panelTabDescription.AutoSize = true;
            this.panelTabDescription.Controls.Add(this.textBoxTabDescription);
            this.panelTabDescription.Controls.Add(this.textBoxTabTitle);
            this.panelTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabDescription.Location = new System.Drawing.Point(10, 10);
            this.panelTabDescription.Name = "panelTabDescription";
            this.panelTabDescription.Size = new System.Drawing.Size(854, 97);
            this.panelTabDescription.TabIndex = 8;
            // 
            // textBoxTabTitle
            // 
            this.textBoxTabTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTabTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxTabTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabTitle.Location = new System.Drawing.Point(0, 0);
            this.textBoxTabTitle.Name = "textBoxTabTitle";
            this.textBoxTabTitle.ReadOnly = true;
            this.textBoxTabTitle.Size = new System.Drawing.Size(854, 22);
            this.textBoxTabTitle.TabIndex = 7;
            this.textBoxTabTitle.Text = "Tab title";
            // 
            // textBoxTabDescription
            // 
            this.textBoxTabDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTabDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxTabDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabDescription.Location = new System.Drawing.Point(0, 22);
            this.textBoxTabDescription.Multiline = true;
            this.textBoxTabDescription.Name = "textBoxTabDescription";
            this.textBoxTabDescription.ReadOnly = true;
            this.textBoxTabDescription.Size = new System.Drawing.Size(854, 75);
            this.textBoxTabDescription.TabIndex = 8;
            this.textBoxTabDescription.Text = "Description";
            // 
            // AnalysisResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Controls.Add(this.panelTabDescription);
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
            this.panelTabDescription.ResumeLayout(false);
            this.panelTabDescription.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Panel panelTabDescription;
        private System.Windows.Forms.TextBox textBoxTabTitle;
        private System.Windows.Forms.TextBox textBoxTabDescription;
    }
}
