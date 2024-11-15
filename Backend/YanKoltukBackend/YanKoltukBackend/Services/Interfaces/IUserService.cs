using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> AuthenticateUserAsync(LoginDto loginDto);
    }
}
