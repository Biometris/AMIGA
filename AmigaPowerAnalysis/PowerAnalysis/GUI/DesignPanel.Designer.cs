namespace AmigaPowerAnalysis.GUI {
    partial class DesignPanel {
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
            this.groupBoxTypeOfDesign = new System.Windows.Forms.GroupBox();
            this.radioButtonSplitPlot = new System.Windows.Forms.RadioButton();
            this.radioButtonRandomizedCompleteBlocks = new System.Windows.Forms.RadioButton();
            this.radioButtonCompletelyRandomized = new System.Windows.Forms.RadioButton();
            this.dataGridViewFactors = new System.Windows.Forms.DataGridView();
            this.groupBoxTypeOfDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTypeOfDesign
            // 
            this.groupBoxTypeOfDesign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTypeOfDesign.AutoSize = true;
            this.groupBoxTypeOfDesign.Controls.Add(this.radioButtonSplitPlot);
            this.groupBoxTypeOfDesign.Controls.Add(this.radioButtonRandomizedCompleteBlocks);
            this.groupBoxTypeOfDesign.Controls.Add(this.radioButtonCompletelyRandomized);
            this.groupBoxTypeOfDesign.Location = new System.Drawing.Point(13, 13);
            this.groupBoxTypeOfDesign.Name = "groupBoxTypeOfDesign";
            this.groupBoxTypeOfDesign.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBoxTypeOfDesign.Size = new System.Drawing.Size(643, 100);
            this.groupBoxTypeOfDesign.TabIndex = 5;
            this.groupBoxTypeOfDesign.TabStop = false;
            this.groupBoxTypeOfDesign.Text = "Type of design";
            // 
            // radioButtonSplitPlot
            // 
            this.radioButtonSplitPlot.AutoSize = true;
            this.radioButtonSplitPlot.Location = new System.Drawing.Point(11, 67);
            this.radioButtonSplitPlot.Name = "radioButtonSplitPlot";
            this.radioButtonSplitPlot.Size = new System.Drawing.Size(168, 17);
            this.radioButtonSplitPlot.TabIndex = 2;
            this.radioButtonSplitPlot.TabStop = true;
            this.radioButtonSplitPlot.Text = "Split plot (not yet implemented)";
            this.radioButtonSplitPlot.UseVisualStyleBackColor = true;
            this.radioButtonSplitPlot.CheckedChanged += new System.EventHandler(this.radioButtonTypeOfDesign_CheckedChanged);
            // 
            // radioButtonRandomizedCompleteBlocks
            // 
            this.radioButtonRandomizedCompleteBlocks.AutoSize = true;
            this.radioButtonRandomizedCompleteBlocks.Location = new System.Drawing.Point(11, 44);
            this.radioButtonRandomizedCompleteBlocks.Name = "radioButtonRandomizedCompleteBlocks";
            this.radioButtonRandomizedCompleteBlocks.Size = new System.Drawing.Size(164, 17);
            this.radioButtonRandomizedCompleteBlocks.TabIndex = 1;
            this.radioButtonRandomizedCompleteBlocks.TabStop = true;
            this.radioButtonRandomizedCompleteBlocks.Text = "Randomized complete blocks";
            this.radioButtonRandomizedCompleteBlocks.UseVisualStyleBackColor = true;
            this.radioButtonRandomizedCompleteBlocks.CheckedChanged += new System.EventHandler(this.radioButtonTypeOfDesign_CheckedChanged);
            // 
            // radioButtonCompletelyRandomized
            // 
            this.radioButtonCompletelyRandomized.AutoSize = true;
            this.radioButtonCompletelyRandomized.Location = new System.Drawing.Point(11, 21);
            this.radioButtonCompletelyRandomized.Name = "radioButtonCompletelyRandomized";
            this.radioButtonCompletelyRandomized.Size = new System.Drawing.Size(133, 17);
            this.radioButtonCompletelyRandomized.TabIndex = 0;
            this.radioButtonCompletelyRandomized.TabStop = true;
            this.radioButtonCompletelyRandomized.Text = "Completely randomized";
            this.radioButtonCompletelyRandomized.UseVisualStyleBackColor = true;
            this.radioButtonCompletelyRandomized.CheckedChanged += new System.EventHandler(this.radioButtonTypeOfDesign_CheckedChanged);
            // 
            // dataGridViewFactors
            // 
            this.dataGridViewFactors.AllowUserToAddRows = false;
            this.dataGridViewFactors.AllowUserToDeleteRows = false;
            this.dataGridViewFactors.AllowUserToResizeRows = false;
            this.dataGridViewFactors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactors.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactors.Location = new System.Drawing.Point(13, 119);
            this.dataGridViewFactors.MultiSelect = false;
            this.dataGridViewFactors.Name = "dataGridViewFactors";
            this.dataGridViewFactors.RowHeadersVisible = false;
            this.dataGridViewFactors.RowHeadersWidth = 24;
            this.dataGridViewFactors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactors.Size = new System.Drawing.Size(643, 295);
            this.dataGridViewFactors.TabIndex = 3;
            this.dataGridViewFactors.SelectionChanged += new System.EventHandler(this.dataGridFactors_SelectionChanged);
            // 
            // DesignPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridViewFactors);
            this.Controls.Add(this.groupBoxTypeOfDesign);
            this.Name = "DesignPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(663, 427);
            this.groupBoxTypeOfDesign.ResumeLayout(false);
            this.groupBoxTypeOfDesign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTypeOfDesign;
        private System.Windows.Forms.RadioButton radioButtonSplitPlot;
        private System.Windows.Forms.RadioButton radioButtonRandomizedCompleteBlocks;
        private System.Windows.Forms.RadioButton radioButtonCompletelyRandomized;
        private System.Windows.Forms.DataGridView dataGridViewFactors;
    }
}
