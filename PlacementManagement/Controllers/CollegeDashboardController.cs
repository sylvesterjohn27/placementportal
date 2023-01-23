using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Implementations;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;

namespace PlacementManagement.Controllers
{
    public class CollegeDashboardController : Controller
    {
        private readonly IPlacementRequestServices _placementRequestService;
        private readonly IStudentServices _studentServices;
        private readonly IMasterServices _masterService;
        private readonly IInterviewProcessServices _interviewProcessServices;
        private readonly IUserServices _userServices;
        private readonly UserManager<IdentityUser> _userManager;

        public CollegeDashboardController(IPlacementRequestServices placementRequestService, IStudentServices studentService, IUserServices userServices, UserManager<IdentityUser> userManager, IMasterServices masterService, IInterviewProcessServices interviewProcessServices) 
        {
            _placementRequestService = placementRequestService;
            _studentServices = studentService;
            _userServices = userServices;
            _userManager = userManager;
            _masterService = masterService;
            _interviewProcessServices = interviewProcessServices;
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

        public IActionResult GetStudents(int id)
        {
            var request = _placementRequestService.GetPlacementRequestById(id);
            var collegeDetails = GetCompanyOrCollegeName().Result;
            var coreAreaMasterData = _masterService.GetCoreAreas();
            var studentList = new List<StudentViewModel>();
            if (request != null)
            {
                ViewBag.PlacementRequestId = id;
                studentList = _studentServices.GetEligibleStudents(collegeDetails.Id, request.CoreAreas, request.CGPA, request.Departments);
                foreach (var student in studentList)
                {
                    var coreAreaIds = student.CoreAreas?.Split(',');
                    student.CoreAreas = string.Join(", ", coreAreaMasterData.Where(c => coreAreaIds.Contains(c.Id.ToString()))
                                        .OrderBy(x => x.CoreArea).Select(c => c.CoreArea).ToList());
                }
            }
            else
            {
                TempData["ErrorMessage"] = $"Placement Request not found with Id {id}";
                return RedirectToAction("Index", "PlacementRequest");
            }

            if (studentList.Count == 0)
            {
                TempData["ErrorMessage"] = "No students found with the given criteria!";
                return RedirectToAction("Index", "PlacementRequest");
            }
            return View(studentList);
        }

        //[HttpPost]
        public IActionResult SaveApproval_Rejection(int id, int placementRequestId)
        {
            try
            {
                var request = _placementRequestService.GetPlacementRequestById(placementRequestId);
                var collegeDetails = GetCompanyOrCollegeName().Result;
                var studentList = _studentServices.GetEligibleStudents(collegeDetails.Id, request.CoreAreas, request.CGPA, request.Departments);
                if (id > 0)
                {
                    request.IsApprovedByCollege = true;
                }
                else
                {
                    request.IsApprovedByCollege = false;
                    TempData["SuccessMessage"] = "Placement request rejected.";
                }
                _placementRequestService.Approve_RejectPlacementRequest(request);
                if (id > 0)
                {
                    // add eligible students to Interview process                   
                    bool isStudentAdded = _interviewProcessServices.AddOrRemoveCandidateForInterviewProcess(placementRequestId, studentList, false);                    
                    TempData["SuccessMessage"] = "Placement request approved.";
                }
                else
                {
                    //remove existing entry if rejected
                    bool isStudentAdded = _interviewProcessServices.AddOrRemoveCandidateForInterviewProcess(placementRequestId, studentList,true);
                    TempData["SuccessMessage"] = "Placement request rejected.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            return RedirectToAction("Index", "PlacementRequest");
        }
    }
}
