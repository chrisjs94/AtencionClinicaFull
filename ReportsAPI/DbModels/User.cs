﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class User
    {
        public User()
        {
            Admissions = new HashSet<Admission>();
            _Appointments = new HashSet<Appointments>();
        }

        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public int AreaId { get; set; }
        public bool Active { get; set; }

        public virtual Area Area { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Admission> Admissions { get; set; }
        public virtual ICollection<Appointments> _Appointments { get; set; }
    }
}
