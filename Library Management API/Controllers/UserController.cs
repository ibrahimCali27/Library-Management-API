using LibraryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library_Management_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (result == null)
            {
                return Unauthorized(new
                {
                    Message = "Geçersiz e-posta veya şifre."
                });
            }
            return Ok(result);
        }
        [Authorize]
        [HttpPut("Update-User")]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserDto dto)
        {
            var result = await _userService.UpdateUserAsync(dto);

            return result ? Ok() : BadRequest();
        }
        [Authorize]
        [HttpPut("Update-Membership")]
        public async Task<IActionResult> UpdateMembershipAsync(UpdateMembershipDto dto)
        {

            var result = await _userService.UpdateMembershipAsync(dto);

            return result ? Ok() : BadRequest();
        }
        [Authorize]
        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUserAsync(DeleteUserDto dto)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Role = role,
                IsAdmin = User.IsInRole("Admin")
            });
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API çalışıyor");
        }
    }
}
