using SchoolSystem.Enums;

namespace SchoolSystem.Models
{
    public class CourseWork
    {
        public Course course { get; set;}
        public int courseId { get; set;}
        public Student student { get; set; }
        public int studentId { get; set;}
        public int studentScore { get; set; }
        public AcademicYear academicYear { get; set; }
        public Rate studentCourseRate { get; set; }

    }
}
