﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class Resource
    {
        public Resource()
        {
            RolResources = new HashSet<RolResource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolResource> RolResources { get; set; }
    }
}
