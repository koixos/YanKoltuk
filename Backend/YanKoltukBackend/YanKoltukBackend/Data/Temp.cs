using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController(YanKoltukDbContext context, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly YanKoltukDbContext _context = context;

        public async Task<ServiceResult<Admin>> CreateAdminAsync()
        {
            var salt = PasswdHelper.CreateSalt();
            var hashedPasswd = PasswdHelper.HashPasswd("admin", salt);

            var admin = new Admin
            {
                Username = "admin",
                PasswordHash = hashedPasswd,
                PasswordSalt = salt,
                Role = "Admin"
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return ServiceResult<Admin>.SuccessResult(admin, "Admin created w/ username & passwd: admin");
        }
    }
}
