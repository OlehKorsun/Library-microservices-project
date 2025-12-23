namespace LibraryService.Application.Requests;

public class BookRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CurrentAmount { get; set; }
    public int AmountMustBe { get; set; }
}