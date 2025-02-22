﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Hubs;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;

namespace YanKoltukBackend.Services.Implementations
{
    public class StudentServiceService(IRepository<Service> serviceRepo, IRepository<Parent> parentRepo, IRepository<Student> studentRepo, IRepository<StudentService> studentServiceRepo, IRepository<ServiceLog> serviceLogRepo, IRepository<ParentNotification> parentNotificationRepo, IHubContext<NotificationHub> hubContext) : IStudentServiceService
    {
        private readonly IRepository<Service> _serviceRepo = serviceRepo;
        private readonly IRepository<Parent> _parentRepo = parentRepo;
        private readonly IRepository<Student> _studentRepo = studentRepo;
        private readonly IRepository<StudentService> _studentServiceRepo = studentServiceRepo;
        private readonly IRepository<ServiceLog> _serviceLogRepo = serviceLogRepo;
        private readonly IRepository<ParentNotification> _parentNotificationRepo = parentNotificationRepo;
        private readonly IHubContext<NotificationHub> _hubContext = hubContext;

        public async Task<ServiceResult<IEnumerable<string?>>> GetAllServicePlatesAsync()
        {
            var services = await _serviceRepo.GetAllAsync();
            if (services == null || !services.Any())
                return ServiceResult<IEnumerable<string?>>.ErrorResult("No services found.");
            var plates = services.Select(s => s.Plate).ToList();
            return ServiceResult<IEnumerable<string?>>.SuccessResult(plates);
        }

        public async Task<ServiceResult<IEnumerable<SendManagerStudentDto>>> GetServiceStudentsAsync(int serviceId)
        {
            var students = await _studentServiceRepo.FindAsync(
                ss => ss.ServiceId == serviceId,
                include: ss => ss.Include(x => x.Student));

            var studentDtos = students.Select(ss => new SendManagerStudentDto
            {
                StudentId = ss.StudentId,
                IdNo = ss.Student.IdNo,
                Name = ss.Student.Name,
                SchoolNo = ss.Student.SchoolNo,
                ParentName = ss.Student.Parent.Name,
                ParentPhone = ss.Student.Parent.Phone,
                Address = ss.Student.Parent.Address,
            });

            return ServiceResult<IEnumerable<SendManagerStudentDto>>.SuccessResult(studentDtos);
        }

        public async Task<ServiceResult<IEnumerable<SendParentStudentDto>>> GetStudentsAsync(int parentId)
        {
            var students = await _studentRepo.FindAsync(
                s => s.ParentId == parentId,
                include: s => s.Include(x => x.StudentService)
                                .ThenInclude(ss => ss.Service));

            var studentDtos = students.Select(s => new SendParentStudentDto
            {
                StudentId = s.StudentId,
                IdNo = s.IdNo,
                Name = s.Name,
                SchoolNo = s.SchoolNo,
                Plate = s.StudentService.Service?.Plate,
                Status = s.StudentService.Status.GetDescription(),
                ExcludedStartDate = s.StudentService.ExcludedStartDate,
                ExcludedEndDate = s.StudentService.ExcludedEndDate,
            });

            return ServiceResult<IEnumerable<SendParentStudentDto>>.SuccessResult(studentDtos);
        }

        public async Task<ServiceResult<int>> GetServiceIdByPlateAsync(string plate)
        {
            var service = (await _serviceRepo.FindAsync(s => s.Plate == plate)).FirstOrDefault();
            if (service == null)
                return ServiceResult<int>.ErrorResult("Service not found");
            return ServiceResult<int>.SuccessResult(service.ServiceId);
        }

        public async Task<ServiceResult<string>> GetServicePlateByIdAsync(int serviceId)
        {
            var service = (await _serviceRepo.FindAsync(s => s.ServiceId == serviceId)).FirstOrDefault();
            if (service == null)
                return ServiceResult<string>.ErrorResult("Service not found");
            return ServiceResult<string>.SuccessResult(service.Plate);
        }

        public async Task<ServiceResult<IEnumerable<SendServiceStudentDto>>> GetAllStudentsAsync(int serviceId)
        {
            var studentServices = await _studentServiceRepo.FindAsync(
                ss => ss.ServiceId == serviceId,
                include: ss => ss.Include(x => x.Student)
                                .ThenInclude(s => s.Parent)
                                .Include(x => x.Service));

            var studentServiceDtos = studentServices.Select(ss => new SendServiceStudentDto
            {
                StudentId = ss.Student.StudentId,
                IdNo = ss.Student.IdNo,
                Name = ss.Student.Name,
                SchoolNo = ss.Student.SchoolNo,
                ParentName = ss.Student.Parent.Name,
                ParentPhone = ss.Student.Parent.Phone,
                Address = ss.Student.Parent.Address,
                Plate = ss.Service.Plate,
                Status = ss.Status.GetDescription(),
                DriverNote = ss.DriverNote,
                SortIndex = ss.SortIndex,
                Latitude = ss.Latitude,
                Longitude = ss.Longitude,
                Direction = ss.Direction.GetDescription(),
                ExcludedStartDate = ss.ExcludedStartDate,
                ExcludedEndDate = ss.ExcludedEndDate,
            });

            return ServiceResult<IEnumerable<SendServiceStudentDto>>.SuccessResult(studentServiceDtos);
        }

