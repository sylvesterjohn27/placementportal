using PlacementManagement.BAL.Models;
using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
