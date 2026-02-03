using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtCookieAuth.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new[]
            {
                "Ranjana",
                "Akhil",
                "Neha"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Admin delete success");
        }
    }
}
