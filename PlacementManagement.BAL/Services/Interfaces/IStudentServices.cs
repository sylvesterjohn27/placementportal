using PlacementManagement.BAL.Models;
using PlacementManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IStudentServices
    {
        void AddOrEditStudent(StudentViewModel student);
        List<StudentViewModel> GetAllStudentMastersByDepartmentIdandCollegeId(int departmentId, int collegeId, string collegeName = null);
        StudentViewModel GetStudentById(int studentId);
        bool DeleteStudent(int id);
    }
}
