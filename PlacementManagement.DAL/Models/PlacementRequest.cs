using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Models
{
    public class PlacementRequest
    {
        public int Id { get; set; }
        
        [DataType(DataType.Date)]
        [Required]
        public DateTime PlacementDate { get; set; }

        [Required]
        public int CollegeId { get; set; }

        [Required]
        public string? Departments { get; set; }

        [Required]
        public string? CoreAreas { get; set; }

        [Required]
        [Range(1,10)]
        public double CGPA { get; set; }
                
        public bool? IsApprovedByCollege { get; set; }

        [NotMapped]
        public string CollegeName { get; set; }
    }
}
