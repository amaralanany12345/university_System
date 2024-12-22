using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        CourseService courseService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CourseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            courseService = new CourseService(_httpContextAccessor);
        }

        [HttpPost]
        public ActionResult<Course> addCourse(string courseName, string courseDescription, int teachetId,int groupId,int score)
        {
            return Ok(courseService.addCourse(courseName,courseDescription,teachetId, groupId,score));
        }
        [HttpGet]
        public ActionResult<Course> getCourse(int courseId)
        {
            return Ok(courseService.getCourse(courseId));
        }
        [HttpDelete]
        public ActionResult<Course> deleteCourse(int courseId)
        {
            courseService.deleteCourse(courseId);
            return Ok();
        }

        [HttpGet("getCourseStudents")]
        public ActionResult<List<Student>> getStudentCourse(int courseId)
        {
            return Ok(courseService.getCourseStudents(courseId));
        }

        [HttpGet("getStudentCourses")]
        public ActionResult<List<Course>> getStudentCourses(int studentId)
        {
            return Ok(courseService.getStudentCourses(studentId));
        }


        [HttpPut("updateScore")]
        public ActionResult<List<Course>> updateScore(int courseId, int score)
        {
            return Ok(courseService.updateCourseScore(courseId, score));
        }
    }
}
