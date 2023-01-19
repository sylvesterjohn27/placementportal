using PlacementManagement.Models;
using Microsoft.AspNetCore.Mvc;
using PlacementManagement.DAL.Repository.Interface;
using PlacementManagement.DAL.Models;

namespace PlacementManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var employees = _unitOfWork.Employee.GetAll();

                List<EmployeeViewModel> employeesList = new List<EmployeeViewModel>();  

                foreach (var employee in employees)
                {
                    var emp = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        Department = employee.Department,
                        Salary = employee.Salary,
                        DateOfBirth = employee.DateOfBirth
                    };
                    employeesList.Add(emp);
                }

                return View(employeesList);
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
                    var employee = new Employee()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Department = model.Department,
                        Salary = model.Salary,
                        DateOfBirth = model.DateOfBirth
                    };

                    _unitOfWork.Employee.Insert(employee);
                    _unitOfWork.Save();
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
                var employee = _unitOfWork.Employee.GetById(Id);

                if (employee != null)
                {
                    var employeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        Department = employee.Department,
                        Salary = employee.Salary,
                        DateOfBirth = employee.DateOfBirth
                    };
                    return View(employeeViewModel);
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
                    var employee = new Employee()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Email = model.Email,
                        Department = model.Department,
                        Salary = model.Salary,
                        DateOfBirth = model.DateOfBirth
                    };

                    _unitOfWork.Employee.Update(employee);
                    _unitOfWork.Save();
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
                var employee = _unitOfWork.Employee.GetById(Id);

                if (employee != null)
                {
                    var employeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        Department = employee.Department,
                        Salary = employee.Salary,
                        DateOfBirth = employee.DateOfBirth
                    };
                    return View(employeeViewModel);
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
                var employee = _unitOfWork.Employee.GetById(Id);

                if (employee != null)
                {
                    _unitOfWork.Employee.Delete(employee);
                    _unitOfWork.Save();
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
