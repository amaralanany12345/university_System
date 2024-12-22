namespace SchoolSystem.Models
{
    public class StudentCourse
    {
        public int courseId { get; set; }
        public Course course { get; set;}
        public int studentId { get; set; }
        public Student student { get; set; }
    }
}
