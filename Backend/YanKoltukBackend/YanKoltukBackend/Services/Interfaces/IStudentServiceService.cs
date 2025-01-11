using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IStudentServiceService
    {
        Task<ServiceResult<IEnumerable<string?>>> GetAllServicePlatesAsync();
        Task<ServiceResult<IEnumerable<SendParentStudentDto>>> GetStudentsAsync(int parentId);
        Task<ServiceResult<int>> GetServiceIdByPlateAsync(string plate);
        Task<ServiceResult<string>> GetServicePlateByIdAsync(int serviceId);
        Task<ServiceResult<IEnumerable<SendManagerStudentDto>>> GetServiceStudentsAsync(int serviceId);
        Task<ServiceResult<IEnumerable<SendServiceStudentDto>>> GetAllStudentsAsync(int serviceId);
        Task<ServiceResult<IEnumerable<SendServiceStudentDto>>> GetDrivingListAsync(int serviceId);
        Task<Student?> GetStudentByIdAsync(int serviceId, int studentId);
        Task<ServiceResult<List<DateTime>>?> GetExcludedDates(int studentId);
        Task<ServiceResult<StudentService>> CreateStudentServiceAsync(Student student, int serviceId, double latitude, double longitude);
        Task<ServiceResult<StudentService>> UpdateStudentServiceAsync(int serviceId, int studentId);
        Task<ServiceResult<StudentService>> UpdateNoteAsync(string note, int studentId);
        Task<ServiceResult<bool>> UpdateStudentOrderAsync(List<UpdateStudentOrderDto> studentOrders);
        Task<ServiceResult<StudentService>> UpdateStudentStatusAsync(UpdateStudentStatusDto updateStudentStatusDto, int studentId);
        Task<ServiceResult<StudentService>> SetExcludedDatesAsync(ExcludedDateDto excludedDateDto, int studentId);
        Task<ServiceResult<Student>> DeleteStudentAsync(int parentId, int studentId);
        Task<ServiceResult<StudentService>> DeleteStudentServiceAsync(int studentId);
    }
}
