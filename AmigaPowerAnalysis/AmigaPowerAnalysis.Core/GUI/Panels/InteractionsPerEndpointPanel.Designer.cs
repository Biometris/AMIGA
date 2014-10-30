namespace AmigaPowerAnalysis.GUI {
    partial class InteractionsPerEndpointPanel {
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
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            this.dataGridViewEndpointInteractionFactors = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpointInteractionFactors)).BeginInit();
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
            this.dataGridViewFactorLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactorLevels.Size = new System.Drawing.Size(776, 227);
            this.dataGridViewFactorLevels.TabIndex = 2;
            this.dataGridViewFactorLevels.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFactorLevels_CellValueChanged);
            this.dataGridViewFactorLevels.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewFactorLevels_CurrentCellDirtyStateChanged);
            this.dataGridViewFactorLevels.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewFactorLevels_DataError);
            // 
            // splitContainerComparisons
            // 
            this.splitContainerComparisons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerComparisons.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerComparisons.Location = new System.Drawing.Point(13, 13);
            this.splitContainerComparisons.Name = "splitContainerComparisons";
            this.splitContainerComparisons.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerComparisons.Panel1
            // 
            this.splitContainerComparisons.Panel1.Controls.Add(this.dataGridViewEndpointInteractionFactors);
            // 
            // splitContainerComparisons.Panel2
            // 
            this.splitContainerComparisons.Panel2.Controls.Add(this.dataGridViewFactorLevels);
            this.splitContainerComparisons.Size = new System.Drawing.Size(776, 426);
            this.splitContainerComparisons.SplitterDistance = 195;
            this.splitContainerComparisons.TabIndex = 4;
            // 
            // dataGridViewEndpointInteractionFactors
            // 
            this.dataGridViewEndpointInteractionFactors.AllowUserToAddRows = false;
            this.dataGridViewEndpointInteractionFactors.AllowUserToDeleteRows = false;
            this.dataGridViewEndpointInteractionFactors.AllowUserToResizeRows = false;
            this.dataGridViewEndpointInteractionFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEndpointInteractionFactors.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEndpointInteractionFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEndpointInteractionFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEndpointInteractionFactors.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEndpointInteractionFactors.MultiSelect = false;
            this.dataGridViewEndpointInteractionFactors.Name = "dataGridViewEndpointInteractionFactors";
            this.dataGridViewEndpointInteractionFactors.RowHeadersVisible = false;
            this.dataGridViewEndpointInteractionFactors.RowHeadersWidth = 24;
            this.dataGridViewEndpointInteractionFactors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpointInteractionFactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEndpointInteractionFactors.Size = new System.Drawing.Size(776, 195);
            this.dataGridViewEndpointInteractionFactors.TabIndex = 4;
            this.dataGridViewEndpointInteractionFactors.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewEndpointInteractionFactors_CurrentCellDirtyStateChanged);
            this.dataGridViewEndpointInteractionFactors.SelectionChanged += new System.EventHandler(this.dataGridViewEndpointInteractionFactors_SelectionChanged);
            // 
            // InteractionsPerEndpointPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Name = "InteractionsPerEndpointPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).EndInit();
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpointInteractionFactors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFactorLevels;
        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.DataGridView dataGridViewEndpointInteractionFactors;
    }
}
