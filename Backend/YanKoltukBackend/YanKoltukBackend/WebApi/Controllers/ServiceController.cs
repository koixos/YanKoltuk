using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceController(IServiceService serviceService, IFileService fileService) : ControllerBase
    {
        private readonly IServiceService _serviceService = serviceService;
        private readonly IFileService _fileService = fileService;

        [HttpGet("services")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpPost("addService")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddService([FromBody] ServiceDto serviceDto)
        {
            var result = await _serviceService.AddServiceAsync(serviceDto);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _serviceService.DeleteServiceAsync(id);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdatedServiceDto updatedServiceDto)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);

            if (service == null)
            {
                return NotFound("Service not found.");
            }

            service.DriverId = updatedServiceDto.DriverId;
            service.StewardessId = updatedServiceDto.StewardessId;

            var result = await _serviceService.UpdateServiceAsync(service);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpGet("DownloadLogs")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetServiceLogs(int serviceId, DateTime date)
        {
            var serviceLogs = await _serviceService.GetServiceLogsExcelAsync(serviceId);
            var fileResult = _fileService.GenerateServiceLogsExcel(serviceLogs, date);
            return fileResult;
        }
    }
}
