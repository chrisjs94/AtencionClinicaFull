using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class OutPutProductState
    {
        public OutPutProductState()
        {
            OutPutProducts = new HashSet<OutPutProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OutPutProduct> OutPutProducts { get; set; }
    }
}
