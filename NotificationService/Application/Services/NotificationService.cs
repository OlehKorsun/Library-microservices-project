using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class NotificationService(INotificationRepository repository) : INotificationService
{
    
    
    public async Task<IEnumerable<NotificationDto>> GetNotificationsAsync(CancellationToken ct = default)
    {
        var notifications = await repository.GetAllAsync(ct);
        
        return notifications.Select(n => new NotificationDto
        {
            To = n.To,
            Subject = n.Subject,
            Body = n.Body,
            SentAt = n.SentAt,
            IsSuccess =  n.IsSuccess,
            ErrorMessage = n.ErrorMessage
        });
    }

    public async Task<NotificationDto> GetNotificationByIdAsync(int id, CancellationToken ct = default)
    {
        var notification = await repository.GetByIdAsync(id, ct) 
                           ?? throw new NotFoundException($"Notification with id: {id} was not found!");;

        return new NotificationDto
        {
            To = notification.To,
            Subject = notification.Subject,
            Body = notification.Body,
            SentAt = notification.SentAt,
            IsSuccess = notification.IsSuccess,
            ErrorMessage = notification.ErrorMessage,
            BookTitle = notification.BookTitle
        };
    }

    public async Task AddNotificationAsync(NotificationRequest request, CancellationToken ct = default)
    {
        NotificationLog notification = new NotificationLog
        {
            To = request.To,
            Subject = request.Subject,
            Body = request.Body,
            BookTitle = request.BookTitle,
            DueDate = request.DueDate,
        };
        await repository.AddAsync(notification, ct);
    }

    public async Task DeleteNotificationAsync(int id, CancellationToken ct = default)
    {
        await  repository.DeleteAsync(id, ct);
    }

    public async Task UpdateNotificationAsync(NotificationRequest request, CancellationToken ct = default)
    {
        NotificationLog notification = new NotificationLog
        {
            To = request.To,
            Subject =  request.Subject,
            Body =  request.Body,
            BookTitle = request.BookTitle,
            DueDate =  request.DueDate,
        };
        
        await repository.UpdateAsync(notification, ct);
    }
}