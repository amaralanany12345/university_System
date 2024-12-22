using SchoolSystem.Enums;

namespace SchoolSystem.Models
{
    public class FinalResult
    {
        public int id { get; set; }
        public int studentId { get; set; }
        public Student student { get; set; }
        public int totalScore { get; set; }
        public int finalScore { get; set; }
        public Rate finalRate { get; set; }
        public AcademicYear academicYear{ get; set; }
        public ICollection<FailedCourse>? failedCourses { get; set; } = new List<FailedCourse>();
    }
}
