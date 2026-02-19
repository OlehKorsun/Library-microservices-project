using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<NotificationLog>> GetAllAsync(CancellationToken ct);
    Task<NotificationLog> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(NotificationLog notification, CancellationToken ct = default);
    Task<bool> UpdateAsync(NotificationLog notification, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}