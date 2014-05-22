namespace AmigaPowerAnalysis.GUI {
    partial class InteractionsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InteractionsForm));
            this.dataGridInteractions = new System.Windows.Forms.DataGridView();
            this.textBoxTabDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInteractions)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridInteractions
            // 
            this.dataGridInteractions.AllowUserToAddRows = false;
            this.dataGridInteractions.AllowUserToDeleteRows = false;
            this.dataGridInteractions.AllowUserToResizeRows = false;
            this.dataGridInteractions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInteractions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridInteractions.Location = new System.Drawing.Point(10, 104);
            this.dataGridInteractions.Name = "dataGridInteractions";
            this.dataGridInteractions.RowHeadersVisible = false;
            this.dataGridInteractions.RowHeadersWidth = 24;
            this.dataGridInteractions.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridInteractions.Size = new System.Drawing.Size(635, 384);
            this.dataGridInteractions.TabIndex = 0;
            this.dataGridInteractions.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridInteractions_CellValueChanged);
            // 
            // textBoxTabDescription
            // 
            this.textBoxTabDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTabDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxTabDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabDescription.Location = new System.Drawing.Point(10, 10);
            this.textBoxTabDescription.Multiline = true;
            this.textBoxTabDescription.Name = "textBoxTabDescription";
            this.textBoxTabDescription.ReadOnly = true;
            this.textBoxTabDescription.Size = new System.Drawing.Size(635, 94);
            this.textBoxTabDescription.TabIndex = 9;
            this.textBoxTabDescription.Text = resources.GetString("textBoxTabDescription.Text");
            // 
            // InteractionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridInteractions);
            this.Controls.Add(this.textBoxTabDescription);
            this.Name = "InteractionsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(655, 498);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInteractions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridInteractions;
        private System.Windows.Forms.TextBox textBoxTabDescription;
    }
}
