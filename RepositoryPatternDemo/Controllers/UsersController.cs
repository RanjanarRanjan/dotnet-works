using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_service.GetUsers());
        }
    }
}
