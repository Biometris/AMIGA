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
            var endpointsBindingSouce = new BindingSource(_project.Endpoints, null);
            dataGridInteractions.AutoGenerateColumns = false;
            dataGridInteractions.DataSource = endpointsBindingSouce;
            dataGridInteractions.AllowUserToAddRows = false;
        }

        private void updateDataGridInteractions() {
            dataGridInteractions.Columns.Clear();

            var column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            dataGridInteractions.Columns.Add(column);

            for (int i = 1; i < _project.Design.Factors.Count; ++i) {

                var checkbox = new DataGridViewCheckBoxColumn();
                //checkbox.DataPropertyName = "IsInteractionWithGMO";
                checkbox.Name = "Factor";
                checkbox.HeaderText = _project.Design.Factors.ElementAt(i).Name;
                dataGridInteractions.Columns.Add(checkbox);

            }
        }
    }
}
