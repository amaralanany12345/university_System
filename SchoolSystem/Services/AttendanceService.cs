using Microsoft.AspNetCore.Http;
using SchoolSystem.General;
using SchoolSystem.Interfaces;
using SchoolSystem.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace SchoolSystem.Services
{
    public class AttendanceService : IAttendance
    {
        AppDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        UserService userService;
        public AttendanceService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            userService = new UserService(_httpContextAccessor);
            context = new AppDbContext();
        }
        public Attendance updateAttendance(int courseId, int studentId, bool attendant)
        {
            var course = (from item in context.courses where item.id == courseId select item).FirstOrDefault();
            if (course == null)
            {
                throw new ArgumentException("course is not found");
            }

            var student = (from item in context.students where item.id== studentId select item).FirstOrDefault();
            if (student == null)
            {
                throw new ArgumentException("student is not found");
            }

            var attendance = (from item in context.attendances where item.studentId== studentId && item.courseId==courseId select item).FirstOrDefault();
            if (attendance == null)
            {
                throw new ArgumentException("attendance is not found");
            }

            if (course.teacherId!= userService.getCurrentUserId())
            {
                throw new ArgumentException("you are not allowed to register the student attendance");
            }
            if (attendant == true)
            {
                attendance.numOfAttendances++;
            }
            else
            {
                attendance.numOfAbsences++;
            }
            context.SaveChanges();
            return attendance;
        }

        public List<Attendance> getStudentAttendace(int studentId)
        {
            var student = (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if (student == null)
            {
                throw new ArgumentException("course is not found");
            }

            if (studentId!=userService.getCurrentUserId())
            {
                throw new ArgumentException("you are not allowed to see the student attendances ");
            }
            var studentAttendances = (from item in context.attendances where item.studentId == studentId select item).ToList();
            return studentAttendances;
        }

        public List<Attendance> getCourseAttendace(int courseId)
        {
            var course = (from item in context.courses where item.id == courseId select item).FirstOrDefault();
            if(course == null)
            {
                throw new ArgumentException("course is not found");
            }

            if (course.teacherId != userService.getCurrentUserId())
            {
                throw new ArgumentException("you are not allowed to see the course attendances");
            }
            var courseAttendances = (from item in context.attendances where item.courseId == courseId select item).ToList();
            Console.WriteLine(courseAttendances.Count);
            return courseAttendances;
        }
        public List<Attendance> getStudentCourseAttendace(int courseId,int studentId)
        {
            var course = (from item in context.courses where item.id == courseId select item).FirstOrDefault();
            if(course== null)
            {
                throw new ArgumentException("course is not found");
            }

            var student= (from item in context.students where item.id == studentId select item).FirstOrDefault();
            if (student== null)
            {
                throw new ArgumentException("student is not found");
            }
            if (studentId==userService.getCurrentUserId() || course.teacherId== userService.getCurrentUserId())
            {
            var studentCourseAttendances = (from item in context.attendances where item.courseId == courseId && item.studentId==studentId select item).ToList();
            Console.WriteLine(studentCourseAttendances.Count);
            return studentCourseAttendances;
            }
            else
            {
                throw new ArgumentException("you are not allowed to see the student attendances");
            }

        }
    }
}
