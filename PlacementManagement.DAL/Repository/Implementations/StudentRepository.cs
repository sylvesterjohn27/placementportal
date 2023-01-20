using Microsoft.EntityFrameworkCore;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Repository.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly PlacementManagementAppDbContext _dbContext;

        public StudentRepository(PlacementManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrEditStudent(StudentMaster student)
        {
            if (student.Id > 0)
                _dbContext.StudentMaster.Update(student);
            else
                _dbContext.StudentMaster.Add(student);
            _dbContext.SaveChanges();
        }

        public List<StudentMaster> GetAllStudentMastersByDepartmentIdandCollegeId(int departmentId, int collegeId)
        {
            if (departmentId > 0)
                return _dbContext.StudentMaster.Where(c => c.DepartmentId == departmentId && c.CollegeId == collegeId).ToList();
            else
                return _dbContext.StudentMaster.Where(c => c.CollegeId == collegeId).ToList();
        }

        public StudentMaster GetStudentById(int studentId)
        {
            return _dbContext.StudentMaster.FirstOrDefault(c => c.Id == studentId);
        }

        public void DeleteStudent(StudentMaster student)
        {
            _dbContext.StudentMaster.Remove(student);
            _dbContext.SaveChanges();
        }
    }
}
