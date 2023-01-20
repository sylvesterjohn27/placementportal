using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}