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

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto userDto)
        {
            var user = await _userService.Register(userDto);
            return Ok(user);
        }
    }
}