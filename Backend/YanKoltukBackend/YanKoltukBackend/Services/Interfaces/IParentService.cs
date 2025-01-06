using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.DTOs.UserDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IParentService
    {
        Task<ServiceResult<int>> GetParentIdAsync();
        Task<ServiceResult<Parent>> GetParentByIdAsync(int parentId);
        Task<ServiceResult<Parent>> CreateParentAsync(ParentSignupDto parentSignupDto);
        Task<Student?> GetStudentByIdAsync(int parentId, int studentId);
        Task<ServiceResult<Student>> AddStudentAsync(StudentDto studentDto, int parentId);
        Task<ServiceResult<Parent>> UpdateParentAsync(UpdateParentDto updateParentDto, int parentId);
    }
}
