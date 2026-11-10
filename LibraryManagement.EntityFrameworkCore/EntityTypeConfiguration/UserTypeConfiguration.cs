using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.EntityFrameworkCore.EntityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasIndex(x => x.Email)
           .IsUnique();

            builder.Property(x => x.PasswordHash)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Surname)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.UserType)
                   .HasConversion<int>();

            builder.Property(x => x.RegisterDate)
                   .IsRequired();

            builder.HasOne(x => x.Membership)
                   .WithMany()
                   .HasForeignKey(x => x.MembershipId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.UserBooks)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId);
        }
    }
}