namespace AmigaPowerAnalysis.GUI {
    partial class FactorsForm {
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
            this.dataGridViewFactors = new System.Windows.Forms.DataGridView();
            this.splitContainerFactors = new System.Windows.Forms.SplitContainer();
            this.panelTabDescription = new System.Windows.Forms.Panel();
            this.textBoxTabDescription = new System.Windows.Forms.TextBox();
            this.textBoxTabTitle = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFactors)).BeginInit();
            this.splitContainerFactors.Panel1.SuspendLayout();
            this.splitContainerFactors.Panel2.SuspendLayout();
            this.splitContainerFactors.SuspendLayout();
            this.panelTabDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewFactorLevels
            // 
            this.dataGridViewFactorLevels.AllowUserToResizeRows = false;
            this.dataGridViewFactorLevels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactorLevels.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactorLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactorLevels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFactorLevels.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactorLevels.MultiSelect = false;
            this.dataGridViewFactorLevels.Name = "dataGridViewFactorLevels";
            this.dataGridViewFactorLevels.RowHeadersWidth = 24;
            this.dataGridViewFactorLevels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactorLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactorLevels.Size = new System.Drawing.Size(380, 310);
            this.dataGridViewFactorLevels.TabIndex = 4;
            this.dataGridViewFactorLevels.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewFactorLevels_CellValidating);
            this.dataGridViewFactorLevels.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewFactorLevels_DataError);
            this.dataGridViewFactorLevels.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridViewFactorLevels_UserDeletingRow);
            // 
            // dataGridViewFactors
            // 
            this.dataGridViewFactors.AllowUserToResizeRows = false;
            this.dataGridViewFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactors.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFactors.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactors.MultiSelect = false;
            this.dataGridViewFactors.Name = "dataGridViewFactors";
            this.dataGridViewFactors.RowHeadersWidth = 24;
            this.dataGridViewFactors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactors.Size = new System.Drawing.Size(259, 310);
            this.dataGridViewFactors.TabIndex = 3;
            this.dataGridViewFactors.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridFactors_CellValidating);
            this.dataGridViewFactors.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridFactors_DataError);
            this.dataGridViewFactors.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridFactors_RowsAdded);
            this.dataGridViewFactors.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridFactors_RowsRemoved);
            this.dataGridViewFactors.SelectionChanged += new System.EventHandler(this.dataGridFactors_SelectionChanged);
            // 
            // splitContainerFactors
            // 
            this.splitContainerFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFactors.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerFactors.Location = new System.Drawing.Point(10, 107);
            this.splitContainerFactors.Name = "splitContainerFactors";
            // 
            // splitContainerFactors.Panel1
            // 
            this.splitContainerFactors.Panel1.Controls.Add(this.dataGridViewFactors);
            // 
            // splitContainerFactors.Panel2
            // 
            this.splitContainerFactors.Panel2.Controls.Add(this.dataGridViewFactorLevels);
            this.splitContainerFactors.Size = new System.Drawing.Size(643, 310);
            this.splitContainerFactors.SplitterDistance = 259;
            this.splitContainerFactors.TabIndex = 5;
            // 
            // panelTabDescription
            // 
            this.panelTabDescription.AutoSize = true;
            this.panelTabDescription.Controls.Add(this.textBoxTabDescription);
            this.panelTabDescription.Controls.Add(this.textBoxTabTitle);
            this.panelTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabDescription.Location = new System.Drawing.Point(10, 10);
            this.panelTabDescription.Name = "panelTabDescription";
            this.panelTabDescription.Size = new System.Drawing.Size(643, 97);
            this.panelTabDescription.TabIndex = 8;
            // 
            // textBoxTabDescription
            // 
            this.textBoxTabDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTabDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxTabDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabDescription.Location = new System.Drawing.Point(0, 22);
            this.textBoxTabDescription.Multiline = true;
            this.textBoxTabDescription.Name = "textBoxTabDescription";
            this.textBoxTabDescription.ReadOnly = true;
            this.textBoxTabDescription.Size = new System.Drawing.Size(643, 75);
            this.textBoxTabDescription.TabIndex = 6;
            this.textBoxTabDescription.Text = "Description";
            // 
            // textBoxTabTitle
            // 
            this.textBoxTabTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTabTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxTabTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabTitle.Location = new System.Drawing.Point(0, 0);
            this.textBoxTabTitle.Name = "textBoxTabTitle";
            this.textBoxTabTitle.ReadOnly = true;
            this.textBoxTabTitle.Size = new System.Drawing.Size(643, 22);
            this.textBoxTabTitle.TabIndex = 7;
            this.textBoxTabTitle.Text = "Tab title";
            // 
            // FactorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerFactors);
            this.Controls.Add(this.panelTabDescription);
            this.Name = "FactorsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(663, 427);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).EndInit();
            this.splitContainerFactors.Panel1.ResumeLayout(false);
            this.splitContainerFactors.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFactors)).EndInit();
            this.splitContainerFactors.ResumeLayout(false);
            this.panelTabDescription.ResumeLayout(false);
            this.panelTabDescription.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFactorLevels;
        private System.Windows.Forms.DataGridView dataGridViewFactors;
        private System.Windows.Forms.SplitContainer splitContainerFactors;
        private System.Windows.Forms.Panel panelTabDescription;
        private System.Windows.Forms.TextBox textBoxTabDescription;
        private System.Windows.Forms.TextBox textBoxTabTitle;
    }
}
