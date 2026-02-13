using Application.Interfaces.Services;

namespace Application.Services;

public class EmailSenderService : IEmailSenderService
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        Console.WriteLine($"Sending email to {email} with subject {subject}...");
        return Task.CompletedTask;
    }
}