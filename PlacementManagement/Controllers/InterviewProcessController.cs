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
        private readonly IMasterServices _masterServices;
        private readonly UserManager<IdentityUser> _userManager;

        public InterviewProcessController(IInterviewProcessServices interviewProcessServices, IUserServices userServices, UserManager<IdentityUser> userManager, IPlacementRequestServices placementRequestService, IMasterServices masterServices)
        {
            _interviewProcessServices = interviewProcessServices;
            _userServices = userServices;
            _userManager = userManager;
            _placementRequestService = placementRequestService;
            _masterServices = masterServices;
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var interviewCandidate = _interviewProcessServices.GetCandidateById(id);
            return View(interviewCandidate);
        }

        [HttpPost]
        public IActionResult Edit(InteviewProcessViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _interviewProcessServices.AddOrUpdateCandidateInterviewProcess(model);
                    TempData["SuccessMessage"] = "Interview process updated!";
                    return RedirectToAction("InterviewProcess", new { placementRequestId = model.PlacementRequestId });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }
    }
}
