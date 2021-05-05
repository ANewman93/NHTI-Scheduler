namespace NHTI_Scheduler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusReportDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLoadProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvAvailableCourses = new NHTI_Scheduler.MyTreeView();
            this.pnlSelectedCourses = new System.Windows.Forms.Panel();
            this.tblSelectedCourses = new System.Windows.Forms.TableLayoutPanel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsbStrip = new System.Windows.Forms.ToolStripButton();
            this.tsbClearAll = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveSchedule = new System.Windows.Forms.ToolStrip();
            this.tsbCustom = new System.Windows.Forms.ToolStripButton();
            this.tsbFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbWarnings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tsbScheduleOpen = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCourseFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.courseOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterCoursesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openExistingOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlSelectedCourses.SuspendLayout();
            this.tsbSaveSchedule.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusReportDate,
            this.tsLoadProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 845);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 20, 0);
            this.statusStrip1.Size = new System.Drawing.Size(2428, 42);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusReportDate
            // 
            this.tsStatusReportDate.Name = "tsStatusReportDate";
            this.tsStatusReportDate.Size = new System.Drawing.Size(195, 32);
            this.tsStatusReportDate.Text = "Tally Report Date";
            // 
            // tsLoadProgress
            // 
            this.tsLoadProgress.Name = "tsLoadProgress";
            this.tsLoadProgress.Size = new System.Drawing.Size(150, 30);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 42);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvAvailableCourses);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlSelectedCourses);
            this.splitContainer1.Size = new System.Drawing.Size(2428, 803);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 2;
            // 
            // tvAvailableCourses
            // 
            this.tvAvailableCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAvailableCourses.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvAvailableCourses.FullRowSelect = true;
            this.tvAvailableCourses.Location = new System.Drawing.Point(0, 0);
            this.tvAvailableCourses.Margin = new System.Windows.Forms.Padding(4);
            this.tvAvailableCourses.Name = "tvAvailableCourses";
            this.tvAvailableCourses.Size = new System.Drawing.Size(362, 803);
            this.tvAvailableCourses.TabIndex = 0;
            this.tvAvailableCourses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvAvailableCourses_MouseDown);
            // 
            // pnlSelectedCourses
            // 
            this.pnlSelectedCourses.BackColor = System.Drawing.SystemColors.Window;
            this.pnlSelectedCourses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelectedCourses.Controls.Add(this.tblSelectedCourses);
            this.pnlSelectedCourses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSelectedCourses.Location = new System.Drawing.Point(0, 0);
            this.pnlSelectedCourses.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSelectedCourses.Name = "pnlSelectedCourses";
            this.pnlSelectedCourses.Size = new System.Drawing.Size(2058, 803);
            this.pnlSelectedCourses.TabIndex = 0;
            this.pnlSelectedCourses.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvSelectedCourses_DragDrop);
            this.pnlSelectedCourses.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvSelectedCourses_DragEnter);
            this.pnlSelectedCourses.DragOver += new System.Windows.Forms.DragEventHandler(this.lvSelectedCourses_DragOver);
            this.pnlSelectedCourses.MouseEnter += new System.EventHandler(this.pnlSelectedCourses_MouseEnter);
            // 
            // tblSelectedCourses
            // 
            this.tblSelectedCourses.BackColor = System.Drawing.SystemColors.Window;
            this.tblSelectedCourses.BackgroundImage = global::NHTI_Scheduler.Properties.Resources.Cropped_Logo;
            this.tblSelectedCourses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tblSelectedCourses.CausesValidation = false;
            this.tblSelectedCourses.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblSelectedCourses.ColumnCount = 7;
            this.tblSelectedCourses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tblSelectedCourses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 298F));
            this.tblSelectedCourses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 298F));
            this.tblSelectedCourses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 298F));
            this.tblSelectedCourses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 298F));
            this.tblSelectedCourses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 298F));
            this.tblSelectedCourses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 317F));
            this.tblSelectedCourses.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tblSelectedCourses.Location = new System.Drawing.Point(0, 0);
            this.tblSelectedCourses.Margin = new System.Windows.Forms.Padding(6);
            this.tblSelectedCourses.Name = "tblSelectedCourses";
            this.tblSelectedCourses.RowCount = 34;
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tblSelectedCourses.Size = new System.Drawing.Size(2000, 3906);
            this.tblSelectedCourses.TabIndex = 0;
            this.tblSelectedCourses.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDoubleClick_1);
            this.tblSelectedCourses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDown);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(46, 36);
            // 
            // tsbStrip
            // 
            this.tsbStrip.Name = "tsbStrip";
            this.tsbStrip.Size = new System.Drawing.Size(46, 36);
            // 
            // tsbClearAll
            // 
            this.tsbClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClearAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearAll.Image")));
            this.tsbClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearAll.Name = "tsbClearAll";
            this.tsbClearAll.Size = new System.Drawing.Size(46, 36);
            this.tsbClearAll.Text = "Clear All";
            this.tsbClearAll.Click += new System.EventHandler(this.tsbClearAll_Click);
            // 
            // tsbSaveSchedule
            // 
            this.tsbSaveSchedule.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsbSaveSchedule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.tsbStrip,
            this.tsbCustom,
            this.tsbClearAll,
            this.tsbFilter,
            this.tsbWarnings,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripButton3,
            this.tsbScheduleOpen});
            this.tsbSaveSchedule.Location = new System.Drawing.Point(0, 40);
            this.tsbSaveSchedule.Name = "tsbSaveSchedule";
            this.tsbSaveSchedule.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.tsbSaveSchedule.Size = new System.Drawing.Size(2428, 42);
            this.tsbSaveSchedule.TabIndex = 0;
            this.tsbSaveSchedule.Text = "toolStrip1";
            this.tsbSaveSchedule.Visible = false;
            // 
            // tsbCustom
            // 
            this.tsbCustom.Name = "tsbCustom";
            this.tsbCustom.Size = new System.Drawing.Size(46, 36);
            // 
            // tsbFilter
            // 
            this.tsbFilter.Name = "tsbFilter";
            this.tsbFilter.Size = new System.Drawing.Size(46, 36);
            // 
            // tsbWarnings
            // 
            this.tsbWarnings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWarnings.Image = ((System.Drawing.Image)(resources.GetObject("tsbWarnings.Image")));
            this.tsbWarnings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWarnings.Name = "tsbWarnings";
            this.tsbWarnings.Size = new System.Drawing.Size(46, 36);
            this.tsbWarnings.Text = "Missing Courses";
            this.tsbWarnings.Click += new System.EventHandler(this.tsbWarnings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(209, 36);
            this.toolStripLabel1.Text = "Schedule Options:";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(46, 36);
            // 
            // tsbScheduleOpen
            // 
            this.tsbScheduleOpen.Name = "tsbScheduleOpen";
            this.tsbScheduleOpen.Size = new System.Drawing.Size(46, 36);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.courseOptionsToolStripMenuItem,
            this.scheduleOptionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2428, 42);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCourseFileToolStripMenuItem,
            this.printToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(72, 38);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCourseFileToolStripMenuItem
            // 
            this.openCourseFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openCourseFileToolStripMenuItem.Image")));
            this.openCourseFileToolStripMenuItem.Name = "openCourseFileToolStripMenuItem";
            this.openCourseFileToolStripMenuItem.Size = new System.Drawing.Size(333, 44);
            this.openCourseFileToolStripMenuItem.Text = "Open Course File";
            this.openCourseFileToolStripMenuItem.Click += new System.EventHandler(this.openCourseFileToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(333, 44);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeToolStripMenuItem.Image")));
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(333, 44);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // courseOptionsToolStripMenuItem
            // 
            this.courseOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterCoursesToolStripMenuItem});
            this.courseOptionsToolStripMenuItem.Name = "courseOptionsToolStripMenuItem";
            this.courseOptionsToolStripMenuItem.Size = new System.Drawing.Size(200, 38);
            this.courseOptionsToolStripMenuItem.Text = "Course Options";
            // 
            // filterCoursesToolStripMenuItem
            // 
            this.filterCoursesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("filterCoursesToolStripMenuItem.Image")));
            this.filterCoursesToolStripMenuItem.Name = "filterCoursesToolStripMenuItem";
            this.filterCoursesToolStripMenuItem.Size = new System.Drawing.Size(293, 44);
            this.filterCoursesToolStripMenuItem.Text = "Filter Courses";
            this.filterCoursesToolStripMenuItem.Click += new System.EventHandler(this.filterCoursesToolStripMenuItem_Click);
            // 
            // scheduleOptionsToolStripMenuItem
            // 
            this.scheduleOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openExistingOpenToolStripMenuItem,
            this.saveCurrentScheduleToolStripMenuItem,
            this.clearScheduleToolStripMenuItem});
            this.scheduleOptionsToolStripMenuItem.Name = "scheduleOptionsToolStripMenuItem";
            this.scheduleOptionsToolStripMenuItem.Size = new System.Drawing.Size(224, 38);
            this.scheduleOptionsToolStripMenuItem.Text = "Schedule Options";
            // 
            // openExistingOpenToolStripMenuItem
            // 
            this.openExistingOpenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openExistingOpenToolStripMenuItem.Image")));
            this.openExistingOpenToolStripMenuItem.Name = "openExistingOpenToolStripMenuItem";
            this.openExistingOpenToolStripMenuItem.Size = new System.Drawing.Size(401, 44);
            this.openExistingOpenToolStripMenuItem.Text = "Open Existing Schedule";
            this.openExistingOpenToolStripMenuItem.Click += new System.EventHandler(this.openExistingOpenToolStripMenuItem_Click);
            // 
            // saveCurrentScheduleToolStripMenuItem
            // 
            this.saveCurrentScheduleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveCurrentScheduleToolStripMenuItem.Image")));
            this.saveCurrentScheduleToolStripMenuItem.Name = "saveCurrentScheduleToolStripMenuItem";
            this.saveCurrentScheduleToolStripMenuItem.Size = new System.Drawing.Size(401, 44);
            this.saveCurrentScheduleToolStripMenuItem.Text = "Save Current Schedule";
            this.saveCurrentScheduleToolStripMenuItem.Click += new System.EventHandler(this.saveCurrentScheduleToolStripMenuItem_Click);
            // 
            // clearScheduleToolStripMenuItem
            // 
            this.clearScheduleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearScheduleToolStripMenuItem.Image")));
            this.clearScheduleToolStripMenuItem.Name = "clearScheduleToolStripMenuItem";
            this.clearScheduleToolStripMenuItem.Size = new System.Drawing.Size(401, 44);
            this.clearScheduleToolStripMenuItem.Text = "Clear Schedule";
            this.clearScheduleToolStripMenuItem.Click += new System.EventHandler(this.clearScheduleToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2428, 887);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsbSaveSchedule);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "NHTI Scheduler 1.5 BETA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlSelectedCourses.ResumeLayout(false);
            this.tsbSaveSchedule.ResumeLayout(false);
            this.tsbSaveSchedule.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        //private System.Windows.Forms.TreeView tvAvailableCourses;
        private NHTI_Scheduler.MyTreeView tvAvailableCourses;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton tsbStrip;
        private System.Windows.Forms.ToolStripButton tsbClearAll;
        private System.Windows.Forms.ToolStrip tsbSaveSchedule;
        private System.Windows.Forms.ToolStripButton tsbWarnings;
        private System.Windows.Forms.ToolStripButton tsbFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton tsbScheduleOpen;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusReportDate;
        private System.Windows.Forms.ToolStripButton tsbCustom;
        private System.Windows.Forms.ToolStripProgressBar tsLoadProgress;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCourseFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem courseOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterCoursesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openExistingOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearScheduleToolStripMenuItem;
        private System.Windows.Forms.Panel pnlSelectedCourses;
        private System.Windows.Forms.TableLayoutPanel tblSelectedCourses;
    }
}

