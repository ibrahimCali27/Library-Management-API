using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Writer)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x=>x.Genre)
            .HasMaxLength (100)
            .IsRequired ();

        builder.Property(x => x.Count)
            .IsRequired();

        builder.Property(x => x.BookISBN)
       .IsRequired()
       .HasMaxLength(13)
       .IsFixedLength();

        builder.HasIndex(x => x.BookISBN)
               .IsUnique();

        builder.HasMany(x => x.UserBooks)
            .WithOne(x => x.Book)
            .HasForeignKey(x=>x.BookId);
    }
}

