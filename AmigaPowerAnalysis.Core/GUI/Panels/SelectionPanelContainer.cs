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
    public partial class SelectionPanelContainer : UserControl, ISelectionForm {

        private ISelectionForm _selectionForm;

        public SelectionPanelContainer(UserControl selectionForm) {
            InitializeComponent();
            if (selectionForm is ISelectionForm) {
                _selectionForm = selectionForm as ISelectionForm;
                _selectionForm.TabVisibilitiesChanged += onVisibilitySettingsChanged;
                this.Name = _selectionForm.Name;
                this.textBoxTabTitle.Text = Name;
                this.textBoxTabDescription.Text = Description;
                selectionForm.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panelSelectionForm.Controls.Add(selectionForm);
            }
        }

        public event EventHandler TabVisibilitiesChanged;

        public string Description {
            get {
                return _selectionForm.Description;
            }
        }

        public void Activate() {
            _selectionForm.Activate();
        }

        public bool IsVisible() {
            return _selectionForm.IsVisible();
        }

        private void showError(string title, string message) {
            MessageBox.Show(
                    message,
                    title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
        }

        private void onVisibilitySettingsChanged(object sender, EventArgs e) {
            var tabVisibilitiesChanged = TabVisibilitiesChanged;
            if (tabVisibilitiesChanged != null) {
                tabVisibilitiesChanged(this, null);
            }
        }

        private void textBoxTabDescription_LinkClicked(object sender, LinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void panelSelectionForm_SizeChanged(object sender, EventArgs e) {
            //this.textBoxTabDescription.Height = 10;
            //Size sz = new Size(this.textBoxTabDescription.ClientSize.Width, int.MaxValue);
            //TextFormatFlags flags = TextFormatFlags.WordBreak;
            //int padding = 3;
            //int borders = this.textBoxTabDescription.Height - this.textBoxTabDescription.ClientSize.Height;
            //sz = TextRenderer.MeasureText(this.textBoxTabDescription.Text, this.textBoxTabDescription.Font, sz, flags);
            //int h = sz.Height + borders + padding;
            //while (this.textBoxTabDescription.Top + h > this.ClientSize.Height - 10) {
            //    h = this.ClientSize.Height - 10 - this.textBoxTabDescription.Top;
            //}
            //this.textBoxTabDescription.Height = h;
        }
    }
}
