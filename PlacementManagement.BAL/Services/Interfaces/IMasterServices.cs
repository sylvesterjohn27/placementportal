using PlacementManagement.BAL.Models;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IMasterServices
    {
        List<AccountTypeMasterViewModel> GetAccountTypes();
        AccountTypeMasterViewModel GetAccountTypeById(int id);

        List<DepartmentMasterViewModel> GetDepartments();
        DepartmentMasterViewModel GetDepartmentById(int id);

        List<CoreAreaMasterViewModel> GetCoreAreas();
        CoreAreaMasterViewModel GetCoreAreaById(int id);

        List<RegisterViewModel> GetUsers();
        RegisterViewModel GetUserById(int id);

        CollegeDepartmentViewModel GetDepartmentsByCollegeId(int collegeId);
    }
}
