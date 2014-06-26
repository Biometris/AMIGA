using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmigaPowerAnalysis.GUI {
    public partial class HtmlReportForm : Form {
        public HtmlReportForm(string html) {
            InitializeComponent();
            webBrowserHtmlReport.DocumentText = html;
        }
    }
}
