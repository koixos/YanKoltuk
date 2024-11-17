using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class AdminService(IRepository<User> userRepo, IRepository<Admin> adminRepo, IRepository<Manager> managerRepo, UserHelper userHelper) : IAdminService
    {
        private readonly IRepository<User> _userRepo = userRepo;
        private readonly IRepository<Admin> _adminRepo = adminRepo;
        private readonly IRepository<Manager> _managerRepo = managerRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<Admin>> CreateAdminAsync()
        {
            var user = _userHelper.CreateUser("admin", "admin", Roles.Admin.GetDescription());
            await _userRepo.AddAsync(user);

            var admin = new Admin { User = user };
            await _adminRepo.AddAsync(admin);
            return ServiceResult<Admin>.SuccessResult(admin, "Admin created w/ username & passwd: admin");
        }

        public async Task<List<Manager>> GetAllManagersAsync()
        {
            return (await _managerRepo.GetAllAsync()).ToList();
        }

        public async Task<ServiceResult<Manager>> AddManagerAsync(ManagerDto managerDto, int adminId)
        {
            try
            {
                var passwd = AuthHelper.GeneratePasswd();
                var user = _userHelper.CreateUser(managerDto.Username, passwd, Roles.Manager.GetDescription());
                await _userRepo.AddAsync(user);

                var manager = new Manager { User = user };
                await _managerRepo.AddAsync(manager);

                var admin = await _adminRepo.GetByIdAsync(adminId);
                if (admin == null)
                {
                    return ServiceResult<Manager>.ErrorResult("Admin not found");
                }

                admin.Managers.Add(manager);
                await _adminRepo.UpdateAsync(admin);

                return ServiceResult<Manager>.SuccessResult(manager, "Manager added with password:\n\t" + passwd);
            }
            catch (Exception ex)
            {
                return ServiceResult<Manager>.ErrorResult("Error: " + ex.Message);
            }
        }

        public int GetAdminId()
        {
            return _userHelper.GetUserId();
        }
    }
}
