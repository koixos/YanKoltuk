using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Models.DTOs.UpdateDTOs;
using YanKoltukBackend.Services.Interfaces;
using YanKoltukBackend.Shared.Helpers;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceController(IServiceService serviceService, IFileService fileService, UserHelper userHelper) : ControllerBase
    {
        private readonly IServiceService _serviceService = serviceService;
        private readonly IFileService _fileService = fileService;
        private readonly UserHelper _userHelper = userHelper;

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

        /*[HttpGet("DownloadLogs")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetServiceLogs(int serviceId, DateTime date)
        {
            var serviceLogs = await _serviceService.GetServiceLogsExcelAsync(serviceId);
            var fileResult = _fileService.GenerateServiceLogsExcel(serviceLogs, date);
            return fileResult;
        }*/
    }
}
