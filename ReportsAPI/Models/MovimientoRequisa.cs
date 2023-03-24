using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class MovimientoRequisa : MovimientoBase
    {
        public string AreaSource
        {
            get;
            set;
        }

        public string AreaTarget
        {
            get;
            set;
        }

        public double ProductRequest
        {
            get;
            set;
        }

        public MovimientoRequisa()
        {
        }
    }
}