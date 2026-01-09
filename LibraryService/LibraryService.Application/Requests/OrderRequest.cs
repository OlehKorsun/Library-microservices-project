namespace LibraryService.Application.Requests;

public sealed class OrderRequest
{
    public int Id { get; set; }
    public int Count { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public string BookTitle { get; set; }
}