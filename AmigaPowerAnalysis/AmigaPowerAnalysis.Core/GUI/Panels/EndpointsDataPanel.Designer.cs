namespace AmigaPowerAnalysis.GUI {
    partial class EndpointsDataPanel {
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
            this.dataGridViewEndpoints = new System.Windows.Forms.DataGridView();
            this.distributionChartPlotView = new OxyPlot.WindowsForms.PlotView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewEndpoints
            // 
            this.dataGridViewEndpoints.AllowUserToAddRows = false;
            this.dataGridViewEndpoints.AllowUserToDeleteRows = false;
            this.dataGridViewEndpoints.AllowUserToResizeRows = false;
            this.dataGridViewEndpoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEndpoints.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEndpoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEndpoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEndpoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewEndpoints.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEndpoints.Name = "dataGridViewEndpoints";
            this.dataGridViewEndpoints.RowHeadersWidth = 24;
            this.dataGridViewEndpoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(854, 273);
            this.dataGridViewEndpoints.TabIndex = 2;
            this.dataGridViewEndpoints.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEndpoints_CellContentClick);
            this.dataGridViewEndpoints.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEndpoints_CellValueChanged);
            this.dataGridViewEndpoints.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewEndpoints_DataError);
            this.dataGridViewEndpoints.SelectionChanged += new System.EventHandler(this.dataGridViewEndpoints_SelectionChanged);
            // 
            // distributionChartPlotView
            // 
            this.distributionChartPlotView.BackColor = System.Drawing.SystemColors.Window;
            this.distributionChartPlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.distributionChartPlotView.Location = new System.Drawing.Point(0, 0);
            this.distributionChartPlotView.Name = "distributionChartPlotView";
            this.distributionChartPlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.distributionChartPlotView.Size = new System.Drawing.Size(854, 206);
            this.distributionChartPlotView.TabIndex = 3;
            this.distributionChartPlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.distributionChartPlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.distributionChartPlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 10);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewEndpoints);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.distributionChartPlotView);
            this.splitContainer1.Size = new System.Drawing.Size(854, 483);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 4;
            // 
            // EndpointsDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainer1);
            this.Name = "EndpointsDataPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
        private OxyPlot.WindowsForms.PlotView distributionChartPlotView;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
