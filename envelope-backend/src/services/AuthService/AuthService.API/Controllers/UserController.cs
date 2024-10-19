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
            try
            {
                var userDTO = await _userService.Register(request);
                return Ok(userDTO);
            }
            catch (UsernameExistsException usernameExistsException)
            {
                return BadRequest(new { message = usernameExistsException.Message});
            }
            catch (InvalidEmailException invalidEmailException)
            {
                return BadRequest(new { message = invalidEmailException.Message });
            }
            catch (EmailExistsException emailExistsException)
            {
                return BadRequest(new { message = emailExistsException.Message });
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var userDTO = await _userService.Login(request);
                return Ok(userDTO);
            }
            catch (InvalidPasswordException invalidPasswordException)
            {
                return BadRequest(new { message = invalidPasswordException.Message });
            }
            catch (EmailNotExistsException emailNotExistsException)
            {
                return BadRequest(new { message = emailNotExistsException.Message });
            }
            catch (UsernameNotExistsException usernameNotExistsException)
            {
                return BadRequest(new { message = usernameNotExistsException.Message });
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}