using PlacementManagement.DAL.Models;

namespace PlacementManagement.DAL.Repository.Interface
{
    public interface IStudentRepository
    {
        void AddOrEditStudent(StudentMaster student);
        List<StudentMaster> GetAllStudentMastersByDepartmentIdandCollegeId(int departmentId, int collegeId);
    }
}
