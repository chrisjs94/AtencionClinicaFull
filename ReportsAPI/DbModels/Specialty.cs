using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class Specialty
    {
        public Specialty()
        {
            Admissions = new HashSet<Admission>();
            _Appointments = new HashSet<Appointments>();
            Doctors = new HashSet<Doctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Admission> Admissions { get; set; }
        public virtual ICollection<Appointments> _Appointments { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
