namespace AmigaPowerAnalysis.GUI {
    partial class HtmlReportForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlReportForm));
            this.webBrowserHtmlReport = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserHtmlReport
            // 
            this.webBrowserHtmlReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserHtmlReport.Location = new System.Drawing.Point(0, 0);
            this.webBrowserHtmlReport.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserHtmlReport.Name = "webBrowserHtmlReport";
            this.webBrowserHtmlReport.Size = new System.Drawing.Size(866, 672);
            this.webBrowserHtmlReport.TabIndex = 0;
            // 
            // HtmlReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 672);
            this.Controls.Add(this.webBrowserHtmlReport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HtmlReportForm";
            this.Text = "HtmlReportForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserHtmlReport;
    }
}