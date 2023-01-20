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
        private readonly PlacementManagementAppDbContext _dbContext;

        public EmployeeRepository(PlacementManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Employee> GetAllEmployees()
        {
            return _dbContext.Employees.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(c => c.Id == id);
        }

        public void AddorEditEmployee(Employee employee)
        {
            if (employee.Id > 0)
                _dbContext.Employees.Update(employee);
            else
                _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }
          
        public void DeleteEmployee(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }
    }
}
