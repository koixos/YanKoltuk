using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ParentService(YanKoltukDbContext context, UserHelper userHelper) : IParentService
    {
        private readonly YanKoltukDbContext _context = context;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<Parent>> CreateParentAsync(ParentSignupDto parentSignupDto)
        {
            var user = _userHelper.CreateUser(parentSignupDto.Phone, parentSignupDto.Password, Roles.Parent.GetDescription());
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var parent = new Parent
            {
                User = user,
                Name = parentSignupDto.Name,
                IdNo = parentSignupDto.IdNo,
                Phone = parentSignupDto.Phone,
                Address = parentSignupDto.Address
            };

            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            return ServiceResult<Parent>.SuccessResult(parent, "Parent created");
        }
    }
}
