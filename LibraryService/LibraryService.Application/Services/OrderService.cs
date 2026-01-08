using LibraryService.Application.DTOs;
using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Domain.Entities;
using LibraryService.Domain.Exceptions;

namespace LibraryService.Application.Services;

public class OrderService(
    IOrderRepository orderRepository, IBookRepository bookRepository) : IOrderService
{

    
    public async Task<OrderDto> AddOrderAsync(int bookId, int amount, CancellationToken token)
    {
        var book = await bookRepository.GetBookByIdAsync(bookId, token);

        if (book == null)
        {
            throw new BookNotFoundException($"Book with id {bookId} was not found!");
        }
        
        var order = new Order()
        {
            Count =  amount,
            CreatedAt = DateTime.Now,
            BookId =  bookId
        };

        await orderRepository.AddOrderAsync(order, token);
        var result = new OrderDto()
        {
            OrderId =  order.OrderId,
            Count = order.Count,
            CreatedAt = order.CreatedAt,
            BookTitle = book.Title,
            Status = order.OrderStatus.ToString(),
        };
        return result;
    }
}