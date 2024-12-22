using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FailedCoursesController : ControllerBase
    {
        FailedCourseService failedCourseService;
        public FailedCoursesController()
        {
            failedCourseService = new FailedCourseService();
        }
        [HttpGet]
        public ActionResult<FailedCourse> getStudentFailedCourse(int studentId)
        {
            return Ok(failedCourseService.getStudentFailedCourse(studentId));
        }
    }
}
