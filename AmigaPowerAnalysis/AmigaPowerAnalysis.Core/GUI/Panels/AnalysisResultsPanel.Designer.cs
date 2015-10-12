namespace AmigaPowerAnalysis.GUI {
    partial class AnalysisResultsPanel {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisResultsPanel));
            this.splitContainerComparisons = new System.Windows.Forms.SplitContainer();
            this.dataGridViewComparisons = new System.Windows.Forms.DataGridView();
            this.tabControlEndpointResult = new System.Windows.Forms.TabControl();
            this.tabPageDifferenceTest = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonReplicatesDifference = new System.Windows.Forms.RadioButton();
            this.radioButtonCsdDifference = new System.Windows.Forms.RadioButton();
            this.plotViewDifference = new OxyPlot.WindowsForms.PlotView();
            this.tabPageEquivalenceTest = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonReplicatesEquivalence = new System.Windows.Forms.RadioButton();
            this.radioButtonCsdEquivalence = new System.Windows.Forms.RadioButton();
            this.plotViewEquivalence = new OxyPlot.WindowsForms.PlotView();
            this.tabPageFullReport = new System.Windows.Forms.TabPage();
            this.webBrowserFullReport = new System.Windows.Forms.WebBrowser();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonExportPdf = new System.Windows.Forms.ToolStripButton();
            this.tabPageAnalysisTemplate = new System.Windows.Forms.TabPage();
            this.textBoxTabDescription = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanelReport = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonExportAnalysisScripts = new System.Windows.Forms.Button();
            this.buttonGenerateDataTemplate = new System.Windows.Forms.Button();
            this.textBoxNumberOfReplicates = new System.Windows.Forms.TextBox();
            this.labelNumberOfReplicates = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.labelOutputNameLabel = new System.Windows.Forms.Label();
            this.labelOutputName = new System.Windows.Forms.Label();
            this.labelAggregationMethod = new System.Windows.Forms.Label();
            this.radioButtonAggregateMin = new System.Windows.Forms.RadioButton();
            this.radioButtonAggregateMean = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).BeginInit();
            this.splitContainerComparisons.Panel1.SuspendLayout();
            this.splitContainerComparisons.Panel2.SuspendLayout();
            this.splitContainerComparisons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).BeginInit();
            this.tabControlEndpointResult.SuspendLayout();
            this.tabPageDifferenceTest.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageEquivalenceTest.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabPageFullReport.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabPageAnalysisTemplate.SuspendLayout();
            this.flowLayoutPanelReport.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerComparisons
            // 
            this.splitContainerComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerComparisons.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerComparisons.Location = new System.Drawing.Point(10, 39);
            this.splitContainerComparisons.Name = "splitContainerComparisons";
            // 
            // splitContainerComparisons.Panel1
            // 
            this.splitContainerComparisons.Panel1.Controls.Add(this.dataGridViewComparisons);
            // 
            // splitContainerComparisons.Panel2
            // 
            this.splitContainerComparisons.Panel2.Controls.Add(this.tabControlEndpointResult);
            this.splitContainerComparisons.Size = new System.Drawing.Size(854, 454);
            this.splitContainerComparisons.SplitterDistance = 283;
            this.splitContainerComparisons.TabIndex = 9;
            // 
            // dataGridViewComparisons
            // 
            this.dataGridViewComparisons.AllowUserToAddRows = false;
            this.dataGridViewComparisons.AllowUserToDeleteRows = false;
            this.dataGridViewComparisons.AllowUserToResizeRows = false;
            this.dataGridViewComparisons.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewComparisons.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewComparisons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComparisons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewComparisons.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewComparisons.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewComparisons.MultiSelect = false;
            this.dataGridViewComparisons.Name = "dataGridViewComparisons";
            this.dataGridViewComparisons.RowHeadersVisible = false;
            this.dataGridViewComparisons.RowHeadersWidth = 24;
            this.dataGridViewComparisons.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewComparisons.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewComparisons.Size = new System.Drawing.Size(283, 454);
            this.dataGridViewComparisons.TabIndex = 3;
            this.dataGridViewComparisons.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewComparisons_CellBeginEdit);
            this.dataGridViewComparisons.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComparisons_CellClick);
            this.dataGridViewComparisons.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComparisons_CellValueChanged);
            this.dataGridViewComparisons.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewComparisons_CurrentCellDirtyStateChanged);
            this.dataGridViewComparisons.Leave += new System.EventHandler(this.dataGridViewComparisons_Leave);
            // 
            // tabControlEndpointResult
            // 
            this.tabControlEndpointResult.Controls.Add(this.tabPageDifferenceTest);
            this.tabControlEndpointResult.Controls.Add(this.tabPageEquivalenceTest);
            this.tabControlEndpointResult.Controls.Add(this.tabPageFullReport);
            this.tabControlEndpointResult.Controls.Add(this.tabPageAnalysisTemplate);
            this.tabControlEndpointResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEndpointResult.Location = new System.Drawing.Point(0, 0);
            this.tabControlEndpointResult.Name = "tabControlEndpointResult";
            this.tabControlEndpointResult.SelectedIndex = 0;
            this.tabControlEndpointResult.Size = new System.Drawing.Size(567, 454);
            this.tabControlEndpointResult.TabIndex = 11;
            this.tabControlEndpointResult.SelectedIndexChanged += new System.EventHandler(this.tabControlEndpointResult_SelectedIndexChanged);
            // 
            // tabPageDifferenceTest
            // 
            this.tabPageDifferenceTest.Controls.Add(this.flowLayoutPanel1);
            this.tabPageDifferenceTest.Controls.Add(this.plotViewDifference);
            this.tabPageDifferenceTest.Location = new System.Drawing.Point(4, 22);
            this.tabPageDifferenceTest.Name = "tabPageDifferenceTest";
            this.tabPageDifferenceTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDifferenceTest.Size = new System.Drawing.Size(559, 428);
            this.tabPageDifferenceTest.TabIndex = 0;
            this.tabPageDifferenceTest.Text = "Chart difference test";
            this.tabPageDifferenceTest.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.radioButtonReplicatesDifference);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonCsdDifference);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(553, 23);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // radioButtonReplicatesDifference
            // 
            this.radioButtonReplicatesDifference.AutoSize = true;
            this.radioButtonReplicatesDifference.Location = new System.Drawing.Point(413, 3);
            this.radioButtonReplicatesDifference.Name = "radioButtonReplicatesDifference";
            this.radioButtonReplicatesDifference.Size = new System.Drawing.Size(137, 17);
            this.radioButtonReplicatesDifference.TabIndex = 9;
            this.radioButtonReplicatesDifference.TabStop = true;
            this.radioButtonReplicatesDifference.Text = "Power versus replicates";
            this.radioButtonReplicatesDifference.UseVisualStyleBackColor = true;
            this.radioButtonReplicatesDifference.CheckedChanged += new System.EventHandler(this.radioButtonReplicatesDifference_CheckedChanged);
            // 
            // radioButtonCsdDifference
            // 
            this.radioButtonCsdDifference.AutoSize = true;
            this.radioButtonCsdDifference.Location = new System.Drawing.Point(293, 3);
            this.radioButtonCsdDifference.Name = "radioButtonCsdDifference";
            this.radioButtonCsdDifference.Size = new System.Drawing.Size(114, 17);
            this.radioButtonCsdDifference.TabIndex = 8;
            this.radioButtonCsdDifference.TabStop = true;
            this.radioButtonCsdDifference.Text = "Power versus CSD";
            this.radioButtonCsdDifference.UseVisualStyleBackColor = true;
            this.radioButtonCsdDifference.CheckedChanged += new System.EventHandler(this.radioButtonCsdDifference_CheckedChanged);
            // 
            // plotViewDifference
            // 
            this.plotViewDifference.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewDifference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewDifference.Location = new System.Drawing.Point(3, 3);
            this.plotViewDifference.Name = "plotViewDifference";
            this.plotViewDifference.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewDifference.Size = new System.Drawing.Size(553, 422);
            this.plotViewDifference.TabIndex = 4;
            this.plotViewDifference.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewDifference.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewDifference.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // tabPageEquivalenceTest
            // 
            this.tabPageEquivalenceTest.Controls.Add(this.flowLayoutPanel2);
            this.tabPageEquivalenceTest.Controls.Add(this.plotViewEquivalence);
            this.tabPageEquivalenceTest.Location = new System.Drawing.Point(4, 22);
            this.tabPageEquivalenceTest.Name = "tabPageEquivalenceTest";
            this.tabPageEquivalenceTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEquivalenceTest.Size = new System.Drawing.Size(559, 428);
            this.tabPageEquivalenceTest.TabIndex = 1;
            this.tabPageEquivalenceTest.Text = "Chart equivalence test";
            this.tabPageEquivalenceTest.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.radioButtonReplicatesEquivalence);
            this.flowLayoutPanel2.Controls.Add(this.radioButtonCsdEquivalence);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(553, 23);
            this.flowLayoutPanel2.TabIndex = 18;
            // 
            // radioButtonReplicatesEquivalence
            // 
            this.radioButtonReplicatesEquivalence.AutoSize = true;
            this.radioButtonReplicatesEquivalence.Location = new System.Drawing.Point(413, 3);
            this.radioButtonReplicatesEquivalence.Name = "radioButtonReplicatesEquivalence";
            this.radioButtonReplicatesEquivalence.Size = new System.Drawing.Size(137, 17);
            this.radioButtonReplicatesEquivalence.TabIndex = 11;
            this.radioButtonReplicatesEquivalence.TabStop = true;
            this.radioButtonReplicatesEquivalence.Text = "Power versus replicates";
            this.radioButtonReplicatesEquivalence.UseVisualStyleBackColor = true;
            this.radioButtonReplicatesEquivalence.CheckedChanged += new System.EventHandler(this.radioButtonReplicatesEquivalence_CheckedChanged);
            // 
            // radioButtonCsdEquivalence
            // 
            this.radioButtonCsdEquivalence.AutoSize = true;
            this.radioButtonCsdEquivalence.Location = new System.Drawing.Point(293, 3);
            this.radioButtonCsdEquivalence.Name = "radioButtonCsdEquivalence";
            this.radioButtonCsdEquivalence.Size = new System.Drawing.Size(114, 17);
            this.radioButtonCsdEquivalence.TabIndex = 10;
            this.radioButtonCsdEquivalence.TabStop = true;
            this.radioButtonCsdEquivalence.Text = "Power versus CSD";
            this.radioButtonCsdEquivalence.UseVisualStyleBackColor = true;
            this.radioButtonCsdEquivalence.CheckedChanged += new System.EventHandler(this.radioButtonCsdEquivalence_CheckedChanged);
            // 
            // plotViewEquivalence
            // 
            this.plotViewEquivalence.BackColor = System.Drawing.SystemColors.Window;
            this.plotViewEquivalence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotViewEquivalence.Location = new System.Drawing.Point(3, 3);
            this.plotViewEquivalence.Name = "plotViewEquivalence";
            this.plotViewEquivalence.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotViewEquivalence.Size = new System.Drawing.Size(553, 422);
            this.plotViewEquivalence.TabIndex = 4;
            this.plotViewEquivalence.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotViewEquivalence.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotViewEquivalence.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // tabPageFullReport
            // 
            this.tabPageFullReport.Controls.Add(this.webBrowserFullReport);
            this.tabPageFullReport.Controls.Add(this.toolStrip);
            this.tabPageFullReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageFullReport.Name = "tabPageFullReport";
            this.tabPageFullReport.Size = new System.Drawing.Size(559, 428);
            this.tabPageFullReport.TabIndex = 4;
            this.tabPageFullReport.Text = "Full report";
            this.tabPageFullReport.UseVisualStyleBackColor = true;
            // 
            // webBrowserFullReport
            // 
            this.webBrowserFullReport.AllowNavigation = false;
            this.webBrowserFullReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserFullReport.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserFullReport.Location = new System.Drawing.Point(0, 25);
            this.webBrowserFullReport.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserFullReport.Name = "webBrowserFullReport";
            this.webBrowserFullReport.Size = new System.Drawing.Size(559, 403);
            this.webBrowserFullReport.TabIndex = 2;
            // 
            // toolStrip
            // 
            this.toolStrip.AllowMerge = false;
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonExportPdf});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip.Size = new System.Drawing.Size(559, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonExportPdf
            // 
            this.toolStripButtonExportPdf.AutoSize = false;
            this.toolStripButtonExportPdf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportPdf.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExportPdf.Image")));
            this.toolStripButtonExportPdf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportPdf.Name = "toolStripButtonExportPdf";
            this.toolStripButtonExportPdf.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExportPdf.Text = "Export pdf";
            this.toolStripButtonExportPdf.ToolTipText = "Export pdf";
            this.toolStripButtonExportPdf.Click += new System.EventHandler(this.toolStripButtonExportPdf_Click);
            // 
            // tabPageAnalysisTemplate
            // 
            this.tabPageAnalysisTemplate.Controls.Add(this.textBoxTabDescription);
            this.tabPageAnalysisTemplate.Controls.Add(this.flowLayoutPanelReport);
            this.tabPageAnalysisTemplate.Location = new System.Drawing.Point(4, 22);
            this.tabPageAnalysisTemplate.Name = "tabPageAnalysisTemplate";
            this.tabPageAnalysisTemplate.Size = new System.Drawing.Size(559, 428);
            this.tabPageAnalysisTemplate.TabIndex = 5;
            this.tabPageAnalysisTemplate.Text = "Analysis template";
            this.tabPageAnalysisTemplate.UseVisualStyleBackColor = true;
            // 
            // textBoxTabDescription
            // 
            this.textBoxTabDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTabDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTabDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTabDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTabDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTabDescription.Location = new System.Drawing.Point(0, 29);
            this.textBoxTabDescription.Name = "textBoxTabDescription";
            this.textBoxTabDescription.ReadOnly = true;
            this.textBoxTabDescription.Size = new System.Drawing.Size(559, 399);
            this.textBoxTabDescription.TabIndex = 20;
            this.textBoxTabDescription.TabStop = false;
            this.textBoxTabDescription.Text = resources.GetString("textBoxTabDescription.Text");
            // 
            // flowLayoutPanelReport
            // 
            this.flowLayoutPanelReport.Controls.Add(this.buttonExportAnalysisScripts);
            this.flowLayoutPanelReport.Controls.Add(this.buttonGenerateDataTemplate);
            this.flowLayoutPanelReport.Controls.Add(this.textBoxNumberOfReplicates);
            this.flowLayoutPanelReport.Controls.Add(this.labelNumberOfReplicates);
            this.flowLayoutPanelReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanelReport.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelReport.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelReport.Name = "flowLayoutPanelReport";
            this.flowLayoutPanelReport.Size = new System.Drawing.Size(559, 29);
            this.flowLayoutPanelReport.TabIndex = 19;
            // 
            // buttonExportAnalysisScripts
            // 
            this.buttonExportAnalysisScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportAnalysisScripts.Location = new System.Drawing.Point(413, 3);
            this.buttonExportAnalysisScripts.Name = "buttonExportAnalysisScripts";
            this.buttonExportAnalysisScripts.Size = new System.Drawing.Size(143, 23);
            this.buttonExportAnalysisScripts.TabIndex = 12;
            this.buttonExportAnalysisScripts.Text = "Export analysis script";
            this.buttonExportAnalysisScripts.UseVisualStyleBackColor = true;
            this.buttonExportAnalysisScripts.Click += new System.EventHandler(this.buttonExportAnalysisScripts_Click);
            // 
            // buttonGenerateDataTemplate
            // 
            this.buttonGenerateDataTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateDataTemplate.Location = new System.Drawing.Point(264, 3);
            this.buttonGenerateDataTemplate.Name = "buttonGenerateDataTemplate";
            this.buttonGenerateDataTemplate.Size = new System.Drawing.Size(143, 23);
            this.buttonGenerateDataTemplate.TabIndex = 0;
            this.buttonGenerateDataTemplate.Text = "Export data template";
            this.buttonGenerateDataTemplate.UseVisualStyleBackColor = true;
            this.buttonGenerateDataTemplate.Click += new System.EventHandler(this.buttonGenerateDataTemplate_Click);
            // 
            // textBoxNumberOfReplicates
            // 
            this.textBoxNumberOfReplicates.Location = new System.Drawing.Point(216, 3);
            this.textBoxNumberOfReplicates.Name = "textBoxNumberOfReplicates";
            this.textBoxNumberOfReplicates.Size = new System.Drawing.Size(42, 20);
            this.textBoxNumberOfReplicates.TabIndex = 11;
            this.textBoxNumberOfReplicates.Text = "2";
            // 
            // labelNumberOfReplicates
            // 
            this.labelNumberOfReplicates.AutoSize = true;
            this.labelNumberOfReplicates.Location = new System.Drawing.Point(103, 8);
            this.labelNumberOfReplicates.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.labelNumberOfReplicates.Name = "labelNumberOfReplicates";
            this.labelNumberOfReplicates.Size = new System.Drawing.Size(107, 13);
            this.labelNumberOfReplicates.TabIndex = 10;
            this.labelNumberOfReplicates.Text = "Number of replicates:";
            // 
            // topPanel
            // 
            this.topPanel.AutoSize = true;
            this.topPanel.Controls.Add(this.labelOutputNameLabel);
            this.topPanel.Controls.Add(this.labelOutputName);
            this.topPanel.Controls.Add(this.labelAggregationMethod);
            this.topPanel.Controls.Add(this.radioButtonAggregateMin);
            this.topPanel.Controls.Add(this.radioButtonAggregateMean);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(10, 10);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.topPanel.Size = new System.Drawing.Size(854, 29);
            this.topPanel.TabIndex = 12;
            // 
            // labelOutputNameLabel
            // 
            this.labelOutputNameLabel.AutoSize = true;
            this.labelOutputNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutputNameLabel.Location = new System.Drawing.Point(3, 3);
            this.labelOutputNameLabel.Name = "labelOutputNameLabel";
            this.labelOutputNameLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelOutputNameLabel.Size = new System.Drawing.Size(49, 18);
            this.labelOutputNameLabel.TabIndex = 11;
            this.labelOutputNameLabel.Text = "Output:";
            // 
            // labelOutputName
            // 
            this.labelOutputName.AutoSize = true;
            this.labelOutputName.Location = new System.Drawing.Point(58, 3);
            this.labelOutputName.Name = "labelOutputName";
            this.labelOutputName.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelOutputName.Size = new System.Drawing.Size(66, 18);
            this.labelOutputName.TabIndex = 10;
            this.labelOutputName.Text = "output name";
            // 
            // labelAggregationMethod
            // 
            this.labelAggregationMethod.AutoSize = true;
            this.labelAggregationMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAggregationMethod.Location = new System.Drawing.Point(130, 3);
            this.labelAggregationMethod.Name = "labelAggregationMethod";
            this.labelAggregationMethod.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.labelAggregationMethod.Size = new System.Drawing.Size(103, 18);
            this.labelAggregationMethod.TabIndex = 12;
            this.labelAggregationMethod.Text = "Aggregate using:";
            // 
            // radioButtonAggregateMin
            // 
            this.radioButtonAggregateMin.AutoSize = true;
            this.radioButtonAggregateMin.Location = new System.Drawing.Point(239, 6);
            this.radioButtonAggregateMin.Name = "radioButtonAggregateMin";
            this.radioButtonAggregateMin.Size = new System.Drawing.Size(97, 17);
            this.radioButtonAggregateMin.TabIndex = 13;
            this.radioButtonAggregateMin.TabStop = true;
            this.radioButtonAggregateMin.Text = "minimum power";
            this.radioButtonAggregateMin.UseVisualStyleBackColor = true;
            this.radioButtonAggregateMin.CheckedChanged += new System.EventHandler(this.radioButtonAggregateMin_CheckedChanged);
            // 
            // radioButtonAggregateMean
            // 
            this.radioButtonAggregateMean.AutoSize = true;
            this.radioButtonAggregateMean.Location = new System.Drawing.Point(342, 6);
            this.radioButtonAggregateMean.Name = "radioButtonAggregateMean";
            this.radioButtonAggregateMean.Size = new System.Drawing.Size(83, 17);
            this.radioButtonAggregateMean.TabIndex = 14;
            this.radioButtonAggregateMean.TabStop = true;
            this.radioButtonAggregateMean.Text = "mean power";
            this.radioButtonAggregateMean.UseVisualStyleBackColor = true;
            this.radioButtonAggregateMean.CheckedChanged += new System.EventHandler(this.radioButtonAggregateMean_CheckedChanged);
            // 
            // AnalysisResultsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.splitContainerComparisons);
            this.Controls.Add(this.topPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AnalysisResultsPanel";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(874, 503);
            this.splitContainerComparisons.Panel1.ResumeLayout(false);
            this.splitContainerComparisons.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerComparisons)).EndInit();
            this.splitContainerComparisons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComparisons)).EndInit();
            this.tabControlEndpointResult.ResumeLayout(false);
            this.tabPageDifferenceTest.ResumeLayout(false);
            this.tabPageDifferenceTest.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPageEquivalenceTest.ResumeLayout(false);
            this.tabPageEquivalenceTest.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tabPageFullReport.ResumeLayout(false);
            this.tabPageFullReport.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabPageAnalysisTemplate.ResumeLayout(false);
            this.flowLayoutPanelReport.ResumeLayout(false);
            this.flowLayoutPanelReport.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerComparisons;
        private System.Windows.Forms.DataGridView dataGridViewComparisons;
        private System.Windows.Forms.TabControl tabControlEndpointResult;
        private System.Windows.Forms.TabPage tabPageDifferenceTest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButtonReplicatesDifference;
        private System.Windows.Forms.RadioButton radioButtonCsdDifference;
        private OxyPlot.WindowsForms.PlotView plotViewDifference;
        private System.Windows.Forms.TabPage tabPageEquivalenceTest;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButtonReplicatesEquivalence;
        private System.Windows.Forms.RadioButton radioButtonCsdEquivalence;
        private OxyPlot.WindowsForms.PlotView plotViewEquivalence;
        private System.Windows.Forms.TabPage tabPageFullReport;
        private System.Windows.Forms.WebBrowser webBrowserFullReport;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportPdf;
        private System.Windows.Forms.TabPage tabPageAnalysisTemplate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelReport;
        private System.Windows.Forms.Button buttonGenerateDataTemplate;
        private System.Windows.Forms.TextBox textBoxNumberOfReplicates;
        private System.Windows.Forms.Label labelNumberOfReplicates;
        private System.Windows.Forms.FlowLayoutPanel topPanel;
        private System.Windows.Forms.Label labelOutputNameLabel;
        private System.Windows.Forms.Label labelOutputName;
        private System.Windows.Forms.Button buttonExportAnalysisScripts;
        private System.Windows.Forms.RichTextBox textBoxTabDescription;
        private System.Windows.Forms.Label labelAggregationMethod;
        private System.Windows.Forms.RadioButton radioButtonAggregateMin;
        private System.Windows.Forms.RadioButton radioButtonAggregateMean;
    }
}
