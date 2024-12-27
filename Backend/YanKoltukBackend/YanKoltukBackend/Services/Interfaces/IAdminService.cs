using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResult<int>> GetAdminIdAsync();
        Task<ServiceResult<Admin>> CreateAdminAsync();
        Task<ServiceResult<List<SendManagerDto>>> GetAllManagersAsync(int adminId);
        Task<ServiceResult<Manager>> AddManagerAsync(ManagerDto managerDto, int AdminId);
        Task<ServiceResult<Manager>> DeleteManagerAsync(int managerId);
    }
}
