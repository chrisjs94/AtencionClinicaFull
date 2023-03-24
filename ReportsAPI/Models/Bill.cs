using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ReportsAPI.Models
{
    public class Bill
    {
        public string Contract
        {
            get;
            set;
        }

        public DateTime CreateAt
        {
            get;
            set;
        }

        public string CreateBy
        {
            get;
            set;
        }

        public string Direccion
        {
            get;
            set;
        }

        public string GrantTotal
        {
            get;
            set;
        }

        [Key]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public decimal Quantity
        {
            get;
            set;
        }

        public string ServiceName
        {
            get;
            set;
        }

        public string TipoIngreso
        {
            get;
            set;
        }

        public decimal Total
        {
            get;
            set;
        }

        public string TypeCustomer
        {
            get;
            set;
        }

        public Bill()
        {
        }
    }
}