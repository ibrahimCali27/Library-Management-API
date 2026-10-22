using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Context // Sizin namespace adınız neyse onu yazın
{
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();

            // Buraya geçici olarak lokal SQL Server bağlantı adresinizi yazın
            optionsBuilder.UseSqlServer("Server=LAPTOP-0O1D3ABP\\SQLEXPRESS;Database=Library Management API;Trusted_Connection=True;TrustServerCertificate=True;");

            return new DBContext(optionsBuilder.Options);
        }
    }
}
