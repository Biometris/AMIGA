using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmigaPowerAnalysis.Core;

// TODO Obligatory to first enter a name for a new endpoint
// TODO Binomial totals greyed out for non fractions
// TODO Binomial totals must be positive
// TODO LOC=NaN should be displayed as empty textbox; also empty textbox store as to NaN. Possibly better to use null
// TODO LOC must be positive

namespace AmigaPowerAnalysis.GUI {
    public partial class AnalysisResultsForm : UserControl, ISelectionForm {

        private Project _project;

        public AnalysisResultsForm(Project project) {
            InitializeComponent();
            Name = "Results";
            this.textBoxTabTitle.Text = Name;
            _project = project;
        }

        public void Activate() {
        }
    }
}
