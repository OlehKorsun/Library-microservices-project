using LibraryService.Domain.Enums;

namespace LibraryService.Domain.Entities;

public class Order
{
    public int OrderId { get; init; }
    public int Count { get; init; }
    public DateTime CreatedAt { get; init; }
    public OrderStatus OrderStatus { get; init; }
    public int BookId { get; init; }
    public Book? Book { get; init; }
}