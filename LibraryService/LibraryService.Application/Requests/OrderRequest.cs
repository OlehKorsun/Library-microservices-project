namespace LibraryService.Application.Requests;

public class OrderRequest
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public string BookTitle { get; set; }
}