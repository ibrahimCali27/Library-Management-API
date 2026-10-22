using Business_.DTOs.User_DTOs;
using Business_.Interfaces;
using Core.Entities;
using DataAccess.Context;
using Library_Management_API.DTOs.User_DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Business_.BusinessLayer.BLL
{
    [ApiController]
    public class UserService(DBContext context) : IUserService
    {
        private readonly DBContext _context = context;
        private readonly ILogger<BookService> _logger;

        public async Task<bool> CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = dto.Password,
                
            };
            if (user.Password.Length < 8)
            {
                throw new Exception("Password must be at least 8 lengths.");
            }
            _context.Users.Add(user);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateUserAsync(UpdateUserDto dto)
        {
            var NoUpdatedUser = await _context.Users.FindAsync(dto.Id);

            if (NoUpdatedUser == null)
                throw new Exception("User not found.");
            User user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Password = dto.Password
            };
            if (user.Password.Length < 8) 
            {
                throw new Exception("Password must be at least 8 lengths.");
            }
            bool noChanges =
                NoUpdatedUser.Name == dto.Name &&
                NoUpdatedUser.Surname == dto.Surname &&
                NoUpdatedUser.Password == dto.Password;
            if (noChanges)
            {
                throw new Exception("No changes detected.");
            }
            
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
