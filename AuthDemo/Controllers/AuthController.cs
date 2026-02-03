using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(User login)
    {
        // Dummy validation
        if (login.Username == "admin" && login.Password == "1234")
        {
            var token = GenerateJwtToken(login.Username);
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }

    private string GenerateJwtToken(string username)
    {
        var key = "THIS_IS_A_VERY_LONG_SUPER_SECRET_JWT_KEY_1234567890123456";
        var keyBytes = Encoding.UTF8.GetBytes(key);

        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            issuer: "MyAuthServer",
            audience: "MyApiUsers",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
