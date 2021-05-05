using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Debug.WriteLine
using System.Diagnostics;
using System.Configuration;

using System.Reflection;
using System.IO;
using System.Drawing.Imaging;

using NHTI_Scheduler;

using SchedulerModel;
using fileRepo;

namespace NHTI_Scheduler
{

// Printing
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
    public partial class Form1 : Form
    {
        const string FILE_HEADER = "NHTI_SCHEDULER";     

        const int COL_MON = 1;
        const int COL_TUES = 2;
        const int COL_WED = 3;
        const int COL_THURS = 4;
        const int COL_FRI = 5;
        const int COL_ONLINE = 6;

        const int PEN_WIDTH = 1;
        const int DAY_WIDTH = 150;
        const int SEL_WIDTH = DAY_WIDTH - PEN_WIDTH;
        const int MONDAY_LEFT = 101;
        const int TUESDAY_LEFT = MONDAY_LEFT + DAY_WIDTH;
        const int WEDNESDAY_LEFT = TUESDAY_LEFT + DAY_WIDTH;
        const int THURSDAY_LEFT = WEDNESDAY_LEFT + DAY_WIDTH;
        const int FRIDAY_LEFT = THURSDAY_LEFT + DAY_WIDTH;
        const int OTHER_LEFT = FRIDAY_LEFT + DAY_WIDTH;

        const int ROW_START = 0;
        const int TIME_START = ROW_START + 1;
        const int COLUMN_START = 100;

        const int ONLINE_COURSE_COL = 6;
        const int ONLINE_COURSE_INCREMENT = 55; // 120;
        const int ONLINE_COURSE_HEIGHT = ONLINE_COURSE_INCREMENT - 1;

        const int OUT_OF_RANGE = -1;

        const int TOTAL_TIMES = 33;

        CCourse _selectedCourse;

        List<CCourse> lstScheduledCourses;
        List<CCustomBlock> customPanels;
        List<string> rejectedLines;

        List<int> tblHrs;
        List<int> tblMins;
        List<string> tblAmPm;
        List<string> tblDay;

        int hour;
        int minute;
        string amPm;

        int existCol;
        int newCol;

        Dictionary<string, List<CCourse>> course_map; // available courses
        int _nextOnlineRow;

        DateTime prevEndTime;
        DateTime prevStartTime;
        int prevTop;
        int prevBot;

        Color _colorSelectedCourse;
        Color _colorAddedCourse;

        ContextMenu popup_menu;

        CFilter filters;

        public Form1()
        {
            InitializeComponent();

            this.Text = "NHTI Scheduler " + typeof(Form1).Assembly.GetName().Version.ToString();

            lstScheduledCourses = new List<CCourse>();
            rejectedLines = new List<string>();
            customPanels = new List<CCustomBlock>();

            tblHrs = new List<int>();
            tblMins = new List<int>();
            tblAmPm = new List<string>();
            tblDay = new List<string>();

            filters = new CFilter();

            pnlSelectedCourses.AutoScroll = true;
            pnlSelectedCourses.AllowDrop = true;

            _nextOnlineRow = TIME_START;
            _selectedCourse = null;

            _colorSelectedCourse = Color.LightGreen;
            _colorAddedCourse = Color.LightGray;

            popup_menu = new ContextMenu();
            popup_menu.MenuItems.Add(new MenuItem("Remove", removeItemMenuOption_Click));

            DateTime start = DateTime.Parse("8:00 AM");

            // add times
            for (int i = 0; i < 33; i++)
            {
                Label t = new Label();
                t.Text = start.ToShortTimeString();
                t.TextAlign = ContentAlignment.MiddleCenter;
                t.Width = 70;
                t.Location = new Point(10, 10);
                t.Size = new Size(200, 30);
                t.Font = new Font("Arial", 12, FontStyle.Bold);

                tblSelectedCourses.SuspendLayout();
                tblSelectedCourses.Controls.Add(t, 0, i + 1);
                tblSelectedCourses.ResumeLayout();

                start = start.AddMinutes(30);

            }// end for each time 

            // add days
            start = new DateTime(2015, 5, 18); // Monday

            for (int i = 0; i < 5; i++)
            {
                Label day = new Label();
                day.Text = start.DayOfWeek.ToString();
                day.TextAlign = ContentAlignment.TopCenter;
                day.Width = 90;
                day.Location = new Point(10, 10);
                day.Size = new Size(200, 30);
                day.Font = new Font("Arial", 12, FontStyle.Bold | FontStyle.Underline);

                tblSelectedCourses.SuspendLayout();
                tblSelectedCourses.Controls.Add(day, i + 1, 0);
                tblSelectedCourses.ResumeLayout();

                start = start.AddHours(24);
            }

            // add last column
            Label online = new Label();
            online.Text = "Online";
            online.TextAlign = ContentAlignment.TopCenter;
            online.Width = 90;
            online.Location = new Point(10, 10);
            online.Size = new Size(200, 30);
            online.Font = new Font("Arial", 12, FontStyle.Bold | FontStyle.Underline);

            tblSelectedCourses.SuspendLayout();
            tblSelectedCourses.Controls.Add(online, 6, 0);
            tblSelectedCourses.ResumeLayout();

            for (int days = 33; days != tblSelectedCourses.Controls.Count - 1; days++)
            {
                string day = tblSelectedCourses.Controls[days].Text;

                tblDay.Add(day);
            }

            for(int i = 0; i < TOTAL_TIMES; i++)
            {
                string time = tblSelectedCourses.Controls[i].Text;

                if(time.Length == 7)
                {
                    hour = Convert.ToInt32(time.Substring(0, 1));
                    minute = Convert.ToInt32(time.Substring(2, 2));
                    amPm = time.Substring(5, 2);
                }
                else 
                {
                    hour = Convert.ToInt32(time.Substring(0, 2));
                    minute = Convert.ToInt32(time.Substring(3, 2));
                    amPm = time.Substring(6, 2);
                }

                tblHrs.Add(hour);
                tblMins.Add(minute);
                tblAmPm.Add(amPm);
            }
        }

        private Panel createCustomBlock(CCustomBlock block)
        {
            Color color = Color.LightGray;

            Panel pnl = new Panel();
            Panel heightAdjust = new Panel();
            TextBox customText = new TextBox();
            Label closeCustom = new Label();

            pnl.Width = SEL_WIDTH;

            pnl.Height = block.height;
            tblSelectedCourses.SetRowSpan(pnl, block.spanset);
            tblSelectedCourses.SetColumn(pnl, block.col);
            tblSelectedCourses.SetRow(pnl, block.row);
            pnl.BackColor = color;

            heightAdjust.Cursor = Cursors.SizeNS; // up - down arrow
            heightAdjust.Anchor = AnchorStyles.Bottom; // glue to bottom of main panel
            heightAdjust.BackColor = color;
            heightAdjust.Location = new Point(0, 213); // on bottom of form
            heightAdjust.Width = pnl.Width;

            // height adjuster callbacks 
            heightAdjust.MouseDown += panel1_MouseDown;
            heightAdjust.MouseUp += panel1_MouseUp;
            heightAdjust.MouseMove += panel1_MouseMove;

            customText.Size = new System.Drawing.Size(116, 173);
            customText.Location = new Point(16, 19);
            customText.BackColor = color;
            customText.BorderStyle = BorderStyle.None;
            customText.TextAlign = HorizontalAlignment.Center;
            customText.Text = block.text;
            customText.Font = new Font("Arial", 12);
            customText.Multiline = true;

            // callback to save text to object attached to panel 
            customText.KeyUp += textBox1_KeyUp;

            closeCustom.Size = new System.Drawing.Size(18, 18);
            closeCustom.Location = new Point(128, 3);
            closeCustom.Text = "  ";

            closeCustom.MouseDown += lblDeletePnl_MouseDown;
            closeCustom.MouseEnter += lblDeletePnl_MouseEnter;
            closeCustom.MouseLeave += lblDeletePnl_MouseLeave;

            // assemble object 
            pnl.Controls.Add(heightAdjust);
            pnl.Controls.Add(customText);
            pnl.Controls.Add(closeCustom);

            CCustomBlock cblock = new CCustomBlock(tblSelectedCourses.GetRowSpan(pnl),
                                                    tblSelectedCourses.GetRow(pnl),
                                                    pnl.Height,                                                     
                                                    tblSelectedCourses.GetColumn(pnl),
                                                     customText.Text.TrimStart(' '));
            pnl.Tag = cblock; // for saving later.  Updated in callbacks.  

            return pnl;
        }

