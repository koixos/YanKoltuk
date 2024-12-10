using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Hubs;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;

namespace YanKoltukBackend.Services.Implementations
{
    public class StudentServiceService(IRepository<Service> serviceRepo, IRepository<Parent> parentRepo, IRepository<Student> studentRepo, IRepository<StudentService> studentServiceRepo, IRepository<ServiceLog> serviceLogRepo, IHubContext<NotificationHub> hubContext) : IStudentServiceService
    {
        private readonly IRepository<Service> _serviceRepo = serviceRepo;
        private readonly IRepository<Parent> _parentRepo = parentRepo;
        private readonly IRepository<Student> _studentRepo = studentRepo;
        private readonly IRepository<StudentService> _studentServiceRepo = studentServiceRepo;
        private readonly IRepository<ServiceLog> _serviceLogRepo = serviceLogRepo;
        private readonly IHubContext<NotificationHub> _hubContext = hubContext;

        public async Task<ServiceResult<IEnumerable<string?>>> GetAllServicePlatesAsync()
        {
            var services = await _serviceRepo.GetAllAsync();
            if (services == null || !services.Any())
                return ServiceResult<IEnumerable<string?>>.ErrorResult("No services found.");
            var plates = services.Select(s => s.Plate).ToList();
            return ServiceResult<IEnumerable<string?>>.SuccessResult(plates);
        }

        public async Task<ServiceResult<IEnumerable<StudentDto>>> GetStudentsAsync(int parentId)
        {
            var students = await _studentRepo.FindAsync(
                s => s.ParentId == parentId,
                include: s => s.Include(x => x.StudentService));

            var studentDtos = students.Select(s => new StudentDto
            {
                IdNo = s.IdNo,
                Name = s.Name,
                SchoolNo = s.SchoolNo,
                Plate = s.StudentService?.Service?.Plate
            });

            return ServiceResult<IEnumerable<StudentDto>>.SuccessResult(studentDtos);
        }

        public async Task<ServiceResult<int>> GetServiceIdByPlateAsync(string plate)
        {
            var service = (await _serviceRepo.FindAsync(s => s.Plate == plate)).FirstOrDefault();
            if (service == null)
                return ServiceResult<int>.ErrorResult("Service not found");
            return ServiceResult<int>.SuccessResult(service.ServiceId);
        }

        public async Task<IEnumerable<Student?>> GetAllStudentsAsync(int serviceId)
        {
            return (await _studentRepo.FindAsync(s => s.StudentService.ServiceId == serviceId)).ToList();
        }

        public async Task<IEnumerable<Student?>> GetDrivingListAsync(int serviceId)
        {
            var today = DateTime.Today;
            return (await _studentRepo.FindAsync(s => (s.StudentService.ServiceId == serviceId) && !((s.StudentService.ExcludedStartDate <= today) && (s.StudentService.ExcludedEndDate >= today)))).ToList();
        }

        public async Task<Student?> GetStudentByIdAsync(int serviceId, int studentId)
        {
            return (await _studentRepo.FindAsync(s => (s.StudentService.ServiceId == serviceId) && (s.StudentId == studentId))).FirstOrDefault();
        }

        public async Task<ServiceResult<StudentService>> CreateStudentServiceAsync(Student student, int serviceId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            if (service == null)
                return ServiceResult<StudentService>.ErrorResult("Error: Service not found");

            var studentService = new StudentService
            {
                StudentId = student.StudentId,
                Student = student,
                ServiceId = serviceId,
                Service = service
            };
            await _studentServiceRepo.AddAsync(studentService);

            service.StudentServices.Add(studentService);
            await _serviceRepo.UpdateAsync(service);

            student.StudentService = studentService;
            await _studentRepo.UpdateAsync(student);

            return ServiceResult<StudentService>.SuccessResult(studentService, "StudentService added");
        }

        public async Task<ServiceResult<StudentService>> UpdateStudentServiceAsync(UpdateStudentServiceDto updateStudentServiceDto, int studentId)
        {
            try
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: Student not found");

                var service = await _serviceRepo.GetByIdAsync(updateStudentServiceDto.ServiceId);
                if (service == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: Service not found");

                var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentId)).FirstOrDefault();
                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                studentService.ServiceId = updateStudentServiceDto.ServiceId;
                await _studentServiceRepo.UpdateAsync(studentService);

