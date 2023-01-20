using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Models
{
    public class CollegeDepartmentViewModel
    {
        public int CollegeId { get; set; }
        public List<DepartmentModel> Departments { get; set; }
    }

    public class DepartmentModel
    {
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
    }

}