        private Panel createCustomBlock()
        {
            Color color = Color.LightGray;

            Panel pnl = new Panel();
            Panel heightAdjust = new Panel();
            TextBox customText = new TextBox();
            Label closeCustom = new Label();

            pnl.Width = SEL_WIDTH;
            pnl.Height = 225; // big enough to show
            pnl.Location = new Point(150, 150);
            pnl.BackColor = color;
            pnl.BorderStyle = BorderStyle.Fixed3D;

            heightAdjust.Cursor = Cursors.SizeNS; // up - down arrow
            heightAdjust.Anchor = AnchorStyles.Bottom; // glue to bottom of main panel
            heightAdjust.BackColor = color;
            heightAdjust.Location = new Point(0, 213); // on bottom of form
            heightAdjust.Width = pnl.Width;

            // height adjuster callbacks 
            heightAdjust.MouseDown += panel1_MouseDown;
            heightAdjust.MouseUp += panel1_MouseUp;
            heightAdjust.MouseMove += panel1_MouseMove;

            customText.Size = new System.Drawing.Size(116, 173);
            customText.Location = new Point(16, 19);
            customText.BackColor = color;
            customText.BorderStyle = BorderStyle.None;
            customText.TextAlign = HorizontalAlignment.Center;
            customText.Font = new Font("Arial", 12);
            customText.Multiline = true;

            // callback to save text to object attached to panel 
            customText.KeyUp += textBox1_KeyUp;

            closeCustom.Size = new System.Drawing.Size(18, 18);
            closeCustom.Location = new Point(128, 3);
            closeCustom.Text = "  ";

            closeCustom.MouseDown += lblDeletePnl_MouseDown;
            closeCustom.MouseEnter += lblDeletePnl_MouseEnter;
            closeCustom.MouseLeave += lblDeletePnl_MouseLeave;

            // assemble object 
            pnl.Controls.Add(heightAdjust);
            pnl.Controls.Add(customText);
            pnl.Controls.Add(closeCustom);

            CCustomBlock cblock = new CCustomBlock(tblSelectedCourses.GetRowSpan(pnl),
                                                    tblSelectedCourses.GetRow(pnl),
                                                    pnl.Height,
                                                    tblSelectedCourses.GetColumn(pnl),
                                                     customText.Text.TrimStart(' '));
            pnl.Tag = cblock; // for saving later.  Updated in callbacks.  

            return pnl;
        }// end create custom block


        private Panel createBlock(CCourse course, CCourseTime time = null)
        {
            Panel pnlTemp = new Panel();

            pnlTemp.BackColor = Color.LightGray;
            pnlTemp.Width = SEL_WIDTH;

            if (time != null)
            {
                TimeSpan span = time.end - time.begin;
                pnlTemp.Height = Convert.ToInt32(span.TotalMinutes - 1.0); // subtract one so the bottom line shows 
            }
            else // no time, default to 
            {
                pnlTemp.Height = ONLINE_COURSE_HEIGHT;
            }

            Label lblCourse = new Label();
            lblCourse.Text = course.course;
            lblCourse.Location = new Point(0, 5); // new Point(0, 10);
            lblCourse.AutoSize = false;
            lblCourse.Height = 20;
            lblCourse.Width = pnlTemp.Width - 2;
            lblCourse.TextAlign = ContentAlignment.MiddleCenter;

            // add ability to select course 
            lblCourse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDown);
            lblCourse.ContextMenu = popup_menu;

            pnlTemp.Controls.Add(lblCourse);

            Label lblCRN = new Label();
            lblCRN.Text = course.crn.ToString();
            lblCRN.Location = new Point(0, 20); //new Point(0, 30);
            lblCRN.AutoSize = false;
            lblCRN.Width = pnlTemp.Width - 2;
            lblCRN.Height = 20;
            lblCRN.TextAlign = ContentAlignment.MiddleCenter;

            // add ability to select course 
            lblCRN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDown);
            lblCRN.ContextMenu = popup_menu;
            pnlTemp.Controls.Add(lblCRN);

            if (time != null) // also have to get hybrid / remote, etc if time == null 
            {
                Label lblLoc = new Label();
                lblLoc.Text = time.location;

                if (course.venue == CCourse.VENUE.HYBRID)
                {
                    lblLoc.Text += "+ HY";
                }

                lblLoc.Location = new Point(0, 35); //new Point(0, 30);
                lblLoc.AutoSize = false;
                lblLoc.Width = pnlTemp.Width - 2;
                lblLoc.Height = 20;
                lblLoc.TextAlign = ContentAlignment.MiddleCenter;

                // add ability to select course 
                lblLoc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDown);
                lblLoc.ContextMenu = popup_menu;

