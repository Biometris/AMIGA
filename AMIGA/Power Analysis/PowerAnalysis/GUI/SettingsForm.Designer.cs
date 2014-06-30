namespace AmigaPowerAnalysis.GUI {
    partial class SettingsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.labelGenstatDirective = new System.Windows.Forms.Label();
            this.textBoxGenstatPath = new System.Windows.Forms.TextBox();
            this.buttonBrowseGenstatExecutable = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGenstatDirective
            // 
            this.labelGenstatDirective.AutoSize = true;
            this.labelGenstatDirective.Location = new System.Drawing.Point(12, 33);
            this.labelGenstatDirective.Name = "labelGenstatDirective";
            this.labelGenstatDirective.Size = new System.Drawing.Size(129, 13);
            this.labelGenstatDirective.TabIndex = 0;
            this.labelGenstatDirective.Text = "Path GenStat executable:";
            // 
            // textBoxGenstatPath
            // 
            this.textBoxGenstatPath.Enabled = false;
            this.textBoxGenstatPath.Location = new System.Drawing.Point(147, 30);
            this.textBoxGenstatPath.Name = "textBoxGenstatPath";
            this.textBoxGenstatPath.Size = new System.Drawing.Size(399, 20);
            this.textBoxGenstatPath.TabIndex = 1;
            // 
            // buttonBrowseGenstatExecutable
            // 
            this.buttonBrowseGenstatExecutable.Location = new System.Drawing.Point(552, 29);
            this.buttonBrowseGenstatExecutable.Name = "buttonBrowseGenstatExecutable";
            this.buttonBrowseGenstatExecutable.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseGenstatExecutable.TabIndex = 2;
            this.buttonBrowseGenstatExecutable.Text = "...";
            this.buttonBrowseGenstatExecutable.UseVisualStyleBackColor = true;
            this.buttonBrowseGenstatExecutable.Click += new System.EventHandler(this.buttonBrowseGenstatExecutable_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(552, 72);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(471, 72);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(639, 108);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonBrowseGenstatExecutable);
            this.Controls.Add(this.textBoxGenstatPath);
            this.Controls.Add(this.labelGenstatDirective);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGenstatDirective;
        private System.Windows.Forms.TextBox textBoxGenstatPath;
        private System.Windows.Forms.Button buttonBrowseGenstatExecutable;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}