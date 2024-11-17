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
        //Task<List<ServiceLog>> GetServiceLogsExcelAsync(int serviceId);
    }
}
