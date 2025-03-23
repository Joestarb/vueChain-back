using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vueChain.Dtos;
using vueChain.Interfaces;
using vueChain.Models;

namespace vueChain.Controllers;
[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUser(id);
        if (user == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(user);
        }
    }

    [HttpGet("users")]
    public async Task<OkObjectResult> GetUsers()
    {
        var users = await _userService.GetAllUsers();
    
        return Ok(users);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditUser(int id, [FromBody] UserDto userDto)
    {
        var user = await _userService.EditUser(id, userDto);
        if (user == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(user);
        }
    }
    
}