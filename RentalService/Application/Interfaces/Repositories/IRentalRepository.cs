using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IRentalRepository
{
    Task<IEnumerable<Rental>> GetRentalsAsync(CancellationToken ct = default);
    Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(int userId, CancellationToken ct = default);
    Task CreateRentalAsync(Rental rental, CancellationToken ct = default);
    Task UpdateRentalAsync(Rental rental, CancellationToken ct = default);
    Task SaveAsync(CancellationToken ct = default);
}