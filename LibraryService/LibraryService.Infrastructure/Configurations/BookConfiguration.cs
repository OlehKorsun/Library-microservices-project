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
        
        builder.Property(b => b.CurrentAmount)
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Books_CurrentAmount_NonNegative",
                "[CurrentAmount] >= 0"
            );
        });

        builder.Property(b => b.AmountMustBe).IsRequired().HasDefaultValue(0);
        
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Books_AmountMustBe_NonNegative",
                "[AmountMustBe] >= 0"
            );
        });
        
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(b => b.ISBN).IsRequired().HasMaxLength(20);
        
        builder.Property(b => b.PublishedAt).IsRequired();
    }
}