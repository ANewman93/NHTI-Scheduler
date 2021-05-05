using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using fileRepo;
using SchedulerModel;

namespace NHTI_Scheduler
{
    class CCustomBlock
    {
        public int _spanset;
        private int _height;
        private string _text;
        private int _row;
        private int _col; 

        public CCustomBlock(int spanset, int height, int row, int col, string text)
        {
            _spanset = spanset;
            _text = text;
            _height = height;
            _row = row;
            _col = col;
        }

        public CCustomBlock(string raw)
        {
            if (raw.Contains("CUSTOM") == false) throw new Exception("Not custom data string");

            string[] parts = raw.Split(',');

            try
            {
                _col = Convert.ToInt32(parts[1]);
                _height = Convert.ToInt32(parts[2]);
                _row = Convert.ToInt32(parts[3]);
                _spanset = Convert.ToInt32(parts[4]);
                _text = parts[5].TrimStart();

                _text = _text.Replace("|", System.Environment.NewLine); 
            }
            catch
            {
                throw new Exception("Not custom data string"); 
            }
        }

        public void resetPoint(int col, int row) { _col = col; _row = row; }
        public void resetHeight(int height) { _height = height;  }
        public void resetText(string text) { _text = text;  }
        public string raw_data
        {
            set
            {
                string txt = text.Replace(System.Environment.NewLine, "|");
                string t = string.Format("CUSTOM, {0}, {1}, {2}, {3}, {4}", _spanset, _height, _row, _col, txt);

            }
            get
            {
                string txt = text.Replace(System.Environment.NewLine, "|");
                string t = string.Format("CUSTOM, {0}, {1}, {2}, {3}, {4}", _spanset, _height, _row, _col, txt);

                return t; 
            }
        }

        public int col { get { return _col;  } }
        public int row { get { return _row;  } }
        public int spanset { get { return _spanset;  } }
        public int height { get { return _height; } }
        public string text { get { return _text;  } }
    } // end CCustomBlock class 

    class CCourseTime
    {
        private DateTime _begin;
        private DateTime _end;
        private string _days;
        private string _location; 

        public CCourseTime(DateTime begin, DateTime end, string days)
        {
            _begin = begin;
            _end = end;
            _days = days;
            _location = "Not yet"; 
        }

        public CCourseTime(DateTime begin, DateTime end, string days, string location)
        {
            _begin = begin;
            _end = end;
            _days = days;
            _location = location;
        }

        public DateTime begin { get { return _begin; } }
        public DateTime end { get { return _end; } }
        public string days { get { return _days; } }

        public string location { get { return _location; } } 

        public bool isSameTime(CCourseTime other)
        {
            if (this.begin != other.begin) return false;
            if (this.end != other.end) return false;

            return true; 
        }

    }// end CCourseTime

    public class CCourseException : Exception
    {
        private bool _bad_course;
        private string _raw; 
        public CCourseException(string msg) : base(msg)
        {
            _bad_course = false; 
        }

        public CCourseException(string msg, string raw) : base(msg)
        {
            _bad_course = true;
            _raw = raw; 
        }

        public bool BadCourse { get { return _bad_course;  } }
        public string Raw {  get { return _raw;  } }
    }

    class CCourse
    {
        public struct VENUE { 
            public const char ONLINE = 'O';
            public const char ONLINE_REMOTE = 'R';
            public const char DAY = 'D';
            public const char EVENING = 'E';
            public const char HYBRID = 'H';
            public const char OFF_CAMPUS = 'C';
            public const char OTHER = '?';
            public const string OFFCMP = "OFFCMP";
        }

        protected const string NO_DAYS = "?";

        public const string S_VENUE_ONLINE = "Online";
        public const string S_VENUE_EVENING = "Evening";
        public const string S_VENUE_DAY = "Day";
        public const string S_VENUE_HYBRID = "Hybrid";

