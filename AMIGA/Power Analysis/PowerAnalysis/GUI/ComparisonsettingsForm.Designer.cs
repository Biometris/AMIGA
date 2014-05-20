namespace AmigaPowerAnalysis.GUI {
    partial class ComparisonSettingsControl {
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
            this.checkedListBoxGMOLevels = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxComparatorLevels = new System.Windows.Forms.CheckedListBox();
            this.labelGMOLevels = new System.Windows.Forms.Label();
            this.labelComparatorLevels = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkedListBoxGMOLevels
            // 
            this.checkedListBoxGMOLevels.FormattingEnabled = true;
            this.checkedListBoxGMOLevels.Location = new System.Drawing.Point(65, 96);
            this.checkedListBoxGMOLevels.Name = "checkedListBoxGMOLevels";
            this.checkedListBoxGMOLevels.Size = new System.Drawing.Size(145, 169);
            this.checkedListBoxGMOLevels.TabIndex = 0;
            // 
            // checkedListBoxComparatorLevels
            // 
            this.checkedListBoxComparatorLevels.FormattingEnabled = true;
            this.checkedListBoxComparatorLevels.Location = new System.Drawing.Point(299, 96);
            this.checkedListBoxComparatorLevels.Name = "checkedListBoxComparatorLevels";
            this.checkedListBoxComparatorLevels.Size = new System.Drawing.Size(145, 169);
            this.checkedListBoxComparatorLevels.TabIndex = 1;
            // 
            // labelGMOLevels
            // 
            this.labelGMOLevels.AutoSize = true;
            this.labelGMOLevels.Location = new System.Drawing.Point(65, 77);
            this.labelGMOLevels.Name = "labelGMOLevels";
            this.labelGMOLevels.Size = new System.Drawing.Size(66, 13);
            this.labelGMOLevels.TabIndex = 2;
            this.labelGMOLevels.Text = "GMO Levels";
            // 
            // labelComparatorLevels
            // 
            this.labelComparatorLevels.AutoSize = true;
            this.labelComparatorLevels.Location = new System.Drawing.Point(296, 77);
            this.labelComparatorLevels.Name = "labelComparatorLevels";
            this.labelComparatorLevels.Size = new System.Drawing.Size(95, 13);
            this.labelComparatorLevels.TabIndex = 3;
            this.labelComparatorLevels.Text = "Comparator Levels";
            // 
            // ComparisonSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.labelComparatorLevels);
            this.Controls.Add(this.labelGMOLevels);
            this.Controls.Add(this.checkedListBoxComparatorLevels);
            this.Controls.Add(this.checkedListBoxGMOLevels);
            this.Name = "ComparisonSettingsControl";
            this.Size = new System.Drawing.Size(662, 459);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBoxGMOLevels;
        private System.Windows.Forms.CheckedListBox checkedListBoxComparatorLevels;
        private System.Windows.Forms.Label labelGMOLevels;
        private System.Windows.Forms.Label labelComparatorLevels;
    }
}
