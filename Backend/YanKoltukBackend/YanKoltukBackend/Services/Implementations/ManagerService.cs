using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Data;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly YanKoltukDbContext _context;
        private readonly DbSet<User> _userDbSet;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Manager> _managerRepo;
        private readonly IRepository<Service> _serviceRepo;
        private readonly UserHelper _userHelper;

        public ManagerService(YanKoltukDbContext context, IRepository<User> userRepo, IRepository<Manager> managerRepo, IRepository<Service> serviceRepo, UserHelper userHelper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userDbSet = _context.Set<User>();
            _userRepo = userRepo;
            _managerRepo = managerRepo;
            _serviceRepo = serviceRepo;
            _userHelper = userHelper;
        }

        public async Task<ServiceResult<int>> GetManagerIdAsync()
        {
            var userId = _userHelper.GetUserId();
            var managers = await _managerRepo.GetAllAsync();
            var manager = managers.FirstOrDefault(m => m.UserId == userId);
            if (manager == null)
                return ServiceResult<int>.ErrorResult("Error: Manager not found");
            return ServiceResult<int>.SuccessResult(manager.ManagerId);
        }

        public async Task<List<SendServiceDto>> GetAllServicesAsync(int managerId)
        {
            var services = await _serviceRepo.FindAsync(s => s.ManagerId == managerId);

            return services.Select(s => new SendServiceDto
            {
                ServiceId = s.ServiceId,
                Plate = s.Plate,
                Capacity = s.Capacity,
                DepartureLocation = s.DepartureLocation,
                DepartureTime = s.DepartureTime,
                DriverIdNo = s.DriverIdNo,
                DriverName = s.DriverName,
                DriverPhone = s.DriverPhone,
                StewardessIdNo = s.StewardessIdNo,
                StewardessName = s.StewardessName,
                StewardessPhone = s.StewardessPhone
            }).ToList();
        }

        public async Task<Service?> GetServiceByIdAsync(int managerId, int serviceId)
        {
            return (await _serviceRepo.FindAsync(s => (s.ManagerId == managerId) && (s.ServiceId == serviceId))).FirstOrDefault();
        }

        public async Task<ServiceResult<Service>> AddServiceAsync(ServiceDto serviceDto, int managerId)
        {
            try
            {
                var manager = await _managerRepo.GetByIdAsync(managerId);
                if (manager == null)
                    return ServiceResult<Service>.ErrorResult("Error: Manager not found");

                var passwd = AuthHelper.GeneratePasswd();
                var user = _userHelper.CreateUser(serviceDto.Plate, passwd, Roles.Service.GetDescription());
                await _userDbSet.AddAsync(user);

                var service = new Service
                {
                    User = user,
                    Plate = serviceDto.Plate,
                    Capacity = serviceDto.Capacity,
                    DepartureLocation = serviceDto.DepartureLocation,
                    DepartureTime = serviceDto.DepartureTime,
                    ManagerId = managerId,
                    Manager = manager,
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
                await _context.SaveChangesAsync();

                manager.Services.Add(service);
                await _managerRepo.UpdateAsync(manager);

                return ServiceResult<Service>.SuccessResult(service, passwd);
            }
            catch (Exception ex)
            {
                return ServiceResult<Service>.ErrorResult("Error: " + ex.InnerException?.Message);
            }
        }

        public async Task<ServiceResult<Service>> UpdateServiceAsync(UpdateServiceDto updateServiceDto, int managerId, int serviceId)
        {
            try
            {
                var manager = await _managerRepo.GetByIdAsync(managerId);
                if (manager == null)
                    return ServiceResult<Service>.ErrorResult("Error: Manager not found");

                var service = await GetServiceByIdAsync(managerId, serviceId);
                if (service == null)
                    return ServiceResult<Service>.ErrorResult("Error: Service not found");

                if (!string.IsNullOrWhiteSpace(updateServiceDto.DriverIdNo))
                    service.DriverIdNo = updateServiceDto.DriverIdNo;
                if (!string.IsNullOrWhiteSpace(updateServiceDto.DriverName))
                    service.DriverName = updateServiceDto.DriverName;
                if (!string.IsNullOrWhiteSpace(updateServiceDto.DriverPhone))
                    service.DriverPhone = updateServiceDto.DriverPhone;
                if (!string.IsNullOrWhiteSpace(updateServiceDto.DriverPhoto))
                    service.DriverPhoto = updateServiceDto.DriverPhoto;

                if (!string.IsNullOrWhiteSpace(updateServiceDto.StewardessIdNo))
                    service.StewardessIdNo = updateServiceDto.StewardessIdNo;
                if (!string.IsNullOrWhiteSpace(updateServiceDto.StewardessName))
                    service.StewardessName = updateServiceDto.StewardessName;
                if (!string.IsNullOrWhiteSpace(updateServiceDto.StewardessPhone))
                    service.StewardessPhone = updateServiceDto.StewardessPhone;
                if (!string.IsNullOrWhiteSpace(updateServiceDto.StewardessPhoto))
                    service.StewardessPhoto = updateServiceDto.StewardessPhoto;

                await _serviceRepo.UpdateAsync(service);
                return ServiceResult<Service>.SuccessResult(service, "Service updated");
            }
            catch (Exception ex)
            {
                return ServiceResult<Service>.ErrorResult("Error: Service not updated - " + ex.InnerException?.Message);
            }
        }

        public async Task<ServiceResult<Service>> DeleteServiceAsync(int managerId, int serviceId)
        {
            var manager = await _managerRepo.GetByIdAsync(managerId);
            if (manager == null)
                return ServiceResult<Service>.ErrorResult("Error: Manager not found");

            var service = await GetServiceByIdAsync(managerId, serviceId);
            if (service == null)
                return ServiceResult<Service>.ErrorResult("Error: Service not found");

            var user = await _userRepo.GetByIdAsync(service.UserId);

            await _serviceRepo.DeleteAsync(service);
            if (user != null)
                await _userRepo.DeleteAsync(user);

            return ServiceResult<Service>.SuccessResult(service);
        }
    }
}
