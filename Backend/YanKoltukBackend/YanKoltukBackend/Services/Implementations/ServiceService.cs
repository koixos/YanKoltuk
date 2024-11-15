using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ServiceService(YanKoltukDbContext context, IRepository<Service> serviceRepo, UserHelper userHelper) : IServiceService
    {
        private readonly YanKoltukDbContext _context = context;
        private readonly IRepository<Service> _serviceRepo = serviceRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<Service>> AddServiceAsync(ServiceDto serviceDto, int managerId)
        {
            try
            {
                var passwd = AuthHelper.GeneratePasswd();
                var user = _userHelper.CreateUser(serviceDto.Plate, passwd, Roles.Service.GetDescription());
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var service = new Service
                {
                    User = user,
                    Plate = serviceDto.Plate,
                    Capacity = serviceDto.Capacity,
                    DepartureLocation = serviceDto.DepartureLocation,
                    DepartureTime = serviceDto.DepartureTime,
                    ManagerId = managerId,
                    DriverIdNo = serviceDto.DriverIdNo,
                    DriverName = serviceDto.DriverName,
                    DriverPhone = serviceDto.DriverPhone,
                    DriverPhoto = serviceDto.DriverPhoto,
                    StewardessIdNo = serviceDto.StewardessIdNo,
                    StewardessName = serviceDto.StewardessName,
                    StewardessPhone = serviceDto.StewardessPhone,
                    StewardessPhoto = serviceDto.StewardessPhoto
                };

                await _serviceRepo.AddAsync(service);
                return ServiceResult<Service>.SuccessResult(service, "Service added with password:\n\t" + passwd);
            }
            catch (Exception ex)
            {
                return ServiceResult<Service>.ErrorResult("Error: " + ex.Message);
            }
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return (await _serviceRepo.GetAllAsync()).ToList();
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceRepo.GetByIdAsync(serviceId);
        }

        public async Task<ServiceResult<Service>> DeleteServiceAsync(int serviceId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            if (service == null) return ServiceResult<Service>.ErrorResult("Not found");
            await _serviceRepo.Delete(service);
            return ServiceResult<Service>.SuccessResult(service);
        }

        public async Task<List<ServiceLog>> GetServiceLogsExcelAsync(int serviceId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            return [.. service.ServiceLogs];
        }

        public async Task<ServiceResult<Service>> UpdateServiceAsync(Service service)
        {
            try
            {
                await _serviceRepo.Update(service);
                return ServiceResult<Service>.SuccessResult(service, "Service updated");
            }
            catch (Exception ex)
            {
                return ServiceResult<Service>.ErrorResult("Error updating service: " + ex.Message);
            }
        }
    }
}
