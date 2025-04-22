using Application.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> AddUserAsync(User user, string rawPassword);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> IsUsernameTakenAsync(string username);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
