namespace Domain.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname{ get; set; }
    public string Email { get; set; }
    public ICollection<Rental> Rentals { get; private set; } = [];
}