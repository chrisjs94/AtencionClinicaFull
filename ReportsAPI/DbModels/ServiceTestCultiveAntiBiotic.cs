﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class ServiceTestCultiveAntiBiotic
    {
        public int Id { get; set; }
        public int ServiceTestCultiveId { get; set; }
        public string TestId { get; set; }
        public int ResultId { get; set; }

        public virtual ServiceTestCultive ServiceTestCultive { get; set; }
    }
}
