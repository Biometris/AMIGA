using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmigaPowerAnalysis.Helpers.ClassExtensionMethods;

namespace AmigaPowerAnalysis.GUI {
    public partial class HtmlReportForm : Form {

        private string _title;
        private string _projectPath;
        private string _tempDir;

        public HtmlReportForm(string htmlContent, string title, string projectPath) {
            InitializeComponent();
            _title = title.Replace(" ", "_");
            _projectPath = projectPath;
            _tempDir = Path.GetTempPath();

            this.Text = "Summary of comparison";

            if (webBrowserHtmlReport.Document == null) {
                webBrowserHtmlReport.Navigate("about:blank");
            }
            var doc = webBrowserHtmlReport.Document.OpenNew(true);
            webBrowserHtmlReport.IsWebBrowserContextMenuEnabled = false;
            //webBrowserHtmlReport.AllowWebBrowserDrop = false;

            var _assembly = Assembly.GetExecutingAssembly();
            var _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("AmigaPowerAnalysis.Resources.print.css"));
            var html = string.Format("<html><head><style>{0}</style></head><body>{1}</body></html>", _textStreamReader.ReadToEnd(), htmlContent);

            doc.Write(html);
            doc.Title = "Report";

        }

        private void toolStripButtonExportPdf_Click(object sender, EventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".pdf";
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = _title.ReplaceInvalidChars("_");
            saveFileDialog.InitialDirectory = _projectPath;
            if (saveFileDialog.FileName.Length == 0) {
                saveFileDialog.FileName = "unknown";
            }

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var filenamePdf = saveFileDialog.FileName;
                var cwd = Path.GetDirectoryName(filenamePdf);
                var filenameHtml = Path.Combine(_tempDir, "tmp_report_amiga_power_analysis.html");
                File.WriteAllText(filenameHtml, webBrowserHtmlReport.Document.Body.Parent.OuterHtml, Encoding.GetEncoding(webBrowserHtmlReport.Document.Encoding));

                var p = new Process();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Resources\\wkhtmltopdf\\wkhtmltopdf.exe");
                p.StartInfo.Arguments = "\"" + filenameHtml + "\"  \"" + filenamePdf + "\"";
                p.StartInfo.UseShellExecute = false;
                p.Start();
                p.WaitForExit();

                File.Delete(filenameHtml);
                Process.Start(filenamePdf);
            }
        }
    }
}
