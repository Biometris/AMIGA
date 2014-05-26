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
            this.textBoxTabTitle.Text = Name;
            createDataGridInteractions();
        }

        public void Activate() {
            updateDataGridInteractions();
            if (_project.UseDefaultInteractions) {
                dataGridInteractions.ReadOnly = true;
                dataGridInteractions.DefaultCellStyle.BackColor = Color.LightGray;
            } else {
                dataGridInteractions.ReadOnly = false;
                dataGridInteractions.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void createDataGridInteractions() {
            dataGridInteractions.DataSource = _interactionsDataTable;
            updateDataGridInteractions();
        }

        private void updateDataGridInteractions() {
            _interactionsDataTable.Clear();
            _interactionsDataTable.Columns.Clear();
            _interactionsDataTable.Columns.Add("Endpoint");
            for (int i = 1; i < _project.Factors.Count; ++i) {
                _interactionsDataTable.Columns.Add(_project.Factors.ElementAt(i).Name, typeof(bool));
                if (!_project.Factors.ElementAt(i).IsInteractionWithVariety) {
                    dataGridInteractions.Columns[i].ReadOnly = true;
                    dataGridInteractions.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            for (int i = 0; i < _project.Endpoints.Count; ++i) {
                DataRow row = _interactionsDataTable.NewRow();
                row["Endpoint"] = _project.Endpoints.ElementAt(i).Name;
                var endpointInteractions = _project.Endpoints.ElementAt(i).InteractionFactors;
                for (int j = 0; j < endpointInteractions.Count(); ++j) {
                    if (_interactionsDataTable.Columns.Contains(endpointInteractions.ElementAt(j).Name)) {
                        row[endpointInteractions.ElementAt(j).Name] = true;
                    }
                }
                _interactionsDataTable.Rows.Add(row);
            }
        }

        private void dataGridInteractions_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex > 0 && e.ColumnIndex - 1 < _project.Factors.Count) {
                var endpoint = _project.Endpoints.ElementAt(e.RowIndex);
                var factor = _project.Factors.ElementAt(e.ColumnIndex);
                var isChecked = (bool)_interactionsDataTable.Rows[e.RowIndex][e.ColumnIndex];
                if (isChecked) {
                    endpoint.AddInteractionFactor(factor);
                } else {
                    endpoint.RemoveInteractionFactor(factor);
                }
            }
        }
    }
}
