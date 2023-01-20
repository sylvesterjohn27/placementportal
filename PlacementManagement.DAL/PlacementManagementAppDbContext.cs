using Microsoft.EntityFrameworkCore;
using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL
{
    public class PlacementManagementAppDbContext : DbContext
    {
        public PlacementManagementAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }                   
        public DbSet<PlacementRequest> PlacementRequest { get; set; }
        public DbSet<DepartmentMaster> DepartmentMaster { get; set; }
        public DbSet<CoreAreaMaster> CoreAreaMaster { get; set; }
        public DbSet<AccountTypeMaster> AccountTypeMaster { get; set; }

    }
}
