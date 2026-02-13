using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services;

public class NotificationOrchestrator(IEmailSenderService emailSender, INotificationRepository repository)
{
    public async Task HandleBookOverdueAsync(BookOverdueDto bookOverdue)
    {
        var subject = "The deadline for submitting the book has passed!";
        var body = $"Hi! You forget to return the {bookOverdue.BookTitle} book to the library! The deadline was: {bookOverdue.DueDate:d}";

        var log = new NotificationLog
        {
            Subject = subject,
            Body = body,
            RecipientEmail = bookOverdue.Email,
        };

        try
        {
            await emailSender.SendEmailAsync(bookOverdue.Email, subject, body);
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