using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ServiceService(IRepository<Service> serviceRepo, UserHelper userHelper) : IServiceService
    {
        private readonly IRepository<Service> _serviceRepo = serviceRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<int>> GetServiceIdAsync()
        {
            var userId = _userHelper.GetUserId();
            var services = await _serviceRepo.GetAllAsync();
            var service = services.FirstOrDefault(s => s.UserId == userId);
            if (service == null)
                return ServiceResult<int>.ErrorResult("Service not found");
            return ServiceResult<int>.SuccessResult(service.ServiceId, "Service found");
        }

        public async Task<ServiceResult<Service>> GetServiceByIdAsync(int serviceId)
        {
            var services = await _serviceRepo.GetAllAsync();
            var service = services.FirstOrDefault(s => s.ServiceId == serviceId);
            if (service == null)
                return ServiceResult<Service>.ErrorResult("Error: Service not found");
            return ServiceResult<Service>.SuccessResult(service);
        }
    }
}
