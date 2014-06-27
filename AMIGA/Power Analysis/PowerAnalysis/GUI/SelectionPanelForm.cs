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
    public partial class SelectionPanelForm : Form {
        public SelectionPanelForm(UserControl selectionForm) {
            InitializeComponent();
            var selectionPanelContainer = new SelectionPanelContainer(selectionForm);
            selectionPanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelectionForm.Controls.Add(selectionPanelContainer);
            this.Name = selectionForm.Name;
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
