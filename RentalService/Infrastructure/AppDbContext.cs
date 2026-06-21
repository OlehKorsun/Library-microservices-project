using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book>  Books => Set<Book>();
    public DbSet<Client>  Clients => Set<Client>();
    public DbSet<Rental> Rentals => Set<Rental>();
}