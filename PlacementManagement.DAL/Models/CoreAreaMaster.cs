﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Models
{
    public class CoreAreaMaster
    {
        public int Id { get; set; }
        public string? CoreArea { get; set; }
        public int DepartmentId { get; set; }
    }
}