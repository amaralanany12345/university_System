using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        AttendanceService attendanceService;
        public AttendanceController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            attendanceService = new AttendanceService(_httpContextAccessor);
        }

        [HttpPut]
        public ActionResult<Attendance> updateAttendance(int courseId, int studentId, bool attendant)
        {
            return Ok(attendanceService.updateAttendance(courseId,studentId,attendant));
        }

        [HttpGet("studentAttendances")]
        public ActionResult<List<Attendance>> getStudentAttendances(int studentId)
        {
            return Ok(attendanceService.getStudentAttendace(studentId));
        }

        [HttpGet("courseAttendance")]
        public ActionResult<List<Attendance>> getCourseAttendance(int courseId)
        {
            return Ok(attendanceService.getCourseAttendace(courseId));
        }


        [HttpGet("studentCourseAttendace")]
        public ActionResult<List<Attendance>> getStudentCourseAttendace(int courseId,int studetndId)
        {
            return Ok(attendanceService.getStudentCourseAttendace(courseId, studetndId));
        }
    }
}
