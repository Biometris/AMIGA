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

namespace AmigaPowerAnalysis.GUI {
    public partial class InteractionsForm : UserControl, ISelectionForm {

        private Project _project;

        private DataTable _interactionsDataTable = new DataTable(); 

        public InteractionsForm(Project project) {
            InitializeComponent();
            _project = project;
            Name = "Interactions";
            createDataGridInteractions();
        }

        public void Activate() {
            updateDataGridInteractions();
        }

        private void createDataGridInteractions() {
            dataGridInteractions.DataSource = _interactionsDataTable;
            updateDataGridInteractions();
        }

        private void updateDataGridInteractions() {
            _interactionsDataTable.Clear();
            _interactionsDataTable.Columns.Clear();
            _interactionsDataTable.Columns.Add("Endpoint");
            for (int i = 1; i < _project.Design.Factors.Count; ++i) {
                _interactionsDataTable.Columns.Add(_project.Design.Factors.ElementAt(i).Name, typeof(bool));
            }

            for (int i = 0; i < _project.Endpoints.Count; ++i) {
                DataRow row = _interactionsDataTable.NewRow();
                row["Endpoint"] = _project.Endpoints.ElementAt(i).Name;
                var endpointInteractions = _project.Endpoints.ElementAt(i).InteractionFactors;
                for (int j = 0; j < endpointInteractions.Count; ++j) {
                    row[endpointInteractions.ElementAt(j).Name] = true;
                }
                _interactionsDataTable.Rows.Add(row);
            }
        }
    }
}
