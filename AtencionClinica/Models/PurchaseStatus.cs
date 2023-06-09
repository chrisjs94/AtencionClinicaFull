﻿using System;
using System.Collections.Generic;

#nullable disable

namespace AtencionClinica.Models
{
    public partial class PurchaseStatus
    {
        public PurchaseStatus()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
