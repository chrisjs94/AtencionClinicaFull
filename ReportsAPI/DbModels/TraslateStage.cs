using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class TraslateStage
    {
        public TraslateStage()
        {
            Traslates = new HashSet<Traslate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Traslate> Traslates { get; set; }
    }
}
