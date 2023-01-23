using System.ComponentModel.DataAnnotations;

namespace PlacementManagement.BAL.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }      
        [Required]
        [Display(Name = "Account Type")]

        public int AccountTypeId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Confirm Password should match with Password")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public List<AccountTypeMasterViewModel> AccountTypes { get; set; }
    }
}
