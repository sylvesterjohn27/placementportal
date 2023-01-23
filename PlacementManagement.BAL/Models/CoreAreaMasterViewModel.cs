
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PlacementManagement.BAL.Models
{
    public class CoreAreaMasterViewModel
    {
        public int Id { get; set; }
        public string CoreArea { get; set; }
        public int DepartmentId { get; set; }

        public List<SelectListItem> CoreAreaDetails { get; set; }
        public long[] CoreAreaIds { get; set; }
    }
}
