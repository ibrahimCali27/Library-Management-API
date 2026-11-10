using Business_.DTOs.User_DTOs;
using LibraryManagement.Application.Contracts.DTOs.Auth_DTOs;
using LibraryManagement.Application.Contracts.DTOs.User_DTOs;
using System.ComponentModel.DataAnnotations;
namespace Business_.Interfaces
{
    public interface IUserAppService
    {
        Task<AuthTokenDto> CreateUserAsync(UserBaseDto dto);
        Task<bool> UpdateUserAsync(UserBaseDto dto);
        Task<bool> UpdateMembershipAsync(UpdateMembershipDto dto);
        Task<AuthTokenDto> LoginAsync(LoginDto dto);
    }
}
