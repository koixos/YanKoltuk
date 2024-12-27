using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IManagerService
    {
        Task<ServiceResult<int>> GetManagerIdAsync();
        Task<List<SendServiceDto>> GetAllServicesAsync(int managerId);
        Task<Service?> GetServiceByIdAsync(int managerId, int serviceId);
        Task<ServiceResult<Service>> AddServiceAsync(ServiceDto serviceDto, int managerId);
        Task<ServiceResult<Service>> UpdateServiceAsync(UpdateServiceDto updateServiceDto, int managerId, int serviceId);
        Task<ServiceResult<Service>> DeleteServiceAsync(int managerId, int serviceId);
    }
}
