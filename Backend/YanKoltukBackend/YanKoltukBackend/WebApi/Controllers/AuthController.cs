using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YanKoltukBackend.Models.DTOs;
using YanKoltukBackend.Services.Interfaces;

namespace YanKoltukBackend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(ParentSignupDto parentSignupDto)
        {
            var result = await _userService.CreateUserAsync(parentSignupDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.AuthenticateUserAsync(loginDto);
            if (token == null)
            {
                return Unauthorized("Invalid credentials");
            }
            return Ok(new { Token = token });
        }
    }
}
