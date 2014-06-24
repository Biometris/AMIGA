namespace AmigaPowerAnalysis.GUI {
    partial class EndpointTypesPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.dataGridViewEndpointGroups = new System.Windows.Forms.DataGridView();
            this.addEndpointButton = new System.Windows.Forms.Button();
            this.buttonDeleteEndpoint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpointGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEndpointGroups
            // 
            this.dataGridViewEndpointGroups.AllowUserToAddRows = false;
            this.dataGridViewEndpointGroups.AllowUserToDeleteRows = false;
            this.dataGridViewEndpointGroups.AllowUserToResizeRows = false;
            this.dataGridViewEndpointGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEndpointGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEndpointGroups.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEndpointGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEndpointGroups.Location = new System.Drawing.Point(13, 13);
            this.dataGridViewEndpointGroups.Name = "dataGridViewEndpointGroups";
            this.dataGridViewEndpointGroups.RowHeadersWidth = 24;
            this.dataGridViewEndpointGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpointGroups.Size = new System.Drawing.Size(848, 444);
            this.dataGridViewEndpointGroups.TabIndex = 2;
            this.dataGridViewEndpointGroups.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewEndpointTypes_CellValidating);
            this.dataGridViewEndpointGroups.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewEndpointTypes_DataError);
            // 
            // addEndpointButton
            // 
            this.addEndpointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addEndpointButton.Location = new System.Drawing.Point(588, 463);
            this.addEndpointButton.Name = "addEndpointButton";
            this.addEndpointButton.Size = new System.Drawing.Size(112, 27);
            this.addEndpointButton.TabIndex = 3;
            this.addEndpointButton.Text = "Add endpoint group";
            this.addEndpointButton.UseVisualStyleBackColor = true;
            this.addEndpointButton.Click += new System.EventHandler(this.addEndpointTypeButton_Click);
            // 
            // buttonDeleteEndpoint
            // 
            this.buttonDeleteEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteEndpoint.Location = new System.Drawing.Point(706, 463);
            this.buttonDeleteEndpoint.Name = "buttonDeleteEndpoint";
            this.buttonDeleteEndpoint.Size = new System.Drawing.Size(155, 27);
            this.buttonDeleteEndpoint.TabIndex = 4;
            this.buttonDeleteEndpoint.Text = "Remove endpoint group";
            this.buttonDeleteEndpoint.UseVisualStyleBackColor = true;
            this.buttonDeleteEndpoint.Click += new System.EventHandler(this.buttonDeleteEndpointType_Click);
            // 
            // EndpointTypesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonDeleteEndpoint);
            this.Controls.Add(this.addEndpointButton);
            this.Controls.Add(this.dataGridViewEndpointGroups);
            this.Name = "EndpointTypesForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpointGroups)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEndpointGroups;
        private System.Windows.Forms.Button addEndpointButton;
        private System.Windows.Forms.Button buttonDeleteEndpoint;
    }
}
