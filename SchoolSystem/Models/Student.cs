namespace SchoolSystem.Models
{
    public class Student:User
    {
        public AcademicYear academicYear { get; set; }
        public Group group { get; set; }
        public int groupId { get; set; }
        public ICollection<StudentCourse> studentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<Attendance> attendances { get; set; } = new List<Attendance>();
        public ICollection<FailedCourse> failedCourses { get; set; } = new List<FailedCourse>();
        public ICollection<FinalResult> finalResults { get; set; } = new List<FinalResult>();

    }
}
