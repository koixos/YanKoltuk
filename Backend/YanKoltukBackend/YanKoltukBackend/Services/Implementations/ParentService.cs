using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs.UserDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ParentService(IRepository<User> userRepo, IRepository<Parent> parentRepo, UserHelper userHelper) : IParentService
    {
        private readonly IRepository<User> _userRepo = userRepo;
        private readonly IRepository<Parent> _parentRepo = parentRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<Parent>> CreateParentAsync(ParentSignupDto parentSignupDto)
        {
            var user = _userHelper.CreateUser(parentSignupDto.Phone, parentSignupDto.Password, Roles.Parent.GetDescription());
            await _userRepo.AddAsync(user);

            var parent = new Parent
            {
                User = user,
                Name = parentSignupDto.Name,
                IdNo = parentSignupDto.IdNo,
                Phone = parentSignupDto.Phone,
                Address = parentSignupDto.Address
            };
            await _parentRepo.AddAsync(parent);
            return ServiceResult<Parent>.SuccessResult(parent, "Parent created");
        }
    }
}
