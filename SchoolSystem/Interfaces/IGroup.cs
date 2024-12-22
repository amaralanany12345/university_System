using SchoolSystem.Models;

namespace SchoolSystem.Interfaces
{
    public interface IGroup
    {
        Group addGroup(AcademicYear academicYear);
        Group getGroup(int groupId);
        void deleteGroup(int groupId);
        Group addStduentToGroup(int groupId,int studentId);
        ICollection<Student> getGroupStudents(int groupId);
        ICollection<Course> getGroupCourses(int groupId);

    }
}
