namespace Domain.Entities;

public class Rental
{
    public int Id { get; set; }
    
    public DateOnly RentedAt { get; set; }
    public DateOnly RentedUntil { get; set; }

    public bool IsReturned { get; set; } = false;
    
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}