using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Author> Authors => Set<Author>();

    
}