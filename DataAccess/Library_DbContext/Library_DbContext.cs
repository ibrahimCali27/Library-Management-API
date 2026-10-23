
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Entity.Entities;
namespace DataAccess.Context
{
    public class DBContext : DbContext {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Book> User_Book { get; set; }

        public async Task FindAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
