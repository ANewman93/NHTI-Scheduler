using System;

namespace SchedulerModel
{
    public class Course
    {
        private string _course;
        //ctor
        public Course(string course)
        {
            _course = course;
        }

        public string course
        {
            get
            {
                return _course;
            }
            private set
            {
                _course = value;
            }
        }

        public string section { get; set; }

        public string crn { get; set; }

        public string title { get; set; }

        public string credits { get; set; }

        public string c { get; set; }

        public string pt { get; set; }

        public string atr { get; set; }

        public string begin { get; set; }

        public string end { get; set; }

        public string days { get; set; }

        public string building { get; set; }

        public string room { get; set; }

        public string max { get; set; }

        public string actual { get; set; }

        public string remaining { get; set; }

        public string r { get; set; }

        public string wl { get; set; }

        public string faculty { get; set; }

        public int span { get; set; }

        public int height { get; set; }

        public int row { get; set; }

        public int column { get; set; }
        
        public string text { get; set; }
    }
}
