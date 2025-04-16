using Application.Entities.DTOS.User;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO model)
    {
        var response = await _authService.RegisterAsync(model);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
    {
        var response = await _authService.LoginAsync(model);
        return StatusCode((int)response.StatusCode, response);
    }
}
