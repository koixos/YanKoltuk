using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Service")]
    public class ServiceController(IServiceService serviceService, IStudentServiceService studentServiceService) : ControllerBase
    {
        private readonly IServiceService _serviceService = serviceService;
        private readonly IStudentServiceService _studentServiceService = studentServiceService;

        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            int serviceId = (await _serviceService.GetServiceIdAsync()).Data;
            var students = await _studentServiceService.GetAllStudentsAsync(serviceId);
            return Ok(students);
        }

        [HttpGet("drivingList")]
        public async Task<IActionResult> GetDrivingList()
        {
            int serviceId = (await _serviceService.GetServiceIdAsync()).Data;
            var students = await _studentServiceService.GetDrivingListAsync(serviceId);
            return Ok(students);
        }

        [HttpPut("editNote/{id}")]
        public async Task<IActionResult> UpdateStudentNote([FromBody] string note, int id)
        {
            int serviceId = (await _serviceService.GetServiceIdAsync()).Data;

            var student = await _studentServiceService.GetStudentByIdAsync(serviceId, id);
            if (student == null)
                return BadRequest("Student not found");

            var result = await _studentServiceService.UpdateNoteAsync(note, id);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("updateStatus/{id}")]
        public async Task<IActionResult> UpdateStudentStatus([FromBody] UpdateStudentStatusDto updateStudentStatusDto, int id)
        {
            var result = await _studentServiceService.UpdateStudentStatusAsync(updateStudentStatusDto, id);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("editOrder")]
        public async Task<IActionResult> UpdateStudentOrder([FromBody] List<StudentOrderDto> studentOrders)
        {
            if (studentOrders == null || studentOrders.Count == 0)
                return BadRequest("Invalid data");
            var result = await _studentServiceService.UpdateStudentOrderAsync(studentOrders);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }
    }
}
