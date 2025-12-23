using System.Runtime.CompilerServices;

namespace LibraryService.Domain.Entities;

public class Book
{
    public int BookId { get; private set; }
    public int CurrentAmount { get; private set; }
    public int AmountMustBe { get; private set; }
    public string Title { get; private set; }
    public Author Author { get; private set; }
    public string Description { get; private set; }
    public string ISBN { get; private set; }
    public DateOnly PublishedAt { get; private set; }
    
    private Book(){ }
    
    public Book(int currentAmount, int amountMustBe, string title, Author author, string description, string isbn, DateOnly publishedAt)
    {
        CurrentAmount = currentAmount;
        AmountMustBe = amountMustBe;
        Title = title;
        Author = author;
        Description = description;
        ISBN = isbn;
        PublishedAt = publishedAt;
    }
    
    public void AddCopies(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero");

        CurrentAmount += amount;
    }
}




