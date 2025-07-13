using Microsoft.AspNetCore.Mvc;
using NotesApplication.DTOs.User;
using NotesApplication.Services;

namespace NotesApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<UserReadDto>>> GetUsersAsync()
    {
        var usersDto = await _userService.GetAsync();

        return Ok(usersDto);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<UserReadDto>> GetUserByIdAsync(Guid id)
    {
        var userDto = await _userService.GetByIdAsync(id);
        
        if (userDto == null) return NotFound();
        
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserReadDto>> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var userDto = await _userService.CreateAsync(userCreateDto);
        
        return CreatedAtRoute("GetUser", new { id = userDto.Id }, userDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserReadDto>> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
    {
        try
        {
            var updatedUserDto = await _userService.UpdateAsync(id, userUpdateDto);
            return Ok(updatedUserDto);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync(Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

}