using Application.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Configurations;

public static class DatabaseConfigurations
{
    public static IServiceCollection ConfigDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Infrastructure.MongoDbSettings>(
            configuration.GetSection("MongoDbSettings"));
        
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });
        
        services.AddScoped<INotificationRepository, MongoNotificationRepository>();
        
        return services;
    }
}