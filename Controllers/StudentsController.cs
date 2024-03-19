using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UdemyCourse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudens()
        {
            string[] studensNames = ["Alex", "Jackob", "David", "Emily"];

            return Ok(studensNames);

        }

    }
}
