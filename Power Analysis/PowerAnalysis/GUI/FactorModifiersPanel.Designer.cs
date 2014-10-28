namespace AmigaPowerAnalysis.GUI {
    partial class FactorModifiersPanel {
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
            this.groupBoxFactorModifiers = new System.Windows.Forms.GroupBox();
            this.checkBoxUseFactorModifiers = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorModifiers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
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
            this.dataGridViewFactorModifiers.Size = new System.Drawing.Size(518, 383);
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
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(260, 383);
            this.dataGridViewEndpoints.TabIndex = 1;
            this.dataGridViewEndpoints.SelectionChanged += new System.EventHandler(this.dataGridViewEndpoints_SelectionChanged);
            // 
            // groupBoxFactorModifiers
            // 
            this.groupBoxFactorModifiers.AutoSize = true;
            this.groupBoxFactorModifiers.Controls.Add(this.checkBoxUseFactorModifiers);
            this.groupBoxFactorModifiers.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFactorModifiers.Location = new System.Drawing.Point(10, 10);
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
            this.checkBoxUseFactorModifiers.Size = new System.Drawing.Size(760, 17);
            this.checkBoxUseFactorModifiers.TabIndex = 8;
            this.checkBoxUseFactorModifiers.Text = "Are there large differences between levels of factors (modifiers) causing part of" +
    " the data to be less informative (e.g. counts below 5, fractions equal to 0 or 1" +
    "?";
            this.checkBoxUseFactorModifiers.UseVisualStyleBackColor = true;
            this.checkBoxUseFactorModifiers.CheckedChanged += new System.EventHandler(this.checkBoxUseFactorModifiers_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(10, 59);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewEndpoints);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewFactorModifiers);
            this.splitContainer1.Size = new System.Drawing.Size(782, 383);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 11;
            // 
            // ModifiersPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBoxFactorModifiers);
            this.Name = "ModifiersPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorModifiers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBoxFactorModifiers;
        private System.Windows.Forms.CheckBox checkBoxUseFactorModifiers;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
