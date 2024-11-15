using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class AdminService(YanKoltukDbContext context, IRepository<Manager> managerRepo, UserHelper userHelper) : IAdminService
    {
        private readonly YanKoltukDbContext _context = context;
        private readonly IRepository<Manager> _managerRepo = managerRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<Admin>> CreateAdminAsync()
        {
            var user = _userHelper.CreateUser("admin", "admin", Roles.Admin.GetDescription());
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var admin = new Admin { User = user };
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return ServiceResult<Admin>.SuccessResult(admin, "Admin created w/ username & passwd: admin");
        }

        public async Task<List<Manager>> GetAllManagersAsync()
        {
            return (await _managerRepo.GetAllAsync()).ToList();
        }

        public async Task<ServiceResult<Manager>> AddManagerAsync(ManagerDto managerDto)
        {
            try
            {
                var passwd = AuthHelper.GeneratePasswd();
                var user = _userHelper.CreateUser(managerDto.Username, passwd, Roles.Manager.GetDescription());
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var manager = new Manager { User = user };
                await _managerRepo.AddAsync(manager);

                return ServiceResult<Manager>.SuccessResult(manager, "Manager added with password:\n\t" + passwd);
            }
            catch (Exception ex)
            {
                return ServiceResult<Manager>.ErrorResult("Error: " + ex.Message);
            }
        }
    }
}
