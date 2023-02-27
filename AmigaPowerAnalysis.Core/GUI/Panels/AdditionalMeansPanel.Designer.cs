namespace AmigaPowerAnalysis.GUI {
    partial class AdditionalMeansPanel {
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
            this.dataGridViewFactorLevels = new System.Windows.Forms.DataGridView();
            this.dataGridViewEndpoints = new System.Windows.Forms.DataGridView();
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFactorLevels
            // 
            this.dataGridViewFactorLevels.AllowUserToAddRows = false;
            this.dataGridViewFactorLevels.AllowUserToDeleteRows = false;
            this.dataGridViewFactorLevels.AllowUserToResizeRows = false;
            this.dataGridViewFactorLevels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactorLevels.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactorLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactorLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFactorLevels.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactorLevels.MultiSelect = false;
            this.dataGridViewFactorLevels.Name = "dataGridViewFactorLevels";
            this.dataGridViewFactorLevels.RowHeadersVisible = false;
            this.dataGridViewFactorLevels.RowHeadersWidth = 24;
            this.dataGridViewFactorLevels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactorLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewFactorLevels.Size = new System.Drawing.Size(512, 426);
            this.dataGridViewFactorLevels.TabIndex = 2;
            this.dataGridViewFactorLevels.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFactorLevels_CellValueChanged);
            this.dataGridViewFactorLevels.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewFactorLevels_DataError);
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
            this.dataGridViewEndpoints.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEndpoints.MultiSelect = false;
            this.dataGridViewEndpoints.Name = "dataGridViewEndpoints";
            this.dataGridViewEndpoints.ReadOnly = true;
            this.dataGridViewEndpoints.RowHeadersVisible = false;
            this.dataGridViewEndpoints.RowHeadersWidth = 24;
            this.dataGridViewEndpoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(260, 426);
            this.dataGridViewEndpoints.TabIndex = 3;
            this.dataGridViewEndpoints.SelectionChanged += new System.EventHandler(this.dataGridComparisons_SelectionChanged);
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
            this.splitContainerComparisons.Panel1.Controls.Add(this.dataGridViewEndpoints);
            // 
            // splitContainerComparisons.Panel2
            // 
            this.splitContainerComparisons.Panel2.Controls.Add(this.dataGridViewFactorLevels);
            this.splitContainerComparisons.Size = new System.Drawing.Size(776, 426);
            this.splitContainerComparisons.SplitterDistance = 260;
            this.splitContainerComparisons.TabIndex = 4;
            // 
            // AdditionalMeansPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Name = "AdditionalMeansPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFactorLevels;
        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
        private System.Windows.Forms.SplitContainer splitContainerComparisons;
    }
}
