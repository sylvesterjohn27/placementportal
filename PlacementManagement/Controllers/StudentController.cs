using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Implementations;
using PlacementManagement.BAL.Services.Interfaces;
using System.Data;
using Microsoft.AspNetCore.Identity;
using PlacementManagement.DAL.Models;

namespace PlacementManagement.Controllers
{
    [Authorize(Roles = "College")]
    public class StudentController : Controller
    {
        private readonly IStudentServices _studentServices;
        private readonly IMasterServices _masterServices;
        private readonly IUserServices _userServices;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentController(IStudentServices studentServices, IMasterServices masterServices, IUserServices userServices, UserManager<IdentityUser> userManager)
        {
            _studentServices = studentServices;
            _masterServices = masterServices;
            _userServices = userServices;
            _userManager = userManager;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private async Task<UserViewModel> GetCollegeName()
        {
            var collegeName = string.Empty;
            var currentUser = await GetCurrentUserAsync();            
            if (currentUser != null)
            {
                return _userServices.GetUserByUserName(currentUser.UserName);              
            }
            return new UserViewModel();
        }

        public async Task<ActionResult> Index()
        {
            var students = new List<StudentViewModel>();
            try
            {
                var collegeDetails = await GetCollegeName();
                students = _studentServices.GetAllStudentMastersByDepartmentIdandCollegeId(0, collegeDetails.Id, collegeDetails.Name);
                return View(students);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {           
            List<SelectListItem> departmentList = new List<SelectListItem>();

            var collegeDetails = GetCollegeName().Result;
            var departments = _masterServices.GetDepartmentsByCollegeId(collegeDetails.Id);
            var coreAreas = _masterServices.GetCoreAreas();
            foreach (var ditem in departments.Departments)
            {
                departmentList.Add(new SelectListItem
                {
                    Text = ditem.DepartmentName,
                    Value = Convert.ToString(ditem.Id)
                });
            }
            var model = new StudentViewModel
            {
                DepartmentList = departmentList,
                CollegeId = collegeDetails.Id,
                CollegeName = collegeDetails.Name,
                CoreAreaIds = new List<long>().ToArray(),
                CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList()
            };
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        public ActionResult Create(StudentViewModel model)
        {
            try
            {
                ModelState.Remove("DepartmentList");
                ModelState.Remove("DepartmentName");                             
                if (ModelState.IsValid)
                {
                    _studentServices.AddOrEditStudent(model);
                    TempData["SuccessMessage"] = "New Student Created.";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {            
            List<SelectListItem> departmentList = new List<SelectListItem>();
            try
            {
                var collegeDetails = GetCollegeName().Result;
                var student = _studentServices.GetStudentById(id);                             
                if (student != null)
                {                   
                    var departments = _masterServices.GetDepartmentsByCollegeId(student.CollegeId);
                    var coreAreas = _masterServices.GetCoreAreas();
                    student.DepartmentList = departments.Departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString(), Selected = (x.Id==student.DepartmentId) }).ToList();
                    student.CollegeName = collegeDetails.Name;
                    student.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();                    
                    student.CoreAreaIds = student.CoreAreas?.Split(',').Select(long.Parse).ToArray();
                    return View(student);
                }
                else
                {
                    TempData["ErrorMessage"] = $"Placement request not found with ID: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: StudentController/Edit/5
        [HttpPost]        
        public ActionResult Edit(StudentViewModel model)
        {
            try
            {

                ModelState.Remove("DepartmentList");
                ModelState.Remove("DepartmentName");                
                if (ModelState.IsValid)
                {
                    _studentServices.AddOrEditStudent(model);
                    TempData["SuccessMessage"] = "Student updated successfuly.";
                    return RedirectToAction("Index");
                }
                var departments = _masterServices.GetDepartmentsByCollegeId(model.CollegeId);
                model.DepartmentList = departments.Departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString(), Selected = (x.Id == model.DepartmentId) }).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var student = _studentServices.GetStudentById(Id);
                if (student != null)
                {
                    var departments = _masterServices.GetDepartmentsByCollegeId(student.CollegeId);
                    var coreAreas = _masterServices.GetCoreAreas();
                    student.DepartmentList = departments.Departments.Select(x => new SelectListItem { Text = x.DepartmentName, Value = x.Id.ToString(), Selected = (x.Id == student.DepartmentId) }).ToList();
                    student.CoreAreaDetails = coreAreas.Select(x => new SelectListItem { Text = x.CoreArea, Value = x.Id.ToString() }).ToList();
                    student.CoreAreaIds = student.CoreAreas?.Split(',').Select(long.Parse).ToArray();
                    return View(student);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentServices.GetStudentById(id);
            try
            {
                if (student != null)
                {
                    bool isDeleted = _studentServices.DeleteStudent(id);
                    if (isDeleted)
                    {
                        TempData["SuccessMessage"] = "Student deleted.";
                        return RedirectToAction("Index");
                    }
                }               
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}