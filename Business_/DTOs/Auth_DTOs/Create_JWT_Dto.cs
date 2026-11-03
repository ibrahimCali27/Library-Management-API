using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business_.DTOs.Auth_DTOs
{
    public class Create_JWT_Dto
    {
        public string Token {  get; set; }
        public string Email {  get; set; }
    }
}