        protected string _subject;
        protected string _course;
        protected string _section;
        protected int _crn;
        protected string _title;
        protected string _short_title;
        protected string _credits;
        protected string _c; // unused 
        protected string _pt; // unused
        protected char _venue; // atr 
        protected string _days;

        private string _extra; 

        // added 
        protected string _building;
        protected string _room;
        protected short _max;
        protected short _actual;
        protected short _remaining;
        protected string _faculty;

        protected List<string> _rawData; // can be saved to recreate the course

        public List<CCourseTime> times;

        public string Extra
        {
            get {
                return _extra; 
            }
            set { _extra = value;  }

        }

        public CCourse(Course course)
        {

            times = new List<CCourseTime>();
            _rawData = new List<string>();

            //text is raw data in Non-custom courses
            string data = course.text;

            _rawData.Add(data);

            _subject = course.course.Substring(0, 4);

            if (_subject == "ECE")
            {
                Console.Write("oops.");
            }

            _course = course.course + "-" + course.section; // ACCT + " " + 101C-1, for example 
            _section = course.section; 
            _crn = Convert.ToInt32(course.crn);

            _title = course.title;

            _credits = course.credits;


            _c = course.c;
            _pt = course.pt;

            _days = NO_DAYS;

            _venue = get_venue(data);

            if (_venue == VENUE.DAY || _venue == VENUE.EVENING)
            {
                DateTime begin = DateTime.Today;  // make the compiler happy
                DateTime end = DateTime.Today;

                try
                {
                    begin = DateTime.Parse(course.begin); // may fail here if online with a day 
                }
                catch (Exception ex)
                {
                    _days = course.days; // find days for online course    
                    _venue = VENUE.ONLINE; // best guess

                    _building = course.building; // ONLINE 
                    _max = Convert.ToInt16(course.max);// short
                    _actual = Convert.ToInt16(course.actual);// short
                    _remaining = Convert.ToInt16(course.remaining);// short

                    _faculty = course.faculty; // faculty is always last in string 

                    return;
                }

                end = DateTime.Parse(course.end);
                _days = course.days;

                _building = course.building;

                if (_building == "" || _building == " ") // no building, straight to max, ac, rem
                {
                    _building = "BLDING NOT LISTED";
                }
                else if (_building != VENUE.OFFCMP) _room = course.room; // clinics have no room number 

                times.Add(new CCourseTime(begin, end, _days, _building + " " + _room)); // XYZ

                _max = Convert.ToInt16(course.max);// short
                _actual = Convert.ToInt16(course.actual);// short
                _remaining = Convert.ToInt16(course.remaining);// short
            }
            else if (_venue == VENUE.ONLINE || _venue == VENUE.HYBRID)
            {
                _building = course.building;

                int n;

                if (Int32.TryParse(course.building, out n) == false) // not a number 
                {
                    if (_venue == VENUE.ONLINE)
                    {
                        _days = course.days;
                        _building = course.building; // "online" 

                        // probably online course with days
                    }
                }

                _max = Convert.ToInt16(course.max);// short
                _actual = Convert.ToInt16(course.actual);// short
                _remaining = Convert.ToInt16(course.remaining);// short
            }
            else if (_venue == VENUE.ONLINE_REMOTE)
            {
                DateTime begin = DateTime.Today;  // make the compiler happy
                DateTime end = DateTime.Today;

                try
                {
                    begin = DateTime.Parse(course.begin); // may fail here if online with a day 
                }
                catch (Exception ex)
                {
                    _days = course.days; // find days for online course    
                    _venue = VENUE.ONLINE; // best guess

                    _building = course.building; // ONLINE 
                    _max = Convert.ToInt16(course.max);// short
                    _actual = Convert.ToInt16(course.actual);// short
                    _remaining = Convert.ToInt16(course.remaining);// short

                    _faculty = course.faculty; // faculty is always last in string 

                    return;
                }

                end = DateTime.Parse(course.end);
                _days = course.days;

                times.Add(new CCourseTime(begin, end, _days, "ONLINE REMOTE"));

                _building = "REMOTE"; //  arrayOfData[_next_pos++];

                if (course.building == "" || course.building == " ") // no building, straight to max, ac, rem
                {
                    _building = "BLDING NOT LISTED";
                }
                else if (_building != VENUE.OFFCMP) _room = course.room; // clinics have no room number 

                _max = Convert.ToInt16(course.max);// short
                _actual = Convert.ToInt16(course.actual);// short
                _remaining = Convert.ToInt16(course.remaining);// short
            }
            else if (_venue == VENUE.OFF_CAMPUS)
            {
                DateTime begin = DateTime.Today;  // make the compiler happy
                DateTime end = DateTime.Today;

                try
                {
                    begin = DateTime.Parse(course.begin); // CHECK FOR date / time 
                }
                catch (Exception ex)
                {
                    _building = course.building; // OFFCMP
                    _max = Convert.ToInt16(course.max);// short
                    _actual = Convert.ToInt16(course.actual);// short
                    _remaining = Convert.ToInt16(course.remaining);// short

                    _faculty = course.faculty; // faculty is always last in string 

                    return;
                }

                end = DateTime.Parse(course.end);
                _days = course.days;

                times.Add(new CCourseTime(begin, end, _days));

                _building = course.building;

                if (course.building == "" || course.building == " ") // no building, straight to max, ac, rem
                {
                    _building = "BLDING NOT LISTED";
                }
                else if (_building != VENUE.OFFCMP) _room = course.room; // clinics have no room number 

            _max = Convert.ToInt16(course.max);// short
            _actual = Convert.ToInt16(course.actual);// short
            _remaining = Convert.ToInt16(course.remaining);// short
        }
            else if(_venue == VENUE.OTHER)
            {
                string temp = building; 
                if (short.TryParse(temp, out _max) == true)
                {
                    // missing building or location of any sort
                    _actual = Convert.ToInt16(course.actual);// short
                    _remaining = Convert.ToInt16(course.remaining);// short
                }
                else
                {
                    _building = course.building; // ONLINE 
                    _max = Convert.ToInt16(course.max);// short
                    _actual = Convert.ToInt16(course.actual);// short
                    _remaining = Convert.ToInt16(course.remaining);// short
                }

            }
            else
            {
                throw new CCourseException("Venue not supported " + _venue.ToString(), course.building); 
            }
 
            _faculty = course.faculty; // faculty is always last in string 

            // add building, etc. 
        }

