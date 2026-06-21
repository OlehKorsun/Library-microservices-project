using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RentalRepository(AppDbContext context) : IRentalRepository
{
    public async Task<IEnumerable<Rental>> GetRentalsAsync(CancellationToken ct = default)
    {
        var rentals = await context.Rentals.AsNoTracking().ToListAsync(ct);
        return rentals;
    }

    public async Task<IEnumerable<Rental>> GetRentalsByUserIdAsync(int userId, CancellationToken ct = default)
    {
        var rentals = await context.Rentals
            .AsNoTracking()
            .Where(a => a.ClientId == userId)
            .ToListAsync(ct);
        return rentals;
    }

    public Task CreateRentalAsync(Rental rental, CancellationToken ct = default)
    {
        context.Rentals.Add(rental);
        return Task.CompletedTask;
    }

    public Task UpdateRentalAsync(Rental rental, CancellationToken ct = default)
    {
        context.Rentals.Update(rental);
        return Task.CompletedTask;
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        await context.SaveChangesAsync(ct);
    }
}