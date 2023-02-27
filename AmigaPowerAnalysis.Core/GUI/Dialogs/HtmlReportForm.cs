using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Biometris.ExtensionMethods;
using AmigaPowerAnalysis.Core.Reporting;

namespace AmigaPowerAnalysis.GUI {
    public partial class HtmlReportForm : Form {

        private string _title;
        private string _html;
        private string _projectPath;
        private string _tempDir;
        private ReportGeneratorBase _reportGenerator;

        public HtmlReportForm(ReportGeneratorBase reportGenerator, string reportTitle, string projectPath) {
            InitializeComponent();
            _reportGenerator = reportGenerator;
            _title = reportTitle.Replace(" ", "_");
            _html = _reportGenerator.Generate(ChartCreationMethod.ExternalPng);
            _projectPath = projectPath;
            _tempDir = Path.GetTempPath();

            this.Text = "Summary of comparison";

            if (webBrowserHtmlReport.Document == null) {
                webBrowserHtmlReport.Navigate("about:blank");
            }
            var doc = webBrowserHtmlReport.Document.OpenNew(true);
            //webBrowserHtmlReport.IsWebBrowserContextMenuEnabled = false;
            //webBrowserHtmlReport.AllowWebBrowserDrop = false;

            doc.Write(_html);
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
                _reportGenerator.SaveAsPdf(filenamePdf);
                Process.Start(filenamePdf);
            }
        }
    }
}
