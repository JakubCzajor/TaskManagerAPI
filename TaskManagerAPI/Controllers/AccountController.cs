using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TaskManagerAPI.Services;
using TaskManagerAPI.Services.Interfaces;
using TaskManagerAPI.Models.Accounts;

namespace TaskManagerAPI.Controllers;

[Route("/api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
    {
        await _accountService.RegisterUser(dto);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser([FromBody] LoginUserDto dto)
    {
        string token = await _accountService.GenerateJwtToken(dto);

        return Ok(token);
    }

    [HttpPut("role/{userId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> ChangeUserRole([FromRoute] int userId, [FromBody] UpdateRoleDto dto)
    {
        await _accountService.UpdateUserRole(userId, dto);

        return Ok();
    }

    [HttpPut("password")]
    public async Task<ActionResult> ChangeUserPassword([FromBody] UpdatePasswordDto dto)
    {
        await _accountService.UpdateUserPassword(dto);

        return Ok();
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var userDtos = await _accountService.GetAll();

        return Ok(userDtos);
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<UserProfileDto>> GetUserProfile()
    {
        var userDto = await _accountService.GetUserProfile();

        return Ok(userDto);
    }

    [HttpPut("profile")]
    [Authorize]
    public async Task<ActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDto dto)
    {
        await _accountService.UpdateUserProfile(dto);

        return Ok();
    }
}