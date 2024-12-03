using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs.AddDTOs;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
        private readonly IAdminService _adminService = adminService;

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminId()
        {
            var adminId = (await _adminService.GetAdminIdAsync()).Data;
            return Ok(adminId);
        }

        [HttpGet("managers")]
        public async Task<IActionResult> GetAllManagers()
        {
            var adminId = (await _adminService.GetAdminIdAsync()).Data;
            var managers = await _adminService.GetAllManagersAsync(adminId);
            return Ok(managers);
        }        

        [HttpPost("addManager")]
        public async Task<IActionResult> AddManager([FromBody] ManagerDto managerDto)
        {
            var adminId = (await _adminService.GetAdminIdAsync()).Data;
            var result = await _adminService.AddManagerAsync(managerDto, adminId);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpDelete("deleteManager/{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var result = await _adminService.DeleteManagerAsync(id);
            return result.Success ? NoContent() : BadRequest(result.Message);
        }
    }
}
