namespace SchoolSystem.Models
{
    public class Course
    {
        public int id { get; set; }
        public AcademicYear academicYear { get; set; }
        public int score { get; set; }
        public string courseName { get; set; }
        public string description { get; set; }
        public Teacher teacher { get; set; }
        public int teacherId { get; set; }
        public Group group { get; set; }
        public int groupId { get; set; }
        public ICollection<StudentCourse> studentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Attendance> attendances { get; set; } = new List<Attendance>();
        public ICollection<CourseWork> courseWorks { get; set; } = new List<CourseWork>();
        public ICollection<FailedCourse> failedCourses { get; set; } = new List<FailedCourse>();
    }
}
