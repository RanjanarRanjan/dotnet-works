using EmployeeLayeredApp.Models;
using EmployeeLayeredApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLayeredApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            _auth.Register(user);
            return Ok("User registered");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var token = _auth.Login(request.Username, request.Password);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // true in production (https)
                SameSite = SameSiteMode.Strict
            });

            return Ok("Login successful");
        }
    }
}
