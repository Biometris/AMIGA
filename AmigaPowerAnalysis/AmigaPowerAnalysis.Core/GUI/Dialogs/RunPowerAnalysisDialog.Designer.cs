namespace AmigaPowerAnalysis.GUI {
    partial class RunPowerAnalysisDialog {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.progressBarCurrentProgress = new System.Windows.Forms.ProgressBar();
            this.labelCurrentActivity = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBarCurrentProgress
            // 
            this.progressBarCurrentProgress.Location = new System.Drawing.Point(23, 45);
            this.progressBarCurrentProgress.Name = "progressBarCurrentProgress";
            this.progressBarCurrentProgress.Size = new System.Drawing.Size(342, 25);
            this.progressBarCurrentProgress.TabIndex = 0;
            // 
            // labelCurrentActivity
            // 
            this.labelCurrentActivity.AutoSize = true;
            this.labelCurrentActivity.Location = new System.Drawing.Point(20, 19);
            this.labelCurrentActivity.Name = "labelCurrentActivity";
            this.labelCurrentActivity.Size = new System.Drawing.Size(119, 13);
            this.labelCurrentActivity.TabIndex = 1;
            this.labelCurrentActivity.Text = "Running power analysis";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(290, 85);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // RunPowerAnalysisDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 118);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelCurrentActivity);
            this.Controls.Add(this.progressBarCurrentProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RunPowerAnalysisDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Running Power Analysis";
            this.Load += new System.EventHandler(this.RunSimulationDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarCurrentProgress;
        private System.Windows.Forms.Label labelCurrentActivity;
        private System.Windows.Forms.Button buttonCancel;
    }
}