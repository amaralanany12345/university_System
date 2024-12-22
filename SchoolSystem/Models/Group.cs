namespace SchoolSystem.Models
{
    public class Group
    {
        public int id { get; set; }
        public int totalScoreOfTheYear { get; set; }
        public AcademicYear academicYear { get; set; }
        public ICollection<Student> students { get; set; } = new List<Student>();
        public ICollection<Course> groupCourses { get; set; } = new List<Course>();
        public ICollection<GroupTeacher> groupTeachers { get; set; } = new List<GroupTeacher>();
    }
}
