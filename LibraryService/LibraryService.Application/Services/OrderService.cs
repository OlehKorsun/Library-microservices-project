using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Domain.Entities;
using LibraryService.Domain.Exceptions;

namespace LibraryService.Application.Services;

public class OrderService(
    IOrderRepository orderRepository, IBookRepository bookRepository) : IOrderService
{
    public async Task AddOrderAsync(int bookId, int amount, CancellationToken ct = default)
    {
        var book = await bookRepository.GetBookByIdAsync(bookId, ct) 
                   ?? throw new BookNotFoundException($"Book with id {bookId} was not found!");
        
        var order = new Order
        {
            Count =  amount,
            CreatedAt = DateTime.Now,
            BookId =  bookId
        };

        await orderRepository.AddOrderAsync(order, ct);
        await orderRepository.SaveChangesAsync(ct);
    }
}