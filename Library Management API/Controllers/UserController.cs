using Business_.BusinessLayer.BLL;
using Business_.DTOs.User_DTOs;
using Business_.Interfaces;
using Core.Entities;
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

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserDto dto)
        {
            var result = await _userService.CreateUserAsync(dto);

            return result ? Ok() : BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> BorrowBookAsync(CreateUserDto dto)
        {
            if (dto.UserType == UserType.Admin)
            {
                var result = await _userService.CreateUserAsync(dto);
                return result ? Ok() : BadRequest();
            }
            else
            {
                throw new Exception("Only admins allowed to do this.");
            }
            
        }
    }
}
