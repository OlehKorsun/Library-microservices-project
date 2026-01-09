using LibraryService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.API.Extensions;

public static class AddDatabaseDependencies
{
    public static IServiceCollection AddDbDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}