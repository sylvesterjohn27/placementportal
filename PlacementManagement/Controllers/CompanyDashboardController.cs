using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;

namespace PlacementManagement.Controllers
{
    public class CompanyDashboardController : Controller
    {        
        private readonly IMasterServices _masterService;
        private readonly IUserServices _userServices;
        private readonly UserManager<IdentityUser> _userManager;

        public CompanyDashboardController(IUserServices userServices, UserManager<IdentityUser> userManager, IMasterServices masterService)
        {           
            _userServices = userServices;
            _userManager = userManager;
            _masterService = masterService;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private async Task<UserViewModel> GetCompanyOrCollegeName()
        {            
            var currentUser = await GetCurrentUserAsync();
            if (currentUser != null)
            {
                return _userServices.GetUserByUserName(currentUser.UserName);
            }
            return new UserViewModel();
        }

        public async Task<IActionResult> Index()
        {
            var collegeDetails = await GetCompanyOrCollegeName();
            return View(collegeDetails);
        }
    }
}
