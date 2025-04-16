using Application.Interfaces.IServices;
using Application.Entities.Base;
using Application.Reponse;
using FusionNews_API.Data;
using FusionNews_API.Services.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var response = await _authService.RegisterAsync(model);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var response = await _authService.LoginAsync(model);
        return StatusCode((int)response.StatusCode, response);
    }
}