        public CCourse(string data)
        {

            times = new List<CCourseTime>();
            _rawData = new List<string>();

            _rawData.Add(data);

            string[] stringSeparators = new string[] { " " };

            string[] arrayOfData = data.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            if (arrayOfData.Length < 15)
                throw new CCourseException("Object failed.");

            if (arrayOfData[0] == "CRSE")
                throw new CCourseException("Object failed.");
            if (arrayOfData[0] == "TE")
                throw new CCourseException("Object failed.");

            _subject = arrayOfData[0];

            if (_subject == "ECE")
            {
                Console.Write("oops.");
            }

            _course = arrayOfData[0] + " " + arrayOfData[1] + "-" + arrayOfData[2]; // ACCT + " " + 101C-1, for example 
            _section = arrayOfData[2];
            _crn = Convert.ToInt32(arrayOfData[3]);


            // find string that contains .

            int credit_pos = 4;

            while (arrayOfData[credit_pos].Contains(".0") == false)
            {
                credit_pos++;
            }

            _short_title = arrayOfData[4];
            _title = "";
            for (int i = 4; i < credit_pos; i++)
                _title += arrayOfData[i] + " ";

            _credits = arrayOfData[credit_pos];

            int _next_pos = credit_pos + 1;

            //unused
            _c = arrayOfData[_next_pos++];
            _pt = arrayOfData[_next_pos++];

            _days = NO_DAYS;

            _venue = get_venue(data);

            if (_venue == VENUE.DAY || _venue == VENUE.EVENING)
            {
                DateTime begin = DateTime.Today;  // make the compiler happy
                DateTime end = DateTime.Today;

                _next_pos++;

                try
                {
                    begin = DateTime.Parse(arrayOfData[_next_pos]); // may fail here if online with a day 
                    _next_pos++;
                }
                catch (Exception ex)
                {
                    _days = arrayOfData[_next_pos++]; // find days for online course    
                    _venue = VENUE.ONLINE; // best guess

                    _building = arrayOfData[_next_pos++]; // ONLINE 
                    _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short

                    _faculty = arrayOfData[arrayOfData.Length - 1]; // faculty is always last in string 

                    return;
                }

                end = DateTime.Parse(arrayOfData[_next_pos++]);
                _days = arrayOfData[_next_pos++];

                _building = arrayOfData[_next_pos++];

                if (_building[0] >= '0' && _building[0] <= '9') // no building, straight to max, ac, rem
                {
                    _next_pos--;
                    _building = "BLDING NOT LISTED";
                }
                else if (_building != VENUE.OFFCMP) _room = arrayOfData[_next_pos++]; // clinics have no room number 

                times.Add(new CCourseTime(begin, end, _days, _building + " " + _room)); // XYZ

                _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short
            }
            else if (_venue == VENUE.ONLINE || _venue == VENUE.HYBRID)
            {
                _next_pos++;
                _building = arrayOfData[_next_pos];

                _next_pos++;
                int n;

                if (Int32.TryParse(arrayOfData[_next_pos], out n) == false) // not a number 
                {
                    if (_venue == VENUE.ONLINE)
                    {
                        _days = arrayOfData[_next_pos - 1];
                        _building = arrayOfData[_next_pos]; // "online" 

                        // probably online course with days

                        _next_pos++;
                    }
                }

                _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short
            }
            else if (_venue == VENUE.ONLINE_REMOTE)
            {
                DateTime begin = DateTime.Today;  // make the compiler happy
                DateTime end = DateTime.Today;

                try
                {
                    _next_pos++;
                    begin = DateTime.Parse(arrayOfData[_next_pos]); // may fail here if online with a day 
                    _next_pos++;
                }
                catch (Exception ex)
                {
                    _days = arrayOfData[_next_pos++]; // find days for online course    
                    _venue = VENUE.ONLINE; // best guess

                    _building = arrayOfData[_next_pos++]; // ONLINE 
                    _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short

                    _faculty = arrayOfData[arrayOfData.Length - 1]; // faculty is always last in string 

                    return;
                }

                end = DateTime.Parse(arrayOfData[_next_pos++]);
                _days = arrayOfData[_next_pos++];

                times.Add(new CCourseTime(begin, end, _days, "ONLINE REMOTE"));

                _building = "REMOTE"; //  arrayOfData[_next_pos++];

                if (_building[0] >= '0' && _building[0] <= '9') // no building, straight to max, ac, rem
                {
                    _next_pos--;
                    _building = "BLDING NOT LISTED";
                }
                else if (_building != VENUE.OFFCMP) _room = arrayOfData[_next_pos++]; // clinics have no room number 

                _next_pos++; // skip REMO and get to numbers

                _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short
            }
            else if (_venue == VENUE.OFF_CAMPUS)
            {
                DateTime begin = DateTime.Today;  // make the compiler happy
                DateTime end = DateTime.Today;

                // _location = "Off Campus"; 
                _next_pos++;

                try
                {
                    begin = DateTime.Parse(arrayOfData[_next_pos]); // CHECK FOR date / time 
                    _next_pos++;
                }
                catch (Exception ex)
                {
                    // _days = arrayOfData[_next_pos++]; // find days for online course    
                    //  _venue = ONLINE; // best guess

                    _building = arrayOfData[_next_pos++]; // OFFCMP
                    _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short

                    _faculty = arrayOfData[arrayOfData.Length - 1]; // faculty is always last in string 

                    return;
                }

                end = DateTime.Parse(arrayOfData[_next_pos++]);
                _days = arrayOfData[_next_pos++];

                times.Add(new CCourseTime(begin, end, _days));

                _building = arrayOfData[_next_pos++];

                if (_building[0] >= '0' && _building[0] <= '9') // no building, straight to max, ac, rem
                {
                    _next_pos--;
                    _building = "BLDING NOT LISTED";
                }
                else if (_building != VENUE.OFFCMP) _room = arrayOfData[_next_pos++]; // clinics have no room number 

                _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short
            }
            else if (_venue == VENUE.OTHER)
            {
                _next_pos++;
                string temp = arrayOfData[_next_pos];
                if (short.TryParse(temp, out _max) == true)
                {
                    // missing building or location of any sort
                    _next_pos++;
                    _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                }
                else
                {
                    _building = arrayOfData[_next_pos++]; // ONLINE 
                    _max = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _actual = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                    _remaining = Convert.ToInt16(arrayOfData[_next_pos++]);// short
                }

            }
            else
            {
                throw new CCourseException("Venue not supporte " + _venue.ToString(), data);
            }

            _faculty = arrayOfData[arrayOfData.Length - 1]; // faculty is always last in string 
        }


