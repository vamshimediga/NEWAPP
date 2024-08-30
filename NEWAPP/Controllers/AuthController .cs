using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NEWAPP.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Validate user credentials (for demonstration purposes, assume validation is successful)
            var userId = "123"; // Get user ID from database or other source
            var userName = model.UserName; // Get user name from login form
            var role = "User"; // Assign user role (you can customize this based on your application logic)

            // Generate JWT token
            var jwtToken = GenerateJwtToken(userId, userName, role);

            // Return token to the client
            return Ok(new { token = jwtToken });
        }

        private string GenerateJwtToken(string userId, string userName, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your-secret-key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
