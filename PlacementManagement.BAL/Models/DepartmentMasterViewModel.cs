using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PlacementManagement.BAL.Models
{
    public class DepartmentMasterViewModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }

        public List<SelectListItem> Departments { get; set; }

        [Display(Name = "Departments")]
        public long[] DepartmentIds { get; set; }
    }
}
