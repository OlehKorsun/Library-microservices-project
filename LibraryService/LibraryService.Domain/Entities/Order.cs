using LibraryService.Domain.Enums;

namespace LibraryService.Domain.Entities;

public class Order
{
    public int OrderId { get; private set; }
    public int Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    
    public int BookId { get; private set; }
    public Book? Book { get; private set; }

    private Order() { }

    public Order(int amount, int bookId)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0", nameof(amount));
        }

        Amount = amount;
        CreatedAt = DateTime.UtcNow;
        OrderStatus = OrderStatus.Created;
        BookId = bookId;
    }
}