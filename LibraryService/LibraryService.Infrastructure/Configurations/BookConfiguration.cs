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
        
        builder.Property(b => b.CurrentCount)
            .IsRequired()
            .HasDefaultValue(0);
        

        builder.Property(b => b.MaxCount).IsRequired().HasDefaultValue(0);
        
        
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(b => b.ISBN).IsRequired().HasMaxLength(20);
        
        builder.Property(b => b.PublishedAt).IsRequired();
    }
}