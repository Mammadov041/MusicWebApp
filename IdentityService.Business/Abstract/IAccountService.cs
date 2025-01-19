using IdentityService.Entities.Dtos;

namespace IdentityService.Business.Abstract
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<bool> RegisterAsync(RegisterDto dto);
    }
}
