using Application.DTOs;
using Application.Requests;

namespace Application.Interfaces.Services;

public interface INotificationService
{
    public Task<IEnumerable<NotificationDto>> GetNotifications();
    public Task<NotificationDto> GetNotificationById(int id);
    public Task AddNotification(NotificationRequest notification);
    public Task UpdateNotification(int id, NotificationRequest notification);
    public Task DeleteNotification(int id);
}