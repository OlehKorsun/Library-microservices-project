using Api.Workers;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Messaging;

namespace Api.Configurations;

public static class ApplicationConfigurations
{
    public static IServiceCollection ConfigAplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new RabbitMqConnection("127.0.0.1", "guest", "guest"));
        
        services.AddScoped<NotificationOrchestrator>();

        services.AddScoped<INotificationService, NotificationService>();

        services.AddHostedService<RabbitMqConsumer>();
        
        return services;
    }
}