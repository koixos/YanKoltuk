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

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role),
                new("UserId", user.Id.ToString())
            };

            return GenerateJwtToken(claims);
        }

        private string GenerateJwtToken(List<Claim> claims) 
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
