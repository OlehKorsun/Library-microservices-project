using Application.DTOs;
using Application.Requests;

namespace Application.Interfaces.Services;

public interface INotificationService
{
    public Task<IEnumerable<NotificationDto>> GetNotificationsAsync(CancellationToken ct = default);
    public Task<NotificationDto> GetNotificationByIdAsync(int id, CancellationToken ct = default);
    public Task AddNotificationAsync(NotificationRequest notification, CancellationToken ct = default);
    public Task UpdateNotificationAsync(NotificationRequest notification, CancellationToken ct = default);
    public Task DeleteNotificationAsync(int id, CancellationToken ct = default);
}