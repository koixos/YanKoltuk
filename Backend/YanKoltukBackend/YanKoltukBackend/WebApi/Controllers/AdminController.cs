using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
        private readonly IAdminService _adminService = adminService;

        [HttpGet("managers")]
        public async Task<IActionResult> GetAllManagers()
        {
            var managers = await _adminService.GetAllManagersAsync();
            return Ok(managers);
        }

        [HttpPost("addManager")]
        public async Task<IActionResult> AddManager([FromBody] ManagerDto managerDto)
        {
            var result = await _adminService.AddManagerAsync(managerDto);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
