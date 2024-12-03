using YanKoltukBackend.Models.DTOs.UserDTOs;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> AuthenticateUserAsync(LoginDto loginDto);
    }
}
