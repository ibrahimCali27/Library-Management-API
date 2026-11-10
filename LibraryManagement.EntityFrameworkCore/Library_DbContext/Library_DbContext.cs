using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.EntityFrameworkCore.Library_DbContext
{
    public class LibraryDbContext : DbContext {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBook> User_Book { get; set; }
    }
}
