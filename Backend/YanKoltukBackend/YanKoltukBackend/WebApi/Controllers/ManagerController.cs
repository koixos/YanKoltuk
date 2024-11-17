using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Services.Implementations;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController(IManagerService managerService) : ControllerBase
    {
        private readonly IManagerService _managerService = managerService;

        [HttpGet("services")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _managerService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _managerService.GetServiceByIdAsync(id);
            if (service == null) return NotFound();
            return Ok(service);
        }

        [HttpPost("addService")]
        public async Task<IActionResult> AddService([FromBody] ServiceDto serviceDto)
        {
            var managerId = (await _managerService.GetManagerIdAsync()).Data;
            var result = await _managerService.AddServiceAsync(serviceDto, managerId);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServiceDto updatedServiceDto, int id)
        {
            var service = await _managerService.GetServiceByIdAsync(id);

            if (service == null)
            {
                return NotFound("Service not found.");
            }

            var result = await _managerService.UpdateServiceAsync(updatedServiceDto, service);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _managerService.DeleteServiceAsync(id);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }
    }
}
