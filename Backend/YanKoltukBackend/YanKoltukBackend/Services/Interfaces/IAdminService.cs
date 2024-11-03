using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<Manager>> GetAllManagersAsync();
        Task<ServiceResult<Manager>> AddManagerAsync(ManagerDto managerDto);
    }
}
