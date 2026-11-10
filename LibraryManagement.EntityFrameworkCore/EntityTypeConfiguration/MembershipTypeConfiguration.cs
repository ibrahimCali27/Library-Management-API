using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.EntityFrameworkCore.EntityTypeConfiguration
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("Membership");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .IsRequired().
                HasPrecision(18, 2); ;
            
            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.IsUnlimited).IsRequired();
            
            builder.HasMany(x => x.Users)
               .WithOne(x => x.Membership)
               .HasForeignKey(x => x.MembershipId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
