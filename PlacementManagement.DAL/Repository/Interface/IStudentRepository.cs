using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IStudentRepository
    {
        void AddOrEditStudent(StudentMaster student);
        List<StudentMaster> GetAllStudentMastersByDepartmentIdandCollegeId(int departmentId, int collegeId);
        StudentMaster GetStudentById(int studentId);
        void DeleteStudent(StudentMaster student);
    }
}
