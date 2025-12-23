using LibraryService.Application.Requests;
using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
}