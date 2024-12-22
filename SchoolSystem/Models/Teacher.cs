namespace SchoolSystem.Models
{
    public class Teacher:User
    {
        public ICollection<Course> courses { get; set; } = new List<Course>(); 
        public ICollection<GroupTeacher> groupTeachers { get; set; } = new List<GroupTeacher>();

    }
}
