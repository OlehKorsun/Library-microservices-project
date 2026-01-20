using LibraryService.Application.DTOs;

namespace LibraryService.Application.Interfaces.Services;

public interface IOrderService
{
    Task AddOrderAsync(int bookId, int amount, CancellationToken ct);
}