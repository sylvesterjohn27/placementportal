using PlacementManagement.DAL.Repository.Interface;
using PlacementManagement.DAL.Repository.Implementations;
using PlacementManagement.DAL.Models;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class EmployeeRepository:GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(PlacementManagementAppDbContext context) : base(context) { }        
    }
}
