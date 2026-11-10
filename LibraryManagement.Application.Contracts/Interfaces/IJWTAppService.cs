using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Contracts.Interfaces
{
    public interface IJWTAppService
    {
        public string GenerateToken(User user);
    }
}
