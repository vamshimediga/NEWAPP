using DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Controllers
{
    [Route("auth")] // Prefix for all routes in this controller
    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginModel login)
        {
            // Validate user credentials (this is a placeholder; implement your own user validation)
            if (login.Username == "user" && login.Password == "password")
            {
                var token = _tokenService.GenerateToken(login.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}