        public List<string> raw_data { get { return _rawData; } }
        public void add_raw_data(string raw) { _rawData.Add(raw); }


        private char get_venue(string raw)
        {      
            if(raw.Contains("ONLINE"))
            {
                if (raw.Contains("REMO")) return VENUE.ONLINE_REMOTE;

                return VENUE.ONLINE; 
            }
            else if( raw.Contains("HYBRID"))
            {
                return VENUE.HYBRID; 
            }
            else if( raw.Contains("OFFCMP"))
            {
                return VENUE.OFF_CAMPUS; 
            }
            else if ( raw.Contains("5D"))
            {
                return VENUE.DAY; 
            }
            else if( raw.Contains("5E"))
            {
                return VENUE.EVENING; 
            }

            return VENUE.OTHER; 
        }

        public virtual string description
        {
            get
            {
                string sDescr = ""; 

                if (_venue == VENUE.ONLINE)
                {
                    if (_days != NO_DAYS)
                        sDescr = string.Format("{0, -15}({1})  ONLINE  {2}", course, crn, _days); 
                    else
                        sDescr = string.Format("{0, -15}({1})  ONLINE", course, crn); 
                }
                else // not online 
                {
                    if (times.Count <= 0)
                    {
                        sDescr = course + " " + crn; 
                    }
                    else
                    {                    
                        string days = times[0].days;

                        for (int i = 1; i < times.Count; i++ )
                        {
                            if (times[0].isSameTime(times[i]))
                            {
                                days += times[i].days;
                            }
                            else // different time 
                            {
                                days += "["; 
                                days += times[i].days;
                                days += "]"; 
                            }
                        }

                        //                                                                                                                                     was:      times[0].days
                        sDescr = string.Format("{0, -15}({1}) {2, -8} - {3, -9}  {4}", course, crn, times[0].begin.ToShortTimeString(), times[0].end.ToShortTimeString(), days);
                    }

                }

                return sDescr; 
            }

        }
        public string subject { get { return _subject;  } }
        public string course { get { return _course;  } }
        public string section { get { return _section;  } }
        public int crn { get { return _crn;  } }
        public string title { get { return _title;  } }
        public string credits { get { return _credits;  } }
        public char venue
        {
            get { return _venue;  }
            set { _venue = value;  }
        }

        public string building { get { return _building; }}
        public string room { get { return _room;  } }
        public short max { get { return _max;  } }
        public short actual { get { return _actual;  } }
        public short remaining { get { return _remaining;  } }
        public string faculty { get { return _faculty;  } }       
    }
}
