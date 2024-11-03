using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class AdminService(IRepository<Manager> managerRepo) : IAdminService
    {
        private readonly IRepository<Manager> _managerRepo = managerRepo;

        public async Task<List<Manager>> GetAllManagersAsync()
        {
            return (await _managerRepo.GetAllAsync()).ToList();
        }

        public async Task<ServiceResult<Manager>> AddManagerAsync(ManagerDto managerDto)
        {
            try
            {
                var passwd = PasswdHelper.GeneratePasswd();
                var salt = PasswdHelper.CreateSalt();
                var hashedPasswd = PasswdHelper.HashPasswd(passwd, salt);

                var manager = new Manager
                {
                    Username = managerDto.Username,
                    PasswordHash = hashedPasswd,
                    PasswordSalt = salt,
                    Role = "Manager"
                };

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
