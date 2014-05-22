﻿namespace AmigaPowerAnalysis.GUI {
    partial class DesignForm {
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
            this.groupBoxTypeOfDesign = new System.Windows.Forms.GroupBox();
            this.radioButtonSplitPlot = new System.Windows.Forms.RadioButton();
            this.radioButtonRandomizedCompleteBlocks = new System.Windows.Forms.RadioButton();
            this.radioButtonCompletelyRandomized = new System.Windows.Forms.RadioButton();
            this.dataGridViewFactorLevels = new System.Windows.Forms.DataGridView();
            this.dataGridViewFactors = new System.Windows.Forms.DataGridView();
            this.checkBoxUseDefaultInteractions = new System.Windows.Forms.CheckBox();
            this.groupBoxInteractions = new System.Windows.Forms.GroupBox();
            this.checkBoxUseInteractions = new System.Windows.Forms.CheckBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.groupBoxTypeOfDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).BeginInit();
            this.groupBoxInteractions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTypeOfDesign
            // 
            this.groupBoxTypeOfDesign.AutoSize = true;
            this.groupBoxTypeOfDesign.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxTypeOfDesign.Controls.Add(this.radioButtonSplitPlot);
            this.groupBoxTypeOfDesign.Controls.Add(this.radioButtonRandomizedCompleteBlocks);
            this.groupBoxTypeOfDesign.Controls.Add(this.radioButtonCompletelyRandomized);
            this.groupBoxTypeOfDesign.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTypeOfDesign.Location = new System.Drawing.Point(10, 10);
            this.groupBoxTypeOfDesign.Name = "groupBoxTypeOfDesign";
            this.groupBoxTypeOfDesign.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBoxTypeOfDesign.Size = new System.Drawing.Size(643, 100);
            this.groupBoxTypeOfDesign.TabIndex = 5;
            this.groupBoxTypeOfDesign.TabStop = false;
            this.groupBoxTypeOfDesign.Text = "Type of design";
            // 
            // radioButtonSplitPlot
            // 
            this.radioButtonSplitPlot.AutoSize = true;
            this.radioButtonSplitPlot.Location = new System.Drawing.Point(11, 67);
            this.radioButtonSplitPlot.Name = "radioButtonSplitPlot";
            this.radioButtonSplitPlot.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSplitPlot.TabIndex = 2;
            this.radioButtonSplitPlot.TabStop = true;
            this.radioButtonSplitPlot.Text = "Split plot";
            this.radioButtonSplitPlot.UseVisualStyleBackColor = true;
            this.radioButtonSplitPlot.CheckedChanged += new System.EventHandler(this.radioButtonTypeOfDesign_CheckedChanged);
            // 
            // radioButtonRandomizedCompleteBlocks
            // 
            this.radioButtonRandomizedCompleteBlocks.AutoSize = true;
            this.radioButtonRandomizedCompleteBlocks.Location = new System.Drawing.Point(11, 44);
            this.radioButtonRandomizedCompleteBlocks.Name = "radioButtonRandomizedCompleteBlocks";
            this.radioButtonRandomizedCompleteBlocks.Size = new System.Drawing.Size(164, 17);
            this.radioButtonRandomizedCompleteBlocks.TabIndex = 1;
            this.radioButtonRandomizedCompleteBlocks.TabStop = true;
            this.radioButtonRandomizedCompleteBlocks.Text = "Randomized complete blocks";
            this.radioButtonRandomizedCompleteBlocks.UseVisualStyleBackColor = true;
            this.radioButtonRandomizedCompleteBlocks.CheckedChanged += new System.EventHandler(this.radioButtonTypeOfDesign_CheckedChanged);
            // 
            // radioButtonCompletelyRandomized
            // 
            this.radioButtonCompletelyRandomized.AutoSize = true;
            this.radioButtonCompletelyRandomized.Location = new System.Drawing.Point(11, 21);
            this.radioButtonCompletelyRandomized.Name = "radioButtonCompletelyRandomized";
            this.radioButtonCompletelyRandomized.Size = new System.Drawing.Size(133, 17);
            this.radioButtonCompletelyRandomized.TabIndex = 0;
            this.radioButtonCompletelyRandomized.TabStop = true;
            this.radioButtonCompletelyRandomized.Text = "Completely randomized";
            this.radioButtonCompletelyRandomized.UseVisualStyleBackColor = true;
            this.radioButtonCompletelyRandomized.CheckedChanged += new System.EventHandler(this.radioButtonTypeOfDesign_CheckedChanged);
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
            this.dataGridViewFactorLevels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactorLevels.Location = new System.Drawing.Point(329, 193);
            this.dataGridViewFactorLevels.MultiSelect = false;
            this.dataGridViewFactorLevels.Name = "dataGridViewFactorLevels";
            this.dataGridViewFactorLevels.RowHeadersVisible = false;
            this.dataGridViewFactorLevels.RowHeadersWidth = 24;
            this.dataGridViewFactorLevels.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactorLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactorLevels.Size = new System.Drawing.Size(324, 224);
            this.dataGridViewFactorLevels.TabIndex = 4;
            // 
            // dataGridViewFactors
            // 
            this.dataGridViewFactors.AllowUserToAddRows = false;
            this.dataGridViewFactors.AllowUserToDeleteRows = false;
            this.dataGridViewFactors.AllowUserToResizeRows = false;
            this.dataGridViewFactors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewFactors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFactors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFactors.Location = new System.Drawing.Point(10, 193);
            this.dataGridViewFactors.MultiSelect = false;
            this.dataGridViewFactors.Name = "dataGridViewFactors";
            this.dataGridViewFactors.RowHeadersVisible = false;
            this.dataGridViewFactors.RowHeadersWidth = 24;
            this.dataGridViewFactors.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewFactors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFactors.Size = new System.Drawing.Size(313, 224);
            this.dataGridViewFactors.TabIndex = 3;
            this.dataGridViewFactors.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridFactors_CellValueChanged);
            this.dataGridViewFactors.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridFactors_CurrentCellDirtyStateChanged);
            this.dataGridViewFactors.SelectionChanged += new System.EventHandler(this.dataGridFactors_SelectionChanged);
            // 
            // checkBoxUseDefaultInteractions
            // 
            this.checkBoxUseDefaultInteractions.AutoSize = true;
            this.checkBoxUseDefaultInteractions.Location = new System.Drawing.Point(11, 43);
            this.checkBoxUseDefaultInteractions.Name = "checkBoxUseDefaultInteractions";
            this.checkBoxUseDefaultInteractions.Size = new System.Drawing.Size(276, 17);
            this.checkBoxUseDefaultInteractions.TabIndex = 6;
            this.checkBoxUseDefaultInteractions.Text = "Use the interactions specified below for all endpoints.";
            this.checkBoxUseDefaultInteractions.UseVisualStyleBackColor = true;
            this.checkBoxUseDefaultInteractions.CheckedChanged += new System.EventHandler(this.checkBoxUseDefaultInteractions_CheckedChanged);
            // 
            // groupBoxInteractions
            // 
            this.groupBoxInteractions.AutoSize = true;
            this.groupBoxInteractions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxInteractions.Controls.Add(this.checkBoxUseInteractions);
            this.groupBoxInteractions.Controls.Add(this.checkBoxUseDefaultInteractions);
            this.groupBoxInteractions.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInteractions.Location = new System.Drawing.Point(10, 110);
            this.groupBoxInteractions.Name = "groupBoxInteractions";
            this.groupBoxInteractions.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.groupBoxInteractions.Size = new System.Drawing.Size(643, 76);
            this.groupBoxInteractions.TabIndex = 7;
            this.groupBoxInteractions.TabStop = false;
            this.groupBoxInteractions.Text = "Interactions";
            // 
            // checkBoxUseInteractions
            // 
            this.checkBoxUseInteractions.AutoSize = true;
            this.checkBoxUseInteractions.Location = new System.Drawing.Point(11, 20);
            this.checkBoxUseInteractions.Name = "checkBoxUseInteractions";
            this.checkBoxUseInteractions.Size = new System.Drawing.Size(214, 17);
            this.checkBoxUseInteractions.TabIndex = 7;
            this.checkBoxUseInteractions.Text = "Do you expect interactions with variety?";
            this.checkBoxUseInteractions.UseVisualStyleBackColor = true;
            this.checkBoxUseInteractions.CheckedChanged += new System.EventHandler(this.checkBoxUseInteractions_CheckedChanged);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // DesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBoxInteractions);
            this.Controls.Add(this.groupBoxTypeOfDesign);
            this.Controls.Add(this.dataGridViewFactorLevels);
            this.Controls.Add(this.dataGridViewFactors);
            this.Name = "DesignForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(663, 427);
            this.groupBoxTypeOfDesign.ResumeLayout(false);
            this.groupBoxTypeOfDesign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactorLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFactors)).EndInit();
            this.groupBoxInteractions.ResumeLayout(false);
            this.groupBoxInteractions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTypeOfDesign;
        private System.Windows.Forms.RadioButton radioButtonSplitPlot;
        private System.Windows.Forms.RadioButton radioButtonRandomizedCompleteBlocks;
        private System.Windows.Forms.RadioButton radioButtonCompletelyRandomized;
        private System.Windows.Forms.DataGridView dataGridViewFactorLevels;
        private System.Windows.Forms.DataGridView dataGridViewFactors;
        private System.Windows.Forms.CheckBox checkBoxUseDefaultInteractions;
        private System.Windows.Forms.GroupBox groupBoxInteractions;
        private System.Windows.Forms.CheckBox checkBoxUseInteractions;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}
