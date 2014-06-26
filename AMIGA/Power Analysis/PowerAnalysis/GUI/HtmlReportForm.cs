using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class HtmlReportForm : Form {
        public HtmlReportForm(string htmlContent) {
            InitializeComponent();

            if (webBrowserHtmlReport.Document == null) {
                webBrowserHtmlReport.Navigate("about:blank");
            }
            var doc = webBrowserHtmlReport.Document.OpenNew(true);

            var _assembly = Assembly.GetExecutingAssembly();
            var _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("AmigaPowerAnalysis.Resources.print.css"));
            var html = string.Format("<html><head><style>{0}</style></head><body>{1}</body></html>", _textStreamReader.ReadToEnd(), htmlContent);

            doc.Write(html);
            doc.Title = "Report";

        }
    }
}
