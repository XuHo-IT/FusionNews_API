using Application.Entities.Base;
using Application.Entities.DTOS.User;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Reponse;
using FusionNews_API.Services.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtService _jwtService;

        public AuthService(IAuthRepository authRepo, IPasswordHasher<User> passwordHasher, JwtService jwtService)
        {
            _authRepo = authRepo;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<APIResponse> RegisterAsync(UserRegisterDTO model)
        {
            var response = new APIResponse();

            if (await _authRepo.IsUsernameTakenAsync(model.Username))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add("Username already exists");
                return response;
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = _passwordHasher.HashPassword(null, model.Password)
            };

            await _authRepo.AddUserAsync(user);

            response.StatusCode = HttpStatusCode.OK;
            response.isSuccess = true;
            response.Result = new { message = "User registered successfully" };
            return response;
        }

        public async Task<APIResponse> LoginAsync(UserLoginDTO model)
        {
            var response = new APIResponse();
            var user = await _authRepo.GetUserByUsernameAsync(model.Username);

            if (user == null || _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, model.Password) == PasswordVerificationResult.Failed)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.isSuccess = false;
                response.ErrorMessages.Add("Invalid username or password");
                return response;
            }

            var token = _jwtService.GenerateToken(user);

            response.StatusCode = HttpStatusCode.OK;
            response.isSuccess = true;
            response.Result = new
            {
                token,
                user = new { user.Id, user.Username, user.Email }
            };
            return response;
        }
    }
}
