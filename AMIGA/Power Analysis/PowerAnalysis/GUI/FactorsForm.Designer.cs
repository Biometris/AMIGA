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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFactors)).BeginInit();
            this.splitContainerFactors.Panel1.SuspendLayout();
            this.splitContainerFactors.Panel2.SuspendLayout();
            this.splitContainerFactors.SuspendLayout();
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
            this.dataGridViewFactorLevels.Size = new System.Drawing.Size(380, 407);
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
            this.dataGridViewFactors.Size = new System.Drawing.Size(259, 407);
            this.dataGridViewFactors.TabIndex = 3;
            this.dataGridViewFactors.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridFactors_CellValidating);
            this.dataGridViewFactors.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridFactors_DataError);
            this.dataGridViewFactors.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridFactors_RowsAdded);
            this.dataGridViewFactors.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridFactors_RowsRemoved);
            this.dataGridViewFactors.SelectionChanged += new System.EventHandler(this.dataGridFactors_SelectionChanged);
            this.dataGridViewFactors.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridViewFactors_UserDeletingRow);
            // 
            // splitContainerFactors
            // 
            this.splitContainerFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFactors.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerFactors.Location = new System.Drawing.Point(10, 10);
            this.splitContainerFactors.Name = "splitContainerFactors";
            // 
            // splitContainerFactors.Panel1
            // 
            this.splitContainerFactors.Panel1.Controls.Add(this.dataGridViewFactors);
            // 
            // splitContainerFactors.Panel2
            // 
            this.splitContainerFactors.Panel2.Controls.Add(this.dataGridViewFactorLevels);
            this.splitContainerFactors.Size = new System.Drawing.Size(643, 407);
            this.splitContainerFactors.SplitterDistance = 259;
            this.splitContainerFactors.TabIndex = 5;
            // 
            // FactorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerFactors);
            this.Name = "FactorsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(663, 427);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).EndInit();
            this.splitContainerFactors.Panel1.ResumeLayout(false);
            this.splitContainerFactors.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFactors)).EndInit();
            this.splitContainerFactors.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFactorLevels;
        private System.Windows.Forms.DataGridView dataGridViewFactors;
        private System.Windows.Forms.SplitContainer splitContainerFactors;
    }
}
