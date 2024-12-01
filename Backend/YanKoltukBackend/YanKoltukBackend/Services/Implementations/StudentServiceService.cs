using Microsoft.AspNetCore.SignalR;
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

                return ServiceResult<StudentService>.SuccessResult(studentService);
            
            } catch (Exception ex)
            {
                return ServiceResult<StudentService>.ErrorResult("Error: Student Status not updated - " + ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateStudentOrderAsync(List<StudentOrderDto> studentOrders)
        {

        }

        public async Task<ServiceResult<StudentService>> SetExcludedDatesAsync(ExcludedDateDto excludedDateDto, int studentId)
        {
            try
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: Student not found");

                var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentId)).FirstOrDefault();
                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                studentService.ExcludedStartDate = excludedDateDto.StartDate;
                studentService.ExcludedEndDate = excludedDateDto.EndDate;

                await _studentServiceRepo.UpdateAsync(studentService);

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
