using LibraryService.Application.DTOs;
using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Domain.Entities;
using LibraryService.Domain.Exceptions;

namespace LibraryService.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;

    public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
    }
    public async Task<OrderDto> AddOrderAsync(int bookId, int amount)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId);

        if (book == null)
        {
            throw new BookNotFoundException(bookId);
        }
        
        var order = new Order(
            amount,
            book
        );

        await _orderRepository.AddOrderAsync(order);
        var result = new OrderDto()
        {
            Amount = order.Amount,
            CreatedAt = order.CreatedAt,
            BookTitle = book.Title,
            Status = order.OrderStatus.ToString(),
        };
        return result;
    }
}