using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementManagement.BAL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }                
        public double Salary { get; set; }        
        public DateTime DateOfBirth { get; set; }
    }
}
