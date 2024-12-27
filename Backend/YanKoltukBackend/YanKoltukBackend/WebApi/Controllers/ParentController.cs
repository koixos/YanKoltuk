using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.SendDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Parent")]
    public class ParentController(IParentService parentService, IStudentServiceService studentServiceService) : ControllerBase
    {
        private readonly IParentService _parentService = parentService;
        private readonly IStudentServiceService _studentServiceService = studentServiceService;

        [HttpGet("info")]
        public async Task<IActionResult> GetParentInfo()
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;
            var parent = await _parentService.GetParentByIdAsync(parentId);
            return Ok(parent);
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;
            var students = await _studentServiceService.GetStudentsAsync(parentId);
            return Ok(students);
        }

        [HttpGet("plates")]
        public async Task<IActionResult> GetAllServicePlates()
        {
            var plates = (await _studentServiceService.GetAllServicePlatesAsync()).Data;
            return Ok(plates);
        }

        [HttpGet("student/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;
            var student = await _parentService.GetStudentByIdAsync(parentId, id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpGet("service/{plate}")]
        public async Task<IActionResult> GetServiceIdByPlate(string plate)
        {
            var serviceId = await _studentServiceService.GetServiceIdByPlateAsync(plate);
            return Ok(serviceId);
        }

        [HttpGet("excludedDates/{id}")]
        public async Task<IActionResult?> GetExcludedDates(int id)
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;
            var student = await _parentService.GetStudentByIdAsync(parentId, id);
            if (student == null)
                return BadRequest("Student not found");
            var excludedDays = (await _studentServiceService.GetExcludedDates(id))?.Data;
            return Ok(excludedDays);
        }

        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto studentDto)
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;
            var result = await _parentService.AddStudentAsync(studentDto, parentId);
            if (result.Success)
            {
                var student = result.Data;
                if (student == null)
                    return BadRequest("Student is null");

                var serviceId = (await _studentServiceService.GetServiceIdByPlateAsync(studentDto.Plate)).Data;
                var resultStudentService = await _studentServiceService.CreateStudentServiceAsync(student, serviceId);
                return resultStudentService.Success ? NoContent() : BadRequest(resultStudentService.Message);
            } else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("updateParent")]
        public async Task<IActionResult> UpdateParent([FromBody] UpdateParentDto updateParentDto)
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;
            var result = await _parentService.UpdateParentAsync(updateParentDto, parentId);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPut("updateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentServiceDto updateStudentServiceDto, int id)
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;

            var student = await _parentService.GetStudentByIdAsync(parentId, id);
            if (student == null)
                return BadRequest("Student not found");

            var serviceId = (await _studentServiceService.GetServiceIdByPlateAsync(updateStudentServiceDto.Plate)).Data;

            var result = await _studentServiceService.UpdateStudentServiceAsync(serviceId, id);

            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpPost("setExcludedDates/{id}")]
        public async Task<IActionResult> SetExcludedDates([FromBody] ExcludedDateDto excludedDateDto, int id)
        {
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            Console.WriteLine($"Request Body: {requestBody}");

            int parentId = (await _parentService.GetParentIdAsync()).Data;

            var student = await _parentService.GetStudentByIdAsync(parentId, id);
            if (student == null)
                return BadRequest("Student not found");

            var result = await _studentServiceService.SetExcludedDatesAsync(excludedDateDto, id);
            return result.Success ? NoContent(): BadRequest(result.Message);
        }

        [HttpDelete("deleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            int parentId = (await _parentService.GetParentIdAsync()).Data;

            var student = await _parentService.GetStudentByIdAsync(parentId, id);
            if (student == null)
                return BadRequest("Student not found");

            var resultStudentService = await _studentServiceService.DeleteStudentServiceAsync(id);

            if (resultStudentService.Success)
            {
                var result = await _parentService.DeleteStudentAsync(parentId, id);
                return result.Success ? NoContent() : BadRequest(result.Message);
            } else
            {
                return BadRequest(resultStudentService.Message);
            }
        }
    }
}
