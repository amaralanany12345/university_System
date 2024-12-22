using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;
using System.Security.Claims;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly Jwt _jwt;
        TeacherService teacherService;
        public TeacherController(Jwt jwt)
        {
            _jwt = jwt;
            teacherService = new TeacherService(_jwt);
        }
        [HttpPost("signin")]
        public ActionResult<Student> signin([FromBody] SigninInfo userInfo)
        {
            return Ok(teacherService.signin(userInfo));
        }

        [HttpPost("registerTeacher")]
        public ActionResult<Student> signup([FromBody] SignUpInfo userInfo)
        {
            return Ok(teacherService.registerTeacher(userInfo));
        }

        [HttpDelete]
        public ActionResult<Student> deleteTeacher(int teacherId)
        {
            teacherService.deleteTeacher(teacherId);
            return Ok();
        }
        [HttpGet]
        public ActionResult<Student> getTeacher(int teacherId)
        {
            //var userId = int.Parse(User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value);
            //if(userId == null)
            //{
            //    throw new ArgumentException("user i snot found");
            //}

            return Ok(teacherService.getTeacher(teacherId));
        }
    }
}
