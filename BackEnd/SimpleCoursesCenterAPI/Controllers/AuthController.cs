using Data.UIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }
        // --- this action responsible for login functionality
        [HttpPost("login")]
        public IActionResult Login(UserDto user)
        {
            // --- login method and it's implementation insde auth service 
            var response = authService.AuthenticateLogin(user);
            return Ok(response);
        }
    }
}
