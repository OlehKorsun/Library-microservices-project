using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");
        
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(a => a.Surname)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(a => a.Email)
            .HasMaxLength(100)
            .IsRequired();
    }
}