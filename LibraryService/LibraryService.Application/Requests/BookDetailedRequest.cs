namespace LibraryService.Application.Requests;

public class BookDetailedRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CurrentAmount { get; set; }
    public int AmountMustBe { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public DateTime PublishedAt { get; set; }
}