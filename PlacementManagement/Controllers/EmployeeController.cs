using PlacementManagement.Models;
using Microsoft.AspNetCore.Mvc;
using PlacementManagement.DAL.Repository.Interface;
using PlacementManagement.DAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.BAL.Models;

namespace PlacementManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeService;

        public EmployeeController(IEmployeeServices employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var employees = _employeeService.GetAllEmployees();
                return View(employees);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.AddorEditEmployee(model);
                    TempData["SuccessMessage"] = "New employee created.";
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

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(Id);
                if (employee != null)
                {                   
                    return View(employee);
                }
                else
                {
                    TempData["ErrorMessage"] = $"Employee details not found with ID: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.AddorEditEmployee(model);                
                    TempData["SuccessMessage"] = "Employee details updated.";
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

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(Id);
                if (employee != null)
                {
                   return View(employee);
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
        public IActionResult DeleteConfirmed(int Id)
        {
            try
            {
                var isDeleted = _employeeService.DeleteEmployee(Id);
                if (isDeleted)
                {
                    TempData["SuccessMessage"] = "Employee details deleted.";
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
    }
}
