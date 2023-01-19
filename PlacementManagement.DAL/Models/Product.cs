using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlacementManagement.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Qty { get; set; }
    }
}
