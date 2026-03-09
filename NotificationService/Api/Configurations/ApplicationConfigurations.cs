using Api.Workers;
using Application.Interfaces.Configurations;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Configurations;
using Infrastructure.Messaging;
using Microsoft.Extensions.Options;

namespace Api.Configurations;

public static class ApplicationConfigurations
{
    public static IServiceCollection ConfigAplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new RabbitMqConnection("127.0.0.1", "guest", "guest"));
        
        services.AddScoped<NotificationOrchestrator>();

        services.AddScoped<INotificationService, NotificationService>();

        services.AddHostedService<RabbitMqConsumer>();
        
        services.Configure<NotificationSettings>(
            configuration.GetSection("NotificationSettings"));
        
        services.AddSingleton<INotificationSettings>(
            sp => sp.GetRequiredService<IOptions<NotificationSettings>>().Value);
        
        return services;
    }
}