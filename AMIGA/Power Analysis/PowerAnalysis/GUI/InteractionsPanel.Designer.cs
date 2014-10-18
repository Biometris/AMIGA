namespace AmigaPowerAnalysis.GUI {
    partial class InteractionsPanel {
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridViewFactors = new System.Windows.Forms.DataGridView();
            this.dataGridViewInteractionFactorLevelCombinations = new System.Windows.Forms.DataGridView();
            this.checkBoxUseInteractions = new System.Windows.Forms.CheckBox();
            this.checkBoxUseDefaultInteractions = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInteractionFactorLevelCombinations)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(13, 59);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridViewFactors);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dataGridViewInteractionFactorLevelCombinations);
            this.splitContainer.Size = new System.Drawing.Size(629, 426);
            this.splitContainer.SplitterDistance = 210;
            this.splitContainer.TabIndex = 10;
            // 
            // dataGridViewFactors
            // 
            this.dataGridViewFactors.AllowUserToAddRows = false;
            this.dataGridViewFactors.AllowUserToDeleteRows = false;
            this.dataGridViewFactors.AllowUserToResizeRows = false;
            this.dataGridViewFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactors.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFactors.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactors.MultiSelect = false;
            this.dataGridViewFactors.Name = "dataGridViewFactors";
            this.dataGridViewFactors.RowHeadersVisible = false;
            this.dataGridViewFactors.RowHeadersWidth = 24;
            this.dataGridViewFactors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactors.Size = new System.Drawing.Size(210, 426);
            this.dataGridViewFactors.TabIndex = 3;
            this.dataGridViewFactors.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFactors_CellValueChanged);
            this.dataGridViewFactors.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewFactors_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewInteractionFactorLevelCombinations
            // 
            this.dataGridViewInteractionFactorLevelCombinations.AllowUserToAddRows = false;
            this.dataGridViewInteractionFactorLevelCombinations.AllowUserToDeleteRows = false;
            this.dataGridViewInteractionFactorLevelCombinations.AllowUserToResizeRows = false;
            this.dataGridViewInteractionFactorLevelCombinations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewInteractionFactorLevelCombinations.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewInteractionFactorLevelCombinations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInteractionFactorLevelCombinations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInteractionFactorLevelCombinations.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewInteractionFactorLevelCombinations.MultiSelect = false;
            this.dataGridViewInteractionFactorLevelCombinations.Name = "dataGridViewInteractionFactorLevelCombinations";
            this.dataGridViewInteractionFactorLevelCombinations.RowHeadersVisible = false;
            this.dataGridViewInteractionFactorLevelCombinations.RowHeadersWidth = 24;
            this.dataGridViewInteractionFactorLevelCombinations.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewInteractionFactorLevelCombinations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInteractionFactorLevelCombinations.Size = new System.Drawing.Size(415, 426);
            this.dataGridViewInteractionFactorLevelCombinations.TabIndex = 5;
            this.dataGridViewInteractionFactorLevelCombinations.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInteractionFactorLevelCombinations_CellValueChanged);
            this.dataGridViewInteractionFactorLevelCombinations.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewInteractionFactorLevelCombinations_CurrentCellDirtyStateChanged);
            // 
            // checkBoxUseInteractions
            // 
            this.checkBoxUseInteractions.AutoSize = true;
            this.checkBoxUseInteractions.Location = new System.Drawing.Point(13, 13);
            this.checkBoxUseInteractions.Name = "checkBoxUseInteractions";
            this.checkBoxUseInteractions.Size = new System.Drawing.Size(407, 17);
            this.checkBoxUseInteractions.TabIndex = 7;
            this.checkBoxUseInteractions.Text = "Exclude data from the GMO vs. CMP comparison based on selected factor levels";
            this.checkBoxUseInteractions.UseVisualStyleBackColor = true;
            this.checkBoxUseInteractions.CheckedChanged += new System.EventHandler(this.checkBoxUseInteractions_CheckedChanged);
            // 
            // checkBoxUseDefaultInteractions
            // 
            this.checkBoxUseDefaultInteractions.AutoSize = true;
            this.checkBoxUseDefaultInteractions.Location = new System.Drawing.Point(13, 36);
            this.checkBoxUseDefaultInteractions.Name = "checkBoxUseDefaultInteractions";
            this.checkBoxUseDefaultInteractions.Size = new System.Drawing.Size(261, 17);
            this.checkBoxUseDefaultInteractions.TabIndex = 6;
            this.checkBoxUseDefaultInteractions.Text = "Use the selection specified below for all endpoints";
            this.checkBoxUseDefaultInteractions.UseVisualStyleBackColor = true;
            this.checkBoxUseDefaultInteractions.CheckedChanged += new System.EventHandler(this.checkBoxUseDefaultInteractions_CheckedChanged);
            // 
            // InteractionsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.checkBoxUseDefaultInteractions);
            this.Controls.Add(this.checkBoxUseInteractions);
            this.Name = "InteractionsPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(655, 498);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInteractionFactorLevelCombinations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dataGridViewFactors;
        private System.Windows.Forms.DataGridView dataGridViewInteractionFactorLevelCombinations;
        private System.Windows.Forms.CheckBox checkBoxUseInteractions;
        private System.Windows.Forms.CheckBox checkBoxUseDefaultInteractions;
    }
}
