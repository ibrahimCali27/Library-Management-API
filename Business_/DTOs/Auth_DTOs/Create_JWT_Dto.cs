using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Auth_DTOs
{
    public class Create_JWT_Dto
    {
        [Required]
        public string Token {  get; set; }
        [Required]
        public string Email {  get; set; }
    }
}
