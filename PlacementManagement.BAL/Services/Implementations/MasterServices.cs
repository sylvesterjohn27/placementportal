using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Repository.Interface;

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

        public List<RegisterViewModel> GetUsers()
        {
            var users = _masterRepository.GetUsers();
            var userList = new List<RegisterViewModel>();
            foreach (var user in users)
            {
                var userDetails = new RegisterViewModel()
                {
                    Id = user.Id,
                    Name = user.Name
                };
                userList.Add(userDetails);
            }
            return userList;
        }

        public RegisterViewModel GetUserById(int id)
        {
            var user = _masterRepository.GetUserById(id);
            if (user != null)
            {
                var userDetails = new RegisterViewModel()
                {
                    Id = user.Id,
                    Name = user.Name
                };
                return userDetails;
            }
            return null;
      }
        public CollegeDepartmentViewModel GetDepartmentsByCollegeId(int collegeId)
        {            
            var departmentList = _masterRepository.GetDepartmentsByCollegeId(collegeId);
            var collegeDepartment = new CollegeDepartmentViewModel { Departments = new List<DepartmentModel> { } };
            foreach (var department in departmentList)
            {
                collegeDepartment.Departments.Add(
                    new DepartmentModel
                    {
                        Id = department.Id,
                        DepartmentName = GetDepartmentById(department.Id).DepartmentName
                    });
            }
            collegeDepartment.CollegeId = collegeId;
            return collegeDepartment;
        }
    }
}
