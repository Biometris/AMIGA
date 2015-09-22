namespace AmigaPowerAnalysis.GUI {
    partial class BlockModifiersPanel {
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
            this.groupBoxBlockModifiers = new System.Windows.Forms.GroupBox();
            this.textBoxCVForMainPlots = new System.Windows.Forms.TextBox();
            this.labelCVForMainPlots = new System.Windows.Forms.Label();
            this.textBoxCVForBlocks = new System.Windows.Forms.TextBox();
            this.labelCVForBlocks = new System.Windows.Forms.Label();
            this.checkBoxUseBlockModifier = new System.Windows.Forms.CheckBox();
            this.checkBoxUseMainPlotModifier = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            this.groupBoxBlockModifiers.SuspendLayout();
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
            this.dataGridViewEndpoints.Location = new System.Drawing.Point(10, 135);
            this.dataGridViewEndpoints.MultiSelect = false;
            this.dataGridViewEndpoints.Name = "dataGridViewEndpoints";
            this.dataGridViewEndpoints.RowHeadersVisible = false;
            this.dataGridViewEndpoints.RowHeadersWidth = 24;
            this.dataGridViewEndpoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(782, 307);
            this.dataGridViewEndpoints.TabIndex = 1;
            // 
            // groupBoxBlockModifiers
            // 
            this.groupBoxBlockModifiers.AutoSize = true;
            this.groupBoxBlockModifiers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxBlockModifiers.Controls.Add(this.textBoxCVForMainPlots);
            this.groupBoxBlockModifiers.Controls.Add(this.labelCVForMainPlots);
            this.groupBoxBlockModifiers.Controls.Add(this.textBoxCVForBlocks);
            this.groupBoxBlockModifiers.Controls.Add(this.labelCVForBlocks);
            this.groupBoxBlockModifiers.Controls.Add(this.checkBoxUseBlockModifier);
            this.groupBoxBlockModifiers.Controls.Add(this.checkBoxUseMainPlotModifier);
            this.groupBoxBlockModifiers.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxBlockModifiers.Location = new System.Drawing.Point(10, 10);
            this.groupBoxBlockModifiers.Name = "groupBoxBlockModifiers";
            this.groupBoxBlockModifiers.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBoxBlockModifiers.Size = new System.Drawing.Size(782, 125);
            this.groupBoxBlockModifiers.TabIndex = 9;
            this.groupBoxBlockModifiers.TabStop = false;
            this.groupBoxBlockModifiers.Text = "Block modifier";
            // 
            // textBoxCVForMainPlots
            // 
            this.textBoxCVForMainPlots.Location = new System.Drawing.Point(139, 92);
            this.textBoxCVForMainPlots.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.textBoxCVForMainPlots.Name = "textBoxCVForMainPlots";
            this.textBoxCVForMainPlots.Size = new System.Drawing.Size(85, 20);
            this.textBoxCVForMainPlots.TabIndex = 13;
            // 
            // labelCVForMainPlots
            // 
            this.labelCVForMainPlots.AutoSize = true;
            this.labelCVForMainPlots.Location = new System.Drawing.Point(6, 95);
            this.labelCVForMainPlots.Name = "labelCVForMainPlots";
            this.labelCVForMainPlots.Size = new System.Drawing.Size(126, 13);
            this.labelCVForMainPlots.TabIndex = 12;
            this.labelCVForMainPlots.Text = "Default CV for main plots:";
            // 
            // textBoxCVForBlocks
            // 
            this.textBoxCVForBlocks.Location = new System.Drawing.Point(139, 41);
            this.textBoxCVForBlocks.Name = "textBoxCVForBlocks";
            this.textBoxCVForBlocks.Size = new System.Drawing.Size(85, 20);
            this.textBoxCVForBlocks.TabIndex = 11;
            this.textBoxCVForBlocks.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCVForBlocks_Validating);
            // 
            // labelCVForBlocks
            // 
            this.labelCVForBlocks.AutoSize = true;
            this.labelCVForBlocks.Location = new System.Drawing.Point(6, 45);
            this.labelCVForBlocks.Name = "labelCVForBlocks";
            this.labelCVForBlocks.Size = new System.Drawing.Size(110, 13);
            this.labelCVForBlocks.TabIndex = 10;
            this.labelCVForBlocks.Text = "Default CV for blocks:";
            // 
            // checkBoxUseBlockModifier
            // 
            this.checkBoxUseBlockModifier.AutoSize = true;
            this.checkBoxUseBlockModifier.Location = new System.Drawing.Point(6, 18);
            this.checkBoxUseBlockModifier.Name = "checkBoxUseBlockModifier";
            this.checkBoxUseBlockModifier.Size = new System.Drawing.Size(553, 17);
            this.checkBoxUseBlockModifier.TabIndex = 9;
            this.checkBoxUseBlockModifier.Text = "Are there large differences between blocks causing part of the data to be less in" +
    "formative (e.g. counts below 5)?";
            this.checkBoxUseBlockModifier.UseVisualStyleBackColor = true;
            this.checkBoxUseBlockModifier.CheckedChanged += new System.EventHandler(this.checkBoxUseBlockModifier_CheckedChanged);
            // 
            // checkBoxUseMainPlotModifier
            // 
            this.checkBoxUseMainPlotModifier.AutoSize = true;
            this.checkBoxUseMainPlotModifier.Location = new System.Drawing.Point(6, 68);
            this.checkBoxUseMainPlotModifier.Name = "checkBoxUseMainPlotModifier";
            this.checkBoxUseMainPlotModifier.Size = new System.Drawing.Size(569, 17);
            this.checkBoxUseMainPlotModifier.TabIndex = 8;
            this.checkBoxUseMainPlotModifier.Text = "Are there large differences between main plots causing part of the data to be les" +
    "s informative (e.g. counts below 5)?";
            this.checkBoxUseMainPlotModifier.UseVisualStyleBackColor = true;
            this.checkBoxUseMainPlotModifier.CheckedChanged += new System.EventHandler(this.checkBoxUseMainPlotModifier_CheckedChanged);
            // 
            // BlockModifiersPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridViewEndpoints);
            this.Controls.Add(this.groupBoxBlockModifiers);
            this.Name = "BlockModifiersPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
            this.groupBoxBlockModifiers.ResumeLayout(false);
            this.groupBoxBlockModifiers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
        private System.Windows.Forms.GroupBox groupBoxBlockModifiers;
        private System.Windows.Forms.CheckBox checkBoxUseMainPlotModifier;
        private System.Windows.Forms.CheckBox checkBoxUseBlockModifier;
        private System.Windows.Forms.TextBox textBoxCVForMainPlots;
        private System.Windows.Forms.Label labelCVForMainPlots;
        private System.Windows.Forms.TextBox textBoxCVForBlocks;
        private System.Windows.Forms.Label labelCVForBlocks;
    }
}
