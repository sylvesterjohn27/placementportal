using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
