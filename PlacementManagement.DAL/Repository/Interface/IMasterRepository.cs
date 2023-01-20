using PlacementManagement.DAL.Models;

namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IMasterRepository
    {
        List<AccountTypeMaster> GetAccountTypes();
        AccountTypeMaster GetAccountTypeById(int id);

        List<DepartmentMaster> GetDepartments();
        DepartmentMaster GetDepartmentById(int id);

        List<CoreAreaMaster> GetCoreAreas();
        CoreAreaMaster GetCoreAreaById(int id);


        List<User> GetUsers();
        User GetUserById(int id);
        
        List<CollegeMaster> GetDepartmentsByCollegeId(int collegeId);
    }
}
