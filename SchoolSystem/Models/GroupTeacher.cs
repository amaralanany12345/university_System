namespace SchoolSystem.Models
{
    public class GroupTeacher
    {
        public Group group { get; set; }
        public int groupId { get; set; } 
        public Teacher teacher { get; set; }
        public int teacherId { get; set; }
    }
}
