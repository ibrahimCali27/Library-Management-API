using Business_.DTOs.User_DTOs;
using Core.Entities;
using Library_Management_API.DTOs.User_DTOs;
namespace Business_.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(UpdateUserDto dto);

    }
}
