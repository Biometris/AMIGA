namespace AmigaPowerAnalysis.GUI {
    partial class OutputPanel {
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
            this.dataGridViewAvailableOutputs = new System.Windows.Forms.DataGridView();
            this.buttonDeleteCurrentOutput = new System.Windows.Forms.Button();
            this.buttonLoadCurrentOutput = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailableOutputs)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAvailableOutputs
            // 
            this.dataGridViewAvailableOutputs.AllowUserToAddRows = false;
            this.dataGridViewAvailableOutputs.AllowUserToDeleteRows = false;
            this.dataGridViewAvailableOutputs.AllowUserToResizeRows = false;
            this.dataGridViewAvailableOutputs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAvailableOutputs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAvailableOutputs.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAvailableOutputs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAvailableOutputs.Location = new System.Drawing.Point(10, 43);
            this.dataGridViewAvailableOutputs.MultiSelect = false;
            this.dataGridViewAvailableOutputs.Name = "dataGridViewAvailableOutputs";
            this.dataGridViewAvailableOutputs.RowHeadersVisible = false;
            this.dataGridViewAvailableOutputs.RowHeadersWidth = 24;
            this.dataGridViewAvailableOutputs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewAvailableOutputs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAvailableOutputs.Size = new System.Drawing.Size(782, 399);
            this.dataGridViewAvailableOutputs.TabIndex = 4;
            // 
            // buttonDeleteCurrentOutput
            // 
            this.buttonDeleteCurrentOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteCurrentOutput.AutoSize = true;
            this.buttonDeleteCurrentOutput.Location = new System.Drawing.Point(701, 10);
            this.buttonDeleteCurrentOutput.Name = "buttonDeleteCurrentOutput";
            this.buttonDeleteCurrentOutput.Size = new System.Drawing.Size(91, 27);
            this.buttonDeleteCurrentOutput.TabIndex = 6;
            this.buttonDeleteCurrentOutput.Text = "Delete output";
            this.buttonDeleteCurrentOutput.UseVisualStyleBackColor = true;
            this.buttonDeleteCurrentOutput.Click += new System.EventHandler(this.buttonDeleteCurrentOutput_Click);
            // 
            // buttonLoadCurrentOutput
            // 
            this.buttonLoadCurrentOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadCurrentOutput.AutoSize = true;
            this.buttonLoadCurrentOutput.Location = new System.Drawing.Point(611, 10);
            this.buttonLoadCurrentOutput.Name = "buttonLoadCurrentOutput";
            this.buttonLoadCurrentOutput.Size = new System.Drawing.Size(84, 27);
            this.buttonLoadCurrentOutput.TabIndex = 5;
            this.buttonLoadCurrentOutput.Text = "Load output";
            this.buttonLoadCurrentOutput.UseVisualStyleBackColor = true;
            this.buttonLoadCurrentOutput.Click += new System.EventHandler(this.buttonLoadCurrentOutput_Click);
            // 
            // OutputPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.buttonDeleteCurrentOutput);
            this.Controls.Add(this.buttonLoadCurrentOutput);
            this.Controls.Add(this.dataGridViewAvailableOutputs);
            this.Name = "OutputPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(802, 452);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAvailableOutputs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAvailableOutputs;
        private System.Windows.Forms.Button buttonDeleteCurrentOutput;
        private System.Windows.Forms.Button buttonLoadCurrentOutput;


    }
}
