using YanKoltukBackend.Application.Results;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResult<int>> GetServiceIdAsync();
    }
}
