using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlacementManagement.DAL.Models;

namespace PlacementManagement.DAL
{
    public class PlacementManagementAppDbContext : IdentityDbContext
    {
        public PlacementManagementAppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AccountTypeMaster> AccountTypeMaster { get; set; }
        public DbSet<DepartmentMaster> DepartmentMaster { get; set; }
        public DbSet<CoreAreaMaster> CoreAreaMaster { get; set; }
        public DbSet<StudentMaster> StudentMaster { get; set; }         
        public DbSet<CollegeMaster> CollegeMaster { get; set; }  
        public DbSet<PlacementRequest> PlacementRequest { get; set; }
        public DbSet<InterviewProcess> InterviewProcess { get; set; }

    }
}