using Application.Interfaces.Configurations;

namespace Infrastructure.Configurations;

public class NotificationSettings : INotificationSettings
{
    public int MaxAttemptCount { get; set; }
}