using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryManagement.Application.Contracts.DTOs.Auth_DTOs
{
    public class AuthTokenDto
    {
        public string Token {  get; set; }
        public string Email {  get; set; }
    }
}
