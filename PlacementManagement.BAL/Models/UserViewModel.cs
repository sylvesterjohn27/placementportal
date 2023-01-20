using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementManagement.BAL.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }      
        [Required]
        [Display(Name = "Account Type")]
        public int AccountTypeId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Confirm Password should match with Password")]
        [Required]
        public string? ConfirmPassword { get; set; }
        public List<AccountTypeMasterViewModel> AccountTypes { get; set; }
    }
}
