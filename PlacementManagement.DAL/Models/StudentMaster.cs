namespace PlacementManagement.DAL.Models
{
    public class StudentMaster
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int CollegeId { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; }
        public double CGPA { get; set; }
        public string CoreAreas { get; set; }   
    }
}
