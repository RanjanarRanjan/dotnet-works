using DotNetEnv;
using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository; 

        // Inject services via constructor
        public AuthController(JwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        [HttpPost("admin/login")]
        public IActionResult AdminLogin([FromBody] LoginRequest request)
        {
            Env.Load();
            if (request.Username == Environment.GetEnvironmentVariable("ADMIN_USERNAME") &&
                request.Password == Environment.GetEnvironmentVariable("ADMIN_PASSWORD"))
            {
                // var token = _jwtService.GenerateToken(request.Username, "Admin");
                var token = _jwtService.GenerateToken(
    0,                    // Admin userId
    request.Username,
    "Admin"
);


                HttpContext.Response.Cookies.Append(
                    "jwt-token",
                    token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.Now.AddHours(2)
                    }
                );

                return Ok(new { Token = token, Message = "Logged in successfully, cookie set." });
            }

            return Unauthorized("Invalid Admin Credentials");
        }

        [HttpPost("user/login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequest request)
        {
            var user = (await _userRepository.GetAllUsers())
                       .FirstOrDefault(u => u.Username == request.Username && !u.IsDeleted);

            if (user == null || user.Password != request.Password || user.IsBlocked)
                return Unauthorized("Invalid credentials or blocked user");

            // var token = _jwtService.GenerateToken(user.Username, "User");
            var token = _jwtService.GenerateToken(
    user.UserID,
    user.Username,
    "User"
);


            HttpContext.Response.Cookies.Append(
                "jwt-token",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddHours(2)
                }
            );

            return Ok(new { Token = token, Message = "User logged in, cookie set." });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies.ContainsKey("jwt-token"))
            {
                HttpContext.Response.Cookies.Delete("jwt-token");
            }

            return Ok("Logged out successfully, cookie cleared.");
        }
    }

    public record LoginRequest(string Username, string Password);
}
