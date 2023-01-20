using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class MasterRepository : IMasterRepository
    {
        private readonly PlacementManagementAppDbContext _dbContext;

        public MasterRepository(PlacementManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AccountTypeMaster> GetAccountTypes()
        {
            return _dbContext.AccountTypeMaster.ToList();
        }
        public AccountTypeMaster GetAccountTypeById(int id)
        {
            return _dbContext.AccountTypeMaster.FirstOrDefault(c => c.Id == id);
        }

        public List<DepartmentMaster> GetDepartments()
        {
            return _dbContext.DepartmentMaster.ToList();
        }

        public List<CollegeMaster> GetDepartmentsByCollegeId(int collegeId)
        {
            return _dbContext.CollegeMaster.Where(C => C.CollegeId == collegeId).ToList();
        }
        public DepartmentMaster GetDepartmentById(int id)
        {
            return _dbContext.DepartmentMaster.FirstOrDefault(c => c.Id == id);
        }

        public List<CoreAreaMaster> GetCoreAreas()
        {
            return _dbContext.CoreAreaMaster.ToList();
        }
        public CoreAreaMaster GetCoreAreaById(int id)
        {
            return _dbContext.CoreAreaMaster.FirstOrDefault(c => c.Id == id);
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(c => c.Id == id);
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users.Where(x => x.AccountTypeId == 1).ToList().OrderBy(x => x.Name).ToList();
        }
    }
}
