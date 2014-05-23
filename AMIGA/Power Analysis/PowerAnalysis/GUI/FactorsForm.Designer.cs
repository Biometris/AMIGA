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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FactorsForm));
            this.dataGridViewFactorLevels = new System.Windows.Forms.DataGridView();
            this.dataGridFactors = new System.Windows.Forms.DataGridView();
            this.splitContainerFactors = new System.Windows.Forms.SplitContainer();
            this.panelTabDescription = new System.Windows.Forms.Panel();
            this.textBoxTabDescription = new System.Windows.Forms.TextBox();
            this.textBoxTabTitle = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFactors)).BeginInit();
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
            // 
            // dataGridFactors
            // 
            this.dataGridFactors.AllowUserToResizeRows = false;
            this.dataGridFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridFactors.BackgroundColor = System.Drawing.Color.White;
            this.dataGridFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridFactors.Location = new System.Drawing.Point(0, 0);
            this.dataGridFactors.MultiSelect = false;
            this.dataGridFactors.Name = "dataGridFactors";
            this.dataGridFactors.RowHeadersWidth = 24;
            this.dataGridFactors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridFactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridFactors.Size = new System.Drawing.Size(259, 310);
            this.dataGridFactors.TabIndex = 3;
            this.dataGridFactors.SelectionChanged += new System.EventHandler(this.dataGridFactors_SelectionChanged);
            // 
            // splitContainerFactors
            // 
            this.splitContainerFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFactors.Location = new System.Drawing.Point(10, 107);
            this.splitContainerFactors.Name = "splitContainerFactors";
            // 
            // splitContainerFactors.Panel1
            // 
            this.splitContainerFactors.Panel1.Controls.Add(this.dataGridFactors);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFactors)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridFactors;
        private System.Windows.Forms.SplitContainer splitContainerFactors;
        private System.Windows.Forms.Panel panelTabDescription;
        private System.Windows.Forms.TextBox textBoxTabDescription;
        private System.Windows.Forms.TextBox textBoxTabTitle;
    }
}
