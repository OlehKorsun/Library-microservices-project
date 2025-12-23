using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Requests;
using LibraryService.Domain.Entities;

namespace LibraryService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly LibraryDbContext _dbContext;

    public OrderRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrderAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }
}