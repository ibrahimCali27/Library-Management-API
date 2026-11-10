using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.EntityFrameworkCore.EntityTypeConfiguration
{
    public class FineConfiguration : IEntityTypeConfiguration<Fine>
    {
        public void Configure(EntityTypeBuilder<Fine> builder)
        {
            builder.ToTable("Fine");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(X => X.IsPaid)
                .IsRequired();

            builder.Property(x => x.FineDate)
                .IsRequired();

            builder.Property(x => x.IsReturned)
                .IsRequired();

            builder.HasOne(x => x.UserBook)
            .WithOne(x => x.Fine)
            .HasForeignKey<Fine>(x => x.UserBookId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}