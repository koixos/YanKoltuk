using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Models.Entities;
using YanKoltukBackend.Services.Implementations;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController(IManagerService managerService, IStudentServiceService studentServiceService) : ControllerBase
    {
        private readonly IManagerService _managerService = managerService;
        private readonly IStudentServiceService _studentServiceService = studentServiceService;

        [HttpGet("manager")]
        public async Task<IActionResult> GetManagerId()
        {
            var managerId = (await _managerService.GetManagerIdAsync()).Data;
            return Ok(managerId);
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetAllServices()
        {
            int managerId = (await _managerService.GetManagerIdAsync()).Data;
            var services = await _managerService.GetAllServicesAsync(managerId);
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            int managerId = (await _managerService.GetManagerIdAsync()).Data;
            var service = await _managerService.GetServiceByIdAsync(managerId, id);
            return service == null ? NotFound() : Ok(service);
        }

        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetServiceStudentsById(int id)
        {
            var students = await _studentServiceService.GetServiceStudentsAsync(id);
            return students == null ? NotFound() : Ok(students);
        }

        [HttpPost("addService")]
        public async Task<IActionResult> AddService([FromBody] ServiceDto serviceDto)
        {
            int managerId = (await _managerService.GetManagerIdAsync()).Data;
            var result = await _managerService.AddServiceAsync(serviceDto, managerId);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPut("updateService/{id}")]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServiceDto updatedServiceDto, int id)
        {
            int managerId = (await _managerService.GetManagerIdAsync()).Data;
            var result = await _managerService.UpdateServiceAsync(updatedServiceDto, managerId, id);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpDelete("deleteService/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            int managerId = (await _managerService.GetManagerIdAsync()).Data;
            var result = await _managerService.DeleteServiceAsync(managerId, id);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpGet("logs/{id}")]
        public async Task<IActionResult> ExportServiceLogs(int id)
        {
            var fileContent = (await _studentServiceService.GenerateServiceLogs(id)).Data;
            if (fileContent == null)
            {
                return NoContent();
            }

            var fileName = $"ServiceLogs_{id}.xlsx";
            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}