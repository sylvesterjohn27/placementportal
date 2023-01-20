using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PlacementManagement.BAL.Models
{
    public class DepartmentMasterViewModel
    {
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; }

        public List<SelectListItem> Departments { get; set; }

        [Display(Name = "Departments")]
        public long[] DepartmentIds { get; set; }
    }
}
