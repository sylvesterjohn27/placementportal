using Microsoft.AspNetCore.Mvc;
using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;

namespace PlacementManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IMasterServices _masterService;
        public UserController(IUserServices userService, IMasterServices masterService)
        {
            _userService = userService;
            _masterService = masterService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            UserViewModel model = new UserViewModel();
            var accountTypes = _masterService.GetAccountTypes();
            model.AccountTypes = accountTypes.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            var accountTypes = _masterService.GetAccountTypes();
            UserViewModel userView = new UserViewModel();
            userView.AccountTypes = accountTypes.ToList();
            try
            {
                ModelState.Remove("AccountTypes");
                if (ModelState.IsValid)
                {
                    _userService.AddUser(model);
                    TempData["SuccessMessage"] = "New user created.";
                    return View(userView);// REDIRECT TO LOGIN PAGE
                }
                return View(userView);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(userView);
            }
        }

}
}
