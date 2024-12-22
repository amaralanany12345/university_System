using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Migrations;
using SchoolSystem.Models;
using SchoolSystem.Services;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseWorkController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        CourseWorkService courseWorkService;
        public CourseWorkController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            courseWorkService = new CourseWorkService(_httpContextAccessor);
        }

        [HttpGet("getCourseWorks")]
        public ActionResult<List<courseWork>> getCourseWork(int courseId)
        {
            return Ok(courseWorkService.getCourseWorks(courseId));
        }

        [HttpPut]
        public ActionResult<courseWork> updateStudentCourseScore(int courseId, int studentId, int score)
        {
            return Ok(courseWorkService.updateStudentCoureScore(courseId,studentId,score));
        }

        [HttpGet("getStudentCoursesWorks")]
        public ActionResult<List<courseWork>> getStudentCoursesWorks(int studentId)
        {
            return Ok(courseWorkService.getStudentCoursesWorks(studentId));
        }

        [HttpPut("remakeCourseWork")]
        public ActionResult<courseWork> remakeCourseWork(int studentId, int courseId, int newScore)
        {
            return Ok(courseWorkService.remakeCourseWork(studentId, courseId, newScore));
        }
    }
}
