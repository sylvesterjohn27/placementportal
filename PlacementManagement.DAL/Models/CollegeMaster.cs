namespace PlacementManagement.DAL.Models
{
    public class CollegeMaster
    {
        public int Id { get; set; }
        public int CollegeId { get; set; }
        public int DepartmentId { get; set; }
        public string CoreAreas { get; set; }
    }
}
