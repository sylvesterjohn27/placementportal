﻿using PlacementManagement.BAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IEmployeeServices
    {
        List<EmployeeViewModel> GetAllEmployees();
        EmployeeViewModel GetEmployeeById(int id);
        void AddorEditEmployee(EmployeeViewModel employee);
        bool DeleteEmployee(int Id);
    }
}