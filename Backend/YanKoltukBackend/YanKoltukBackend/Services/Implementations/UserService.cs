using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class UserService(YanKoltukDbContext context, IConfiguration configuration) : IUserService
    {
        private readonly YanKoltukDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ServiceResult<User>> CreateUserAsync(ParentSignupDto parentSignupDto)
        {
            var salt = PasswdHelper.CreateSalt();
            var hashedPasswd = PasswdHelper.HashPasswd(parentSignupDto.Password, salt);

            var user = new User
            {
                Username = parentSignupDto.Phone,
                PasswordHash = hashedPasswd,
                PasswordSalt = salt,
                Role = "Parent"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ServiceResult<User>.SuccessResult(user, "User created");
        }

        public async Task<string?> AuthenticateUserAsync(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null || !PasswdHelper.VerifyPasswd(loginDto.Password, user.PasswordSalt, user.PasswordHash))
            {
                return null;
            }
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
