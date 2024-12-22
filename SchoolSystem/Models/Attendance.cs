namespace SchoolSystem.Models
{
    public class Attendance
    {
        public Student student { get; set; }
        public int studentId { get; set; }
        public Course course { get; set; }
        public int courseId { get; set; }
        public int numOfAttendances { get; set;}
        public int numOfAbsences { get;set;}
    }
}