                pnlTemp.Controls.Add(lblLoc);
            }
            else if (course.venue == CCourse.VENUE.ONLINE)
            {
                Label lblLoc = new Label();


                lblLoc.Text = "ONLINE";

                lblLoc.Location = new Point(0, 35); //new Point(0, 30);
                lblLoc.AutoSize = false;
                lblLoc.Width = pnlTemp.Width - 2;
                lblLoc.Height = 20;
                lblLoc.TextAlign = ContentAlignment.MiddleCenter;

                // add ability to select course 
                lblLoc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDown);
                lblLoc.ContextMenu = popup_menu;

                pnlTemp.Controls.Add(lblLoc);
            }
            else if (course.venue == CCourse.VENUE.OTHER)
            {
                Label lblLoc = new Label();


                lblLoc.Text = course.building;

                lblLoc.Location = new Point(0, 35); //new Point(0, 30);
                lblLoc.AutoSize = false;
                lblLoc.Width = pnlTemp.Width - 2;
                lblLoc.Height = 20;
                lblLoc.TextAlign = ContentAlignment.MiddleCenter;

                // add ability to select course 
                lblLoc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDown);
                lblLoc.ContextMenu = popup_menu;

                pnlTemp.Controls.Add(lblLoc);
            }


            // add drag drop capability
            pnlTemp.AllowDrop = true;
            pnlTemp.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvSelectedCourses_DragDrop);
            pnlTemp.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvSelectedCourses_DragEnter);
            pnlTemp.DragOver += new System.Windows.Forms.DragEventHandler(this.lvSelectedCourses_DragOver);

            // add ability to select course 
            pnlTemp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblSelectedCourses_MouseDown);
            pnlTemp.ContextMenu = popup_menu;
            pnlTemp.Tag = course; // link back to course

            return pnlTemp;
        }
        

        private void displayCourseMap(Dictionary<string, List<CCourse>> courseMap, CFilter filter)
        {
            TreeNode node;
            TreeNode node_course;

            tvAvailableCourses.Tag = courseMap;

            tvAvailableCourses.Nodes.Clear();

            foreach (string subject in courseMap.Keys)
            {
                node = new TreeNode();

                node.Text = subject;
                node.Tag = courseMap[subject];

                foreach (CCourse c in course_map[subject])
                {
                    node_course = new TreeNode();
                    node_course.Text = c.description;
                    node_course.Tag = c;

                    foreach (CCourseTime t in c.times) // courses can have multiple times
                    {
                        string tm = string.Format("{0, -8} - {1, -8}  {2}", t.begin.ToShortTimeString(), t.end.ToShortTimeString(), t.days);

                        tm += " (" + t.location + ")";

                        node_course.Nodes.Add(tm);
                    }

                    string venue = "";

                    switch (c.venue)
                    {
                        case CCourse.VENUE.DAY:

                            venue += c.building + " " + c.room;

                            break;
                        case CCourse.VENUE.HYBRID:
                            venue += "HYBRID";
                            break;
                        case CCourse.VENUE.EVENING:
                            venue += c.building + " " + c.room;
                            break;
                        case CCourse.VENUE.OFF_CAMPUS:
                            venue += "Off Campus";
                            break;
                        case CCourse.VENUE.ONLINE:
                            venue += "Online";
                            break;
                        case CCourse.VENUE.ONLINE_REMOTE:
                            venue += "Online - Remote";
                            break;
                        case CCourse.VENUE.OTHER:
                            venue += c.building;
                            break;
                        default:
                            venue += "Unknown";
                            break;
                    }

                    node_course.Nodes.Add(venue);

                    if (c.Extra != null)
                    {
                        node_course.Nodes.Add(c.Extra);
                    }

                    node_course.Nodes.Add(c.title);
                    node_course.Nodes.Add("Credits: " + c.credits);
                    node_course.Nodes.Add("Max: " + c.max.ToString());
                    node_course.Nodes.Add("Remaining: " + c.remaining.ToString());
                    node_course.Nodes.Add(c.faculty);

                    if (filter != null)
                    {

                        if (filter[(int)CFilter.FILTERS.REMAINING_MIN].action != CFilter.FILTERS_ACTION.NONE)
                        {
                            // default is highlight 
                            if (c.remaining <= Int32.Parse(filter[(int)CFilter.FILTERS.REMAINING_MIN].filter_value))
                            {
                                node_course.BackColor = Color.LightGray;
                            }
                        }

                        if (filter[(int)CFilter.FILTERS.DAYS_INCLUDED].action != CFilter.FILTERS_ACTION.NONE)
                        {
                            // check against days

                            string days_to_include = filter[(int)CFilter.FILTERS.DAYS_INCLUDED].filter_value;
                            bool includes_days = false; // days not included

                            foreach (CCourseTime t in c.times) // courses can have multiple times
                            {
                                foreach (char d in t.days)
                                {
                                    if (days_to_include.IndexOf(d) >= 0) includes_days = true;
                                }
                            }

                            if (!includes_days) node_course.BackColor = Color.LightGray;

                        }

                    }// end filter != null 

                    node.Nodes.Add(node_course);

                }

                tvAvailableCourses.Nodes.Add(node);
            }

        }

        private void tvAvailableCourses_MouseDown(object sender, MouseEventArgs e)
        {
            // prepare to drag and drop course

            TreeNode selected_node = null;

            Debug.WriteLine("Mouse down.");

            try
            {
                selected_node = tvAvailableCourses.GetNodeAt(e.X, e.Y);

                tvAvailableCourses.SelectedNode = selected_node;
            }
            catch { return; } // no node 

            if (selected_node == null) return; // belt and suspenders

            if (selected_node.Tag is CCourse)
            {
                Debug.WriteLine("Start drag drop.");
                tvAvailableCourses.DoDragDrop(selected_node.Tag, DragDropEffects.Copy | DragDropEffects.Move);
            }

        }

        private void lvSelectedCourses_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(CCourse)) is CCourse)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lvSelectedCourses_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(CCourse)) is CCourse)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lvSelectedCourses_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(CCourse)) is CCourse)
            {
                CCourse addMe = (e.Data.GetData(typeof(CCourse)) as CCourse);

                scheduleCourse(addMe);
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void scheduleCourse(CCourse addMe)
        {
            Panel tempPanel;
            int top = 0;
            int bot = 0;
            int rowCount = 0;
            string classFullTime;
            Control onlinePos;

            if (addMe.venue == CCourse.VENUE.ONLINE || addMe.venue == CCourse.VENUE.OTHER)
            {
                tempPanel = createBlock(addMe, null);

                for (int r = 1; r < tblSelectedCourses.RowCount - 1; r++)
                {
                    onlinePos = tblSelectedCourses.GetControlFromPosition(tblSelectedCourses.ColumnCount - 1, r);

                    if(onlinePos != null)
                    {
                        if (onlinePos is Panel)
                        {
                            Panel p = onlinePos as Panel;

                            if (p.Tag is CCourse)
                            {
                                CCourse existing = p.Tag as CCourse; // get course to show CRN

                                if(existing.crn == addMe.crn)
                                {
                                    MessageBox.Show(null, addMe.crn.ToString() + " already exists.", "Warning", MessageBoxButtons.OK);

                                    return;
                                }
                            }
                        }
                    }

                }

                for (int i = 1; i < tblSelectedCourses.RowCount - 1; i++)
                {
                    onlinePos = tblSelectedCourses.GetControlFromPosition(tblSelectedCourses.ColumnCount - 1, i);

                    if(onlinePos == null)
                    {
                        tblSelectedCourses.SetColumn(tempPanel, tblSelectedCourses.ColumnCount - 1);
                        tblSelectedCourses.SetRow(tempPanel, i);
                        tblSelectedCourses.SetRowSpan(tempPanel, 2);
                        tempPanel.Height = 118;

                        tblSelectedCourses.SuspendLayout();
                        tblSelectedCourses.Controls.Add(tempPanel);
                        tblSelectedCourses.ResumeLayout();

                        return;
                    }                    
                }

                lstScheduledCourses.Add(addMe);

                return; // done adding online course
            }

            List<Panel> allPartsOfCourse = new List<Panel>();

            // create a panel for each course time
            foreach (CCourseTime time in addMe.times)
            {
                // insert time in each day
                for (int i = 0; i < time.days.Length; i++)
                {
                    tempPanel = createBlock(addMe, time);

                    char day = time.days[i];

                    int left = dayToIndex(day);

                    if (left == OUT_OF_RANGE) return; // do not add course 

                    string beginTime = time.begin.ToShortTimeString();
                    string endTimeHour = time.end.ToShortTimeString().Substring(0, 2);

                    int endTimeMin = time.end.Minute;
                    int endTimeLength = time.end.ToShortTimeString().Length;
                    string endTimeAmPm = time.end.ToShortTimeString().Substring(endTimeLength - 2, 2);

                    if (endTimeHour.Contains(":"))
                    {
                        string singleDigit = endTimeHour.TrimEnd(':');

                        endTimeHour = singleDigit;
                    }

                    //start time of course
                    for (int r = 1; r < 34; r++)
                    {
                        if (tblSelectedCourses.GetControlFromPosition(0, r).ToString().Contains(beginTime) && beginTime.Length == 8) // EX: 12:00 PM
                        {
                            top = r;

                            break;
                        }
                        else if (tblSelectedCourses.GetControlFromPosition(0, r).ToString().Contains(beginTime) && beginTime.Length == 7) // EX: 9:00 AM
                        {
                            if (r == 9 || r == 10)
                            {
                                //do nothing
                            }
                            else
                            {
                                top = r;

                                break;
                            }
                        }
                        else if (prevEndTime.AddMinutes(1).Equals(time.begin))
                        {
                            top = prevBot + 1;

                            break;
                        }
                        //edge case 1
                        else if (beginTime.Contains("8") && beginTime.Contains("31"))
                        {
                            top = 26;

                            break;
                        }
                        //edge case 2
                        else if(beginTime.Contains("7") && beginTime.Contains("11"))
                        {
                            top = 24;

                            break;
                        }
                    }

                    //set end time of course
                    for (int b = 1; b < 34; b++)
                    {
                        classFullTime = tblSelectedCourses.GetControlFromPosition(0, b).ToString();

                        if (classFullTime.Contains(endTimeHour) && classFullTime.Contains(endTimeAmPm) && time.end.ToShortTimeString().Length == 8)
                        {
                            bot = b;

                            break;
                        }
                        else if (classFullTime.Contains(endTimeHour + ':') && classFullTime.Contains(endTimeAmPm) && endTimeHour.Length == 1)
                        {
                            if (b == 9 || b == 10)
                            {
                                //do nothing
                            }
                            else
                            {
                                bot = b;

                                break;
                            }
                        }
                        else if (classFullTime.Contains(endTimeHour + ':') && classFullTime.Contains(endTimeAmPm) && endTimeHour.Length == 31)
                        {
                            bot = b;
                        }
                    }

                    //keep track of previous times
                    prevStartTime = time.begin;
                    prevEndTime = time.end;
                    prevTop = top;
                    prevBot = bot;

                    //determines how many time slots the class spans
                    if (endTimeMin <= 30 && endTimeMin > 0)
                    {
                        rowCount = (bot - top) + 1;
                    }
                    else if (endTimeMin > 30 && endTimeMin < 60)
                    {
                        rowCount = (bot - top) + 2;
                    }
                    else if (endTimeMin == 00)
                    {
                        rowCount = (bot - top);
                    }

                    tempPanel.Location = new Point(left, top);

                    tblSelectedCourses.SetRowSpan(tempPanel, rowCount);

                    //slot adjustments for online courses
                    if (rowCount == 2)
                    {
                        tempPanel.Height += ONLINE_COURSE_HEIGHT;
                    }
                    else if (rowCount == 3)
                    {
                        tempPanel.Height += ONLINE_COURSE_HEIGHT + 30;
                    }
                    else if (rowCount == 4)
                    {
                        tempPanel.Height += ONLINE_COURSE_HEIGHT + 60;
                    }
                    else if (rowCount == 5)
                    {
                        tempPanel.Height += ONLINE_COURSE_HEIGHT + 90;
                    }
                    else if (rowCount == 6)
                    {
                        tempPanel.Height += ONLINE_COURSE_HEIGHT + 120;
                    }
                    else if (rowCount == 7)
                    {
                        tempPanel.Height += ONLINE_COURSE_HEIGHT + 150;
                    }
                    else if (rowCount == 8)
                    {
                        tempPanel.Height += ONLINE_COURSE_HEIGHT + 180;
                    }

                    allPartsOfCourse.Add(tempPanel);
                }
            }

            // check each panel for overlap with existing course
            foreach (Panel panel in allPartsOfCourse)
            {
                // search all existing courses for overlap
                foreach (Control ctrl in tblSelectedCourses.Controls)
                {
                    int col = tblSelectedCourses.GetColumn(ctrl);
                    int row = tblSelectedCourses.GetRow(ctrl);
                    int rowHeight = tblSelectedCourses.GetRowSpan(ctrl) + row - 1;

                    if (ctrl is Panel)
                    {
                        Panel p = ctrl as Panel;

                        if (p.Tag is CCourse)
                        {
                            CCourse existing = p.Tag as CCourse; // get course to show CRN

                            //start and end time of course being added
                            for (int i = 0; i < addMe.times.Count; i++)
                            {
                                string begin = addMe.times[i].begin.ToShortTimeString();
                                int begHr = addMe.times[i].begin.Hour;
                                int begMin = addMe.times[i].begin.Minute;
                                string day = addMe.times[i].days;

                                string end = addMe.times[i].end.ToShortTimeString();
                                int endHr = addMe.times[i].end.Hour;
                                int endMin = addMe.times[i].end.Minute;

                                //start and end time of existing
                                for (int e = 0; e < existing.times.Count; e++)
                                {
                                    string existBegin = existing.times[e].begin.ToShortTimeString();
                                    int existBegHr = existing.times[e].begin.Hour;
                                    int existBegMin = existing.times[e].begin.Minute;
                                    string existDay = existing.times[e].days;

                                    string existEnd = existing.times[e].end.ToShortTimeString();
                                    int existEndHr = existing.times[e].end.Hour;
                                    int existEndMin = existing.times[e].end.Minute;

                                    existCol = 0;
                                    newCol = 0;

                                    if(day.Length > 1 || existDay.Length > 1)
                                    {
                                        existCol = col;
                                        newCol = panel.Location.X;
                                    }
                                    else if(day.Length == 1 && existDay.Length == 1)
                                    {
                                        existCol = findDayCol(day);
                                        newCol = findDayCol(existDay);
                                    }

                                    //only if in the same column
                                    if(newCol.Equals(existCol))
                                    {
                                        //the same
                                        if (begHr == existBegHr && endHr == existEndHr)
                                        {
                                            MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                            return;
                                        }
                                        //inside one another
                                        else if (begHr >= existBegHr && endHr <= existEndHr)
                                        {
                                            MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                            return;
                                        }
                                        else if (begHr <= existBegHr && endHr >= existEndHr)
                                        {
                                            MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                            return;
                                        }
                                        else if (begHr >= existBegHr && begHr <= existEndHr && endHr >= existEndHr)
                                        {
                                            if (existEndMin < 30 && begMin < 30)
                                            {
                                                MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                                return;
                                            }
                                            else if (existEndMin >= 30 && begMin >= 30)
                                            {
                                                MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                                return;
                                            }
                                            else if (existEndMin >= endMin)
                                            {
                                                MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                                return;
                                            }
                                        }
                                        else if (begHr <= existBegHr && endHr >= existBegHr && endHr <= existEndHr)
                                        {
                                            if (existEndMin < 30 && begMin < 30)
                                            {
                                                MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                                return;
                                            }
                                            else if (existEndMin >= 30 && begMin >= 30)
                                            {
                                                MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                                return;
                                            }
                                            else if (existBegMin <= endMin)
                                            {
                                                MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing " + existing.crn.ToString() + ".  Remove existing course.", "Warning", MessageBoxButtons.OK);

                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if(p.Tag is CCustomBlock)
                        {
                            //check for conflict with custom block
                            if (top >= row && top <= rowHeight && panel.Location.X.Equals(col) ||
                                top <= row && top >= rowHeight && panel.Location.X.Equals(col) ||
                                top < row && bot > rowHeight && panel.Location.X.Equals(col) ||
                                bot <= rowHeight && bot >= row && panel.Location.X.Equals(col) ||
                                bot >= rowHeight && bot <= row && panel.Location.X.Equals(col))
                            {                             


                                MessageBox.Show(null, addMe.crn.ToString() + " Overlaps with existing custom block. Please resize or remove this block.", "Warning", MessageBoxButtons.OK);

                                return;
                            }
                        }
                    }
                }
            }

            // no overlap, add each panel to the schedule
            foreach (Panel panel in allPartsOfCourse)
            {
                string customCellPos = tblSelectedCourses.GetRowSpan(panel).ToString();

                tblSelectedCourses.SuspendLayout();

                tblSelectedCourses.Controls.Add(panel, panel.Left, panel.Top); //left is column, top is row

                tblSelectedCourses.ResumeLayout();
            }

            lstScheduledCourses.Add(addMe);

            return;
        }

        // location service  
        private int dayToIndex(char d)
        {
            switch (d)
            {
                case 'M':
                case 'm':
                    return COL_MON;
                case 'T':
                case 't':
                    return COL_TUES;
                case 'W':
                case 'w':
                    return COL_WED;
                case 'R':
                case 'r':
                    return COL_THURS;
                case 'F':
                case 'f':
                    return COL_FRI;
            }

            return -1;
        }

        // location service
        private int dayToX(char day)
        {
            switch (day)
            {
                case 'M':
                case 'm':
                    return MONDAY_LEFT;
                case 'T':
                case 't':
                    return TUESDAY_LEFT;
                case 'W':
                case 'w':
                    return WEDNESDAY_LEFT;
                case 'R':
                case 'r':
                    return THURSDAY_LEFT;
                case 'F':
                case 'f':
                    return FRIDAY_LEFT;
            }

            return OUT_OF_RANGE; // error 
        }

        private void removeItemMenuOption_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = null;
            Control sourceControl = null;

            // Try to cast the sender to a MenuItem
            if (sender is MenuItem)
                menuItem = sender as MenuItem;
            else return;

            if (menuItem != null)
            {
                // Retrieve the ContextMenu that contains this MenuItem
                ContextMenu menu = menuItem.GetContextMenu();

                // Get the control that is displaying this context menu
                sourceControl = menu.SourceControl;
            }
            else return;

            Panel thePanel = null;

            if (sourceControl is Panel)
            {
                thePanel = sourceControl as Panel;
            }
            else if (sourceControl is Label)
            {
                if ((sourceControl as Label).Parent is Panel)
                {
                    thePanel = (sourceControl as Label).Parent as Panel;
                }
            }

            if (thePanel == null) return; // not valid source control 

            CCourse selCourse = null;

            if (thePanel.Tag is CCourse)
                selCourse = thePanel.Tag as CCourse;

            if (selCourse == null) return;

            DialogResult result = MessageBox.Show("Remove " + selCourse.crn.ToString(),
                                                        "Remove Course?", MessageBoxButtons.YesNoCancel,
                                                        MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                for (int i = tblSelectedCourses.Controls.Count - 1; i >= 0; i--)
                {
                    Control obj = tblSelectedCourses.Controls[i];

                    if (obj is Panel)
                    {
                        if ((obj as Panel).Tag is CCourse)
                        {
                            if ((obj as Panel).Tag == selCourse)
                            {
                                tblSelectedCourses.Controls.Remove(obj);
                                lstScheduledCourses.Remove(selCourse);
                            }
                        }
                    }
                }
            }
        }

        //future function
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Panel thePanel = null;

            if (sender is Panel)
            {
                thePanel = sender as Panel;
            }
            else if (sender is Label)
            {
                if ((sender as Label).Parent is Panel)
                {
                    thePanel = (sender as Label).Parent as Panel;
                }
            }

            if (thePanel == null) return;

            CCourse selCourse = null;

            if (thePanel.Tag is CCourse)
                selCourse = thePanel.Tag as CCourse;

            if (selCourse == null) return;

            MessageBox.Show("CRN: " + selCourse.crn.ToString(), "Course Info",
                                                       MessageBoxButtons.OK,
                                                        MessageBoxIcon.Information);

        }

        private void tblSelectedCourses_MouseDown(object sender, MouseEventArgs e)
        {
            // change color of selected course 

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Panel thePanel = null;

                if (sender is Panel)
                {
                    thePanel = sender as Panel;
                }
                else if (sender is Label)
                {
                    if ((sender as Label).Parent is Panel)
                    {
                        thePanel = (sender as Label).Parent as Panel;
                    }
                }

                if (thePanel == null) return;

                CCourse selCourse = null;

                if (thePanel.Tag is CCourse)
                    selCourse = thePanel.Tag as CCourse;

                if (selCourse == null)
                {
                    if (_selectedCourse == null) return;

                    // undo selected course color
                    foreach (Control obj in tblSelectedCourses.Controls)
                    {
                        if (obj is Panel)
                        {
                            if ((obj as Panel).Tag == _selectedCourse)
                                (obj as Panel).BackColor = this._colorAddedCourse;
                        }
                    }

                    _selectedCourse = null; // no course now selected

                    return;
                }

                if (_selectedCourse == null) // no previously selected course
                {
                    _selectedCourse = selCourse;
                }
                else if (_selectedCourse == selCourse)
                {
                    return; // course already selected 
                }
                else
                {
                    foreach (Control obj in tblSelectedCourses.Controls)
                    {
                        if (obj is Panel)
                        {
                            if ((obj as Panel).Tag == _selectedCourse)
                                (obj as Panel).BackColor = this._colorAddedCourse;
                        }
                    }

                    _selectedCourse = selCourse;
                }

                foreach (Control obj in tblSelectedCourses.Controls)
                {
                    if (obj is Panel)
                    {
                        if ((obj as Panel).Tag == _selectedCourse)
                            (obj as Panel).BackColor = this._colorSelectedCourse;
                    }
                }
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;

            Bitmap memoryImage = getScheduleAsBitmap();

            // write the image to the printer args object e: 
            e.Graphics.DrawImage(memoryImage, 0, 0, memoryImage.Width - 250, memoryImage.Height - 1180);     
        }

        private void ClearSchedule()
        {
            //if (lstScheduledCourses.Count < 1 && customPanels.Count < 1) return; // nothing to clear 

            if(tblSelectedCourses.Controls.Count == 39)
            {
                MessageBox.Show("No courses to clear.", "No Courses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DialogResult result = MessageBox.Show("Clear all scheduled courses and custom blocks?",
                                            "Clear Courses?", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                tblSelectedCourses.SuspendLayout();

                for (int i = tblSelectedCourses.Controls.Count - 1; i >= 0; i--)
                {
                    if (tblSelectedCourses.Controls[i] is Panel)
                    {
                        if (tblSelectedCourses.Controls[i].Tag is CCourse || tblSelectedCourses.Controls[i].Tag is CCustomBlock)
                        {
                            tblSelectedCourses.Controls.RemoveAt(i);
                        }
                    }

                }

                tblSelectedCourses.ResumeLayout();

                if (lstScheduledCourses != null) lstScheduledCourses.Clear();

            }// end user wants to delete all courses 
        }

        private void tsbClearAll_Click(object sender, EventArgs e)
        {
            ClearSchedule();
        }

        private void tsbWarnings_Click(object sender, EventArgs e)
        {
            frmWarnings fw = new frmWarnings(rejectedLines);

            fw.ShowDialog();
        }

        private Bitmap getScheduleAsBitmap()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(1050, 2500);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); // setup background

                int left = COLUMN_START;

                var p = new Pen(Color.Black, PEN_WIDTH);

                // draw vertical lines
                for (int i = 0; i < 6; i++)
                {
                    //                            top                  bottom 
                    g.DrawLine(p, new Point(left, 10), new Point(left, 1000));

                    left += DAY_WIDTH;
                }

                // draw horizontal lines
                int top = ROW_START;

                for (int i = 0; i < 17; i++)
                {
                    //                    left                 right
                    g.DrawLine(p, new Point(0, top), new Point(1000, top));
                    top += 60;
                }

                foreach (Control ctrl in pnlSelectedCourses.Controls)
                {
                    if (ctrl is Label) // then draw day and times to bitmap
                    {
                        Label lblTmp = ctrl as Label;
                        g.DrawString(lblTmp.Text, lblTmp.Font, new SolidBrush(Color.Black), new PointF(lblTmp.Left, lblTmp.Top));
                    }
                    else if (ctrl is Panel) // then draw course to bitmap
                    {
                        Panel pnlTmp = ctrl as Panel;
                        pnlTmp.DrawToBitmap(bmp, new Rectangle(pnlTmp.Left, pnlTmp.Top, pnlTmp.Width, pnlTmp.Height));
                    }

                }// end for each thing in panel 

            }// end using graphics 

            return bmp;
        }

        #region CUSTOM_BLOCK_VERTICAL_RESIZE_EVENTS
        bool allowResize = false;
        bool panelClicked = false;
        int customRowSpan = 0;
        int customRow = 0;
        int customColumn = 0;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            allowResize = true;
            panelClicked = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            var cellPos = GetRowColIndex(tblSelectedCourses, tblSelectedCourses.PointToClient(Cursor.Position));

            if (panelClicked == true)
            {
                if (sender is Panel)
                {
                    Panel resizer = (sender as Panel);

                    if (resizer.Parent is Panel)
                    {
                        try
                        {
                            Panel parent = resizer.Parent as Panel;

                            if (allowResize && parent.Height >= parent.MinimumSize.Height)
                                parent.Height = resizer.Top + e.Y;

                            customRowSpan = tblSelectedCourses.GetRow(parent) - 1;

                            customColumn = tblSelectedCourses.GetColumn(parent);

                            customRow = tblSelectedCourses.GetRow(parent) + tblSelectedCourses.GetRowSpan(parent);

                            Control dontBumpExist = tblSelectedCourses.GetControlFromPosition( customColumn, customRow);

                            if (dontBumpExist != null)
                            {
                                return;
                            }
                            else if(dontBumpExist == null )
                            {
                                allowResize = true;                                
                                tblSelectedCourses.SetRowSpan(parent, cellPos.Value.Y - customRowSpan);
                                return;
                            }                            
                        }
                        catch
                        {
                            return;
                        }                   
                    }
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            allowResize = false;
            panelClicked = false;

            if (sender is Panel)
            {
                Panel pnl = sender as Panel;

                if (pnl.Parent is Panel) pnl = pnl.Parent as Panel; // don't need child ctrl 


                if (pnl.Tag is CCustomBlock)
                {
                    CCustomBlock cblk = (pnl.Tag as CCustomBlock);
                    cblk.resetHeight(pnl.Height);
                }
            }
        }
        #endregion CUSTOM_BLOCK_VERTICAL_RESIZE_EVENTS

        private void lblDeletePnl_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Label)
            {
                Label lbl = sender as Label;

                if (lbl.Parent is Panel)
                {
                    Panel pnl = lbl.Parent as Panel;

                    try
                    {
                        customPanels.Remove(pnl.Tag as CCustomBlock);
                    }
                    catch
                    {
                        // nothing to do 
                    }

                    tblSelectedCourses.SuspendLayout();

                    for (int i = tblSelectedCourses.Controls.Count - 1; i >= 0; i--)
                    {
                        if (tblSelectedCourses.Controls[i] is Panel)
                        {
                            if ((tblSelectedCourses.Controls[i] as Panel) == pnl)
                            {

                                tblSelectedCourses.Controls.RemoveAt(i);
                            }
                        }
                    }

                    tblSelectedCourses.ResumeLayout();
                }
            }
        }

        private void lblDeletePnl_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Label) (sender as Label).Text = "  ";
        }

        private void lblDeletePnl_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Label) (sender as Label).Text = "X";
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox)
            {
                if ((sender as TextBox).Parent is Panel)
                {
                    // ((sender as TextBox).Parent) as Panel
                    try
                    {
                        (((sender as TextBox).Parent as Panel).Tag as CCustomBlock).resetText((sender as TextBox).Text);
                    }
                    catch { return; }

                }
            }
        }

        private void pnlSelectedCourses_MouseEnter(object sender, EventArgs e)
        {
            // enable scroll for panel
            tblSelectedCourses.Focus();
        }

        //funtion for finding cell mouse click
        Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
            {
                return null;
            }

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
            {
                w -= widths[i];
            }
                
            int col = i + 1;
            int[] heights = tlp.GetRowHeights();

            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
            {
                h -= heights[i];
            }                

            int row = i + 1;

            return new Point(col, row);
        }

        private void openCourseFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open file

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "txt files (*.txt)|*.txt";
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            string course_file = openFile.FileName;

            List<Course> allCourses;

            CourseFileRepo fi_repo = new CourseFileRepo(course_file);

            allCourses = fi_repo.FindAll();

            if(allCourses[0].course == "INVALID")
            {
                MessageBox.Show("Not a tally file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            course_map = new Dictionary<string, List<CCourse>>();

            rejectedLines.Clear();
            tsLoadProgress.Value = 0;

            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(openFile.FileName);

                string date;
                string line;

                tsLoadProgress.Enabled = true;
                tsLoadProgress.Minimum = 0;
                tsLoadProgress.Maximum = 2000;

                CCourse course;
                List<CCourse> courses = new List<CCourse>();

                bool foundDate = false;

                foreach(Course c in allCourses)
                {
                    line = c.course.ToString() + " " +
                           c.section.ToString() + " " +
                           c.crn.ToString() + " " +
                           c.title.ToString() + " " +
                           c.credits.ToString() + " " +
                           c.c.ToString() + " " +
                           c.pt.ToString() + " " +
                           c.atr.ToString() + " " +
                           c.begin.ToString() + " " +
                           c.end.ToString() + " " +
                           c.days.ToString() + " " +
                           c.building.ToString() + " " +
                           c.room.ToString() + " " +
                           c.max.ToString() + " " +
                           c.actual.ToString() + " " +
                           c.remaining.ToString() + " " +
                           c.r.ToString() + " " +
                           c.wl.ToString() + " " +
                           c.faculty.ToString();

                    while ((date = reader.ReadLine()) != null)
                    {

                        if (date.StartsWith("DATE:") && !foundDate)
                        {
                            try
                            {
                                string sd = date.TrimStart(); // remove spaces at beginning

                                sd = sd.Remove(0, 5); // remove DATE:
                                sd = sd.Remove(30);   // remove all data after date
                                sd = sd.TrimEnd();    // remove spaces at end 

                                DateTime dt = DateTime.Parse(sd); // let date time object figure out the rest 

                                tsStatusReportDate.Text = dt.ToShortDateString();

                                break;
                            }
                            catch
                            {
                                tsStatusReportDate.Text = "No date found"; // not good 
                            }
                            finally
                            {
                                foundDate = true; // if it didn't work, don't bother
                            }

                        }// end display date
                        
                    }                 

                    try
                    {
                        course = new CCourse(c);
                        courses.Add(course);

                        tsLoadProgress.Value += 1;

                        this.Update();
                    }
                    catch (CCourseException ex)
                    {

                    }
                    catch (Exception ex) // failed to create course 
                    {

                        rejectedLines.Add(line);
                    }

                    //lines.Add(line);  // not sure we need this 
                }

                // consolidate CRNs
                for (int i = courses.Count - 1; i > 0; i--)
                {
                    if (courses[i].crn == courses[i - 1].crn) // matching crn
                    {
                        foreach (string original_file_data in courses[i].raw_data)
                        {
                            courses[i - 1].add_raw_data(original_file_data);
                        }

                        foreach (CCourseTime t in courses[i].times)
                        {
                            courses[i - 1].times.Add(t);
                        }

                        if (courses[i].times.Count <= 0 && courses[i].venue == CCourse.VENUE.HYBRID)
                        {
                            courses[i - 1].venue = CCourse.VENUE.HYBRID;
                        }

                        courses.RemoveAt(i);
                    }
                }

                // create keys in dictionary
                foreach (CCourse c in courses)
                {
                    if (course_map.Keys.Contains<string>(c.subject) == false)  // subject not yet added   
                        course_map.Add(c.subject, new List<CCourse>());  // add subject and create a list to store courses
                }

                // store courses for each subject 
                foreach (CCourse c in courses)
                {
                    course_map[c.subject].Add(c);
                }

                // load tree:
                displayCourseMap(course_map, null);

                tsLoadProgress.Value = tsLoadProgress.Maximum; // progress complete 
                tsLoadProgress.Enabled = false;

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Failed to open file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // http://rkinfopedia.blogspot.com/2008/07/printing-contents-of-panel-control.html

            // Print 

            PrintDialog printDlg = new PrintDialog();

            printDlg.AllowPrintToFile = false;

            printDlg.Document = printDocument1; // added as a control to the form

            if (printDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PrinterSettings values;
                values = printDlg.PrinterSettings;

                printDocument1.PrintController = new StandardPrintController();

                try
                {
                    printDocument1.Print();
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    MessageBox.Show("Print failed.  Check network connection.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                }
            }

            printDocument1.Dispose();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveOnClose();

            Dispose();
        }

        private void filterCoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (course_map == null)
            {
                MessageBox.Show("There are no courses to filter.", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frmFilter ff = new frmFilter(filters);

            DialogResult result = ff.ShowDialog();

            int? num_filter = null;

            if (result == System.Windows.Forms.DialogResult.Yes || result == System.Windows.Forms.DialogResult.OK)
            {


                displayCourseMap(course_map, filters);

                for (int i = 0; i < filters.count(); i++)
                {
                    if (filters[i].action != CFilter.FILTERS_ACTION.NONE)
                    {
                        if (filters[i].filter == CFilter.FILTERS.REMAINING_MIN)
                        {
                            try
                            {
                                num_filter = Int32.Parse(filters[i].filter_value);
                            }
                            catch
                            {
                                MessageBox.Show("Invalid filter value.");
                                return;
                            }
                        }
                    }

                } // end for

                if (num_filter != null)
                {

                }
            }
        }

        private void openExistingOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CCourse course;

            if (tblSelectedCourses.Controls.Count > 39)
            {
                MessageBox.Show("Schedule is already populated. Please clear schedule before opening an existing one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "txt files (*.txt)|*.txt";
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            string course_file = openFile.FileName;

            List<Course> allCourses;

            CourseFileRepo fi_repo = new CourseFileRepo(course_file);

            allCourses = fi_repo.Load();

            List<CCourse> tempList = new List<CCourse>();

            try
            {
                foreach (Course c in allCourses)
                {
                    string header;

                    System.IO.StreamReader reader = new System.IO.StreamReader(openFile.FileName);

                    while ((header = reader.ReadLine()) != null)
                    {
                        if (header.Contains(FILE_HEADER) == false)
                        {
                            reader.Close();
                            MessageBox.Show("Not a schedule file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else if(header.Contains(FILE_HEADER))
                        {
                            //done checking
                            break;
                        }
                    }

                    if (c.course == "CUSTOM")
                    {
                        //rebuild custom string
                        string line = c.course + ", " +
                                      c.span + ", " +
                                      c.height + ", " +
                                      c.row + ", " +
                                      c.column + ", " +
                                      c.text;

                        try
                        {
                            CCustomBlock blk = new CCustomBlock(line);
                            customPanels.Add(blk);
                            tblSelectedCourses.Controls.Add(createCustomBlock(blk));
                        }
                        catch { MessageBox.Show("Failed to add block for for line: " + line); }
                    }
                    else
                    {
                        try
                        {
                            //add course object
                            course = new CCourse(c);
                            tempList.Add(course);
                        }
                        catch (Exception ex) // failed to create course 
                        {
                            MessageBox.Show("Failed to add course.");
                        }
                    }
                    //}// end while reading lines

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + ex.Message);
            }

            // consolidate CRNs
            for (int i = tempList.Count - 1; i > 0; i--)
            {
                if (tempList[i].crn == tempList[i - 1].crn) // matching crn
                {
                    foreach (string original_file_data in tempList[i].raw_data) tempList[i - 1].add_raw_data(original_file_data);

                    foreach (CCourseTime t in tempList[i].times) tempList[i - 1].times.Add(t);

                    tempList.RemoveAt(i);
                }
            } // end consolidate CRNS

            foreach (CCourse c in tempList) scheduleCourse(c);
        }

        private void saveCurrentScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // save schedule 

            if (tblSelectedCourses.Controls.Count == 39)
            {
                MessageBox.Show("No courses to save");
                return;
            }
            bool exists = false;

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Title = "Save schedule";
            dlg.Filter = "Text Files | *.txt";
            dlg.AddExtension = true;

            DialogResult result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return; // user does not want to save 
            }

            string file_name = dlg.FileName;

            List<CCourse> courses = new List<CCourse>();

            TableLayoutControlCollection contList = tblSelectedCourses.Controls;

            customPanels.Clear();

            for (int i = tblSelectedCourses.Controls.Count - 1; i >= 0; i--)
            {
                if (tblSelectedCourses.Controls[i] is Panel)
                {
                    if (tblSelectedCourses.Controls[i].Tag is CCourse)
                    {
                        foreach (string raw_file_data in (tblSelectedCourses.Controls[i].Tag as CCourse).raw_data)
                        {
                            // separate courses 
                            try
                            {
                                CCourse c = new CCourse(raw_file_data);

                                bool courseExists = false;

                                //see if course exists in file
                                foreach (CCourse cCourse in courses)
                                {
                                    for(int t = 0; t < cCourse.times.Count(); t++)
                                    {
                                        if (c.crn == cCourse.crn && 
                                            c.times[0].begin == cCourse.times[t].begin && 
                                            c.times[0].end == cCourse.times[t].end     &&
                                            c.times[0].days == cCourse.times[t].days)
                                        {
                                            courseExists = true;
                                        }
                                    } 
                                }

                                //adds course to file if false
                                if (courseExists == false)
                                {
                                    courses.Add(c);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("ERROR with " + raw_file_data);
                            }
                        }// end for each chunk of raw data 

                    }
                    else if (tblSelectedCourses.Controls[i].Tag is CCustomBlock)
                    {
                        string set_span = tblSelectedCourses.GetRowSpan(tblSelectedCourses.Controls[i]).ToString();
                        string height = tblSelectedCourses.Controls[i].Height.ToString();
                        string row = tblSelectedCourses.GetRow(tblSelectedCourses.Controls[i]).ToString();
                        string col = tblSelectedCourses.GetColumn(tblSelectedCourses.Controls[i]).ToString();
                        string text = (tblSelectedCourses.Controls[i].Tag as CCustomBlock).text.TrimStart(' ');

                        string data = "CUSTOM, " +
                            set_span + ", " +
                            height + ", " +
                            row + ", " +
                            col + ", " +
                            text.TrimStart(' ');

                        // separate courses 
                        try
                        {
                            foreach (CCustomBlock cCustom in customPanels)
                            {
                                if (row == cCustom.row.ToString() && col == cCustom.col.ToString())
                                {
                                    exists = true;

                                    if (text == "")
                                    {
                                        text = cCustom.text;
                                    }
                                }
                            }

                            if (exists == false)
                            {
                                CCustomBlock c = new CCustomBlock(data);

                                customPanels.Add(c);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("ERROR with " + data);
                        }
                    }
                }

            }// end for each thing in panel 

            try
            {
                using (StreamWriter file = new StreamWriter(file_name))
                {
                    file.WriteLine(FILE_HEADER);

                    if (courses.Count != 0)
                    {
                        foreach (CCourse c in courses)
                        {
                            foreach (string r in c.raw_data) file.WriteLine(r);
                        }
                    }
                    foreach (CCustomBlock cb in customPanels)
                    {
                        file.WriteLine(cb.raw_data);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error saving file", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MessageBox.Show("Schedule saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tblSelectedCourses_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            Panel custom = createCustomBlock();

            //get row and column on click
            var cellPos = GetRowColIndex(tblSelectedCourses, tblSelectedCourses.PointToClient(Cursor.Position));

            Control selCell = tblSelectedCourses.GetControlFromPosition(cellPos.Value.X, cellPos.Value.Y);

            if(selCell != null) //Checks if cell is empty or not
            {
                return;
            }
            else if(cellPos.Value.X == 0 && cellPos.Value.Y == 0) //prevents custom block in first cell
            {
                return;
            }

            tblSelectedCourses.SetRowSpan(custom, 1);
            custom.Height = 53;

            tblSelectedCourses.Controls.Add(custom, cellPos.Value.X, cellPos.Value.Y);
        }

        private void clearScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSchedule();
        }      

        static string getSetting(string key)
        {
            // adapted from: 
            // https://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.appsettings(v=vs.110).aspx
            // see app.config file for how to add the setting

            string setting = "";

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                setting = appSettings[key] ?? "Not Found"; // if appSettings[key] != null, then appSettings[key], else "Not found"
            }
            catch (ConfigurationErrorsException)
            {
                setting = "ERROR: Not found";
            }

            return setting;
        }

        private int findDayCol(string day)
        {
            int col = 0;

            if(day == "M")
            {
                col = 1;                
            }
            else if(day == "T")
            {
                col = 2;
            }
            else if (day == "W")
            {
                col = 3;
            }
            else if (day == "R")
            {
                col = 4;
            }
            else if (day == "F")
            {
                col = 5;
            }

            return col;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(tblSelectedCourses.Controls.Count != 39)
            {
                DialogResult save = MessageBox.Show("Would you like to save before closing?", "Save Before Closing?", MessageBoxButtons.YesNo);
                if (save == DialogResult.Yes) saveOnClose();
                else if (save == DialogResult.No) Dispose();
            }

        }

        private void saveOnClose()
        {
            if (tblSelectedCourses.Controls.Count == 39)
            {
                return;
            }
            bool exists = false;

            SaveFileDialog dlg = new SaveFileDialog();

            dlg.Title = "Save schedule";
            dlg.Filter = "Text Files | *.txt";
            dlg.AddExtension = true;

            DialogResult result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return; // user does not want to save 
            }

            string file_name = dlg.FileName;

            List<CCourse> courses = new List<CCourse>();

            TableLayoutControlCollection contList = tblSelectedCourses.Controls;

            customPanels.Clear();

            for (int i = tblSelectedCourses.Controls.Count - 1; i >= 0; i--)
            {
                if (tblSelectedCourses.Controls[i] is Panel)
                {
                    if (tblSelectedCourses.Controls[i].Tag is CCourse)
                    {
                        foreach (string raw_file_data in (tblSelectedCourses.Controls[i].Tag as CCourse).raw_data)
                        {
                            // separate courses 
                            try
                            {
                                CCourse c = new CCourse(raw_file_data);

                                bool courseExists = false;

                                //see if course exists in file
                                foreach (CCourse cCourse in courses)
                                {
                                    for (int t = 0; t < cCourse.times.Count(); t++)
                                    {
                                        if (c.crn == cCourse.crn &&
                                            c.times[0].begin == cCourse.times[t].begin &&
                                            c.times[0].end == cCourse.times[t].end &&
                                            c.times[0].days == cCourse.times[t].days)
                                        {
                                            courseExists = true;
                                        }
                                    }
                                }

                                //adds course to file if false
                                if (courseExists == false)
                                {
                                    courses.Add(c);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("ERROR with " + raw_file_data);
                            }
                        }// end for each chunk of raw data 

                    }
                    else if (tblSelectedCourses.Controls[i].Tag is CCustomBlock)
                    {
                        string set_span = tblSelectedCourses.GetRowSpan(tblSelectedCourses.Controls[i]).ToString();
                        string height = tblSelectedCourses.Controls[i].Height.ToString();
                        string row = tblSelectedCourses.GetRow(tblSelectedCourses.Controls[i]).ToString();
                        string col = tblSelectedCourses.GetColumn(tblSelectedCourses.Controls[i]).ToString();
                        string text = (tblSelectedCourses.Controls[i].Tag as CCustomBlock).text.TrimStart(' ');

                        string data = "CUSTOM, " +
                            set_span + ", " +
                            height + ", " +
                            row + ", " +
                            col + ", " +
                            text.TrimStart(' ');

                        // separate courses 
                        try
                        {
                            foreach (CCustomBlock cCustom in customPanels)
                            {
                                if (row == cCustom.row.ToString() && col == cCustom.col.ToString())
                                {
                                    exists = true;

                                    if (text == "")
                                    {
                                        text = cCustom.text;
                                    }
                                }
                            }

                            if (exists == false)
                            {
                                CCustomBlock c = new CCustomBlock(data);

                                customPanels.Add(c);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("ERROR with " + data);
                        }
                    }
                }

            }// end for each thing in panel 

            try
            {
                using (StreamWriter file = new StreamWriter(file_name))
                {
                    file.WriteLine(FILE_HEADER);

                    if (courses.Count != 0)
                    {
                        foreach (CCourse c in courses)
                        {
                            foreach (string r in c.raw_data) file.WriteLine(r);
                        }
                    }
                    foreach (CCustomBlock cb in customPanels)
                    {
                        file.WriteLine(cb.raw_data);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error saving file", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MessageBox.Show("Schedule saved!", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
