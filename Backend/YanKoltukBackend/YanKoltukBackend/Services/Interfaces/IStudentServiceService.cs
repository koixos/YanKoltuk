using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IStudentServiceService
    {
        Task<ServiceResult<IEnumerable<string?>>> GetAllServicePlatesAsync();
        Task<ServiceResult<IEnumerable<StudentDto>>> GetStudentsAsync(int parentId);
        Task<ServiceResult<int>> GetServiceIdByPlateAsync(string plate);
        Task<IEnumerable<Student?>> GetAllStudentsAsync(int serviceId);
        Task<IEnumerable<Student?>> GetDrivingListAsync(int serviceId);
        Task<Student?> GetStudentByIdAsync(int serviceId, int studentId);
        Task<ServiceResult<StudentService>> CreateStudentServiceAsync(Student student, int serviceId);
        Task<ServiceResult<StudentService>> UpdateStudentServiceAsync(UpdateStudentServiceDto updateStudenServicetDto, int studentId);
        Task<ServiceResult<StudentService>> UpdateNoteAsync(string note, int studentId);
        Task<ServiceResult<bool>> UpdateStudentOrderAsync(List<UpdateStudentOrderDto> studentOrders);
        Task<ServiceResult<StudentService>> UpdateStudentStatusAsync(UpdateStudentStatusDto updateStudentStatusDto, int studentId);
        Task<ServiceResult<StudentService>> SetExcludedDatesAsync(ExcludedDateDto excludedDateDto, int studentId);
        Task<ServiceResult<StudentService>> DeleteStudentServiceAsync(int studentId);
    }
}
