using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Models
{
    public class CoreAreaMasterViewModel
    {
        public int Id { get; set; }
        public string? CoreArea { get; set; }
        public int DepartmentId { get; set; }
    }
}
