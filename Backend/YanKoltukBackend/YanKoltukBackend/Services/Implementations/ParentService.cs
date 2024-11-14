using DocumentFormat.OpenXml.InkML;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ParentService(YanKoltukDbContext context) : IParentService
    {
        private readonly YanKoltukDbContext _context = context;

        public async Task<ServiceResult<Parent>> CreateParentAsync(ParentSignupDto parentSignupDto)
        {
            var salt = PasswdHelper.CreateSalt();
            var hashedPasswd = PasswdHelper.HashPasswd(parentSignupDto.Password, salt);

            var parent = new Parent
            {
                Username = parentSignupDto.Phone,
                PasswordHash = hashedPasswd,
                PasswordSalt = salt,
                Name = parentSignupDto.Name,
                IdNo = parentSignupDto.IdNo,
                Phone = parentSignupDto.Phone,
                Address = parentSignupDto.Address,
                Role = "Parent"
            };

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return ServiceResult<Parent>.SuccessResult(parent, "Parent created");
        }
    }
}
