namespace AmigaPowerAnalysis.GUI {
    partial class EndpointsDataForm {
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridEndpoints
            // 
            this.dataGridViewEndpoints.AllowUserToAddRows = false;
            this.dataGridViewEndpoints.AllowUserToDeleteRows = false;
            this.dataGridViewEndpoints.AllowUserToResizeRows = false;
            this.dataGridViewEndpoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEndpoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEndpoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEndpoints.Location = new System.Drawing.Point(10, 10);
            this.dataGridViewEndpoints.MultiSelect = false;
            this.dataGridViewEndpoints.Name = "dataGridEndpoints";
            this.dataGridViewEndpoints.RowHeadersVisible = false;
            this.dataGridViewEndpoints.RowHeadersWidth = 24;
            this.dataGridViewEndpoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(854, 182);
            this.dataGridViewEndpoints.TabIndex = 2;
            this.dataGridViewEndpoints.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridEndpoints_UserAddedRow);
            this.dataGridViewEndpoints.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridEndpoints_UserDeletingRow);
            // 
            // EndpointsDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridViewEndpoints);
            this.Name = "EndpointsDataForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
    }
}
