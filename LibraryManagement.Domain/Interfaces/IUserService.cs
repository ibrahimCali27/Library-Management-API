using Business_.DTOs.Auth_DTOs;
using Business_.DTOs.User_DTOs;
using System.ComponentModel.DataAnnotations;
namespace LibraryManagement.Domain.Interfaces
{
    public interface IUserService
    {
        Task<Create_JWT_Dto> CreateUserAsync(UserBaseDto dto);
        Task<bool> UpdateUserAsync(UpdateUserDto dto);
        Task<bool> UpdateMembershipAsync(UpdateMembershipDto dto);
        Task<Create_JWT_Dto> LoginAsync(LogInDto dto);
        Task<bool> DeleteUserAsync(UserDeleteDto dto);
    }
}
