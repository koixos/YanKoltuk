using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResult<Service>> AddServiceAsync(ServiceDto serviceDto);
        Task<List<Service>> GetAllServicesAsync();
        Task<Service> GetServiceByIdAsync(int serviceId);
        Task<ServiceResult<Service>> DeleteServiceAsync(int serviceId);
        Task<ServiceResult<Service>> UpdateServiceAsync(Service service);
        Task<List<ServiceLog>> GetServiceLogsExcelAsync(int serviceId);
        bool VerifyServiceLogin(string enteredPasswd, Service service);
    }
}
