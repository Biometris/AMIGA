namespace AmigaPowerAnalysis.GUI {
    partial class IntroductionPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (this.pictureBoxCover.BackgroundImage != null)) {
                pictureBoxCover.BackgroundImage.Dispose();
                pictureBoxCover.BackgroundImage = null;
                pictureBoxCover.Dispose();
            }
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
            this.panelTabDescription = new System.Windows.Forms.Panel();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTabDescription
            // 
            this.panelTabDescription.AutoSize = true;
            this.panelTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabDescription.Location = new System.Drawing.Point(10, 10);
            this.panelTabDescription.Name = "panelTabDescription";
            this.panelTabDescription.Size = new System.Drawing.Size(854, 0);
            this.panelTabDescription.TabIndex = 8;
            // 
            // pictureBoxCover
            // 
            this.pictureBoxCover.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCover.BackgroundImage = global::AmigaPowerAnalysis.Properties.Resources.CoverImage;
            this.pictureBoxCover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxCover.Location = new System.Drawing.Point(10, 13);
            this.pictureBoxCover.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.pictureBoxCover.Size = new System.Drawing.Size(854, 480);
            this.pictureBoxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCover.TabIndex = 9;
            this.pictureBoxCover.TabStop = false;
            // 
            // IntroductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pictureBoxCover);
            this.Controls.Add(this.panelTabDescription);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "IntroductionForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTabDescription;
        private System.Windows.Forms.PictureBox pictureBoxCover;
    }
}
