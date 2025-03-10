using Microsoft.AspNetCore.Mvc;
using vueChain.Interfaces;
using vueChain.Dtos;
using vueChain.Models;
using System.Threading.Tasks;

namespace vueChain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IUserTokenService _userTokenService;
        
        public AuthController(IUserService userService, IConfiguration configuration, IAuthService authService, IUserTokenService userTokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _authService = authService;
            _userTokenService = userTokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto userDto)
        {
            var user = await _userService.Register(userDto);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authService.Login(loginDto);
            if (token == null)
            {
                return Unauthorized();
            }
            var user = await _userService.GetUserByEmail(loginDto.Email);

            var userTokenDto = new UserTokenDto
            {
                Username = user.Username,
                Token = token,
                Email = user.Email,
                Role = user.Role

            };
            var userToken = await _userTokenService.CreateUserToken(userTokenDto);

            return Ok(new
            {
                token,
                user.Id,
                user.Username,
                user.Email,
                user.Role,
            });
        }
        
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout([FromBody] TokenDto tokenDto)
        {
            var result = await _userTokenService.DeleteUserTokenByToken(tokenDto.Token);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost("verify-token")]
        public async Task<IActionResult> VerifyToken([FromBody] TokenDto tokenDto)
        {
            var userToken = await _userTokenService.GetUserTokenByToken(tokenDto.Token);
            if (userToken == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                userToken.Username,
                userToken.Email,
                userToken.Role,
                ExpiresAt = userToken.expires_at 
            });
        }
    }
}