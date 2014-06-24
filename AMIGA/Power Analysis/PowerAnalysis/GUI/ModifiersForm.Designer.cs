namespace AmigaPowerAnalysis.GUI {
    partial class ModifiersForm {
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
            this.dataGridViewFactorModifiers = new System.Windows.Forms.DataGridView();
            this.dataGridViewEndpoints = new System.Windows.Forms.DataGridView();
            this.groupBoxBlockModifiers = new System.Windows.Forms.GroupBox();
            this.textBoxCVForMainPlots = new System.Windows.Forms.TextBox();
            this.labelCVForMainPlots = new System.Windows.Forms.Label();
            this.textBoxCVForBlocks = new System.Windows.Forms.TextBox();
            this.labelCVForBlocks = new System.Windows.Forms.Label();
            this.checkBoxUseBlockModifier = new System.Windows.Forms.CheckBox();
            this.checkBoxUseMainPlotModifier = new System.Windows.Forms.CheckBox();
            this.groupBoxFactorModifiers = new System.Windows.Forms.GroupBox();
            this.checkBoxUseFactorModifiers = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorModifiers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            this.groupBoxBlockModifiers.SuspendLayout();
            this.groupBoxFactorModifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFactorModifiers
            // 
            this.dataGridViewFactorModifiers.AllowUserToAddRows = false;
            this.dataGridViewFactorModifiers.AllowUserToDeleteRows = false;
            this.dataGridViewFactorModifiers.AllowUserToResizeRows = false;
            this.dataGridViewFactorModifiers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactorModifiers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactorModifiers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactorModifiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFactorModifiers.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactorModifiers.MultiSelect = false;
            this.dataGridViewFactorModifiers.Name = "dataGridViewFactorModifiers";
            this.dataGridViewFactorModifiers.RowHeadersWidth = 24;
            this.dataGridViewFactorModifiers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactorModifiers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactorModifiers.Size = new System.Drawing.Size(518, 252);
            this.dataGridViewFactorModifiers.TabIndex = 0;
            this.dataGridViewFactorModifiers.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewFactorModifiers_DataError);
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
            this.dataGridViewEndpoints.RowHeadersVisible = false;
            this.dataGridViewEndpoints.RowHeadersWidth = 24;
            this.dataGridViewEndpoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(260, 252);
            this.dataGridViewEndpoints.TabIndex = 1;
            this.dataGridViewEndpoints.SelectionChanged += new System.EventHandler(this.dataGridViewEndpoints_SelectionChanged);
            // 
            // groupBoxBlockModifiers
            // 
            this.groupBoxBlockModifiers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBlockModifiers.AutoSize = true;
            this.groupBoxBlockModifiers.Controls.Add(this.textBoxCVForMainPlots);
            this.groupBoxBlockModifiers.Controls.Add(this.labelCVForMainPlots);
            this.groupBoxBlockModifiers.Controls.Add(this.textBoxCVForBlocks);
            this.groupBoxBlockModifiers.Controls.Add(this.labelCVForBlocks);
            this.groupBoxBlockModifiers.Controls.Add(this.checkBoxUseBlockModifier);
            this.groupBoxBlockModifiers.Controls.Add(this.checkBoxUseMainPlotModifier);
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
            this.textBoxCVForMainPlots.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCVForMainPlots_Validating);
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
            this.checkBoxUseBlockModifier.Size = new System.Drawing.Size(379, 17);
            this.checkBoxUseBlockModifier.TabIndex = 9;
            this.checkBoxUseBlockModifier.Text = "Is there an expectation that there will be large differences between blocks?";
            this.checkBoxUseBlockModifier.UseVisualStyleBackColor = true;
            this.checkBoxUseBlockModifier.CheckedChanged += new System.EventHandler(this.checkBoxUseBlockModifier_CheckedChanged);
            // 
            // checkBoxUseMainPlotModifier
            // 
            this.checkBoxUseMainPlotModifier.AutoSize = true;
            this.checkBoxUseMainPlotModifier.Location = new System.Drawing.Point(6, 68);
            this.checkBoxUseMainPlotModifier.Name = "checkBoxUseMainPlotModifier";
            this.checkBoxUseMainPlotModifier.Size = new System.Drawing.Size(395, 17);
            this.checkBoxUseMainPlotModifier.TabIndex = 8;
            this.checkBoxUseMainPlotModifier.Text = "Is there an expectation that there will be large differences between main plots?";
            this.checkBoxUseMainPlotModifier.UseVisualStyleBackColor = true;
            this.checkBoxUseMainPlotModifier.CheckedChanged += new System.EventHandler(this.checkBoxUseMainPlotModifier_CheckedChanged);
            // 
            // groupBoxFactorModifiers
            // 
            this.groupBoxFactorModifiers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFactorModifiers.AutoSize = true;
            this.groupBoxFactorModifiers.Controls.Add(this.checkBoxUseFactorModifiers);
            this.groupBoxFactorModifiers.Location = new System.Drawing.Point(10, 135);
            this.groupBoxFactorModifiers.Name = "groupBoxFactorModifiers";
            this.groupBoxFactorModifiers.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBoxFactorModifiers.Size = new System.Drawing.Size(782, 49);
            this.groupBoxFactorModifiers.TabIndex = 10;
            this.groupBoxFactorModifiers.TabStop = false;
            this.groupBoxFactorModifiers.Text = "Factor modifiers";
            // 
            // checkBoxUseFactorModifiers
            // 
            this.checkBoxUseFactorModifiers.AutoSize = true;
            this.checkBoxUseFactorModifiers.Location = new System.Drawing.Point(6, 19);
            this.checkBoxUseFactorModifiers.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.checkBoxUseFactorModifiers.Name = "checkBoxUseFactorModifiers";
            this.checkBoxUseFactorModifiers.Size = new System.Drawing.Size(458, 17);
            this.checkBoxUseFactorModifiers.TabIndex = 8;
            this.checkBoxUseFactorModifiers.Text = "Is there an expectation that other experimental factors will lower the mean compa" +
    "rator level?";
            this.checkBoxUseFactorModifiers.UseVisualStyleBackColor = true;
            this.checkBoxUseFactorModifiers.CheckedChanged += new System.EventHandler(this.checkBoxUseFactorModifiers_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(10, 190);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewEndpoints);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewFactorModifiers);
            this.splitContainer1.Size = new System.Drawing.Size(782, 252);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 11;
            // 
            // ModifiersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBoxFactorModifiers);
            this.Controls.Add(this.groupBoxBlockModifiers);
            this.Name = "ModifiersForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorModifiers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
            this.groupBoxBlockModifiers.ResumeLayout(false);
            this.groupBoxBlockModifiers.PerformLayout();
            this.groupBoxFactorModifiers.ResumeLayout(false);
            this.groupBoxFactorModifiers.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFactorModifiers;
        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
        private System.Windows.Forms.GroupBox groupBoxBlockModifiers;
        private System.Windows.Forms.CheckBox checkBoxUseMainPlotModifier;
        private System.Windows.Forms.CheckBox checkBoxUseBlockModifier;
        private System.Windows.Forms.GroupBox groupBoxFactorModifiers;
        private System.Windows.Forms.CheckBox checkBoxUseFactorModifiers;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxCVForMainPlots;
        private System.Windows.Forms.Label labelCVForMainPlots;
        private System.Windows.Forms.TextBox textBoxCVForBlocks;
        private System.Windows.Forms.Label labelCVForBlocks;
    }
}
