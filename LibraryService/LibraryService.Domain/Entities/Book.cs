namespace LibraryService.Domain.Entities;

public class Book
{
    public int BookId { get; init; }
    public int CurrentCount { get; set; }
    public int MaxCount { get; init; }
    public string Title { get; init; }
    public Author Author { get; init; }
    public int AuthorId { get; init; }
    public string Description { get; init; }
    public string ISBN { get; init; }
    public DateTime PublishedAt { get; init; }
}




