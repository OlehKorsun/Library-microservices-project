using LibraryService.Application.DTOs;
using LibraryService.Application.Requests;
using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> AddOrderAsync(int bookId, int amount, CancellationToken token);
}