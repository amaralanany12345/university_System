using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;
using System.Security.Claims;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly Jwt _jwt;
        StudentService studentService;
        public StudentController(Jwt jwt)
        {
            _jwt = jwt;
            studentService = new StudentService(_jwt);
        }

        [HttpPost("signin")]
        public ActionResult<Student> signin([FromBody] SigninInfo userInfo)
        {
            return Ok(studentService.signin(userInfo));
        }

        [HttpPost("registerStudent")]
        public ActionResult<Student> signup([FromBody] SignUpInfo userInfo,int groupId)
        {
            return Ok(studentService.registerStudent(userInfo, groupId));
        }

        [HttpDelete]
        public ActionResult<Student> deleteStudent(int studentId)
        {
            studentService.deleteStudent(studentId);
            return Ok();
        }

        [HttpGet]
        public ActionResult<Student> getStudent(int studentId)
        {
            return Ok(studentService.getStudent(studentId));
        }

        [HttpPut]
        public ActionResult<Student> updateStudent(int studentId, AcademicYear studentAcademicYear, int groupId)
        {
            return Ok(studentService.updateStudent(studentId,studentAcademicYear,groupId));
        }
    }
}
