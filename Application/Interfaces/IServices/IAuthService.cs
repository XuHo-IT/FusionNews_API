using Application.Entities.DTOS.User;
using Application.Reponse;


namespace Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<APIResponse> RegisterAsync(UserRegisterDTO model);
        Task<APIResponse> LoginAsync(UserLoginDTO model);
    }
}
