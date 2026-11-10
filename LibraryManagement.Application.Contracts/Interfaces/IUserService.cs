using LibraryManagement.Application.Contracts.DTOs.Auth_DTOs;
using LibraryManagement.Application.Contracts.DTOs.User_DTOs;
using System.ComponentModel.DataAnnotations;
namespace Business_.Interfaces
{
    public interface IUserService
    {
        Task<AuthTokenDto> CreateUserAsync(UserBaseDto dto);
        Task<bool> UpdateUserAsync(UpdateUserDto dto);
        Task<bool> UpdateMembershipAsync(UpdateMembershipDto dto);
        Task<AuthTokenDto> LoginAsync(LogInDto dto);
        Task<bool> DeleteUserAsync(UserDeleteDto dto);
    }
}
