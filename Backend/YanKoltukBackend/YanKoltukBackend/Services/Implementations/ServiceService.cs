using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ServiceService(IRepository<User> userRepo, IRepository<Service> serviceRepo, UserHelper userHelper) : IServiceService
    {
        private readonly IRepository<User> _userRepo = userRepo;
        private readonly IRepository<Service> _serviceRepo = serviceRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return (await _serviceRepo.GetAllAsync()).ToList();
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceRepo.GetByIdAsync(serviceId);
        }

        /*public async Task<List<ServiceLog>> GetServiceLogsExcelAsync(int serviceId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            return [.. service.ServiceLogs];
        }*/
    }
}
