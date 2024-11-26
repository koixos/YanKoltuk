using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResult<int>> GetAdminIdAsync();
        Task<ServiceResult<Admin>> CreateAdminAsync();
        Task<List<Manager>> GetAllManagersAsync();
        Task<ServiceResult<Manager>> AddManagerAsync(ManagerDto managerDto, int adminId);
    }
}
