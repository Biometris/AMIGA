namespace Amiga_Power_Analysis {
    partial class MainWindow {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstripEndpointTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstripAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabEndpoints = new System.Windows.Forms.TabPage();
            this.dataGridEndPoints = new System.Windows.Forms.DataGridView();
            this.tabDesign = new System.Windows.Forms.TabPage();
            this.tabInteractions = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabEndpoints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEndPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripEndpointTypes});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // toolstripEndpointTypes
            // 
            this.toolstripEndpointTypes.Name = "toolstripEndpointTypes";
            this.toolstripEndpointTypes.Size = new System.Drawing.Size(156, 22);
            this.toolstripEndpointTypes.Text = "Endpoint Types";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstripAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolstripAbout
            // 
            this.toolstripAbout.Name = "toolstripAbout";
            this.toolstripAbout.Size = new System.Drawing.Size(152, 22);
            this.toolstripAbout.Text = "About";
            this.toolstripAbout.Click += new System.EventHandler(this.toolstripAbout_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(760, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabEndpoints);
            this.tabControl.Controls.Add(this.tabDesign);
            this.tabControl.Controls.Add(this.tabInteractions);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 49);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(760, 571);
            this.tabControl.TabIndex = 2;
            // 
            // tabEndpoints
            // 
            this.tabEndpoints.Controls.Add(this.dataGridEndPoints);
            this.tabEndpoints.Location = new System.Drawing.Point(4, 22);
            this.tabEndpoints.Name = "tabEndpoints";
            this.tabEndpoints.Padding = new System.Windows.Forms.Padding(3);
            this.tabEndpoints.Size = new System.Drawing.Size(752, 545);
            this.tabEndpoints.TabIndex = 0;
            this.tabEndpoints.Text = "Endpoints";
            this.tabEndpoints.UseVisualStyleBackColor = true;
            // 
            // dataGridEndPoints
            // 
            this.dataGridEndPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridEndPoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridEndPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridEndPoints.Location = new System.Drawing.Point(6, 6);
            this.dataGridEndPoints.Name = "dataGridEndPoints";
            this.dataGridEndPoints.Size = new System.Drawing.Size(738, 174);
            this.dataGridEndPoints.TabIndex = 1;
            // 
            // tabDesign
            // 
            this.tabDesign.Location = new System.Drawing.Point(4, 22);
            this.tabDesign.Name = "tabDesign";
            this.tabDesign.Padding = new System.Windows.Forms.Padding(3);
            this.tabDesign.Size = new System.Drawing.Size(752, 545);
            this.tabDesign.TabIndex = 1;
            this.tabDesign.Text = "Design";
            this.tabDesign.UseVisualStyleBackColor = true;
            // 
            // tabInteractions
            // 
            this.tabInteractions.Location = new System.Drawing.Point(4, 22);
            this.tabInteractions.Name = "tabInteractions";
            this.tabInteractions.Padding = new System.Windows.Forms.Padding(3);
            this.tabInteractions.Size = new System.Drawing.Size(752, 545);
            this.tabInteractions.TabIndex = 2;
            this.tabInteractions.Text = "interactions";
            this.tabInteractions.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 620);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Amiga Power Analysis";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabEndpoints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridEndPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolstripAbout;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEndpoints;
        private System.Windows.Forms.TabPage tabDesign;
        private System.Windows.Forms.TabPage tabInteractions;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolstripEndpointTypes;
        private System.Windows.Forms.DataGridView dataGridEndPoints;
    }
}

