using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YanKoltukBackend.Application.Results;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.DTOs.UserDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Repositories.Interfaces;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.Services.Implementations
{
    public class ParentService(IRepository<User> userRepo, IRepository<Parent> parentRepo, IRepository<Student> studentRepo, UserHelper userHelper) : IParentService
    {
        private readonly IRepository<User> _userRepo = userRepo;
        private readonly IRepository<Parent> _parentRepo = parentRepo;
        private readonly IRepository<Student> _studentRepo = studentRepo;
        private readonly UserHelper _userHelper = userHelper;

        public async Task<ServiceResult<int>> GetParentIdAsync()
        {
            var userId = _userHelper.GetUserId();
            var parents = await _parentRepo.GetAllAsync();
            var parent = parents.FirstOrDefault(p => p.UserId == userId);
            if (parent == null)
                return ServiceResult<int>.ErrorResult("Error: Parent not found");
            return ServiceResult<int>.SuccessResult(parent.ParentId);
        }

        public async Task<ServiceResult<Parent>> GetParentByIdAsync(int parentId)
        {
            var parents = await _parentRepo.GetAllAsync();
            var parent = parents.FirstOrDefault(p => p.ParentId == parentId);
            if (parent == null)
                return ServiceResult<Parent>.ErrorResult("Error: Parent not found");
            return ServiceResult<Parent>.SuccessResult(parent);
        }

        public async Task<ServiceResult<Parent>> CreateParentAsync([FromBody] ParentSignupDto parentSignupDto)
        {
            var user = _userHelper.CreateUser(parentSignupDto.Phone, parentSignupDto.Password, Roles.Parent.GetDescription());
            await _userRepo.AddAsync(user);

            var parent = new Parent
            {
                User = user,
                Name = parentSignupDto.Name,
                IdNo = parentSignupDto.IdNo,
                Phone = parentSignupDto.Phone,
                Address = parentSignupDto.Address
            };
            await _parentRepo.AddAsync(parent);
            return ServiceResult<Parent>.SuccessResult(parent, "Parent created");
        }

        public async Task<Student?> GetStudentByIdAsync(int parentId, int studentId)
        {
            return (await _studentRepo.FindAsync(s => (s.ParentId == parentId) && (s.StudentId == studentId))).FirstOrDefault();
        }

        public async Task<ServiceResult<Student>> AddStudentAsync(StudentDto studentDto, int parentId)
        {
            try
            {
                var parent = await _parentRepo.GetByIdAsync(parentId);
                if (parent == null)
                    return ServiceResult<Student>.ErrorResult("Error: Parent not found");

                var student = new Student
                {
                    Name = studentDto.Name,
                    IdNo = studentDto.IdNo,
                    SchoolNo = studentDto.SchoolNo,
                    ParentId = parentId,
                    Parent = parent
                };
                await _studentRepo.AddAsync(student);

                parent.Students.Add(student);
                await _parentRepo.UpdateAsync(parent);

                return ServiceResult<Student>.SuccessResult(student, "Student added");
            }
            catch (Exception ex)
            {
                return ServiceResult<Student>.ErrorResult("Error: " + ex.InnerException?.Message);
            }
        }

        public async Task<ServiceResult<Parent>> UpdateParentAsync(UpdateParentDto updateParentDto, int parentId)
        {
            try
            {
                var parent = (await _parentRepo.FindAsync(
                    p => p.ParentId == parentId,
                    include: p => p.Include(x => x.User)
                )).FirstOrDefault();

                if (parent == null)
                    return ServiceResult<Parent>.ErrorResult("Error: Parent not found");

                if (!string.IsNullOrWhiteSpace(updateParentDto.Phone)) {
                    if (updateParentDto.Phone != parent.Phone)
                    {
                        parent.User.Username = updateParentDto.Phone;
                        parent.Phone = updateParentDto.Phone;
                        await _userRepo.UpdateAsync(parent.User);
                    }
                }

                if (!string.IsNullOrWhiteSpace(updateParentDto.Address)) {
                    if (updateParentDto.Address != parent.Address)
                        parent.Address = updateParentDto.Address;
                }

                await _parentRepo.UpdateAsync(parent);
                return ServiceResult<Parent>.SuccessResult(parent, "Parent updated");
            }
            catch (Exception ex)
            {
                return ServiceResult<Parent>.ErrorResult("Error: Parent not updated - " + ex.InnerException?.Message);
            }
        }      
    }
}