namespace LibraryService.Domain.Entities;

public class Order
{
    public int OrderId { get; private set; }
    public int Amount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public Book Book { get; private set; }

    private Order() { }

    public Order(int amount, DateTime createdAt, OrderStatus orderStatus, Book book)
    {
        Amount = amount;
        CreatedAt = createdAt;
        OrderStatus = orderStatus;
        Book = book;
    }
}