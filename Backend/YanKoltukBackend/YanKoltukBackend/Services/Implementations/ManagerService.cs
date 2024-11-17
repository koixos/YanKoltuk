using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ManagerService(IRepository<User> userRepo, IRepository<Manager> managerRepo, IRepository<Service> serviceRepo, UserHelper userHelper) : IManagerService
    {
        private readonly IRepository<User> _userRepo = userRepo;
        private readonly IRepository<Manager> _managerRepo = managerRepo;
        private readonly IRepository<Service> _serviceRepo = serviceRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<int>> GetManagerIdAsync()
        {
            var userId = _userHelper.GetUserId();
            var managers = await _managerRepo.GetAllAsync();
            var manager = managers.FirstOrDefault(m => m.UserId == userId);
            if (manager == null)
            {
                return ServiceResult<int>.ErrorResult("Manager not found");
            }
            return ServiceResult<int>.SuccessResult(manager.ManagerId);
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return (await _serviceRepo.GetAllAsync()).ToList();
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceRepo.GetByIdAsync(serviceId);
        }

        public async Task<ServiceResult<Service>> AddServiceAsync(ServiceDto serviceDto, int managerId)
        {
            try
            {
                var passwd = AuthHelper.GeneratePasswd();
                var user = _userHelper.CreateUser(serviceDto.Plate, passwd, Roles.Service.GetDescription());
                await _userRepo.AddAsync(user);

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

                var manager = await _managerRepo.GetByIdAsync(managerId);
                if (manager == null)
                {
                    return ServiceResult<Service>.ErrorResult("Manager not found");
                }

                manager.Services.Add(service);
                await _managerRepo.UpdateAsync(manager);

                return ServiceResult<Service>.SuccessResult(service, "Service added with password:\n\t" + passwd);
            }
            catch (Exception ex)
            {
                return ServiceResult<Service>.ErrorResult("Error: " + ex.InnerException.Message);
            }
        }

        public async Task<ServiceResult<Service>> UpdateServiceAsync(UpdateServiceDto updateServiceDto, Service service)
        {
            try
            {
                service.DriverIdNo = updateServiceDto.DriverIdNo;
                service.DriverName = updateServiceDto.DriverName;
                service.DriverPhone = updateServiceDto.DriverPhone;
                service.DriverPhoto = updateServiceDto.DriverPhoto;

                service.StewardessIdNo = updateServiceDto.StewardessIdNo;
                service.StewardessName = updateServiceDto.StewardessName;
                service.StewardessPhone = updateServiceDto.StewardessPhone;
                service.StewardessPhoto = updateServiceDto.StewardessPhoto;

                await _serviceRepo.UpdateAsync(service);
                return ServiceResult<Service>.SuccessResult(service, "Service updated");
            }
            catch (Exception ex)
            {
                return ServiceResult<Service>.ErrorResult("Error updating service: " + ex.Message);
            }
        }

        public async Task<ServiceResult<Service>> DeleteServiceAsync(int serviceId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            var user = await _userRepo.GetByIdAsync(service.UserId);

            if (service == null)
            {
                return ServiceResult<Service>.ErrorResult("Service not found");
            }

            await _serviceRepo.DeleteAsync(service);
            await _userRepo.DeleteAsync(user);

            return ServiceResult<Service>.SuccessResult(service);
        }
    }
}
