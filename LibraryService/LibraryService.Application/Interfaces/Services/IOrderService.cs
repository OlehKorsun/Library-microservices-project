using LibraryService.Application.DTOs;

namespace LibraryService.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> AddOrderAsync(int bookId, int amount, CancellationToken ct);
}