using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryService.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.HasKey(o => o.OrderId);
        
        builder.Property(o => o.Amount).IsRequired();
        
        builder.ToTable(o => o.HasCheckConstraint(
                "CK_Orders_Amount_NonNegative",
                "[Amount] >= 0"
            ));
        
        builder.Property(o => o.CreatedAt).IsRequired();
        
        builder.Property(o => o.OrderStatus).IsRequired();

        builder.Property(o => o.BookId).IsRequired();
    }
}