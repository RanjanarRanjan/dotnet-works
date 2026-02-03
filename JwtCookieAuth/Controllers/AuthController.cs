using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtCookieAuth.Models;

namespace JwtCookieAuth.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly string _connStr =
            Environment.GetEnvironmentVariable("DB_CONNECTION")!;

        [HttpPost("login")]
        public IActionResult Login(UserLogin login)
        {
            string role = "";

            using var conn = new SqlConnection(_connStr);
            using var cmd = new SqlCommand("sp_LoginUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", login.Username);
            cmd.Parameters.AddWithValue("@Password", login.Password);

            conn.Open();
            var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return Unauthorized("Invalid credentials");

            role = reader["Role"].ToString()!;

            var token = GenerateJwt(login.Username, role);

            // 🍪 STORE JWT IN COOKIE
            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // true in production (HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            return Ok("Login successful");
        }

        private string GenerateJwt(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_KEY")!)
            );

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(
                        Environment.GetEnvironmentVariable("JWT_EXPIRE_MINUTES"))),
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
