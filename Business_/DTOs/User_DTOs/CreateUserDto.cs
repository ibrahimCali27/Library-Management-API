using Entity.Entities;
using System;
using System.ComponentModel.DataAnnotations;
namespace Business_.DTOs.User_DTOs
{
    public class CreateUserDto
    {
        public UserType UserType { get; set; } 
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Surname { get; set; }= string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }=string.Empty;
        [Required]
        [MinLength(8),MaxLength(255)]
        public string Password { get; set; } = string.Empty;
        public MembershipType MembershipType { get; set; }
    }
}
