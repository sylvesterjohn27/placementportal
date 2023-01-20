using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementManagement.BAL.Models
{
    public class PlacementRequestViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Placement Date")]
        public DateTime PlacementDate { get; set; }

        [Required]        
        public int CollegeId { get; set; }        

        [Required]
        [DisplayName("Department")]
        public string Departments { get; set; }

        [Required]
        [DisplayName("Core Areas")]
        public string CoreAreas { get; set; }

        [Required]
        [Range(1, 10)]
        public double CGPA { get; set; }
                
        public List<SelectListItem> DepartmentDetails { get; set; }        
        public long[] DepartmentIds { get; set; }
        public List<SelectListItem> CoreAreaDetails { get; set; }
        public long[] CoreAreaIds { get; set; }

        [NotMapped]
        [DisplayName("College")]
        public string CollegeName { get; set; }

    }
}
