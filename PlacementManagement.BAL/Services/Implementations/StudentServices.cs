﻿using PlacementManagement.BAL.Models;
using PlacementManagement.BAL.Services.Interfaces;
using PlacementManagement.DAL.Models;
using PlacementManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                StudentName = student.StudentName,
                Email= student.Email,
                DepartmentId = student.DepartmentId,
                CollegeId= student.CollegeId,
                CGPA = student.CGPA,
                CoreAreas= student.CoreAreas
            };
            _studentRepository.AddOrEditStudent(studentMaster);
        }

        public List<StudentViewModel> GetAllStudentMastersByDepartmentIdandCollegeId(int departmentId, int collegeId, string collegeName = null)
        {
            var studentList  = new List<StudentViewModel>();           
            var studentMaster = _studentRepository.GetAllStudentMastersByDepartmentIdandCollegeId(departmentId, collegeId);
            foreach(var student in studentMaster)
            {
                studentList.Add(new StudentViewModel {
                    StudentName = student.StudentName, 
                    Email = student.Email, 
                    DepartmentId = student.DepartmentId, 
                    CGPA= student.CGPA,
                    CoreAreas= student.CoreAreas,
                    CollegeId   = student.CollegeId,
                    DepartmentName = _masterRepository.GetDepartmentById(student.DepartmentId)?.DepartmentName,
                    CollegeName = collegeName
                });
            }
            return studentList;
        }
    }
}