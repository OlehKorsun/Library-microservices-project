namespace LibraryService.Domain.Entities;

public class Author
{
    public int AuthorId { get; private set; }
    public string Name { get; private set; }
    
    private Author() { }

    public Author(string name)
    {
        Name = name;
    }
}