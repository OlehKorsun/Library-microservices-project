using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class NotificationOrchestrator(IEmailSenderService emailSender, INotificationRepository repository)
{
    public async Task<NotificationResult> HandleBookOverdueAsync(NotificationDto notification)
    {
        // var subject = "The deadline for submitting the book has passed!";
        // var body = $"Hi! You forget to return the {notification.BookTitle} book to the library! The deadline was: {notification.DueDate:d}";

        var log = await GetOrCreateAsync(notification);
        
        if (log.IsSuccess)
        {
            return NotificationResult.Success;
        }
        
        if (!log.CanRetry)
        {
            return NotificationResult.Failed;
        }

        try
        {
            log.AttemptCount++;
            
            await emailSender.SendEmailAsync(
                notification.To, 
                notification.Subject, 
                notification.Body);
            
            log.IsSuccess = true;
            log.SentAt = DateTime.UtcNow;
            log.ErrorMessage = null;
            
            await repository.UpdateAsync(log);
            
            return NotificationResult.Success;
        }
        catch (Exception ex)
        {
            // log.IsSuccess = false;
            log.ErrorMessage = ex.Message;
            
            await repository.UpdateAsync(log);
            
            if (log.CanRetry)
                return NotificationResult.Retry;

            return NotificationResult.Failed;
        }
    }
    
    private async Task<NotificationLog> GetOrCreateAsync(NotificationDto dto)
    {
        var log = await repository.GetByBusinessKeyAsync(
            dto.To,
            dto.Subject,
            dto.DueDate,
            dto.BookTitle);

        if (log != null)
            return log;

        log = new NotificationLog
        {
            To = dto.To,
            Subject = dto.Subject,
            Body = dto.Body,
            DueDate = dto.DueDate,
            BookTitle = dto.BookTitle,
            AttemptCount = 0,
            MaxAttemptCount = 3,
            SentAt = DateTime.UtcNow
        };

        await repository.AddAsync(log);

        return log;
    }
}