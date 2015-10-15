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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndpointTypesPanel));
            this.dataGridViewDefaultEndpointGroups = new System.Windows.Forms.DataGridView();
            this.addDefaultEndpointGroupButton = new System.Windows.Forms.Button();
            this.buttonDeleteDefaultEndpointGroup = new System.Windows.Forms.Button();
            this.labelMyEndpointGroups = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelMyEndpointGroups = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelProjectEndpointGroups = new System.Windows.Forms.Panel();
            this.dataGridViewProjectEndpointGroups = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonAddToProject = new System.Windows.Forms.Button();
            this.buttonAddProjectEndpoint = new System.Windows.Forms.Button();
            this.buttonAddToDefault = new System.Windows.Forms.Button();
            this.labelProjectEndpointGroups = new System.Windows.Forms.Label();
            this.buttonRemoveProjectEndpoint = new System.Windows.Forms.Button();
            this.buttonResetToDefaults = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefaultEndpointGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelMyEndpointGroups.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelProjectEndpointGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjectEndpointGroups)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewDefaultEndpointGroups
            // 
            this.dataGridViewDefaultEndpointGroups.AllowUserToAddRows = false;
            this.dataGridViewDefaultEndpointGroups.AllowUserToDeleteRows = false;
            this.dataGridViewDefaultEndpointGroups.AllowUserToResizeRows = false;
            this.dataGridViewDefaultEndpointGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDefaultEndpointGroups.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDefaultEndpointGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDefaultEndpointGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDefaultEndpointGroups.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewDefaultEndpointGroups.Location = new System.Drawing.Point(0, 30);
            this.dataGridViewDefaultEndpointGroups.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.dataGridViewDefaultEndpointGroups.Name = "dataGridViewDefaultEndpointGroups";
            this.dataGridViewDefaultEndpointGroups.RowHeadersWidth = 24;
            this.dataGridViewDefaultEndpointGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewDefaultEndpointGroups.Size = new System.Drawing.Size(858, 215);
            this.dataGridViewDefaultEndpointGroups.TabIndex = 2;
            this.dataGridViewDefaultEndpointGroups.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDefaultEndpointGroups_CellClick);
            this.dataGridViewDefaultEndpointGroups.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewEndpointTypes_CellValidating);
            this.dataGridViewDefaultEndpointGroups.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDefaultEndpointGroups_CellValueChanged);
            this.dataGridViewDefaultEndpointGroups.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewEndpointTypes_DataError);
            // 
            // addDefaultEndpointGroupButton
            // 
            this.addDefaultEndpointGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addDefaultEndpointGroupButton.Location = new System.Drawing.Point(586, 0);
            this.addDefaultEndpointGroupButton.Name = "addDefaultEndpointGroupButton";
            this.addDefaultEndpointGroupButton.Size = new System.Drawing.Size(112, 27);
            this.addDefaultEndpointGroupButton.TabIndex = 3;
            this.addDefaultEndpointGroupButton.Text = "Add endpoint group";
            this.addDefaultEndpointGroupButton.UseVisualStyleBackColor = true;
            this.addDefaultEndpointGroupButton.Click += new System.EventHandler(this.buttonAddDefaultEndpointGroup_Click);
            // 
            // buttonDeleteDefaultEndpointGroup
            // 
            this.buttonDeleteDefaultEndpointGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteDefaultEndpointGroup.Location = new System.Drawing.Point(704, 0);
            this.buttonDeleteDefaultEndpointGroup.Name = "buttonDeleteDefaultEndpointGroup";
            this.buttonDeleteDefaultEndpointGroup.Size = new System.Drawing.Size(155, 27);
            this.buttonDeleteDefaultEndpointGroup.TabIndex = 4;
            this.buttonDeleteDefaultEndpointGroup.Text = "Remove endpoint group";
            this.buttonDeleteDefaultEndpointGroup.UseVisualStyleBackColor = true;
            this.buttonDeleteDefaultEndpointGroup.Click += new System.EventHandler(this.buttonDeleteDefaultEndpointGroup_Click);
            // 
            // labelMyEndpointGroups
            // 
            this.labelMyEndpointGroups.AutoSize = true;
            this.labelMyEndpointGroups.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMyEndpointGroups.Location = new System.Drawing.Point(-1, 4);
            this.labelMyEndpointGroups.Name = "labelMyEndpointGroups";
            this.labelMyEndpointGroups.Size = new System.Drawing.Size(182, 19);
            this.labelMyEndpointGroups.TabIndex = 5;
            this.labelMyEndpointGroups.Text = "My endpoint groups:";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(8, 3);
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
            this.splitContainer.Size = new System.Drawing.Size(858, 497);
            this.splitContainer.SplitterDistance = 245;
            this.splitContainer.TabIndex = 6;
            // 
            // panelMyEndpointGroups
            // 
            this.panelMyEndpointGroups.Controls.Add(this.dataGridViewDefaultEndpointGroups);
            this.panelMyEndpointGroups.Controls.Add(this.panel1);
            this.panelMyEndpointGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMyEndpointGroups.Location = new System.Drawing.Point(0, 0);
            this.panelMyEndpointGroups.Name = "panelMyEndpointGroups";
            this.panelMyEndpointGroups.Size = new System.Drawing.Size(858, 245);
            this.panelMyEndpointGroups.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.buttonResetToDefaults);
            this.panel1.Controls.Add(this.buttonDeleteDefaultEndpointGroup);
            this.panel1.Controls.Add(this.addDefaultEndpointGroupButton);
            this.panel1.Controls.Add(this.labelMyEndpointGroups);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 30);
            this.panel1.TabIndex = 9;
            // 
            // panelProjectEndpointGroups
            // 
            this.panelProjectEndpointGroups.Controls.Add(this.dataGridViewProjectEndpointGroups);
            this.panelProjectEndpointGroups.Controls.Add(this.panel2);
            this.panelProjectEndpointGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProjectEndpointGroups.Location = new System.Drawing.Point(0, 0);
            this.panelProjectEndpointGroups.Name = "panelProjectEndpointGroups";
            this.panelProjectEndpointGroups.Size = new System.Drawing.Size(858, 248);
            this.panelProjectEndpointGroups.TabIndex = 0;
            // 
            // dataGridViewProjectEndpointGroups
            // 
            this.dataGridViewProjectEndpointGroups.AllowUserToAddRows = false;
            this.dataGridViewProjectEndpointGroups.AllowUserToDeleteRows = false;
            this.dataGridViewProjectEndpointGroups.AllowUserToResizeRows = false;
            this.dataGridViewProjectEndpointGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProjectEndpointGroups.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProjectEndpointGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProjectEndpointGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewProjectEndpointGroups.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewProjectEndpointGroups.Location = new System.Drawing.Point(0, 29);
            this.dataGridViewProjectEndpointGroups.Name = "dataGridViewProjectEndpointGroups";
            this.dataGridViewProjectEndpointGroups.RowHeadersWidth = 24;
            this.dataGridViewProjectEndpointGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewProjectEndpointGroups.Size = new System.Drawing.Size(858, 219);
            this.dataGridViewProjectEndpointGroups.TabIndex = 6;
            this.dataGridViewProjectEndpointGroups.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProjectEndpointGroups_CellClick);
            this.dataGridViewProjectEndpointGroups.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProjectEndpointGroups_CellValueChanged);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.buttonAddToProject);
            this.panel2.Controls.Add(this.buttonAddProjectEndpoint);
            this.panel2.Controls.Add(this.buttonAddToDefault);
            this.panel2.Controls.Add(this.labelProjectEndpointGroups);
            this.panel2.Controls.Add(this.buttonRemoveProjectEndpoint);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(858, 29);
            this.panel2.TabIndex = 10;
            // 
            // buttonAddToProject
            // 
            this.buttonAddToProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddToProject.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddToProject.Image")));
            this.buttonAddToProject.Location = new System.Drawing.Point(510, -1);
            this.buttonAddToProject.Name = "buttonAddToProject";
            this.buttonAddToProject.Size = new System.Drawing.Size(32, 27);
            this.buttonAddToProject.TabIndex = 6;
            this.buttonAddToProject.UseVisualStyleBackColor = true;
            this.buttonAddToProject.Click += new System.EventHandler(this.buttonAddToProject_Click);
            // 
            // buttonAddProjectEndpoint
            // 
            this.buttonAddProjectEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddProjectEndpoint.Location = new System.Drawing.Point(586, -1);
            this.buttonAddProjectEndpoint.Name = "buttonAddProjectEndpoint";
            this.buttonAddProjectEndpoint.Size = new System.Drawing.Size(112, 27);
            this.buttonAddProjectEndpoint.TabIndex = 8;
            this.buttonAddProjectEndpoint.Text = "Add endpoint group";
            this.buttonAddProjectEndpoint.UseVisualStyleBackColor = true;
            this.buttonAddProjectEndpoint.Click += new System.EventHandler(this.buttonAddProjectEndpointGroup_Click);
            // 
            // buttonAddToDefault
            // 
            this.buttonAddToDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddToDefault.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddToDefault.Image")));
            this.buttonAddToDefault.Location = new System.Drawing.Point(548, -1);
            this.buttonAddToDefault.Name = "buttonAddToDefault";
            this.buttonAddToDefault.Size = new System.Drawing.Size(32, 27);
            this.buttonAddToDefault.TabIndex = 7;
            this.buttonAddToDefault.UseVisualStyleBackColor = true;
            this.buttonAddToDefault.Click += new System.EventHandler(this.buttonAddToDefault_Click);
            // 
            // labelProjectEndpointGroups
            // 
            this.labelProjectEndpointGroups.AutoSize = true;
            this.labelProjectEndpointGroups.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProjectEndpointGroups.Location = new System.Drawing.Point(-1, 3);
            this.labelProjectEndpointGroups.Name = "labelProjectEndpointGroups";
            this.labelProjectEndpointGroups.Size = new System.Drawing.Size(287, 19);
            this.labelProjectEndpointGroups.TabIndex = 7;
            this.labelProjectEndpointGroups.Text = "Current project endpoint groups:";
            // 
            // buttonRemoveProjectEndpoint
            // 
            this.buttonRemoveProjectEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveProjectEndpoint.Location = new System.Drawing.Point(704, -1);
            this.buttonRemoveProjectEndpoint.Name = "buttonRemoveProjectEndpoint";
            this.buttonRemoveProjectEndpoint.Size = new System.Drawing.Size(155, 27);
            this.buttonRemoveProjectEndpoint.TabIndex = 9;
            this.buttonRemoveProjectEndpoint.Text = "Remove endpoint group";
            this.buttonRemoveProjectEndpoint.UseVisualStyleBackColor = true;
            this.buttonRemoveProjectEndpoint.Click += new System.EventHandler(this.buttonDeleteProjectEndpointGroup_Click);
            // 
            // buttonResetToDefaults
            // 
            this.buttonResetToDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetToDefaults.Location = new System.Drawing.Point(447, 0);
            this.buttonResetToDefaults.Name = "buttonResetToDefaults";
            this.buttonResetToDefaults.Size = new System.Drawing.Size(133, 27);
            this.buttonResetToDefaults.TabIndex = 6;
            this.buttonResetToDefaults.Text = "Reset to default groups";
            this.buttonResetToDefaults.UseVisualStyleBackColor = true;
            this.buttonResetToDefaults.Click += new System.EventHandler(this.buttonResetToDefaults_Click);
            // 
            // EndpointTypesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainer);
            this.Name = "EndpointTypesPanel";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Size = new System.Drawing.Size(874, 503);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDefaultEndpointGroups)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelMyEndpointGroups.ResumeLayout(false);
            this.panelMyEndpointGroups.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelProjectEndpointGroups.ResumeLayout(false);
            this.panelProjectEndpointGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjectEndpointGroups)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDefaultEndpointGroups;
        private System.Windows.Forms.Button addDefaultEndpointGroupButton;
        private System.Windows.Forms.Button buttonDeleteDefaultEndpointGroup;
        private System.Windows.Forms.Label labelMyEndpointGroups;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelMyEndpointGroups;
        private System.Windows.Forms.Panel panelProjectEndpointGroups;
        private System.Windows.Forms.Label labelProjectEndpointGroups;
        private System.Windows.Forms.DataGridView dataGridViewProjectEndpointGroups;
        private System.Windows.Forms.Button buttonAddToDefault;
        private System.Windows.Forms.Button buttonAddToProject;
        private System.Windows.Forms.Button buttonRemoveProjectEndpoint;
        private System.Windows.Forms.Button buttonAddProjectEndpoint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonResetToDefaults;
    }
}
