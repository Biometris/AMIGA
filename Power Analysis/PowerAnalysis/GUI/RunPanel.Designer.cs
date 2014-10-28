namespace AmigaPowerAnalysis.GUI {
    partial class RunPanel {
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
            this.buttonRunPowerAnalysis = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRunPowerAnalysis
            // 
            this.buttonRunPowerAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRunPowerAnalysis.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonRunPowerAnalysis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRunPowerAnalysis.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRunPowerAnalysis.Location = new System.Drawing.Point(537, 393);
            this.buttonRunPowerAnalysis.Name = "buttonRunPowerAnalysis";
            this.buttonRunPowerAnalysis.Size = new System.Drawing.Size(252, 46);
            this.buttonRunPowerAnalysis.TabIndex = 0;
            this.buttonRunPowerAnalysis.Text = "Run power analysis";
            this.buttonRunPowerAnalysis.UseVisualStyleBackColor = false;
            this.buttonRunPowerAnalysis.Click += new System.EventHandler(this.buttonRunPowerAnalysis_Click);
            // 
            // RunPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonRunPowerAnalysis);
            this.Name = "RunPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRunPowerAnalysis;

    }
}
