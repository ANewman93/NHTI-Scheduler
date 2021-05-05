using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHTI_Scheduler
{
    public class CFilter
    {
        public enum FILTERS
        {
            REMAINING_MIN,
            DAYS_INCLUDED,
            SUBJECTS_INCLUDED,
            NUM_FILTERS
        }

        public enum FILTERS_ACTION
        {
            NONE,
            HIDE,
            HIGHLIGHT
        }

        public struct FILTER
        {
            public FILTERS_ACTION action; 
            public FILTERS filter;
            public string filter_value; 
        }

        public FILTER[] _theFilters; 

        public CFilter()
        {
            _theFilters = new FILTER[(int)FILTERS.NUM_FILTERS];

            for (int i = 0; i < _theFilters.Count(); i++ )
            {
                _theFilters[i].action = FILTERS_ACTION.NONE;
            }

        }

        public FILTER this[int i] // operator []
        {
            get
            {
                return _theFilters[i];
            }
            set
            {
                _theFilters[i] = value; 

                //_theFilters[i].action = value.action;
                //_theFilters[i].filter = value.filter;
            }
        }

        public int count()
        {
            return (int)FILTERS.NUM_FILTERS; 
        }

    }
}
