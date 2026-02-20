using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class NotificationOrchestrator(IEmailSenderService emailSender, INotificationRepository repository)
{
    public async Task HandleBookOverdueAsync(NotificationDto notification)
    {
        var subject = "The deadline for submitting the book has passed!";
        var body = $"Hi! You forget to return the {notification.BookTitle} book to the library! The deadline was: {notification.DueDate:d}";

        var log = new NotificationLog
        {
            Subject = subject,
            Body = body,
            To = notification.To,
        };

        try
        {
            await emailSender.SendEmailAsync(notification.To, subject, body);
            log.IsSuccess = true;
        }
        catch (Exception ex)
        {
            log.IsSuccess = false;
            log.ErrorMessage = ex.Message;
            Console.WriteLine("An error occured while sending the email!");
        }
        
        await  repository.AddAsync(log);

    }

}