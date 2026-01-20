using LibraryService.Application.Interfaces.Services;
using LibraryService.Application.Services;

namespace LibraryService.API.Configurations;

public static class ApplicationConfigurations
{
    public static IServiceCollection ConfigApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IOrderService, OrderService>();
        
        return services;
    }
}