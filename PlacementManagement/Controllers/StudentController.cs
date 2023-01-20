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

        public async Task<ActionResult> Index(int collegeId)
        {
            var students = new List<StudentViewModel>();
            try
            {
                var currentUser = await GetCurrentUserAsync();
                if (currentUser != null)
                {
                    var userDetails = _userServices.GetUserByUserName(currentUser.UserName);
                    if(userDetails != null)
                    {
                        collegeId = userDetails.AccountTypeId;                        
                        students = _studentServices.GetAllStudentMastersByDepartmentIdandCollegeId(0, collegeId, userDetails.Name);
                        ViewData["CollegeId"] = collegeId;
                    }
                }                
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
        public ActionResult Create(int id)
        {
            var model = new StudentViewModel();
            List<SelectListItem> department = new List<SelectListItem>();
            var departments = _masterServices.GetDepartmentsByCollegeId(id);
            foreach (var ditem in departments.Departments)
            {
                department.Add(new SelectListItem
                {
                    Text = ditem.DepartmentName,
                    Value = Convert.ToString(ditem.Id)
                });
            }       
            return View(model);
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
