using System;
using System.ComponentModel.DataAnnotations;
namespace Business_.DTOs.User_DTOs
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Surname { get; set; }= string.Empty;
        [MinLength(8), MaxLength(255)]
        public string Password { get; set; } = string.Empty;
    }
}
