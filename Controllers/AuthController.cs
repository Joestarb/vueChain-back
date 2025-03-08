using Microsoft.AspNetCore.Mvc;
using vueChain.Interfaces;
using vueChain.Dtos;
using vueChain.Models;

namespace vueChain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IConfiguration configuration, IAuthService authService)
        {
            _userService = userService;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto userDto)
        {
            var user = await _userService.Register(userDto);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var token = await _authService.Login(userDto);
            if (token == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByUsername(userDto.Username);
            return Ok(new
            {
                token,
                user.Id,
                user.Username,
                user.Email
            });
        }
    }
}