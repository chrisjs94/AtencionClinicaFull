using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class VwLastMedicinesByPrivate
    {
        public int PrivateCustomerId { get; set; }
        public int WorkOrderId { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public double Quantity { get; set; }
        public string CreateBy { get; set; }
        public string Doctor { get; set; }
    }
}
