using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.DTOs.UserDTOs;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<SendUserDto> AuthenticateUserAsync(LoginDto loginDto);
    }
}
