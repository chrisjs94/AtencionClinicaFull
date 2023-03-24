using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class DatesParameters
    {
        public DateTime End
        {
            get;
            set;
        }

        public DateTime Start
        {
            get;
            set;
        }

        public DatesParameters()
        {
        }
    }
}