using Application.Interfaces.Services;
using Application.Services;
using Application.Settings;
using SendGrid;

namespace Api.Configurations;

public static class SendGridConfigurations
{
    public static IServiceCollection ConfigSendGrid(this IServiceCollection services, IConfiguration configuration)
    {
        var sendGridSection = configuration.GetSection(SendGridSettings.SectionName);
        services.Configure<SendGridSettings>(sendGridSection);

        var apiKey = sendGridSection["ApiKey"]; 
        services.AddSingleton<ISendGridClient>(new SendGridClient(apiKey));

        services.AddScoped<IEmailSenderService, SendGridEmailSender>();
        
        return services;
    }
}