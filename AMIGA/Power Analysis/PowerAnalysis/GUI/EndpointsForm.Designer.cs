﻿namespace AmigaPowerAnalysis.GUI {
    partial class EndpointsForm {
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
            this.dataGridEndpoints = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEndpoints)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridEndpoints
            // 
            this.dataGridEndpoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridEndpoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridEndpoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEndpoints.Location = new System.Drawing.Point(3, 3);
            this.dataGridEndpoints.Name = "dataGridEndpoints";
            this.dataGridEndpoints.Size = new System.Drawing.Size(871, 179);
            this.dataGridEndpoints.TabIndex = 2;
            this.dataGridEndpoints.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridEndpoints_UserAddedRow);
            this.dataGridEndpoints.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridEndpoints_UserDeletingRow);
            // 
            // EndpointsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridEndpoints);
            this.Name = "EndpointsForm";
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEndpoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridEndpoints;
    }
}
