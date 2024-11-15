using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.UserDTOs;
using YanKoltukBackend.Models.Entities;

namespace YanKoltukBackend.Services.Interfaces
{
    public interface IParentService
    {
        Task<ServiceResult<Parent>> CreateParentAsync(ParentSignupDto parentSignupDto);
    }
}
