using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;


namespace PlacementManagement.BAL.Services.Implementations
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employee1Repository;
        public EmployeeServices(IEmployeeRepository employee1Repository)
        {
            _employee1Repository = employee1Repository;
        }

        public List<EmployeeViewModel> GetAllEmployees()
        {
            var employees = _employee1Repository.GetAllEmployees();
            var empList = new List<EmployeeViewModel>();
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
                empList.Add(emp);
            }
            return empList;
        }

        public EmployeeViewModel GetEmployeeById(int id)
        {
            var employee = _employee1Repository.GetEmployeeById(id);           
            if (employee!=null)
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
                return employeeViewModel;
            }
            return null;
        }

        public void AddorEditEmployee(EmployeeViewModel employee)
        {
            var emp = new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary
            };
            _employee1Repository.AddorEditEmployee(emp);
        }

        public bool DeleteEmployee(int id)
        {
            var emp = _employee1Repository.GetEmployeeById(id);
            if (emp != null)
            {
                _employee1Repository.DeleteEmployee(emp);
                return true;
            }
            return false;
        }
    }
}
    
    

