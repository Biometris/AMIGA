namespace AmigaPowerAnalysis.GUI {
    partial class ComparisonsForm {
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
            this.dataGridComparisons = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridComparisons)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridComparisons
            // 
            this.dataGridComparisons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridComparisons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridComparisons.Location = new System.Drawing.Point(10, 10);
            this.dataGridComparisons.Name = "dataGridComparisons";
            this.dataGridComparisons.Size = new System.Drawing.Size(269, 432);
            this.dataGridComparisons.TabIndex = 0;
            this.dataGridComparisons.SelectionChanged += new System.EventHandler(this.dataGridComparisons_SelectionChanged);
            // 
            // ComparisonsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.dataGridComparisons);
            this.Name = "ComparisonsForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(575, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridComparisons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridComparisons;
    }
}
