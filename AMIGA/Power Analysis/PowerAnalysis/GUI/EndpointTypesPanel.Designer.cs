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
            this.labelMyEndpointGroups = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelMyEndpointGroups = new System.Windows.Forms.Panel();
            this.panelProjectEndpointGroups = new System.Windows.Forms.Panel();
            this.labelProjectEndpointGroups = new System.Windows.Forms.Label();
            this.dataGridViewProjectEndpointGroups = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpointGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelMyEndpointGroups.SuspendLayout();
            this.panelProjectEndpointGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjectEndpointGroups)).BeginInit();
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
            this.dataGridViewEndpointGroups.Location = new System.Drawing.Point(0, 27);
            this.dataGridViewEndpointGroups.Name = "dataGridViewEndpointGroups";
            this.dataGridViewEndpointGroups.RowHeadersWidth = 24;
            this.dataGridViewEndpointGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpointGroups.Size = new System.Drawing.Size(848, 172);
            this.dataGridViewEndpointGroups.TabIndex = 2;
            this.dataGridViewEndpointGroups.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewEndpointTypes_CellValidating);
            this.dataGridViewEndpointGroups.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewEndpointTypes_DataError);
            // 
            // addEndpointButton
            // 
            this.addEndpointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addEndpointButton.Location = new System.Drawing.Point(569, 205);
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
            this.buttonDeleteEndpoint.Location = new System.Drawing.Point(687, 205);
            this.buttonDeleteEndpoint.Name = "buttonDeleteEndpoint";
            this.buttonDeleteEndpoint.Size = new System.Drawing.Size(155, 27);
            this.buttonDeleteEndpoint.TabIndex = 4;
            this.buttonDeleteEndpoint.Text = "Remove endpoint group";
            this.buttonDeleteEndpoint.UseVisualStyleBackColor = true;
            this.buttonDeleteEndpoint.Click += new System.EventHandler(this.buttonDeleteEndpointType_Click);
            // 
            // labelMyEndpointGroups
            // 
            this.labelMyEndpointGroups.AutoSize = true;
            this.labelMyEndpointGroups.Location = new System.Drawing.Point(3, 7);
            this.labelMyEndpointGroups.Name = "labelMyEndpointGroups";
            this.labelMyEndpointGroups.Size = new System.Drawing.Size(103, 13);
            this.labelMyEndpointGroups.TabIndex = 5;
            this.labelMyEndpointGroups.Text = "My endpoint groups:";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(13, 13);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelMyEndpointGroups);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelProjectEndpointGroups);
            this.splitContainer.Size = new System.Drawing.Size(848, 477);
            this.splitContainer.SplitterDistance = 238;
            this.splitContainer.TabIndex = 6;
            // 
            // panelMyEndpointGroups
            // 
            this.panelMyEndpointGroups.Controls.Add(this.labelMyEndpointGroups);
            this.panelMyEndpointGroups.Controls.Add(this.buttonDeleteEndpoint);
            this.panelMyEndpointGroups.Controls.Add(this.dataGridViewEndpointGroups);
            this.panelMyEndpointGroups.Controls.Add(this.addEndpointButton);
            this.panelMyEndpointGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMyEndpointGroups.Location = new System.Drawing.Point(0, 0);
            this.panelMyEndpointGroups.Name = "panelMyEndpointGroups";
            this.panelMyEndpointGroups.Size = new System.Drawing.Size(848, 238);
            this.panelMyEndpointGroups.TabIndex = 0;
            // 
            // panelProjectEndpointGroups
            // 
            this.panelProjectEndpointGroups.Controls.Add(this.labelProjectEndpointGroups);
            this.panelProjectEndpointGroups.Controls.Add(this.dataGridViewProjectEndpointGroups);
            this.panelProjectEndpointGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProjectEndpointGroups.Location = new System.Drawing.Point(0, 0);
            this.panelProjectEndpointGroups.Name = "panelProjectEndpointGroups";
            this.panelProjectEndpointGroups.Size = new System.Drawing.Size(848, 235);
            this.panelProjectEndpointGroups.TabIndex = 0;
            // 
            // labelProjectEndpointGroups
            // 
            this.labelProjectEndpointGroups.AutoSize = true;
            this.labelProjectEndpointGroups.Location = new System.Drawing.Point(3, 6);
            this.labelProjectEndpointGroups.Name = "labelProjectEndpointGroups";
            this.labelProjectEndpointGroups.Size = new System.Drawing.Size(158, 13);
            this.labelProjectEndpointGroups.TabIndex = 7;
            this.labelProjectEndpointGroups.Text = "Current project endpoint groups:";
            // 
            // dataGridViewProjectEndpointGroups
            // 
            this.dataGridViewProjectEndpointGroups.AllowUserToAddRows = false;
            this.dataGridViewProjectEndpointGroups.AllowUserToDeleteRows = false;
            this.dataGridViewProjectEndpointGroups.AllowUserToResizeRows = false;
            this.dataGridViewProjectEndpointGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProjectEndpointGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProjectEndpointGroups.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProjectEndpointGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProjectEndpointGroups.Location = new System.Drawing.Point(0, 27);
            this.dataGridViewProjectEndpointGroups.Name = "dataGridViewProjectEndpointGroups";
            this.dataGridViewProjectEndpointGroups.RowHeadersWidth = 24;
            this.dataGridViewProjectEndpointGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProjectEndpointGroups.Size = new System.Drawing.Size(848, 208);
            this.dataGridViewProjectEndpointGroups.TabIndex = 6;
            // 
            // EndpointTypesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainer);
            this.Name = "EndpointTypesPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpointGroups)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelMyEndpointGroups.ResumeLayout(false);
            this.panelMyEndpointGroups.PerformLayout();
            this.panelProjectEndpointGroups.ResumeLayout(false);
            this.panelProjectEndpointGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjectEndpointGroups)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEndpointGroups;
        private System.Windows.Forms.Button addEndpointButton;
        private System.Windows.Forms.Button buttonDeleteEndpoint;
        private System.Windows.Forms.Label labelMyEndpointGroups;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelMyEndpointGroups;
        private System.Windows.Forms.Panel panelProjectEndpointGroups;
        private System.Windows.Forms.Label labelProjectEndpointGroups;
        private System.Windows.Forms.DataGridView dataGridViewProjectEndpointGroups;
    }
}
