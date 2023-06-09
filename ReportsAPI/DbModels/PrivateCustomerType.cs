﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class PrivateCustomerType
    {
        public PrivateCustomerType()
        {
            PrivateCustomers = new HashSet<PrivateCustomer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PrivateCustomer> PrivateCustomers { get; set; }
    }
}
