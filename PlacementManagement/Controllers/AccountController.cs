using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PlacementManagement.BAL.Models;
using System.Text.Encodings.Web;
using System.Text;
using PlacementManagement.BAL.Services.Interfaces;

namespace PlacementManagement.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserServices _userService;
        private readonly IMasterServices _masterService;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserServices userService, IMasterServices masterService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;            
            _userService = userService;
            _masterService = masterService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }        

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                                    model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Employee");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        public IActionResult Register()
        {
            var accountTypes = _masterService.GetAccountTypes();
            RegisterViewModel model = new RegisterViewModel
            {
                AccountTypes = accountTypes.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var accountTypes = _masterService.GetAccountTypes();
            RegisterViewModel viewModel = new RegisterViewModel
            {
                AccountTypes = accountTypes.ToList()
            };

            ModelState.Remove("AccountTypes");            
            if (ModelState.IsValid)
            {
                try
                {
                    //Save User to UserMaster
                    _userService.AddUser(model);                    
                    var user = new IdentityUser
                    {
                        UserName = model.UserName,
                        Email = model.UserName,
                    };
                    //Create user account in Asp.Net Users table
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //Assign Role based on Account type dropdown
                        var defaultRole = (model.AccountTypeId == 1) ? _roleManager.FindByNameAsync("College").Result : _roleManager.FindByNameAsync("Company").Result;                     
                        if (defaultRole != null)
                        {
                            IdentityResult roleresult = await _userManager.AddToRoleAsync(user, defaultRole.Name);
                        }
                        //Login user
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        //Redirect to Home page
                        TempData["SuccessMessage"] = "New user created.";
                        return RedirectToAction("index", "Home");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    TempData["ErrorMessage"] = "Invalid Registration";
                    return View(viewModel);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }
    }
}