using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ServiceService(IRepository<Service> serviceRepo) : IServiceService
    {
        private readonly IRepository<Service> _serviceRepo = serviceRepo;

        public async Task<ServiceResult<Service>> AddServiceAsync(ServiceDto serviceDto)
        {
            try
            {
                var passwd = PasswdHelper.GeneratePasswd();
                var salt = PasswdHelper.CreateSalt();
                var hashedPasswd = PasswdHelper.HashPasswd(passwd, salt);

                var service = new Service
                {
                    Plate = serviceDto.Plate,
                    Capacity = serviceDto.Capacity,
                    DepartureLocation = serviceDto.DepartureLocation,
                    DepartureTime = serviceDto.DepartureTime,
                    DriverId = serviceDto.DriverId,
                    StewardessId = serviceDto.StewardessId,
                    Username = serviceDto.Plate,
                    PasswordHash = hashedPasswd,
                    PasswordSalt = salt,
                    Role = "Service"
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

        public bool VerifyServiceLogin(string enteredPasswd, Service service)
        {
            return PasswdHelper.VerifyPasswd(enteredPasswd, service.PasswordSalt, service.PasswordHash);
        }
    }
}
