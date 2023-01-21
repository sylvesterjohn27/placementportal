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
        private readonly IUserServices _userServices;
        private readonly UserManager<IdentityUser> _userManager;

        public CollegeDashboardController(IPlacementRequestServices placementRequestService, IStudentServices studentService, IUserServices userServices, UserManager<IdentityUser> userManager, IMasterServices masterService) 
        {
            _placementRequestService= placementRequestService;
            _studentServices= studentService;
            _userServices = userServices;
            _userManager = userManager;
            _masterService = masterService;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        private async Task<UserViewModel> GetCompanyOrCollegeName()
        {
            var collegeName = string.Empty;
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
                studentList = _studentServices.GetEligibleStudents(collegeDetails.Id, request.CoreAreas, request.CGPA, request.Departments);
                foreach (var student in studentList)
                {
                    var coreAreaIds = student.CoreAreas?.Split(',');
                    student.CoreAreas = string.Join(", ", coreAreaMasterData.Where(c => coreAreaIds.Contains(c.Id.ToString()))
                                        .OrderBy(x => x.CoreArea).Select(c => c.CoreArea).ToList());
                }

               
            }
            return View(studentList);
        }
    }
}
