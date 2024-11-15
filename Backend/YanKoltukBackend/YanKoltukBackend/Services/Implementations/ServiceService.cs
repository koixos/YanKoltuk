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
                return ServiceResult<Service>.SuccessResult(service, "Service added with password:\n\t" + passwd);
            }
            catch (Exception ex)
            {
                return ServiceResult<Service>.ErrorResult("Error: " + ex.Message);
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
            if (service == null)
            {
                return ServiceResult<Service>.ErrorResult("Not found");
            }
            await _serviceRepo.DeleteAsync(service);
            return ServiceResult<Service>.SuccessResult(service);
        }

        /*public async Task<List<ServiceLog>> GetServiceLogsExcelAsync(int serviceId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            return [.. service.ServiceLogs];
        }*/
    }
}
