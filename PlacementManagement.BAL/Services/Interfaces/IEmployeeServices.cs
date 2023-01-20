using PlacementManagement.BAL.Models;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IEmployeeServices
    {
        List<EmployeeViewModel> GetAllEmployees();
        EmployeeViewModel GetEmployeeById(int id);
        void AddorEditEmployee(EmployeeViewModel employee);
        bool DeleteEmployee(int Id);
    }
}
