using Microsoft.AspNetCore.Mvc;

namespace PlacementManagement.Controllers
{
    public class CollegeDashboardController : Controller
    {

        public CollegeDashboardController() 
        {

        }

        public IActionResult Index()
        {
            return View();
        }       
    }
}
