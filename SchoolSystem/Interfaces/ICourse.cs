using SchoolSystem.Models;

namespace SchoolSystem.Interfaces

{
    public interface ICourse
    {
        Course addCourse(string courseName, string courseDescription, int teachetId,int groupId, int score);
        Course getCourse(int courseId);
        Course updateCourseScore(int courseId, int score);
        ICollection<Student> getCourseStudents(int courseId);
        ICollection<Course> getStudentCourses(int studentId);
        void deleteCourse(int courseId);        
    }
}
