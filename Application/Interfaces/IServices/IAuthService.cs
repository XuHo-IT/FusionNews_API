using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Reponse;
using Application.Entities.Base;


namespace Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<APIResponse> RegisterAsync(RegisterModel model);
        Task<APIResponse> LoginAsync(LoginModel model);
    }
}
