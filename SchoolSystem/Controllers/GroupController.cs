using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        GroupService groupService;
        public GroupController()
        {
            groupService = new GroupService();
        }
        [HttpPost]
        public ActionResult<Group> addGroup(AcademicYear academicYear)
        {
            return Ok(groupService.addGroup(academicYear));
        }
        [HttpGet]
        public ActionResult<Group> getGroup(int groupId)
        {
            return Ok(groupService.getGroup(groupId));
        }
        [HttpDelete]
        public ActionResult deleteGroup(int groupId)
        {
            groupService.deleteGroup(groupId);
            return Ok();
        }
        [HttpPut]
        public ActionResult<Group> addStudentToGroup(int groupId,int studentId)
        {
            return Ok(groupService.addStduentToGroup(groupId,studentId));
        }
        [HttpGet("getGroupStudnets")]
        public ActionResult<Student> getGroupStudent(int groupId)
        {
            return Ok(groupService.getGroupStudents(groupId));
        }

        [HttpGet("getGroupCourses")]
        public ActionResult<Student> getGroupCourses(int groupId)
        {
            return Ok(groupService.getGroupCourses(groupId));
        }
    }
}
