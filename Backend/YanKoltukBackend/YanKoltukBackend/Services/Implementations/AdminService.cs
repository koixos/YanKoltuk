using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.SendDTOs;
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

        public async Task<ServiceResult<int>> GetAdminIdAsync()
        {
            var userId = _userHelper.GetUserId();
            var admins = await _adminRepo.GetAllAsync();
            var admin = admins.FirstOrDefault(a => a.UserId == userId);
            if (admin == null)
                return ServiceResult<int>.ErrorResult("Error: Admin not found");
            return ServiceResult<int>.SuccessResult(admin.AdminId);
        }

        public async Task<ServiceResult<Admin>> CreateAdminAsync()
        {
            var user = _userHelper.CreateUser("admin", "admin", Roles.Admin.GetDescription());
            await _userRepo.AddAsync(user);

            var admin = new Admin
            {
                UserId = user.UserId,
                User = user
            };
            await _adminRepo.AddAsync(admin);
            return ServiceResult<Admin>.SuccessResult(admin, "Admin created w/ username & passwd: admin");
        }

        public async Task<ServiceResult<List<SendManagerDto>>> GetAllManagersAsync(int adminId)
        {
            try
            {
                var managers = await _managerRepo.FindAsync(
                    m => m.AdminId == adminId,
                    include: query => query.Include(m => m.User));

                var managerDtos = managers.Select(m => new SendManagerDto
                {
                    ManagerId = m.ManagerId,
                    Username = m.User?.Username ?? "Hata"
                }).ToList();

                return ServiceResult<List<SendManagerDto>>.SuccessResult(managerDtos);
            } catch (Exception ex)
            {
                return ServiceResult<List<SendManagerDto>>.ErrorResult(ex.Message);
            }
        }

        public async Task<ServiceResult<Manager>> AddManagerAsync(ManagerDto managerDto, int adminId)
        {
            try
            {
                var admin = await _adminRepo.GetByIdAsync(adminId);
                if (admin == null)
                    return ServiceResult<Manager>.ErrorResult("Error: Admin not found");

                var passwd = AuthHelper.GeneratePasswd();
                var user = _userHelper.CreateUser(managerDto.Username, passwd, Roles.Manager.GetDescription());

                if (user == null)
                    return ServiceResult<Manager>.ErrorResult("Error: User creation failed");

                await _userRepo.AddAsync(user);

                var manager = new Manager
                {
                    UserId = user.UserId,
                    User = user,
                    AdminId = adminId,
                    Admin = admin
                };
                await _managerRepo.AddAsync(manager);

                admin.Managers.Add(manager);
                await _adminRepo.UpdateAsync(admin);

                return ServiceResult<Manager>.SuccessResult(manager, passwd);
            }
            catch (Exception ex)
            {
                return ServiceResult<Manager>.ErrorResult("Error: " + ex.Message);
            }
        }

        public async Task<ServiceResult<Manager>> DeleteManagerAsync(int managerId)
        {
            var manager = await _managerRepo.GetByIdAsync(managerId);
            if (manager == null)
                return ServiceResult<Manager>.ErrorResult("Error: Manager not found");

            var user = await _userRepo.GetByIdAsync(manager.UserId);

            await _managerRepo.DeleteAsync(manager);
            await _userRepo.DeleteAsync(user);

            return ServiceResult<Manager>.SuccessResult(manager);
        }
    }
}
