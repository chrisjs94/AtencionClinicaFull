using System;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class MovimientoBase
    {
        public string Area
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public string Estado
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public string Observation
        {
            get;
            set;
        }

        public decimal ProductCost
        {
            get;
            set;
        }

        public int ProductId
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public decimal ProductPrice
        {
            get;
            set;
        }

        public double ProductQuantity
        {
            get;
            set;
        }

        public decimal ProductTotal
        {
            get;
            set;
        }

        public string Tipo
        {
            get;
            set;
        }

        public decimal Total
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public MovimientoBase()
        {
        }
    }
}