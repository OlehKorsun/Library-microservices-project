using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Requests;
using LibraryService.Domain.Entities;

namespace LibraryService.Infrastructure.Repositories;

public class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    
    public async Task AddOrderAsync(Order order, CancellationToken token)
    {
        await dbContext.Orders.AddAsync(order,  token);
        await dbContext.SaveChangesAsync(token);
    }
}