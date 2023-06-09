﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class InPutProductType
    {
        public InPutProductType()
        {
            InPutProducts = new HashSet<InPutProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InPutProduct> InPutProducts { get; set; }
    }
}
