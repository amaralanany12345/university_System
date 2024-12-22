using SchoolSystem.Models;

namespace SchoolSystem.Interfaces
{
    public interface ICourseWork
    {
        List<CourseWork> getCourseWorks(int courseId);
        List<CourseWork> getStudentCoursesWorks(int studentId);
        CourseWork updateStudentCoureScore(int courseId, int studentId, int score);
    }
}
