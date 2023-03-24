using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class Movimiento : MovimientoBase
    {
        public string ProviderOrClient
        {
            get;
            set;
        }

        public string Reference
        {
            get;
            set;
        }

        public Movimiento()
        {
        }
    }
}