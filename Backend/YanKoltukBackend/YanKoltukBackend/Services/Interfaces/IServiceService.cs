using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<Service>> GetAllServicesAsync();
        Task<Service> GetServiceByIdAsync(int serviceId);
        Task<ServiceResult<Service>> AddServiceAsync(ServiceDto serviceDto, int managerId);
        Task<ServiceResult<Service>> UpdateServiceAsync(UpdateServiceDto updateServiceDto, Service service);
        Task<ServiceResult<Service>> DeleteServiceAsync(int serviceId);
        //Task<List<ServiceLog>> GetServiceLogsExcelAsync(int serviceId);
    }
}
