using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client.RP;

namespace Infrastructure.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("Rental");
        
        builder.HasKey(a => a.Id);

        builder.Property(a => a.RentedAt)
            .HasColumnType("date")
            .HasDefaultValueSql("CAST(GETUTCDATE() AS DATE)");
        
        builder.Property(a => a.RentedUntil)
            .HasColumnType("date")
            .HasDefaultValueSql("CAST(GETUTCDATE() AS DATE)");
        
        builder.Property(a => a.IsReturned)
            .HasDefaultValue(false);
        
        builder.HasOne(a => a.Client)
            .WithMany(a => a.Rentals)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(a => a.Book)
            .WithMany(a => a.Rentals)
            .HasForeignKey(a => a.BookId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(r => r.ClientId).HasDatabaseName("IX_Rental_ClientId");
        builder.HasIndex(r => r.BookId).HasDatabaseName("IX_Rental_BookId");
    }
}