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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisResultsPerComparisonPanel));
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            this.dataGridViewComparisons = new System.Windows.Forms.DataGridView();
            this.tabControlEndpointResult = new System.Windows.Forms.TabControl();
            this.tabPageDifferenceTest = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonReplicatesDifference = new System.Windows.Forms.RadioButton();
            this.radioButtonRatioDifference = new System.Windows.Forms.RadioButton();
            this.plotViewDifference = new OxyPlot.WindowsForms.PlotView();
            this.tabPageEquivalenceTest = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonReplicatesEquivalence = new System.Windows.Forms.RadioButton();
            this.radioButtonRatioEquivalence = new System.Windows.Forms.RadioButton();
            this.plotViewEquivalence = new OxyPlot.WindowsForms.PlotView();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.webBrowserSettingsReport = new System.Windows.Forms.WebBrowser();
            this.tabPageFullReport = new System.Windows.Forms.TabPage();
            this.webBrowserFullReport = new System.Windows.Forms.WebBrowser();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportPdf = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelOutputNameLabel = new System.Windows.Forms.Label();
            this.labelOutputName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            this.tabControlEndpointResult.SuspendLayout();
            this.tabPageDifferenceTest.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageEquivalenceTest.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabPageFullReport.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
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
            this.splitContainerComparisons.Panel1.Controls.Add(this.flowLayoutPanel3);
            this.splitContainerComparisons.Panel1.Controls.Add(this.dataGridViewComparisons);
            // 
            // splitContainerComparisons.Panel2
            // 
            this.splitContainerComparisons.Panel2.Controls.Add(this.tabControlEndpointResult);
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
            this.dataGridViewComparisons.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
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
            this.dataGridViewComparisons.SelectionChanged += new System.EventHandler(this.dataGridViewComparisons_SelectionChanged);
            // 
            // tabControlEndpointResult
            // 
            this.tabControlEndpointResult.Controls.Add(this.tabPageDifferenceTest);
            this.tabControlEndpointResult.Controls.Add(this.tabPageEquivalenceTest);
            this.tabControlEndpointResult.Controls.Add(this.tabPageSettings);
            this.tabControlEndpointResult.Controls.Add(this.tabPageFullReport);
            this.tabControlEndpointResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEndpointResult.Location = new System.Drawing.Point(0, 0);
            this.tabControlEndpointResult.Name = "tabControlEndpointResult";
            this.tabControlEndpointResult.SelectedIndex = 0;
            this.tabControlEndpointResult.Size = new System.Drawing.Size(561, 477);
            this.tabControlEndpointResult.TabIndex = 10;
            this.tabControlEndpointResult.SelectedIndexChanged += new System.EventHandler(this.tabControlEndpointResult_SelectedIndexChanged);
            // 
            // tabPageDifferenceTest
            // 
            this.tabPageDifferenceTest.Controls.Add(this.flowLayoutPanel1);
            this.tabPageDifferenceTest.Controls.Add(this.plotViewDifference);
            this.tabPageDifferenceTest.Location = new System.Drawing.Point(4, 22);
            this.tabPageDifferenceTest.Name = "tabPageDifferenceTest";
            this.tabPageDifferenceTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDifferenceTest.Size = new System.Drawing.Size(553, 451);
            this.tabPageDifferenceTest.TabIndex = 0;
            this.tabPageDifferenceTest.Text = "Chart difference test";
            this.tabPageDifferenceTest.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.radioButtonReplicatesDifference);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonRatioDifference);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(547, 23);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // radioButtonReplicatesDifference
            // 
            this.radioButtonReplicatesDifference.AutoSize = true;
            this.radioButtonReplicatesDifference.Location = new System.Drawing.Point(407, 3);
            this.radioButtonReplicatesDifference.Name = "radioButtonReplicatesDifference";
            this.radioButtonReplicatesDifference.Size = new System.Drawing.Size(137, 17);
            this.radioButtonReplicatesDifference.TabIndex = 9;
            this.radioButtonReplicatesDifference.TabStop = true;
            this.radioButtonReplicatesDifference.Text = "Power versus replicates";
            this.radioButtonReplicatesDifference.UseVisualStyleBackColor = true;
            this.radioButtonReplicatesDifference.CheckedChanged += new System.EventHandler(this.radioButtonReplicatesDifference_CheckedChanged);
            // 
            // radioButtonRatioDifference
            // 
            this.radioButtonRatioDifference.AutoSize = true;
            this.radioButtonRatioDifference.Location = new System.Drawing.Point(289, 3);
            this.radioButtonRatioDifference.Name = "radioButtonRatioDifference";
            this.radioButtonRatioDifference.Size = new System.Drawing.Size(112, 17);
            this.radioButtonRatioDifference.TabIndex = 8;
            this.radioButtonRatioDifference.TabStop = true;
            this.radioButtonRatioDifference.Text = "Power versus ratio";
            this.radioButtonRatioDifference.UseVisualStyleBackColor = true;
            this.radioButtonRatioDifference.CheckedChanged += new System.EventHandler(this.radioButtonRatioDifference_CheckedChanged);
            // 
            // plotViewDifference
            // 
            this.plotViewDifference.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewDifference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewDifference.Location = new System.Drawing.Point(3, 3);
            this.plotViewDifference.Name = "plotViewDifference";
            this.plotViewDifference.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewDifference.Size = new System.Drawing.Size(547, 445);
            this.plotViewDifference.TabIndex = 4;
            this.plotViewDifference.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifference.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifference.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // tabPageEquivalenceTest
            // 
            this.tabPageEquivalenceTest.Controls.Add(this.flowLayoutPanel2);
            this.tabPageEquivalenceTest.Controls.Add(this.plotViewEquivalence);
            this.tabPageEquivalenceTest.Location = new System.Drawing.Point(4, 22);
            this.tabPageEquivalenceTest.Name = "tabPageEquivalenceTest";
            this.tabPageEquivalenceTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEquivalenceTest.Size = new System.Drawing.Size(553, 451);
            this.tabPageEquivalenceTest.TabIndex = 1;
            this.tabPageEquivalenceTest.Text = "Chart equivalence test";
            this.tabPageEquivalenceTest.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.radioButtonReplicatesEquivalence);
            this.flowLayoutPanel2.Controls.Add(this.radioButtonRatioEquivalence);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(547, 23);
            this.flowLayoutPanel2.TabIndex = 18;
            // 
            // radioButtonReplicatesEquivalence
            // 
            this.radioButtonReplicatesEquivalence.AutoSize = true;
            this.radioButtonReplicatesEquivalence.Location = new System.Drawing.Point(407, 3);
            this.radioButtonReplicatesEquivalence.Name = "radioButtonReplicatesEquivalence";
            this.radioButtonReplicatesEquivalence.Size = new System.Drawing.Size(137, 17);
            this.radioButtonReplicatesEquivalence.TabIndex = 11;
            this.radioButtonReplicatesEquivalence.TabStop = true;
            this.radioButtonReplicatesEquivalence.Text = "Power versus replicates";
            this.radioButtonReplicatesEquivalence.UseVisualStyleBackColor = true;
            this.radioButtonReplicatesEquivalence.CheckedChanged += new System.EventHandler(this.radioButtonReplicatesEquivalence_CheckedChanged);
            // 
            // radioButtonRatioEquivalence
            // 
            this.radioButtonRatioEquivalence.AutoSize = true;
            this.radioButtonRatioEquivalence.Location = new System.Drawing.Point(289, 3);
            this.radioButtonRatioEquivalence.Name = "radioButtonRatioEquivalence";
            this.radioButtonRatioEquivalence.Size = new System.Drawing.Size(112, 17);
            this.radioButtonRatioEquivalence.TabIndex = 10;
            this.radioButtonRatioEquivalence.TabStop = true;
            this.radioButtonRatioEquivalence.Text = "Power versus ratio";
            this.radioButtonRatioEquivalence.UseVisualStyleBackColor = true;
            this.radioButtonRatioEquivalence.CheckedChanged += new System.EventHandler(this.radioButtonRatioEquivalence_CheckedChanged);
            // 
            // plotViewEquivalence
            // 
            this.plotViewEquivalence.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalence.Location = new System.Drawing.Point(3, 3);
            this.plotViewEquivalence.Name = "plotViewEquivalence";
            this.plotViewEquivalence.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalence.Size = new System.Drawing.Size(547, 445);
            this.plotViewEquivalence.TabIndex = 4;
            this.plotViewEquivalence.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewEquivalence.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewEquivalence.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.webBrowserSettingsReport);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(553, 451);
            this.tabPageSettings.TabIndex = 3;
            this.tabPageSettings.Text = "Power analysis settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // webBrowserSettingsReport
            // 
            this.webBrowserSettingsReport.AllowNavigation = false;
            this.webBrowserSettingsReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserSettingsReport.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserSettingsReport.Location = new System.Drawing.Point(0, 0);
            this.webBrowserSettingsReport.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserSettingsReport.Name = "webBrowserSettingsReport";
            this.webBrowserSettingsReport.Size = new System.Drawing.Size(553, 451);
            this.webBrowserSettingsReport.TabIndex = 1;
            // 
            // tabPageFullReport
            // 
            this.tabPageFullReport.Controls.Add(this.webBrowserFullReport);
            this.tabPageFullReport.Controls.Add(this.toolStrip);
            this.tabPageFullReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageFullReport.Name = "tabPageFullReport";
            this.tabPageFullReport.Size = new System.Drawing.Size(553, 451);
            this.tabPageFullReport.TabIndex = 4;
            this.tabPageFullReport.Text = "Endpoint report";
            this.tabPageFullReport.UseVisualStyleBackColor = true;
            // 
            // webBrowserFullReport
            // 
            this.webBrowserFullReport.AllowNavigation = false;
            this.webBrowserFullReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserFullReport.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserFullReport.Location = new System.Drawing.Point(0, 25);
            this.webBrowserFullReport.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserFullReport.Name = "webBrowserFullReport";
            this.webBrowserFullReport.Size = new System.Drawing.Size(553, 426);
            this.webBrowserFullReport.TabIndex = 2;
            // 
            // toolStrip
            // 
            this.toolStrip.AllowMerge = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExportPdf});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(553, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonExportPdf
            // 
            this.toolStripButtonExportPdf.AutoSize = false;
            this.toolStripButtonExportPdf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportPdf.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportPdf.Image")));
            this.toolStripButtonExportPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportPdf.Name = "toolStripButtonExportPdf";
            this.toolStripButtonExportPdf.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExportPdf.Text = "Export pdf";
            this.toolStripButtonExportPdf.ToolTipText = "Export pdf";
            this.toolStripButtonExportPdf.Click += new System.EventHandler(this.toolStripButtonExportPdf_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.labelOutputNameLabel);
            this.flowLayoutPanel3.Controls.Add(this.labelOutputName);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.flowLayoutPanel3.Size = new System.Drawing.Size(283, 19);
            this.flowLayoutPanel3.TabIndex = 13;
            // 
            // labelOutputNameLabel
            // 
            this.labelOutputNameLabel.AutoSize = true;
            this.labelOutputNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutputNameLabel.Location = new System.Drawing.Point(3, 3);
            this.labelOutputNameLabel.Name = "labelOutputNameLabel";
            this.labelOutputNameLabel.Size = new System.Drawing.Size(45, 13);
            this.labelOutputNameLabel.TabIndex = 11;
            this.labelOutputNameLabel.Text = "Ouput:";
            // 
            // labelOutputName
            // 
            this.labelOutputName.AutoSize = true;
            this.labelOutputName.Location = new System.Drawing.Point(54, 3);
            this.labelOutputName.Name = "labelOutputName";
            this.labelOutputName.Size = new System.Drawing.Size(66, 13);
            this.labelOutputName.TabIndex = 10;
            this.labelOutputName.Text = "output name";
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
            this.splitContainerComparisons.Panel1.PerformLayout();
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.tabControlEndpointResult.ResumeLayout(false);
            this.tabPageDifferenceTest.ResumeLayout(false);
            this.tabPageDifferenceTest.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPageEquivalenceTest.ResumeLayout(false);
            this.tabPageEquivalenceTest.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageFullReport.ResumeLayout(false);
            this.tabPageFullReport.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
        private System.Windows.Forms.TabControl tabControlEndpointResult;
        private System.Windows.Forms.TabPage tabPageDifferenceTest;
        private System.Windows.Forms.TabPage tabPageEquivalenceTest;
        private System.Windows.Forms.TabPage tabPageSettings;
        private OxyPlot.WindowsForms.PlotView plotViewDifference;
        private OxyPlot.WindowsForms.PlotView plotViewEquivalence;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButtonReplicatesDifference;
        private System.Windows.Forms.RadioButton radioButtonRatioDifference;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButtonRatioEquivalence;
        private System.Windows.Forms.RadioButton radioButtonReplicatesEquivalence;
        private System.Windows.Forms.WebBrowser webBrowserSettingsReport;
        private System.Windows.Forms.TabPage tabPageFullReport;
        private System.Windows.Forms.WebBrowser webBrowserFullReport;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportPdf;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label labelOutputNameLabel;
        private System.Windows.Forms.Label labelOutputName;
    }
}
