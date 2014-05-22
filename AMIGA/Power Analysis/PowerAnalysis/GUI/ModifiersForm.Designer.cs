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
            this.dataGridViewModifiers = new System.Windows.Forms.DataGridView();
            this.dataGridViewEndpoints = new System.Windows.Forms.DataGridView();
            this.groupBoxBlockModifiers = new System.Windows.Forms.GroupBox();
            this.checkBoxUseMainPlotModifier = new System.Windows.Forms.CheckBox();
            this.checkBoxUseBlockModifier = new System.Windows.Forms.CheckBox();
            this.groupBoxFactorModifiers = new System.Windows.Forms.GroupBox();
            this.checkBoxUseFactorModifiers = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModifiers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            this.groupBoxBlockModifiers.SuspendLayout();
            this.groupBoxFactorModifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewModifiers
            // 
            this.dataGridViewModifiers.AllowUserToAddRows = false;
            this.dataGridViewModifiers.AllowUserToDeleteRows = false;
            this.dataGridViewModifiers.AllowUserToResizeRows = false;
            this.dataGridViewModifiers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewModifiers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModifiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewModifiers.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewModifiers.MultiSelect = false;
            this.dataGridViewModifiers.Name = "dataGridViewModifiers";
            this.dataGridViewModifiers.ReadOnly = true;
            this.dataGridViewModifiers.RowHeadersWidth = 24;
            this.dataGridViewModifiers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewModifiers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewModifiers.Size = new System.Drawing.Size(518, 299);
            this.dataGridViewModifiers.TabIndex = 0;
            // 
            // dataGridViewEndpoints
            // 
            this.dataGridViewEndpoints.AllowUserToAddRows = false;
            this.dataGridViewEndpoints.AllowUserToDeleteRows = false;
            this.dataGridViewEndpoints.AllowUserToResizeRows = false;
            this.dataGridViewEndpoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(260, 299);
            this.dataGridViewEndpoints.TabIndex = 1;
            this.dataGridViewEndpoints.SelectionChanged += new System.EventHandler(this.dataGridViewEndpoints_SelectionChanged);
            // 
            // groupBoxBlockModifiers
            // 
            this.groupBoxBlockModifiers.AutoSize = true;
            this.groupBoxBlockModifiers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxBlockModifiers.Controls.Add(this.checkBoxUseBlockModifier);
            this.groupBoxBlockModifiers.Controls.Add(this.checkBoxUseMainPlotModifier);
            this.groupBoxBlockModifiers.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxBlockModifiers.Location = new System.Drawing.Point(10, 10);
            this.groupBoxBlockModifiers.Name = "groupBoxBlockModifiers";
            this.groupBoxBlockModifiers.Size = new System.Drawing.Size(782, 78);
            this.groupBoxBlockModifiers.TabIndex = 9;
            this.groupBoxBlockModifiers.TabStop = false;
            this.groupBoxBlockModifiers.Text = "Block modifier";
            // 
            // checkBoxUseMainPlotModifier
            // 
            this.checkBoxUseMainPlotModifier.AutoSize = true;
            this.checkBoxUseMainPlotModifier.Location = new System.Drawing.Point(6, 42);
            this.checkBoxUseMainPlotModifier.Name = "checkBoxUseMainPlotModifier";
            this.checkBoxUseMainPlotModifier.Size = new System.Drawing.Size(453, 17);
            this.checkBoxUseMainPlotModifier.TabIndex = 8;
            this.checkBoxUseMainPlotModifier.Text = "I there an expectation that other experimental factors will lower the mean compar" +
    "ator level?";
            this.checkBoxUseMainPlotModifier.UseVisualStyleBackColor = true;
            this.checkBoxUseMainPlotModifier.CheckedChanged += new System.EventHandler(this.checkBoxUseMainPlotModifier_CheckedChanged);
            // 
            // checkBoxUseBlockModifier
            // 
            this.checkBoxUseBlockModifier.AutoSize = true;
            this.checkBoxUseBlockModifier.Location = new System.Drawing.Point(6, 19);
            this.checkBoxUseBlockModifier.Name = "checkBoxUseBlockModifier";
            this.checkBoxUseBlockModifier.Size = new System.Drawing.Size(379, 17);
            this.checkBoxUseBlockModifier.TabIndex = 9;
            this.checkBoxUseBlockModifier.Text = "Is there an expectation that there will be large differences between blocks?";
            this.checkBoxUseBlockModifier.UseVisualStyleBackColor = true;
            this.checkBoxUseBlockModifier.CheckedChanged += new System.EventHandler(this.checkBoxUseBlockModifier_CheckedChanged);
            // 
            // groupBoxFactorModifiers
            // 
            this.groupBoxFactorModifiers.AutoSize = true;
            this.groupBoxFactorModifiers.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxFactorModifiers.Controls.Add(this.checkBoxUseFactorModifiers);
            this.groupBoxFactorModifiers.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFactorModifiers.Location = new System.Drawing.Point(10, 88);
            this.groupBoxFactorModifiers.Name = "groupBoxFactorModifiers";
            this.groupBoxFactorModifiers.Size = new System.Drawing.Size(782, 55);
            this.groupBoxFactorModifiers.TabIndex = 10;
            this.groupBoxFactorModifiers.TabStop = false;
            this.groupBoxFactorModifiers.Text = "Factor modifiers";
            // 
            // checkBoxUseFactorModifiers
            // 
            this.checkBoxUseFactorModifiers.AutoSize = true;
            this.checkBoxUseFactorModifiers.Location = new System.Drawing.Point(6, 19);
            this.checkBoxUseFactorModifiers.Name = "checkBoxUseFactorModifiers";
            this.checkBoxUseFactorModifiers.Size = new System.Drawing.Size(453, 17);
            this.checkBoxUseFactorModifiers.TabIndex = 8;
            this.checkBoxUseFactorModifiers.Text = "I there an expectation that other experimental factors will lower the mean compar" +
    "ator level?";
            this.checkBoxUseFactorModifiers.UseVisualStyleBackColor = true;
            this.checkBoxUseFactorModifiers.CheckedChanged += new System.EventHandler(this.checkBoxUseFactorModifiers_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 143);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewEndpoints);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewModifiers);
            this.splitContainer1.Size = new System.Drawing.Size(782, 299);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModifiers)).EndInit();
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

        private System.Windows.Forms.DataGridView dataGridViewModifiers;
        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
        private System.Windows.Forms.GroupBox groupBoxBlockModifiers;
        private System.Windows.Forms.CheckBox checkBoxUseMainPlotModifier;
        private System.Windows.Forms.CheckBox checkBoxUseBlockModifier;
        private System.Windows.Forms.GroupBox groupBoxFactorModifiers;
        private System.Windows.Forms.CheckBox checkBoxUseFactorModifiers;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
