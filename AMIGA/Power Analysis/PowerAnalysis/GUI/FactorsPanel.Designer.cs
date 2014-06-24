namespace AmigaPowerAnalysis.GUI {
    partial class FactorsPanel {
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
            this.buttonRemoveFactor = new System.Windows.Forms.Button();
            this.buttonAddFactor = new System.Windows.Forms.Button();
            this.buttonRemoveFactorLevel = new System.Windows.Forms.Button();
            this.addFactorLevelButton = new System.Windows.Forms.Button();
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
            this.dataGridViewFactorLevels.AllowUserToAddRows = false;
            this.dataGridViewFactorLevels.AllowUserToDeleteRows = false;
            this.dataGridViewFactorLevels.AllowUserToResizeRows = false;
            this.dataGridViewFactorLevels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFactorLevels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactorLevels.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactorLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactorLevels.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactorLevels.MultiSelect = false;
            this.dataGridViewFactorLevels.Name = "dataGridViewFactorLevels";
            this.dataGridViewFactorLevels.RowHeadersWidth = 24;
            this.dataGridViewFactorLevels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactorLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactorLevels.Size = new System.Drawing.Size(380, 374);
            this.dataGridViewFactorLevels.TabIndex = 4;
            this.dataGridViewFactorLevels.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewFactorLevels_CellValidating);
            this.dataGridViewFactorLevels.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewFactorLevels_DataError);
            // 
            // dataGridViewFactors
            // 
            this.dataGridViewFactors.AllowUserToAddRows = false;
            this.dataGridViewFactors.AllowUserToDeleteRows = false;
            this.dataGridViewFactors.AllowUserToResizeRows = false;
            this.dataGridViewFactors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactors.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactors.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFactors.MultiSelect = false;
            this.dataGridViewFactors.Name = "dataGridViewFactors";
            this.dataGridViewFactors.RowHeadersWidth = 24;
            this.dataGridViewFactors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactors.Size = new System.Drawing.Size(259, 374);
            this.dataGridViewFactors.TabIndex = 3;
            this.dataGridViewFactors.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridFactors_CellValidating);
            this.dataGridViewFactors.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridFactors_DataError);
            this.dataGridViewFactors.SelectionChanged += new System.EventHandler(this.dataGridFactors_SelectionChanged);
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
            this.splitContainerFactors.Panel1.Controls.Add(this.buttonRemoveFactor);
            this.splitContainerFactors.Panel1.Controls.Add(this.buttonAddFactor);
            this.splitContainerFactors.Panel1.Controls.Add(this.dataGridViewFactors);
            // 
            // splitContainerFactors.Panel2
            // 
            this.splitContainerFactors.Panel2.Controls.Add(this.buttonRemoveFactorLevel);
            this.splitContainerFactors.Panel2.Controls.Add(this.addFactorLevelButton);
            this.splitContainerFactors.Panel2.Controls.Add(this.dataGridViewFactorLevels);
            this.splitContainerFactors.Size = new System.Drawing.Size(643, 407);
            this.splitContainerFactors.SplitterDistance = 259;
            this.splitContainerFactors.TabIndex = 5;
            // 
            // buttonRemoveFactor
            // 
            this.buttonRemoveFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveFactor.Location = new System.Drawing.Point(134, 380);
            this.buttonRemoveFactor.Name = "buttonRemoveFactor";
            this.buttonRemoveFactor.Size = new System.Drawing.Size(125, 27);
            this.buttonRemoveFactor.TabIndex = 8;
            this.buttonRemoveFactor.Text = "Remove factor";
            this.buttonRemoveFactor.UseVisualStyleBackColor = true;
            this.buttonRemoveFactor.Click += new System.EventHandler(this.buttonRemoveFactor_Click);
            // 
            // buttonAddFactor
            // 
            this.buttonAddFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddFactor.Location = new System.Drawing.Point(16, 380);
            this.buttonAddFactor.Name = "buttonAddFactor";
            this.buttonAddFactor.Size = new System.Drawing.Size(112, 27);
            this.buttonAddFactor.TabIndex = 7;
            this.buttonAddFactor.Text = "Add factor";
            this.buttonAddFactor.UseVisualStyleBackColor = true;
            this.buttonAddFactor.Click += new System.EventHandler(this.buttonAddFactor_Click);
            // 
            // buttonRemoveFactorLevel
            // 
            this.buttonRemoveFactorLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveFactorLevel.Location = new System.Drawing.Point(255, 380);
            this.buttonRemoveFactorLevel.Name = "buttonRemoveFactorLevel";
            this.buttonRemoveFactorLevel.Size = new System.Drawing.Size(125, 27);
            this.buttonRemoveFactorLevel.TabIndex = 6;
            this.buttonRemoveFactorLevel.Text = "Remove factor level";
            this.buttonRemoveFactorLevel.UseVisualStyleBackColor = true;
            this.buttonRemoveFactorLevel.Click += new System.EventHandler(this.buttonRemoveFactorLevel_Click);
            // 
            // addFactorLevelButton
            // 
            this.addFactorLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addFactorLevelButton.Location = new System.Drawing.Point(137, 380);
            this.addFactorLevelButton.Name = "addFactorLevelButton";
            this.addFactorLevelButton.Size = new System.Drawing.Size(112, 27);
            this.addFactorLevelButton.TabIndex = 5;
            this.addFactorLevelButton.Text = "Add factor level";
            this.addFactorLevelButton.UseVisualStyleBackColor = true;
            this.addFactorLevelButton.Click += new System.EventHandler(this.addFactorLevelButton_Click);
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
        private System.Windows.Forms.Button buttonRemoveFactorLevel;
        private System.Windows.Forms.Button addFactorLevelButton;
        private System.Windows.Forms.Button buttonRemoveFactor;
        private System.Windows.Forms.Button buttonAddFactor;
    }
}
