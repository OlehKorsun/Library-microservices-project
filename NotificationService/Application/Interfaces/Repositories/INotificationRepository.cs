using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<NotificationLog>> GetAllAsync(CancellationToken ct = default);
    Task<NotificationLog> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(NotificationLog notification, CancellationToken ct = default);
    Task<bool> UpdateAsync(NotificationLog notification, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}