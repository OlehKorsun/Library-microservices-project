using Application.DTOs;
using Application.Interfaces.Configurations;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class NotificationOrchestrator(
    IEmailSenderService emailSender, 
    INotificationRepository repository,
    INotificationSettings notificationSettings)
{
    public async Task<NotificationResult> HandleBookOverdueAsync(NotificationDto notification)
    {
        var notificationLog = await GetOrCreateAsync(notification);
        
        if (notificationLog.Status == NotificationResult.Success)
            return NotificationResult.Success;
        
        if (notificationLog.AttemptCount >= notificationSettings.MaxAttemptCount)
        {
            notificationLog.Status = NotificationResult.Failed;
            await repository.UpdateAsync(notificationLog);
            
            return NotificationResult.Failed;
        }

        try
        {
            notificationLog.AttemptCount++;
            
            await emailSender.SendEmailAsync(
                notification.To, 
                notification.Subject, 
                notification.Body);
            
            notificationLog.Status = NotificationResult.Success;
            notificationLog.SentAt = DateTime.UtcNow;
            notificationLog.ErrorMessage = null;
            
            await repository.UpdateAsync(notificationLog);
            
            return NotificationResult.Success;
        }
        catch (Exception ex)
        {
            notificationLog.ErrorMessage = ex.Message;
            
            if (notificationLog.AttemptCount >= notificationSettings.MaxAttemptCount)
            {
                notificationLog.Status = NotificationResult.Failed;
                await repository.UpdateAsync(notificationLog);
                
                return NotificationResult.Failed;
            }
            
            await repository.UpdateAsync(notificationLog);

            return NotificationResult.Retry;
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
            Status = NotificationResult.New,
            AttemptCount = 0
        };

        await repository.AddAsync(log);

        return log;
    }
}