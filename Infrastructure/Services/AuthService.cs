﻿using Application.Entities.Base;
using Application.Entities.DTOS.User;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Reponse;
using FusionNews_API.Services.Jwt;
using System.Net;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly JwtService _jwtService;

        public AuthService(IAuthRepository authRepo, JwtService jwtService)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
        }

        public async Task<APIResponse> RegisterAsync(UserRegisterDTO model)
        {
            var response = new APIResponse();

            // Kiểm tra định dạng email trước
            if (string.IsNullOrWhiteSpace(model.Email) || !model.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add("Email must be a valid Gmail address (end with @gmail.com)");
                return response;
            }

            if (await _authRepo.IsUsernameTakenAsync(model.Username))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages.Add("Username already exists");
                return response;
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _authRepo.AddUserAsync(user, model.Password);

            if (!result.Succeeded)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErrorMessages = result.Errors.Select(e => e.Description).ToList();
                return response;
            }

            response.StatusCode = HttpStatusCode.OK;
            response.isSuccess = true;
            response.Result = new { message = "User registered successfully" };
            return response;
        }


        public async Task<APIResponse> LoginAsync(UserLoginDTO model)
        {
            var response = new APIResponse();
            var user = await _authRepo.GetUserByUsernameAsync(model.Username);

            if (user == null || !await _authRepo.CheckPasswordAsync(user, model.Password))
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.isSuccess = false;
                response.ErrorMessages.Add("Invalid username or password");
                return response;
            }

            var token = _jwtService.GenerateToken(user);

            response.StatusCode = HttpStatusCode.OK;
            response.isSuccess = true;
            response.Result = new AuthResponse { Token = token, UserId = user.Id, Username = user.UserName, Email = user.Email };
            return response;
        }
    }
}
