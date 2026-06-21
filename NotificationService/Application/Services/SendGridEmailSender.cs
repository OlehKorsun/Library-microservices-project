using Application.Interfaces.Services;
using Application.Settings;
using Domain.Exceptions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Application.Services;

public class SendGridEmailSender : IEmailSenderService
{
    private readonly SendGridSettings _settings;
    private readonly ISendGridClient _client;

    public SendGridEmailSender(IOptions<SendGridSettings> settings, ISendGridClient client)
    {
        _settings = settings.Value;
        _client = client;
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var @from = new EmailAddress(_settings.FromEmail, _settings.FromName);
        var receiver = new EmailAddress(to);
        
        var msg = MailHelper.CreateSingleEmail(from, receiver, subject, body, body);

        var response = await _client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Body.ReadAsStringAsync();
            throw new SendGridException($"SendGrid error: {response.StatusCode}. Details: {errorBody}");
        }

        Console.WriteLine($" [SendGrid] Email successfully queued for {to}");
    }
    
}