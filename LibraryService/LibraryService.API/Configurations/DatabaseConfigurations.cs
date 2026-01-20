using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Infrastructure;
using LibraryService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.API.Configurations;

public static class DatabaseConfigurations
{
    public static IServiceCollection ConfigDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        
        return services;
    }
}