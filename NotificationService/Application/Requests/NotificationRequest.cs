namespace Application.Requests;

public class NotificationRequest
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body  { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public string BookTitle { get; set; } = string.Empty;
}