        public async Task<ServiceResult<IEnumerable<SendServiceStudentDto>>> GetDrivingListAsync(int serviceId)
        {
            var today = DateTime.Today;

            var studentServices = await _studentServiceRepo.FindAsync(
                ss => ss.ServiceId == serviceId &&
                        !(ss.ExcludedStartDate != null && ss.ExcludedEndDate != null &&
                        ss.ExcludedStartDate <= today && ss.ExcludedEndDate >= today),
                include: ss => ss.Include(x => x.Student)
                                .ThenInclude(s => s.Parent)
                                .Include(x => x.Service));

            var studentServiceDtos = studentServices.Select(ss => new SendServiceStudentDto
            {
                StudentId = ss.Student.StudentId,
                IdNo = ss.Student.IdNo,
                Name = ss.Student.Name,
                SchoolNo = ss.Student.SchoolNo,
                ParentName = ss.Student.Parent.Name,
                ParentPhone = ss.Student.Parent.Phone,
                Address = ss.Student.Parent.Address,
                Plate = ss.Service.Plate,
                Status = ss.Status.GetDescription(),
                DriverNote = ss.DriverNote,
                SortIndex = ss.SortIndex,
                Latitude = ss.Latitude,
                Longitude = ss.Longitude,
                Direction = ss.Direction.GetDescription(),
                ExcludedStartDate = ss.ExcludedStartDate,
                ExcludedEndDate = ss.ExcludedEndDate,
            });

            return ServiceResult<IEnumerable<SendServiceStudentDto>>.SuccessResult(studentServiceDtos);
        }

        public async Task<Student?> GetStudentByIdAsync(int serviceId, int studentId)
        {
            return (await _studentRepo.FindAsync(s => (s.StudentService.ServiceId == serviceId) && (s.StudentId == studentId))).FirstOrDefault();
        }

        public async Task<ServiceResult<List<DateTime>>?> GetExcludedDates(int studentId)
        {
            var studentService = (await _studentServiceRepo.FindAsync(
                ss => ss.StudentId == studentId)).FirstOrDefault();
            List <DateTime> excludedDays = [];
            if (studentService.ExcludedStartDate == null || studentService.ExcludedEndDate == null)
                return null;
            DateTime day = studentService.ExcludedStartDate.Value;
            while (day <= studentService.ExcludedEndDate)
            {
                excludedDays.Add(day);
                day = day.AddDays(1);
            }
            return ServiceResult<List<DateTime>>.SuccessResult(excludedDays);
        }

        public async Task<ServiceResult<StudentService>> CreateStudentServiceAsync(Student student, int serviceId, double latitude, double longitude)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            if (service == null)
                return ServiceResult<StudentService>.ErrorResult("Error: Service not found");

            var studentService = new StudentService
            {
                StudentId = student.StudentId,
                Student = student,
                ServiceId = serviceId,
                Service = service,
                Latitude = latitude,
                Longitude = longitude
            };
            await _studentServiceRepo.AddAsync(studentService);

            service.StudentServices.Add(studentService);
            await _serviceRepo.UpdateAsync(service);

            student.StudentService = studentService;
            await _studentRepo.UpdateAsync(student);

            return ServiceResult<StudentService>.SuccessResult(studentService, "StudentService added");
        }

        public async Task<ServiceResult<StudentService>> UpdateStudentServiceAsync(int serviceId, int studentId)
        {
            try
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: Student not found");

                var service = await _serviceRepo.GetByIdAsync(serviceId);
                if (service == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: Service not found");

                var studentService = (await _studentServiceRepo.FindAsync(ss => ss.StudentId == studentId)).FirstOrDefault();
                if (studentService == null)
                    return ServiceResult<StudentService>.ErrorResult("Error: StudentService not found");

                studentService.ServiceId = serviceId;
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

                /*var parentId = studentService.Student.ParentId;
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
                }*/

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

                /*var serviceId = studentService.ServiceId;
                if (serviceId == null)
                    if (studentService == null)
                        return ServiceResult<StudentService>.ErrorResult("Error: Service not found");

                var serviceUserId = (await _serviceRepo.GetByIdAsync(serviceId)).UserId.ToString();
                if (serviceUserId != null)
                {

                    var student = await _studentRepo.GetByIdAsync(studentId);
                    if (student == null)
                        return ServiceResult<StudentService>.ErrorResult("Error: Student not found");

                    var notification = new ParentNotification
                    {
                        ParentId = student.ParentId,
                        ServiceId = serviceId,
                        Date = DateTime.UtcNow,
                    };

                    if (studentService.ExcludedStartDate == studentService.ExcludedEndDate)
                    {
                        var message = $"{studentService.SortIndex} numaralı {student.Name} {studentService.ExcludedStartDate:dd.MM.yyyy} tarihinde izinlidir.";
                        notification.Message = message;
                        await _hubContext.Clients.User(serviceUserId).SendAsync("ReceiveNotification", message);
                    } else
                    {
                        var message = $"{studentService.SortIndex} numaralı {student.Name} {studentService.ExcludedStartDate:dd.MM.yyyy} - {studentService.ExcludedEndDate:dd.MM.yyyy} tarihleri aralığında izinlidir.";
                        notification.Message = message;
                        await _hubContext.Clients.User(serviceUserId).SendAsync("ReceiveNotification", message);
                    }

                    await _parentNotificationRepo.AddAsync(notification);
                }*/

                return ServiceResult<StudentService>.SuccessResult(studentService, "Attendancy updated");
            }
            catch (Exception ex)
            {
                return ServiceResult<StudentService>.ErrorResult("Error: StudentService not updated - " + ex.Message);
            }
        }

