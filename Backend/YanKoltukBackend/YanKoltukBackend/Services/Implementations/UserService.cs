using System.Security.Claims;
using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.DTOs.UserDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class UserService(IRepository<User> userRepo, AuthHelper authHelper) : IUserService
    {
        private readonly IRepository<User> _userRepo = userRepo;
        private readonly AuthHelper _authHelper = authHelper;

        public async Task<SendUserDto> AuthenticateUserAsync(LoginDto loginDto)
        {
            ArgumentNullException.ThrowIfNull(loginDto);
            var userList = await _userRepo.FindAsync(u => u.Username == loginDto.Username);
            var user = userList.FirstOrDefault();

            if (user == null || !AuthHelper.VerifyPasswd(loginDto.Password, user.PasswordSalt, user.PasswordHash))
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role)
            };

            var token = _authHelper.GenerateJwtToken(claims);

            return new SendUserDto
            {
                Token = token,
                Role = user.Role
            };
        }
    }
}
