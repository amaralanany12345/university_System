using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Models;
using SchoolSystem.Services;

namespace SchoolSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinalResultController : ControllerBase
    {
        FinalResultService finalResultService;
        public FinalResultController()
        {
            finalResultService = new FinalResultService();
        }
        [HttpPost]
        public ActionResult<FinalResult> addStudentFinalResult(int studentId)
        {
            return Ok(finalResultService.createFinalResult(studentId));
        }
        [HttpGet]
        public ActionResult<FinalResult> getFinalResult(int studentId)
        {
            return Ok(finalResultService.getStudentFinalResult(studentId));
        }

        [HttpDelete]
        public ActionResult<FinalResult> deleteStudentFinalResult(int studentId)
        {
            finalResultService.deleteFinalResult(studentId);
            return Ok();
        }
    }
}
