using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
namespace Library_Management_API.DTOs.User_DTOs
{
    public class UpdateUserDto
    {
        [Required]
        internal readonly object []? Id;
        /// <summary>
        /// Değiştirilebilir kısımlar:Name,Surname,Password
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Surname { get; set; }= string.Empty;
        [Required]
        [MinLength(8), MaxLength(255)]
        public string Password { get; set; } = string.Empty;
    }
}
