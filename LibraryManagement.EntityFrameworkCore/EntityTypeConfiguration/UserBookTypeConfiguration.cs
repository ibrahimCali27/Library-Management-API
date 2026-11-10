using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.EntityFrameworkCore.EntityTypeConfiguration
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBook>
    {
        public void Configure(EntityTypeBuilder<UserBook> builder)
        {
            builder.ToTable("UserBook");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BorrowDate)
                .IsRequired();

            builder.Property(x=>x.ReturnDate).IsRequired();

            builder.Property(x => x.IsReturned).IsRequired();

            builder.HasOne(x => x.Book).WithMany().HasForeignKey(x=>x.BookId).OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Fine).WithOne(x => x.UserBook);

        }
    }
}
