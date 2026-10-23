using Business_.DTOs.Auth_DTOs;
using Business_.DTOs.User_DTOs;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
namespace Business_.Interfaces
{
    public interface IUserService
    {
        Task<Create_JWT_Dto> CreateUserAsync(CreateUserDto dto);
        Task<bool> UpdateUserAsync(UpdateUserDto dto);
        Task<bool> UpdateMembershipAsync(UpdateMembershipDto dto);
        Task<Create_JWT_Dto> LoginAsync(LogInDto dto);
    }
}
