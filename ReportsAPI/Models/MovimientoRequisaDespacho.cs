using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class MovimientoRequisaDespacho : MovimientoRequisa
    {
        public DateTime ModifyAt
        {
            get;
            set;
        }

        public string ModifyBy
        {
            get;
            set;
        }

        public double ProductResponse
        {
            get;
            set;
        }

        public MovimientoRequisaDespacho()
        {
        }
    }
}