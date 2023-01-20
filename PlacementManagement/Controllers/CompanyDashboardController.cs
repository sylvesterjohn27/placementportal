using Microsoft.AspNetCore.Mvc;

namespace PlacementManagement.Controllers
{
    public class CompanyDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
