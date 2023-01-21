using Microsoft.AspNetCore.Mvc.Rendering;
using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Implementations;
using PlacementManagement.DAL.Repository.Interface;

namespace PlacementManagement.BAL.Services.Implementations
{
    public class StudentServices :IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMasterRepository _masterRepository;
        public StudentServices(IStudentRepository studentRepository, IMasterRepository masterRepository)
        { 
            _studentRepository= studentRepository;
            _masterRepository= masterRepository;
        }

        public void AddOrEditStudent(StudentViewModel student)
        {
            var studentMaster = new StudentMaster
            {
                Id= student.Id,
                StudentName = student.StudentName,
                Email= student.Email,
                DepartmentId = student.DepartmentId,
                CollegeId= student.CollegeId,
                CGPA = student.CGPA,
                CoreAreas= string.Join(",", student.CoreAreaIds.ToArray())
        };
            _studentRepository.AddOrEditStudent(studentMaster);
        }

        public List<StudentViewModel> GetAllStudentMastersByDepartmentIdandCollegeId(int departmentId, int collegeId, string collegeName = null)
        {
            var studentList  = new List<StudentViewModel>();           
            var coreAreas = _masterRepository.GetCoreAreas();
            var studentMaster = _studentRepository.GetAllStudentMastersByDepartmentIdandCollegeId(departmentId, collegeId);
            foreach(var student in studentMaster)
            {
                studentList.Add(new StudentViewModel {
                    Id = student.Id,
                    StudentName = student.StudentName, 
                    Email = student.Email, 
                    DepartmentId = student.DepartmentId, 
                    CGPA= student.CGPA,
                    CoreAreas= string.Join(", ", coreAreas.Where(c => student.CoreAreas.Split(',').Contains(c.Id.ToString()))
                                        .OrderBy(x => x.CoreArea).Select(c => c.CoreArea).ToList()),
                    CollegeId   = student.CollegeId,
                    DepartmentName = _masterRepository.GetDepartmentById(student.DepartmentId)?.DepartmentName,
                    CollegeName = collegeName
                });
            }
            return studentList;
        }
        public StudentViewModel GetStudentById(int studentId)
        {
            var student = _studentRepository.GetStudentById(studentId);           
            var studentViewModel = new StudentViewModel
            {
                Id = studentId,
                StudentName = student.StudentName,
                Email = student.Email,
                DepartmentId = student.DepartmentId,                              
                CollegeId = student.CollegeId,
                CoreAreas = student.CoreAreas,
                CGPA = student.CGPA
            };            
            return studentViewModel;
        }

        public bool DeleteStudent(int id)
        {
            var student = _studentRepository.GetStudentById(id);
            if (student != null)
            {
                _studentRepository.DeleteStudent(student);
                return true;
            }
            return false;
        }

        public List<StudentViewModel> GetEligibleStudents(int collegeId, string coreAreas, double cgpa, string department)
        {
            var studentList = new List<StudentViewModel>();
            var studentMasterList = _studentRepository.GetAllStudentMastersByDepartmentIdandCollegeId(0, collegeId);
            var expectedCoreAres = new HashSet<string>(coreAreas.Split(','));
            var expectedDepts = new HashSet<string>(department.Split(","));
            var result = (from student in studentMasterList
                          .Where(student => student.CoreAreas.Split(',').Any(code => expectedCoreAres.Contains(code)) &&
                                 student.DepartmentId.ToString().Any(code => expectedDepts.Contains(code.ToString())) &&
                                 student.CGPA>=cgpa
                                )select student).ToList();

            foreach (var student in result)
            {
                studentList.Add(new StudentViewModel
                {
                    Id = student.Id,
                    StudentName = student.StudentName,
                    Email = student.Email,
                    DepartmentId = student.DepartmentId,
                    CGPA = student.CGPA,
                    CoreAreas = student.CoreAreas,
                    CollegeId = student.CollegeId,
                    DepartmentName = _masterRepository.GetDepartmentById(student.DepartmentId)?.DepartmentName                    
                });
            }
            return studentList;
        }
    }
}
