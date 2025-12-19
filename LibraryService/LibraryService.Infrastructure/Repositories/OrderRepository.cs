using LibraryService.Application.Interfaces.Repositories;

namespace LibraryService.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly LibraryDbContext _dbContext;

    public OrderRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}