namespace LibraryService.Application.Requests;

public sealed class BookRequest
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int CurrentCount { get; set; }
    public int MaxCount { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public DateTime PublishedAt { get; set; }
    public string Description { get; set; }
}