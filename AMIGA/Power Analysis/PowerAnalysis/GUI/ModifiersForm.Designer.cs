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
            this.groupBoxModifier = new System.Windows.Forms.GroupBox();
            this.checkBoxUseModifier = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModifiers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            this.groupBoxModifier.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewModifiers
            // 
            this.dataGridViewModifiers.AllowUserToAddRows = false;
            this.dataGridViewModifiers.AllowUserToDeleteRows = false;
            this.dataGridViewModifiers.AllowUserToResizeRows = false;
            this.dataGridViewModifiers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewModifiers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewModifiers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModifiers.Location = new System.Drawing.Point(264, 78);
            this.dataGridViewModifiers.MultiSelect = false;
            this.dataGridViewModifiers.Name = "dataGridViewModifiers";
            this.dataGridViewModifiers.ReadOnly = true;
            this.dataGridViewModifiers.RowHeadersWidth = 24;
            this.dataGridViewModifiers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewModifiers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewModifiers.Size = new System.Drawing.Size(525, 364);
            this.dataGridViewModifiers.TabIndex = 0;
            // 
            // dataGridViewEndpoints
            // 
            this.dataGridViewEndpoints.AllowUserToAddRows = false;
            this.dataGridViewEndpoints.AllowUserToDeleteRows = false;
            this.dataGridViewEndpoints.AllowUserToResizeRows = false;
            this.dataGridViewEndpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEndpoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEndpoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEndpoints.Location = new System.Drawing.Point(13, 78);
            this.dataGridViewEndpoints.MultiSelect = false;
            this.dataGridViewEndpoints.Name = "dataGridViewEndpoints";
            this.dataGridViewEndpoints.ReadOnly = true;
            this.dataGridViewEndpoints.RowHeadersVisible = false;
            this.dataGridViewEndpoints.RowHeadersWidth = 24;
            this.dataGridViewEndpoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(245, 364);
            this.dataGridViewEndpoints.TabIndex = 1;
            this.dataGridViewEndpoints.SelectionChanged += new System.EventHandler(this.dataGridViewEndpoints_SelectionChanged);
            // 
            // groupBoxModifier
            // 
            this.groupBoxModifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxModifier.Controls.Add(this.checkBoxUseModifier);
            this.groupBoxModifier.Location = new System.Drawing.Point(13, 13);
            this.groupBoxModifier.Name = "groupBoxModifier";
            this.groupBoxModifier.Size = new System.Drawing.Size(776, 50);
            this.groupBoxModifier.TabIndex = 9;
            this.groupBoxModifier.TabStop = false;
            this.groupBoxModifier.Text = "Modifier";
            // 
            // checkBoxUseModifier
            // 
            this.checkBoxUseModifier.AutoSize = true;
            this.checkBoxUseModifier.Location = new System.Drawing.Point(6, 19);
            this.checkBoxUseModifier.Name = "checkBoxUseModifier";
            this.checkBoxUseModifier.Size = new System.Drawing.Size(453, 17);
            this.checkBoxUseModifier.TabIndex = 8;
            this.checkBoxUseModifier.Text = "I there an expectation that other experimental factors will lower the mean compar" +
    "ator level?";
            this.checkBoxUseModifier.UseVisualStyleBackColor = true;
            this.checkBoxUseModifier.CheckedChanged += new System.EventHandler(this.checkBoxUseModifier_CheckedChanged);
            // 
            // ModifiersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxModifier);
            this.Controls.Add(this.dataGridViewEndpoints);
            this.Controls.Add(this.dataGridViewModifiers);
            this.Name = "ModifiersForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModifiers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
            this.groupBoxModifier.ResumeLayout(false);
            this.groupBoxModifier.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewModifiers;
        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
        private System.Windows.Forms.GroupBox groupBoxModifier;
        private System.Windows.Forms.CheckBox checkBoxUseModifier;
    }
}
