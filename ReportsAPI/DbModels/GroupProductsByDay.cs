﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ReportsAPI.DbModels
{
    public partial class GroupProductsByDay
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int DayOfWeek { get; set; }

        public virtual Group Group { get; set; }
    }
}
