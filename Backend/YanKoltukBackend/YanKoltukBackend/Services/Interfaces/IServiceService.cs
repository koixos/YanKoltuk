using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResult<int>> GetServiceIdAsync();
        Task<ServiceResult<Service>> GetServiceByIdAsync(int serviceId);
    }
}
