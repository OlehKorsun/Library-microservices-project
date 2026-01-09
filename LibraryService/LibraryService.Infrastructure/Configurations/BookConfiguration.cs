using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");
        
        builder.HasKey(b => b.BookId);

        builder.Property(b => b.CurrentCount);

        builder.Property(b => b.MaxCount);
        
        builder.Property(b => b.Title)
            .HasMaxLength(100);
        
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(b => b.Description)
            .HasMaxLength(200);
        
        builder.Property(b => b.ISBN).HasMaxLength(20);
    }
}