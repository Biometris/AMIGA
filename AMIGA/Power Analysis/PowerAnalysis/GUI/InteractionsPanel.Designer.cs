﻿namespace AmigaPowerAnalysis.GUI {
    partial class InteractionsPanel {
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
            this.dataGridInteractions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInteractions)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridInteractions
            // 
            this.dataGridInteractions.AllowUserToAddRows = false;
            this.dataGridInteractions.AllowUserToDeleteRows = false;
            this.dataGridInteractions.AllowUserToResizeRows = false;
            this.dataGridInteractions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridInteractions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridInteractions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInteractions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridInteractions.Location = new System.Drawing.Point(10, 10);
            this.dataGridInteractions.Name = "dataGridInteractions";
            this.dataGridInteractions.RowHeadersVisible = false;
            this.dataGridInteractions.RowHeadersWidth = 24;
            this.dataGridInteractions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridInteractions.Size = new System.Drawing.Size(635, 478);
            this.dataGridInteractions.TabIndex = 0;
            this.dataGridInteractions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridInteractions_CellValueChanged);
            // 
            // InteractionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridInteractions);
            this.Name = "InteractionsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(655, 498);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInteractions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridInteractions;
    }
}