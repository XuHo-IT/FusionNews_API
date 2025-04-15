using Application.Reponse;
using FusionNews_API.Data;
using FusionNews_API.Models;
using FusionNews_API.Services.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtService _jwtService;

    public AuthController(UserDbContext context, IPasswordHasher<User> passwordHasher, JwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var response = new APIResponse();

        if (await _context.Users.AnyAsync(u => u.Username == model.Username))
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            response.isSuccess = false;
            response.ErrorMessages.Add("Username already exists");
            return BadRequest(response);
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            PasswordHash = _passwordHasher.HashPassword(null, model.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        response.StatusCode = HttpStatusCode.OK;
        response.isSuccess = true;
        response.Result = new { message = "User registered successfully" };
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var response = new APIResponse();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (user == null || _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, model.Password) == PasswordVerificationResult.Failed)
        {
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.isSuccess = false;
            response.ErrorMessages.Add("Invalid username or password");
            return Unauthorized(response);
        }

        var token = _jwtService.GenerateToken(user);

        response.StatusCode = HttpStatusCode.OK;
        response.isSuccess = true;
        response.Result = new
        {
            token,
            user = new { user.Id, user.Username, user.Email }
        };
        return Ok(response);
    }
}
