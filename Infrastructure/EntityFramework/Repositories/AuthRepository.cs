using Application.Interfaces.IRepositories;
using Application.Entities.Base;
using FusionNews_API.Data;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserDbContext _context;

        public AuthRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> IsUsernameTakenAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}



