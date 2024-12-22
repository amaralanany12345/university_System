using SchoolSystem.Enums;

namespace SchoolSystem.Models
{
    public class FailedCourse
    {
        public int studentId { get; set; }
        public Student student { get; set; }
        public int courseId { get; set;}
        public Course course { get; set; }
        public int finalResultId { get; set; }
        public FinalResult finalResult { get; set; }
        public int studentCourseScore { get; set; }
        public Rate studentCourseRate { get; set; }
    }
}
