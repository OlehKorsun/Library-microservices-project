namespace LibraryService.Application.DTOs;

public sealed class OrderDto
{
    public int OrderId { get; set; }
    public int Count { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public string BookTitle { get; set; }
}