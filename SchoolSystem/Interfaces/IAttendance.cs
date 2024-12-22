using SchoolSystem.Models;

namespace SchoolSystem.Interfaces
{
    public interface IAttendance
    {
        Attendance updateAttendance(int courseId,int studentId, bool attendant);
        List<Attendance> getStudentAttendace(int studentId);
        List<Attendance> getCourseAttendace(int courseId);
        List<Attendance> getStudentCourseAttendace(int courseId,int studentId);
    }
}
