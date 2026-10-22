using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public enum UserType
    {
        User = 0,
        Admin = 1
    }
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JWT { get; set; }
        public UserType UserType { get; set; }
        /// <summary>
        /// Değiştirilebilir kısımlar:Name,Surname,Password
        /// </summary>
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly RegisterDate { get; set; }
        public ICollection<User_Book> User_Books { get; set; } 
        public ICollection<User_Membership> User_Memberships { get; set; }
    }
}
