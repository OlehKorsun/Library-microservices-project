namespace LibraryService.Domain.Entities;

public class Author
{
    public int Id { get; init; }
    public string Name { get; init; }
    public ICollection<Book> Books { get; private set; } = new List<Book>();
}