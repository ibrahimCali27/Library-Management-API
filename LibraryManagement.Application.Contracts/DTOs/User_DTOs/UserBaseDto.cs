using System;
using System.ComponentModel.DataAnnotations;
using LibraryManagement.Domain.Entities;
namespace LibraryManagement.Application.Contracts.DTOs.User_DTOs
{
    public class UserBaseDto
    {
        public string Name { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Surname { get; set; } = string.Empty;
        [EmailAddress]
        
        [MinLength(8), MaxLength(255)]
        public string Password { get; set; } = string.Empty;
        public MembershipType MembershipType { get; set; }
    }
    public class UserCreateDto : UserBaseDto
    {
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        public UserType UserType { get; set; }
    }
    public class UserUpdateDto : UserBaseDto
    {

    }
}