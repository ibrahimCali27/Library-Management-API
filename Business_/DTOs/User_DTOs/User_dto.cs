using Core.Entities;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_.DTOs.User_DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
  
        public UserType UserType { get; set; }
        /// <summary>
        /// Değiştirilebilir kısımlar:Name,Surname,Password
        /// </summary>
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly RegisterDate { get; set; }
        public ICollection<Fine> Fines { get; set; } 
        public ICollection<User_Book> User_Books { get; set; } 
        public ICollection<User_Membership> User_Memberships { get; set; }
    }
}
