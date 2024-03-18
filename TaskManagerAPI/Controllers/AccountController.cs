﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;
using TaskManagerAPI.Services.Interfaces;

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
}