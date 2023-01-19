using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PlacementManagementAppDbContext _DbContext;

        public EmployeeRepository(PlacementManagementAppDbContext DbContext)
        {
            _DbContext= DbContext;
        }

        public List<Employee> GetAllEmployees()
        {
            return _DbContext.Employees.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            return _DbContext.Employees.FirstOrDefault(c => c.Id == id);
        }

        public void AddorEditEmployee(Employee employee)
        {
            if (employee.Id != 0)
                _DbContext.Employees.Update(employee);
            else
                _DbContext.Employees.Add(employee);   
            _DbContext.SaveChanges();
        }
          
        public void DeleteEmployee(Employee employee)
        {
            _DbContext.Employees.Remove(employee);
            _DbContext.SaveChanges();
        }
    }
}
