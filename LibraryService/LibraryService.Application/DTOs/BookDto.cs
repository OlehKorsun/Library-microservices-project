namespace LibraryService.Application.DTOs;

public class BookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public DateOnly PublishedAt { get; set; }
    public int Amount { get; set; }
}