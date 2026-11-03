using Business_.DTOs.Auth_DTOs;
using Business_.DTOs.User_DTOs;
using Business_.Interfaces;
using DataAccess.Library_DbContext;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Business_.BusinessLayer.BLL
{

    public class UserService(DBContext context,IJWTService jwtService,ILogger<UserService> logger) : IUserService
    {
        private readonly DBContext _context = context;
        private readonly IJWTService _jwtService = jwtService;
        private readonly ILogger<UserService> _logger = logger;

        public async Task<Create_JWT_Dto?> CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                _logger.LogWarning("Bu kullanıcı zaten üye.");
                return null;
            }

            if (dto.Password.Length < 8)
            {
                _logger.LogWarning("Şifre en az 8 harf uzunluğunda olmalıdır.");
                return null;
            }
            var membership = await _context.Memberships
    .FirstOrDefaultAsync(x => x.Type == dto.MembershipType);

            if (membership == null)
            {
                _logger.LogWarning("Üyelik bulunamadı.");
                return null;
            }
            var user = new User
            {
                UserType =dto.UserType,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),

                MembershipId = membership.Id,

                RegisterDate = DateOnly.FromDateTime(DateTime.Now)
            };
            Console.WriteLine(dto.MembershipType);
            Console.WriteLine(user.MembershipId);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var token = _jwtService.GenerateToken(user);
            return new Create_JWT_Dto
            {
                Token = token,
                Email = user.Email
            };
            
        }

        public Microsoft.Extensions.Logging.ILogger Get_logger1()
        {
            return _logger;
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (user == null)
            {
                _logger.LogWarning("Kullanıcı bulunamadı. Id: {UserId}", dto.Id);
                return false;
            }

            bool samePassword = BCrypt.Net.BCrypt.Verify(dto.Password,user.Password);

            bool noChanges =
                user.Name == dto.Name &&
                user.Surname == dto.Surname &&
                samePassword;

            if (noChanges)
            {
                _logger.LogInformation("Bu kullanıcı güncellenmemiştir. Id: {UserId}", dto.Id);
                return false;
            }

            user.Name = dto.Name;
            user.Surname = dto.Surname;

            if (!samePassword)
            {
                if (dto.Password.Length < 8)
                {
                    _logger.LogWarning("Şifre en az 8 karakterden oluşmalıdır.");
                    return false;
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMembershipAsync(UpdateMembershipDto dto)
        {
            var User = await _context.Users
    .Include(x => x.Membership)
    .FirstOrDefaultAsync(x => x.Id == dto.UserId);

            if (User == null)
            {
                _logger.LogWarning("Kullanıcı bulunamadı.");
                return false;
            }

            int borrowedBooksCount = await _context.User_Book
    .CountAsync(x => x.UserId == dto.UserId);
            
            var targetMembership = await _context.Memberships
    .FirstOrDefaultAsync(x => x.Type == dto.MembershipType);
            
            if (targetMembership == null) 
            {
                _logger.LogWarning("Üyelik bilgisi girilmedi.");
                return false;
            }
            
            int NewBorrowLimit = targetMembership.Borrow_Limit;
            
            if(!User.Membership.IsUnlimited && borrowedBooksCount > NewBorrowLimit)
            {
                _logger.LogWarning("Üyeliğini değiştiremezsin.Bazı kitaplarını iade ettikten sonra tekrar dene.");
                return false;
            }

            User.MembershipId = targetMembership.Id;
            _context.Users.Update(User);
            
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<Create_JWT_Dto?> LoginAsync(LogInDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                _logger.LogWarning("Geçersiz eposta ya da şifre.");
                return null;
            }

            bool validPassword = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.Password);

            if (!validPassword)
            {
                _logger.LogWarning("Geçersiz eposta ya da şifre.");
                return null;
            }

            var token = _jwtService.GenerateToken(user);

            return new Create_JWT_Dto
            {
                Email = user.Email,
                Token = token
            };
        }

        public async Task<bool> DeleteUserAsync(DeleteUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserType == dto.Id); 
            if (user ==null){ _logger.LogWarning("Kullanıcı bulunamadı."); return false; }
            
            Console.WriteLine(dto.Id);
            Console.WriteLine((int)dto.Id);

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
