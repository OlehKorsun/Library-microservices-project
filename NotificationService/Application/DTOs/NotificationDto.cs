namespace Application.DTOs;

public record NotificationDto
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public DateTime? SentAt { get; set; }
    public bool? IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}