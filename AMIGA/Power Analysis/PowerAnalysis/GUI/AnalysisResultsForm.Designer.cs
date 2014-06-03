namespace AmigaPowerAnalysis.GUI {
    partial class AnalysisResultsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisResultsForm));
            this.panelTabDescription = new System.Windows.Forms.Panel();
            this.textBoxTabDescription = new System.Windows.Forms.TextBox();
            this.textBoxTabTitle = new System.Windows.Forms.TextBox();
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            this.dataGridViewComparisons = new System.Windows.Forms.DataGridView();
            this.panelTabDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTabDescription
            // 
            this.panelTabDescription.AutoSize = true;
            this.panelTabDescription.Controls.Add(this.textBoxTabDescription);
            this.panelTabDescription.Controls.Add(this.textBoxTabTitle);
            this.panelTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabDescription.Location = new System.Drawing.Point(10, 10);
            this.panelTabDescription.Name = "panelTabDescription";
            this.panelTabDescription.Size = new System.Drawing.Size(854, 97);
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
            this.textBoxTabDescription.Size = new System.Drawing.Size(854, 75);
            this.textBoxTabDescription.TabIndex = 6;
            this.textBoxTabDescription.Text = resources.GetString("textBoxTabDescription.Text");
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
            this.textBoxTabTitle.Size = new System.Drawing.Size(854, 22);
            this.textBoxTabTitle.TabIndex = 7;
            this.textBoxTabTitle.Text = "Tab title";
            // 
            // splitContainerComparisons
            // 
            this.splitContainerComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerComparisons.Location = new System.Drawing.Point(10, 107);
            this.splitContainerComparisons.Name = "splitContainerComparisons";
            // 
            // splitContainerComparisons.Panel1
            // 
            this.splitContainerComparisons.Panel1.Controls.Add(this.dataGridViewComparisons);
            this.splitContainerComparisons.Size = new System.Drawing.Size(854, 386);
            this.splitContainerComparisons.SplitterDistance = 283;
            this.splitContainerComparisons.TabIndex = 9;
            // 
            // dataGridViewComparisons
            // 
            this.dataGridViewComparisons.AllowUserToAddRows = false;
            this.dataGridViewComparisons.AllowUserToDeleteRows = false;
            this.dataGridViewComparisons.AllowUserToResizeRows = false;
            this.dataGridViewComparisons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewComparisons.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewComparisons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewComparisons.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewComparisons.MultiSelect = false;
            this.dataGridViewComparisons.Name = "dataGridViewComparisons";
            this.dataGridViewComparisons.ReadOnly = true;
            this.dataGridViewComparisons.RowHeadersVisible = false;
            this.dataGridViewComparisons.RowHeadersWidth = 24;
            this.dataGridViewComparisons.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewComparisons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComparisons.Size = new System.Drawing.Size(283, 386);
            this.dataGridViewComparisons.TabIndex = 3;
            this.dataGridViewComparisons.SelectionChanged += new System.EventHandler(this.dataGridViewComparisons_SelectionChanged);
            // 
            // AnalysisResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Controls.Add(this.panelTabDescription);
            this.Name = "AnalysisResultsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            this.panelTabDescription.ResumeLayout(false);
            this.panelTabDescription.PerformLayout();
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTabDescription;
        private System.Windows.Forms.TextBox textBoxTabDescription;
        private System.Windows.Forms.TextBox textBoxTabTitle;
        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
    }
}
