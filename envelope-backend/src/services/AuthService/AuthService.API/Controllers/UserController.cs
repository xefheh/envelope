using Microsoft.AspNetCore.Mvc;
using AuthService.Application.Requests;
using AuthService.Application.Services;
using AuthService.Application.Exceptions;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _userService.Register(request);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            var typeException = result.Exception.GetType();

            var isIncorrectData = typeException == typeof(UsernameExistsException)
                || typeException == typeof(InvalidEmailException)
                || typeException == typeof(EmailExistsException);

            if (isIncorrectData)
            {
                return BadRequest(new { message = result.Exception.Message });
            }

            return StatusCode(500, result.Exception.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _userService.Login(request);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            var typeException = result.Exception.GetType();

            var isIncorrectData = typeException == typeof(InvalidPasswordException)
                || typeException == typeof(EmailNotExistsException)
                || typeException == typeof(UsernameNotExistsException);

            if (isIncorrectData)
            {
                return BadRequest(new { message = result.Exception.Message });
            }

            return StatusCode(500, result.Exception.Message);
        }
    }
}