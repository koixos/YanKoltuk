using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class UserService(YanKoltukDbContext context, AuthHelper authHelper) : IUserService
    {
        private readonly YanKoltukDbContext _context = context;
        private readonly AuthHelper _authHelper = authHelper;

        public async Task<string?> AuthenticateUserAsync(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null || !AuthHelper.VerifyPasswd(loginDto.Password, user.PasswordSalt, user.PasswordHash))
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role),
                new("UserId", user.UserId.ToString())
            };

            return _authHelper.GenerateJwtToken(claims);
        }
    }
}
