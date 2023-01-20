using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required]        
        public string StudentName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public double CGPA { get; set; }
        public string CoreAreas { get; set; }
    }
}
