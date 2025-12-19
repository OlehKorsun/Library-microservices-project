namespace LibraryService.Domain.Entities;

public class OrderStatus
{
    public int OrderStatusId { get; private set; }
    public string Title { get; private set; }
    
    private OrderStatus() { }

    public OrderStatus(string title)
    {
        Title = title;
    }
}