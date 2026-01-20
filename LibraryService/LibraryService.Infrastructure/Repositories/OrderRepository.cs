using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;

namespace LibraryService.Infrastructure.Repositories;

public class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    public async Task AddOrderAsync(Order order, CancellationToken ct = default)
    {
        await dbContext.Orders.AddAsync(order,  ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}