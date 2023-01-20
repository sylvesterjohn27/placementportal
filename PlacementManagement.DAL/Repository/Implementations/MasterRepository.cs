using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
