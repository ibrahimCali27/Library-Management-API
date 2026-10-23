using Business_.DTOs.Auth_DTOs;
using Business_.DTOs.User_DTOs;
using Business_.Interfaces;
using DataAccess.Context;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Business_.BusinessLayer.BLL
{

    public class UserService(DBContext context,IJWTService jwtService) : IUserService
    {
        private readonly DBContext? _context = context;
        private readonly IJWTService _jwtService = jwtService;
        private readonly ILogger<BookService> _logger;

        public async Task<Create_JWT_Dto> CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            if (dto.Password.Length < 8)
            {
                throw new InvalidOperationException("Password must be at least 8 characters.");
            }

            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            var token = _jwtService.GenerateToken(user);
            return new Create_JWT_Dto
            {
                Token = token,
                Email = user.Email
            };
        }
        public async Task<bool> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            bool samePassword = BCrypt.Net.BCrypt.Verify(dto.Password,user.Password);

            bool noChanges =
                user.Name == dto.Name &&
                user.Surname == dto.Surname &&
                samePassword;

            if (noChanges)
            {
                throw new Exception("No changes detected.");
            }

            user.Name = dto.Name;
            user.Surname = dto.Surname;

            if (!samePassword)
            {
                if (dto.Password.Length < 8)
                {
                    throw new Exception("Password must be at least 8 characters.");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMembershipAsync(UpdateMembershipDto dto)
        {
            var User = await _context.Users.Include(x => x.CurrentMembership).FirstOrDefaultAsync(x => x.Id == dto.UserId);
            
            if (User == null)
            {
                throw new Exception("User not found.");
            }

            int borrowedBooksCount = await _context.User_Book
    .CountAsync(x => x.User_Id == dto.UserId);
            
            var targetMembership = await _context.Memberships
    .FirstOrDefaultAsync(x => x.Type == dto.MembershipType);
            
            if (targetMembership == null) 
            {
                throw new Exception("Membership information is not entered.");
            }
            
            int NewBorrowLimit = targetMembership.Borrow_Limit;
            
            if(borrowedBooksCount > NewBorrowLimit)
            {
                throw new Exception("You cannot switch memberships.");
            }
            
            User.CurrentMembership = targetMembership;
            _context.Users.Update(User);
            
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Create_JWT_Dto> LoginAsync(LogInDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.Password);

            if (!validPassword)
            {
                throw new Exception("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user);

            return new Create_JWT_Dto
            {
                Email = user.Email,
                Token = token
            };
        }
    }
}
