using Application.DTOs;
using Application.Interfaces.Services;
using Application.Requests;

namespace Application.Services;

public class NotificationService : INotificationService
{
    public async Task<IEnumerable<NotificationDto>> GetNotifications()
    {
        throw new NotImplementedException();
    }

    public async Task<NotificationDto> GetNotificationById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddNotification(NotificationRequest notification)
    {
        
    }

    public async Task DeleteNotification(int id)
    {
        
    }

    public async Task UpdateNotification(int id, NotificationRequest notification)
    {
        
    }
}