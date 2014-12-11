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
            this.labelPathGenstat = new System.Windows.Forms.Label();
            this.textBoxPathGenstat = new System.Windows.Forms.TextBox();
            this.buttonBrowseExecutableGenstat = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonBrowseExecutableR = new System.Windows.Forms.Button();
            this.textBoxPathR = new System.Windows.Forms.TextBox();
            this.labelPathR = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPathGenstat
            // 
            this.labelPathGenstat.AutoSize = true;
            this.labelPathGenstat.Location = new System.Drawing.Point(12, 36);
            this.labelPathGenstat.Name = "labelPathGenstat";
            this.labelPathGenstat.Size = new System.Drawing.Size(129, 13);
            this.labelPathGenstat.TabIndex = 0;
            this.labelPathGenstat.Text = "Path GenStat executable:";
            // 
            // textBoxPathGenstat
            // 
            this.textBoxPathGenstat.Enabled = false;
            this.textBoxPathGenstat.Location = new System.Drawing.Point(147, 33);
            this.textBoxPathGenstat.Name = "textBoxPathGenstat";
            this.textBoxPathGenstat.Size = new System.Drawing.Size(399, 20);
            this.textBoxPathGenstat.TabIndex = 1;
            // 
            // buttonBrowseExecutableGenstat
            // 
            this.buttonBrowseExecutableGenstat.Location = new System.Drawing.Point(552, 29);
            this.buttonBrowseExecutableGenstat.Name = "buttonBrowseExecutableGenstat";
            this.buttonBrowseExecutableGenstat.Size = new System.Drawing.Size(75, 28);
            this.buttonBrowseExecutableGenstat.TabIndex = 2;
            this.buttonBrowseExecutableGenstat.Text = "...";
            this.buttonBrowseExecutableGenstat.UseVisualStyleBackColor = true;
            this.buttonBrowseExecutableGenstat.Click += new System.EventHandler(this.buttonBrowseGenstatExecutable_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(552, 97);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 28);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(471, 97);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 28);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonBrowseExecutableR
            // 
            this.buttonBrowseExecutableR.Location = new System.Drawing.Point(552, 63);
            this.buttonBrowseExecutableR.Name = "buttonBrowseExecutableR";
            this.buttonBrowseExecutableR.Size = new System.Drawing.Size(75, 28);
            this.buttonBrowseExecutableR.TabIndex = 7;
            this.buttonBrowseExecutableR.Text = "...";
            this.buttonBrowseExecutableR.UseVisualStyleBackColor = true;
            this.buttonBrowseExecutableR.Click += new System.EventHandler(this.buttonBrowseExecutableR_Click);
            // 
            // textBoxPathR
            // 
            this.textBoxPathR.Enabled = false;
            this.textBoxPathR.Location = new System.Drawing.Point(147, 67);
            this.textBoxPathR.Name = "textBoxPathR";
            this.textBoxPathR.Size = new System.Drawing.Size(399, 20);
            this.textBoxPathR.TabIndex = 6;
            // 
            // labelPathR
            // 
            this.labelPathR.AutoSize = true;
            this.labelPathR.Location = new System.Drawing.Point(12, 70);
            this.labelPathR.Name = "labelPathR";
            this.labelPathR.Size = new System.Drawing.Size(122, 13);
            this.labelPathR.TabIndex = 5;
            this.labelPathR.Text = "Path RScript executable";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(639, 139);
            this.Controls.Add(this.buttonBrowseExecutableR);
            this.Controls.Add(this.textBoxPathR);
            this.Controls.Add(this.labelPathR);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonBrowseExecutableGenstat);
            this.Controls.Add(this.textBoxPathGenstat);
            this.Controls.Add(this.labelPathGenstat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPathGenstat;
        private System.Windows.Forms.TextBox textBoxPathGenstat;
        private System.Windows.Forms.Button buttonBrowseExecutableGenstat;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonBrowseExecutableR;
        private System.Windows.Forms.TextBox textBoxPathR;
        private System.Windows.Forms.Label labelPathR;
    }
}