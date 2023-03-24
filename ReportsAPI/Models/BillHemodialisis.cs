using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class BillHemodialisis : Bill
    {
        public DateTime? AddAt
        {
            get;
            set;
        }

        public int ClientId
        {
            get;
            set;
        }

        public int? DetalleId
        {
            get;
            set;
        }

        public int? Inss
        {
            get;
            set;
        }

        public DateTime RegisterAt
        {
            get;
            set;
        }

        public long RowNumber
        {
            get;
            set;
        }

        public BillHemodialisis()
        {
        }
    }
}