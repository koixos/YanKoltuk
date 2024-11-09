using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController(YanKoltukDbContext context) : ControllerBase
    {
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
