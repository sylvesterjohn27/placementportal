using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Implementations;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Services.Implementations
{
    public class MasterServices : IMasterServices
    {
        private readonly IMasterRepository _masterRepository;

        public MasterServices(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public List<AccountTypeMasterViewModel> GetAccountTypes()
        {
            var accountTypes = _masterRepository.GetAccountTypes();
            var accountTypesList = new List<AccountTypeMasterViewModel>();
            foreach (var account in accountTypes)
            {
                var accountType = new AccountTypeMasterViewModel()
                {
                    Id = account.Id,
                    AcccountType = account.AcccountType
                };
                accountTypesList.Add(accountType);
            }
            return accountTypesList;
        }
        public AccountTypeMasterViewModel GetAccountTypeById(int id)
        {
            var accountType = _masterRepository.GetAccountTypeById(id);
            if (accountType != null)
            {
                var accountTypeMasterViewModel = new AccountTypeMasterViewModel()
                {
                    Id = accountType.Id,
                    AcccountType = accountType.AcccountType
                };
                return accountTypeMasterViewModel;
            }
            return null;
        }

        public List<DepartmentMasterViewModel> GetDepartments()
        {
            var departments = _masterRepository.GetDepartments();
            var departmentsList = new List<DepartmentMasterViewModel>();
            foreach (var department in departments)
            {
                var departmentView = new DepartmentMasterViewModel()
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    DepartmentCode = department.DepartmentCode
                };
                departmentsList.Add(departmentView);
            }
            return departmentsList;
        }
        public DepartmentMasterViewModel GetDepartmentById(int id)
        {
            var department = _masterRepository.GetDepartmentById(id);
            if (department != null)
            {
                var departmentMasterViewModel = new DepartmentMasterViewModel()
                {
                    Id = department.Id,
                    DepartmentName = department.DepartmentName,
                    DepartmentCode = department.DepartmentCode
                };
                return departmentMasterViewModel;
            }
            return null;
        }        

        public List<CoreAreaMasterViewModel> GetCoreAreas()
        {
            var coreAreas = _masterRepository.GetCoreAreas();
            var coreAreasList = new List<CoreAreaMasterViewModel>();
            foreach (var area in coreAreas)
            {
                var coreArea = new CoreAreaMasterViewModel()
                {
                    Id = area.Id,
                    CoreArea = area.CoreArea
                };
                coreAreasList.Add(coreArea);
            }
            return coreAreasList;
        }
        public CoreAreaMasterViewModel GetCoreAreaById(int id)
        {
            var coreArea = _masterRepository.GetCoreAreaById(id);
            if (coreArea != null)
            {
                var coreAreaMasterViewModel = new CoreAreaMasterViewModel()
                {
                    Id = coreArea.Id,
                    CoreArea = coreArea.CoreArea
                };
                return coreAreaMasterViewModel;
            }
            return null;
        }
    }
}
