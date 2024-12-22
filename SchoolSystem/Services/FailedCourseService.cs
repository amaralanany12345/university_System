using SchoolSystem.General;
using SchoolSystem.Models;

namespace SchoolSystem.Services
{
    public class FailedCourseService
    {
        AppDbContext context;
        public FailedCourseService()
        {
            context = new AppDbContext();
        }
        public List<FailedCourse> getStudentFailedCourse(int studentId)
        {
            var studentFailedCourses = (from item in context.failedCourses where item.studentId==studentId select item).ToList();
            return studentFailedCourses;
        }
    }
}
