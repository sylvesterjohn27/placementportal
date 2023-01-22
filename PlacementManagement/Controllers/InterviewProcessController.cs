using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Implementations;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.Controllers
{
    public class InterviewProcessController : Controller
    {
        private readonly IInterviewProcessServices _interviewProcessServices;
        private readonly IPlacementRequestServices _placementRequestService;
        private readonly IUserServices _userServices;
        private readonly UserManager<IdentityUser> _userManager;

        public InterviewProcessController(IInterviewProcessServices interviewProcessServices, IUserServices userServices, UserManager<IdentityUser> userManager, IPlacementRequestServices placementRequestService)
        {
            _interviewProcessServices = interviewProcessServices;
            _userServices = userServices;
            _userManager = userManager;
            _placementRequestService = placementRequestService;
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

        public IActionResult Index()
        {
            try
            {
                var loggedInCompany = GetCompanyOrCollegeName().Result;
                var placementRequests = _placementRequestService.GetPlacementRequestsByCompanyOrCollegeId(loggedInCompany.Id, loggedInCompany.AccountTypeId);
                if (placementRequests != null)
                {
                    foreach (var placement in placementRequests)
                    {
                        placement.Status = placement.IsApprovedByCollege == null ? "Awaiting for Approval" : placement.IsApprovedByCollege == true ? "Approved" : "Rejected";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "No Placement Requests found!";
                }
                return View(placementRequests);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "PlacementRequest");
            }

        }

        public IActionResult InterviewProcess(int placementRequestId)
        {
            var interviewProcess = _interviewProcessServices.GetInterviewProcessByPlacementRequestId(placementRequestId);
            return View(interviewProcess);

        }


    }
}
