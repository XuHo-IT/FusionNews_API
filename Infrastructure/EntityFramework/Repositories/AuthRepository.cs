using Application.Entities.Base;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EntityFramework.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string rawPassword)
        {
            // Không gán password vào PasswordHash nữa!
            return await _userManager.CreateAsync(user, rawPassword);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user != null;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }

}
