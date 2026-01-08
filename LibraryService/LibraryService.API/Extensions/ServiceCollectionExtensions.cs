using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Application.Services;
using LibraryService.Infrastructure.Repositories;

namespace LibraryService.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        
        return services;
    }
}