using Business_.BusinessLayer.BLL;
using Business_.DTOs.User_DTOs;
using Business_.Interfaces;
using Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Library_Management_API.Controllers
{
    [ApiController]
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
    }
}
