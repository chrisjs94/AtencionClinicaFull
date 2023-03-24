using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class VwProductInfo
    {
        public int AreaId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Um { get; set; }
        public string Presentation { get; set; }
        public int StateId { get; set; }
        public int CurrencyId { get; set; }
        public double Stock { get; set; }
        public bool Exists { get; set; }
        public decimal Price { get; set; }
    }
}
