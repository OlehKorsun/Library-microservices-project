namespace LibraryService.Application.DTOs;

public class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
}