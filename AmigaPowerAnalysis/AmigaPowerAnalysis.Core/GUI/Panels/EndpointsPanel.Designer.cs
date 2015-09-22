namespace AmigaPowerAnalysis.GUI {
    partial class EndpointsPanel {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndpointsPanel));
            this.dataGridViewEndpoints = new System.Windows.Forms.DataGridView();
            this.addEndpointButton = new System.Windows.Forms.Button();
            this.buttonDeleteEndpoint = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEndpoints
            // 
            this.dataGridViewEndpoints.AllowUserToAddRows = false;
            this.dataGridViewEndpoints.AllowUserToDeleteRows = false;
            this.dataGridViewEndpoints.AllowUserToResizeRows = false;
            this.dataGridViewEndpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEndpoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEndpoints.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEndpoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEndpoints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewEndpoints.Location = new System.Drawing.Point(13, 46);
            this.dataGridViewEndpoints.Name = "dataGridViewEndpoints";
            this.dataGridViewEndpoints.RowHeadersWidth = 24;
            this.dataGridViewEndpoints.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewEndpoints.Size = new System.Drawing.Size(809, 444);
            this.dataGridViewEndpoints.TabIndex = 2;
            this.dataGridViewEndpoints.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewEndpoints_CellValidating);
            this.dataGridViewEndpoints.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewEndpoints_DataError);
            this.dataGridViewEndpoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewEndpoints_KeyDown);
            // 
            // addEndpointButton
            // 
            this.addEndpointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addEndpointButton.Location = new System.Drawing.Point(618, 13);
            this.addEndpointButton.Name = "addEndpointButton";
            this.addEndpointButton.Size = new System.Drawing.Size(112, 27);
            this.addEndpointButton.TabIndex = 3;
            this.addEndpointButton.Text = "Add endpoint";
            this.addEndpointButton.UseVisualStyleBackColor = true;
            this.addEndpointButton.Click += new System.EventHandler(this.addEndpointButton_Click);
            // 
            // buttonDeleteEndpoint
            // 
            this.buttonDeleteEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteEndpoint.Location = new System.Drawing.Point(736, 13);
            this.buttonDeleteEndpoint.Name = "buttonDeleteEndpoint";
            this.buttonDeleteEndpoint.Size = new System.Drawing.Size(125, 27);
            this.buttonDeleteEndpoint.TabIndex = 4;
            this.buttonDeleteEndpoint.Text = "Remove endpoint";
            this.buttonDeleteEndpoint.UseVisualStyleBackColor = true;
            this.buttonDeleteEndpoint.Click += new System.EventHandler(this.buttonDeleteEndpoint_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveUp.Image")));
            this.buttonMoveUp.Location = new System.Drawing.Point(828, 46);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(32, 27);
            this.buttonMoveUp.TabIndex = 9;
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonMoveDown.Image")));
            this.buttonMoveDown.Location = new System.Drawing.Point(828, 79);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(32, 27);
            this.buttonMoveDown.TabIndex = 8;
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // EndpointsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonDeleteEndpoint);
            this.Controls.Add(this.addEndpointButton);
            this.Controls.Add(this.dataGridViewEndpoints);
            this.Name = "EndpointsPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEndpoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEndpoints;
        private System.Windows.Forms.Button addEndpointButton;
        private System.Windows.Forms.Button buttonDeleteEndpoint;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
    }
}