        public async Task<ServiceResult<Student>> DeleteStudentAsync(int parentId, int studentId)
        {
            var parent = await _parentRepo.GetByIdAsync(parentId);
            if (parent == null)
                return ServiceResult<Student>.ErrorResult("Error: Parent not found");

            var student = (await _studentRepo.FindAsync(
                s => s.StudentId == studentId && s.ParentId == parentId,
                include: s => s.Include(x => x.StudentService)
                                .ThenInclude(ss => ss.ServiceLogs))
            ).FirstOrDefault();

            if (student == null)
                return ServiceResult<Student>.ErrorResult("Error: Student not found");

            if (student.StudentService != null)
            {
                student.StudentService.ServiceLogs.Clear();
                await _studentServiceRepo.DeleteAsync(student.StudentService); 
            }

            await _studentRepo.DeleteAsync(student);
            return ServiceResult<Student>.SuccessResult(student);
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

        public async Task<ServiceResult<byte[]>> GenerateServiceLogs(int serviceId)
        {
            var service = await _serviceRepo.GetByIdAsync(serviceId);
            var studentServices = (await _studentServiceRepo.FindAsync(ss => ss.ServiceId == serviceId)).ToList();
            
            var serviceLogs = new List<ServiceLog>();

            foreach (var studentService in studentServices)
            {
                var logs = await _serviceLogRepo.FindAsync(
                    sl => sl.StudentServiceId == studentService.StudentServiceId,
                    include: sl => sl.Include(x => x.StudentService)
                                     .ThenInclude(ss => ss.Student));
                serviceLogs.AddRange(logs);
            }

            var groupedLogs = serviceLogs.GroupBy(log => log.Date.Value.Date);

            using var package = new ExcelPackage();

            foreach (var group in groupedLogs)
            {
                var date = group.Key.ToString("dd-MM-yyyy");
                var toSchoolLogs = group.Where(log => log.Direction == "Okula Gidiş").ToList();
                var fromSchoolLogs = group.Where(log => log.Direction == "Okuldan Dönüş").ToList();

                var toSchoolSheet = package.Workbook.Worksheets.Add($"Okula Gidiş - {date}");
                var fromSchoolSheet = package.Workbook.Worksheets.Add($"Okuldan Dönüş - {date}");

                PopulateSheet(toSchoolSheet, toSchoolLogs);
                PopulateSheet(fromSchoolSheet, fromSchoolLogs);

                toSchoolSheet.Cells["A1"].Value = $"Servis Plaka: {service.Plate}, Yön: Okula Gidiş, Tarih: {date}";
                fromSchoolSheet.Cells["A1"].Value = $"Servis Plaka: {service.Plate}, Yön: Okuldan Dönüş, Tarih: {date}";
            }

            var fileContent = package.GetAsByteArray();
            return ServiceResult<byte[]>.SuccessResult(fileContent);
        }

        private static void PopulateSheet(ExcelWorksheet sheet, List<ServiceLog> logs)
        {
            sheet.Cells["A2"].Value = "Ad/Soyad";
            sheet.Cells["B2"].Value = "Okul No";
            sheet.Cells["C2"].Value = "Servise Bindi";
            sheet.Cells["D2"].Value = "Servisten İndi";

            int row = 3;
            foreach (var log in logs)
            {
                sheet.Cells[row, 1].Value = log.StudentService?.Student?.Name ?? "N/A";
                sheet.Cells[row, 2].Value = log.StudentService?.Student?.SchoolNo ?? "N/A";
                sheet.Cells[row, 3].Value = log.PickupTime.HasValue ? log.PickupTime.Value.ToString(@"hh\:mm\:ss") : string.Empty;
                sheet.Cells[row, 4].Value = log.DropOffTime.ToString(@"hh\:mm\:ss");
                row++;
            }
        }
    }
}
