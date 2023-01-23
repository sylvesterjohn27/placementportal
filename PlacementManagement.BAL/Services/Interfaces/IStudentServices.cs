using PlacementManagement.BAL.Models;

namespace PlacementManagement.BAL.Services.Interfaces
{
    public interface IStudentServices
    {
        void AddOrEditStudent(StudentViewModel student);
        List<StudentViewModel> GetAllStudentMastersByDepartmentIdandCollegeId(int departmentId, int collegeId, string collegeName = null);
        StudentViewModel GetStudentById(int studentId);
        bool DeleteStudent(int id);
        List<StudentViewModel> GetEligibleStudents(int collegeId, string coreAreas, double cgpa, string department);
    }
}
