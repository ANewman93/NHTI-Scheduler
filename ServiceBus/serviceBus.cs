using System;
using ApiRepo;
using fileRepo;
using SchedulerModel;
using System.Collections.Generic;

namespace ServiceBus
{
    public class serviceBus
    {
        private ApiRepo.IRepository<Course> _apiCourseRepo;
        private fileRepo.IRepository<Course> _fileCourseRepo;

        public serviceBus(ApiRepo.IRepository<Course> apiRepoy, fileRepo.IRepository<Course> fileRepoy)
        {
            _apiCourseRepo = apiRepoy;
            _fileCourseRepo = fileRepoy;
        }

        public List<courseViewModel> getAllCourses()
        {
            List<courseViewModel> course = new List<courseViewModel>();

            List<Course> coList = _fileCourseRepo.FindAll();

            foreach (Course c in coList)
            {
                course.Add( new courseViewModel(c.course, c.section, c.crn, c.title, c.credits, c.c, c.pt, c.atr, c.begin, c.end, c.days, c.building,
                           c.room, c.max, c.actual, c.remaining, c.r, c.wl, c.faculty, c.span, c.height, c.row, c.column, c.text));
            }

            return course;
        }

        public List<courseViewModel> loadSavedFile()
        {
            List<courseViewModel> course = new List<courseViewModel>();

            List<Course> coList = _fileCourseRepo.FindAll();

            foreach (Course c in coList)
            {
                course.Add(new courseViewModel(c.course, c.section, c.crn, c.title, c.credits, c.c, c.pt, c.atr, c.begin, c.end, c.days, c.building,
                           c.room, c.max, c.actual, c.remaining, c.r, c.wl, c.faculty, c.span, c.height, c.row, c.column, c.text));
            }

            return course;
        }

    }

    public class courseViewModel
    {
        public string Course { get; set; }
        public string Section { get; set; }
        public string Crn { get; set; }
        public string Title { get; set; }
        public string Credits { get; set; }
        public string C { get; set; }
        public string Pt { get; set; }
        public string Atr { get; set; }
        public string Begin { get; set; }
        public string End { get; set; }
        public string Days { get; set; }
        public string Building { get; set; }
        public string Room { get; set; }
        public string Max { get; set; }
        public string Actual { get; set; }
        public string Remaining { get; set; }
        public string R { get; set; }
        public string Wl { get; set; }
        public string Faculty { get; set; }
        public int Span { get; set; }
        public int Height { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Text { get; set; }

        public courseViewModel(string course, string section, string crn, string title, string credits, string c, string pt, string atr, string begin, string end, string days, string building,
            string room, string max, string actual, string remaining, string r, string wl, string faculty, int span, int height, int row, int column, string text)
        {
            Course = course;
            Section = section;
            Crn = crn;
            Title = title;
            Credits = credits;
            C = c;
            Pt = pt;
            Atr = atr;
            Begin = begin;
            End = end;
            Days = days;
            Building = building;
            Room = room;
            Max = max;
            Actual = actual;
            Remaining = remaining;
            R = r;
            Wl = wl;
            Faculty = faculty;
            Span = span;
            Height = height;
            Row = row;
            Column = column;
            Text = text;
        }
    }
}
