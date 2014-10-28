namespace AmigaPowerAnalysis.GUI {
    partial class SelectionPanelContainer {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionPanelContainer));
            this.panelTabDescription = new System.Windows.Forms.Panel();
            this.textBoxTabDescription = new System.Windows.Forms.RichTextBox();
            this.textBoxTabTitle = new System.Windows.Forms.TextBox();
            this.panelSelectionForm = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelTabDescription.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTabDescription
            // 
            this.panelTabDescription.AutoSize = true;
            this.panelTabDescription.Controls.Add(this.pictureBoxLogo);
            this.panelTabDescription.Controls.Add(this.textBoxTabDescription);
            this.panelTabDescription.Controls.Add(this.textBoxTabTitle);
            this.panelTabDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabDescription.Location = new System.Drawing.Point(0, 0);
            this.panelTabDescription.Name = "panelTabDescription";
            this.panelTabDescription.Size = new System.Drawing.Size(874, 161);
            this.panelTabDescription.TabIndex = 10;
            // 
            // textBoxTabDescription
            // 
            this.textBoxTabDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTabDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTabDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTabDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabDescription.Location = new System.Drawing.Point(5, 28);
            this.textBoxTabDescription.Name = "textBoxTabDescription";
            this.textBoxTabDescription.ReadOnly = true;
            this.textBoxTabDescription.Size = new System.Drawing.Size(654, 130);
            this.textBoxTabDescription.TabIndex = 10;
            this.textBoxTabDescription.TabStop = false;
            this.textBoxTabDescription.Text = "";
            this.textBoxTabDescription.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.textBoxTabDescription_LinkClicked);
            // 
            // textBoxTabTitle
            // 
            this.textBoxTabTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTabTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTabTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabTitle.Location = new System.Drawing.Point(5, 3);
            this.textBoxTabTitle.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxTabTitle.Name = "textBoxTabTitle";
            this.textBoxTabTitle.ReadOnly = true;
            this.textBoxTabTitle.Size = new System.Drawing.Size(864, 22);
            this.textBoxTabTitle.TabIndex = 9;
            this.textBoxTabTitle.TabStop = false;
            this.textBoxTabTitle.Text = "Tab title";
            // 
            // panelSelectionForm
            // 
            this.panelSelectionForm.AutoSize = true;
            this.panelSelectionForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelectionForm.Location = new System.Drawing.Point(0, 161);
            this.panelSelectionForm.Margin = new System.Windows.Forms.Padding(0);
            this.panelSelectionForm.Name = "panelSelectionForm";
            this.panelSelectionForm.Size = new System.Drawing.Size(874, 342);
            this.panelSelectionForm.TabIndex = 11;
            this.panelSelectionForm.SizeChanged += new System.EventHandler(this.panelSelectionForm_SizeChanged);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxLogo.ErrorImage = null;
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.Location = new System.Drawing.Point(665, 28);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(209, 95);
            this.pictureBoxLogo.TabIndex = 11;
            this.pictureBoxLogo.TabStop = false;
            // 
            // SelectionPanelContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panelSelectionForm);
            this.Controls.Add(this.panelTabDescription);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SelectionPanelContainer";
            this.Size = new System.Drawing.Size(874, 503);
            this.panelTabDescription.ResumeLayout(false);
            this.panelTabDescription.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTabDescription;
        private System.Windows.Forms.TextBox textBoxTabTitle;
        private System.Windows.Forms.Panel panelSelectionForm;
        private System.Windows.Forms.RichTextBox textBoxTabDescription;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
    }
}
