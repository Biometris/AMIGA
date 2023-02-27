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
            this.labelElapsed = new System.Windows.Forms.Label();
            this.labelRemaining = new System.Windows.Forms.Label();
            this.labelElapsedValue = new System.Windows.Forms.Label();
            this.labelRemainingValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarCurrentProgress
            // 
            this.progressBarCurrentProgress.Location = new System.Drawing.Point(12, 67);
            this.progressBarCurrentProgress.Name = "progressBarCurrentProgress";
            this.progressBarCurrentProgress.Size = new System.Drawing.Size(365, 25);
            this.progressBarCurrentProgress.TabIndex = 0;
            // 
            // labelCurrentActivity
            // 
            this.labelCurrentActivity.AutoSize = true;
            this.labelCurrentActivity.Location = new System.Drawing.Point(12, 14);
            this.labelCurrentActivity.Name = "labelCurrentActivity";
            this.labelCurrentActivity.Size = new System.Drawing.Size(119, 13);
            this.labelCurrentActivity.TabIndex = 1;
            this.labelCurrentActivity.Text = "Running power analysis";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(302, 101);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelElapsed
            // 
            this.labelElapsed.AutoSize = true;
            this.labelElapsed.Location = new System.Drawing.Point(12, 43);
            this.labelElapsed.Name = "labelElapsed";
            this.labelElapsed.Size = new System.Drawing.Size(73, 13);
            this.labelElapsed.TabIndex = 3;
            this.labelElapsed.Text = "Time elapsed:";
            // 
            // labelRemaining
            // 
            this.labelRemaining.AutoSize = true;
            this.labelRemaining.Location = new System.Drawing.Point(162, 43);
            this.labelRemaining.Name = "labelRemaining";
            this.labelRemaining.Size = new System.Drawing.Size(81, 13);
            this.labelRemaining.TabIndex = 4;
            this.labelRemaining.Text = "Time remaining:";
            // 
            // labelElapsedValue
            // 
            this.labelElapsedValue.AutoSize = true;
            this.labelElapsedValue.Location = new System.Drawing.Point(91, 43);
            this.labelElapsedValue.Name = "labelElapsedValue";
            this.labelElapsedValue.Size = new System.Drawing.Size(43, 13);
            this.labelElapsedValue.TabIndex = 5;
            this.labelElapsedValue.Text = "0:00:00";
            // 
            // labelRemainingValue
            // 
            this.labelRemainingValue.AutoSize = true;
            this.labelRemainingValue.Location = new System.Drawing.Point(249, 43);
            this.labelRemainingValue.Name = "labelRemainingValue";
            this.labelRemainingValue.Size = new System.Drawing.Size(43, 13);
            this.labelRemainingValue.TabIndex = 6;
            this.labelRemainingValue.Text = "0:00:00";
            // 
            // RunPowerAnalysisDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 136);
            this.Controls.Add(this.labelRemainingValue);
            this.Controls.Add(this.labelElapsedValue);
            this.Controls.Add(this.labelRemaining);
            this.Controls.Add(this.labelElapsed);
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
        private System.Windows.Forms.Label labelElapsed;
        private System.Windows.Forms.Label labelRemaining;
        private System.Windows.Forms.Label labelElapsedValue;
        private System.Windows.Forms.Label labelRemainingValue;
    }
}