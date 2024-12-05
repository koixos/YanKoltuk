using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs.UserDTOs;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService, IAdminService adminService, IParentService parentService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAdminService _adminService = adminService;
        private readonly IParentService _parentService = parentService;

        [HttpGet("admin")]
        public async Task<IActionResult> AdminSignup()
        {
            var result = await _adminService.CreateAdminAsync();
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> ParentSignup([FromBody] ParentSignupDto parentSignupDto)
        {
            var result = await _parentService.CreateParentAsync(parentSignupDto);
            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var userDto = await _userService.AuthenticateUserAsync(loginDto);
            return userDto.Token == null ? Unauthorized("Invalid credentials") : Ok(userDto);
        }
    }
}
