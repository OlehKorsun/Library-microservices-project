namespace LibraryService.Application.DTOs;

public class OrderDto
{
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public string BookTitle { get; set; }
}