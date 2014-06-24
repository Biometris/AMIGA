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
    public partial class SelectionPaneForm : Form {
        public SelectionPaneForm(UserControl selectionForm) {
            InitializeComponent();
            selectionForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelectionForm.Controls.Add(selectionForm);
            this.Name = selectionForm.Name;
        }
    }
}