                return ServiceResult<StudentService>.SuccessResult(studentService, "StudentService updated");
            }
            catch (Exception ex)
            {
                return ServiceResult<StudentService>.ErrorResult("Error: StudentService not updated - " + ex.Message);
            }
        }

        public async Task<ServiceResult<StudentService>> UpdateNoteAsync(string note, int studentId)
        {
            try
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: Student not found");

                var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentId)).FirstOrDefault();
                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                if (!string.IsNullOrWhiteSpace(note) || note.Length <= 50)
                    studentService.DriverNote = note;
                await _studentServiceRepo.UpdateAsync(studentService);

                return ServiceResult<StudentService>.SuccessResult(studentService);
            }
            catch (Exception ex)
            {
                return ServiceResult<StudentService>.ErrorResult("Error: Note not added - " + ex.Message);
            }
        }

        public async Task<ServiceResult<StudentService>> UpdateStudentStatusAsync(UpdateStudentStatusDto updateStudentStatusDto, int studentId)
        {
            try
            {
                var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentId)).FirstOrDefault();
                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                var _status = updateStudentStatusDto.Status.GetStudentStatus();
                var _direction = updateStudentStatusDto.Direction.GetTripType();

                studentService.Status = _status;
                studentService.Direction = _direction;

                var serviceLog = new ServiceLog
                {
                    StudentServiceId = studentService.StudentServiceId,
                    Date = DateTime.Now.Date,
                    Direction = _direction.GetDescription()
                };

                if (_status == StudentStatus.GetOn)
                    serviceLog.PickupTime = DateTime.Now.TimeOfDay;

                else if (_status == StudentStatus.GetOff)
                    serviceLog.DropOffTime = DateTime.Now.TimeOfDay;

                await _serviceLogRepo.AddAsync(serviceLog);

                studentService.ServiceLogs.Add(serviceLog);
                await _studentServiceRepo.UpdateAsync(studentService);

                var parentId = studentService.Student.ParentId;
                var parentUserId = (await _parentRepo.GetByIdAsync(parentId))?.UserId.ToString();
                if (parentUserId != null)
                    await _hubContext.Clients.User(parentUserId)
                        .SendAsync("ReceiveNotification", $"{studentService.Student.Name} Durum: {_status.GetDescription()} ({_direction.GetDescription()}).");

                if (_status == StudentStatus.GetOn && _direction == TripType.ToSchool)
                {
                    var nextStudentService = (await _studentServiceRepo
                        .FindAsync(ss => ss.ServiceId == studentService.ServiceId && ss.SortIndex > studentService.SortIndex))
                        .OrderBy(ss => ss.SortIndex)
                        .FirstOrDefault();

                    if (nextStudentService != null)
                    {
                        var nextParentId = nextStudentService.Student.ParentId;
                        var nextParentUserId = (await _parentRepo.GetByIdAsync(nextParentId))?.UserId.ToString();
                        if (nextParentUserId != null)
                        {
                            await _hubContext.Clients.User(nextParentUserId)
                                .SendAsync("ReceiveNotification", "Hazırlanın, servis aracı evinize yaklaşmakta.");
                        }
                    }
                }

                return ServiceResult<StudentService>.SuccessResult(studentService);
            
            } catch (Exception ex)
            {
                return ServiceResult<StudentService>.ErrorResult("Error: Student Status not updated - " + ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> UpdateStudentOrderAsync(List<UpdateStudentOrderDto> studentOrdersDto)
        {
            if (studentOrdersDto == null || studentOrdersDto.Count == 0)
                return ServiceResult<bool>.ErrorResult("Error: Student orders list is empty.");

            try
            {
                foreach (var studentOrderDto in studentOrdersDto)
                {
                    var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentOrderDto.StudentId)).FirstOrDefault();
                    if (studentService == null)
                        return ServiceResult<bool>.ErrorResult("Error: Student not found - student id: " + studentOrderDto.StudentId);

                    studentService.SortIndex = studentOrderDto.SortIndex;
                    await _studentServiceRepo.UpdateAsync(studentService);
                }

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.ErrorResult("Error: StudentService not updated - " + ex.Message);
            }
        }

        public async Task<ServiceResult<StudentService>> SetExcludedDatesAsync(ExcludedDateDto excludedDateDto, int studentId)
        {
            try
            {
                var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentId)).FirstOrDefault();
                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                studentService.ExcludedStartDate = excludedDateDto.StartDate;
                studentService.ExcludedEndDate = excludedDateDto.EndDate;

                await _studentServiceRepo.UpdateAsync(studentService);

                var serviceId = studentService.ServiceId;
                var serviceUserId = (await _serviceRepo.GetByIdAsync(serviceId))?.UserId.ToString();
                if (serviceUserId != null)
                {

                    var student = await _studentRepo.GetByIdAsync(studentId);
                    if (student == null)
                        return ServiceResult<StudentService>.ErrorResult("Error: Student not found");

                    if (studentService.ExcludedStartDate == studentService.ExcludedEndDate)
                        await _hubContext.Clients.User(serviceUserId)
                                .SendAsync("ReceiveNotification", $"{studentService.SortIndex} numaralı {student.Name} {studentService.ExcludedStartDate:dd.MM.yyyy} tarihinde izinlidir.");
                    await _hubContext.Clients.User(serviceUserId)
                                .SendAsync("ReceiveNotification", $"{studentService.SortIndex} numaralı {student.Name} {studentService.ExcludedStartDate:dd.MM.yyyy} - {studentService.ExcludedEndDate:dd.MM.yyyy} tarihleri aralığında izinlidir.");
                }

                return ServiceResult<StudentService>.SuccessResult(studentService, "Attendancy updated");
            }
            catch (Exception ex)
            {
                return ServiceResult<StudentService>.ErrorResult("Error: StudentService not updated - " + ex.Message);
            }
        }

        public async Task<ServiceResult<StudentService>> DeleteStudentServiceAsync(int studentId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student == null)
                return ServiceResult<StudentService>.ErrorResult("Error: Student not found");

            var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentId)).FirstOrDefault();
            if (studentService == null)
                return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

            await _studentServiceRepo.DeleteAsync(studentService);
            return ServiceResult<StudentService>.SuccessResult(studentService);
        }
    }
}
