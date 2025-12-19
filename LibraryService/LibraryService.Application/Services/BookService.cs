using LibraryService.Application.DTOs;
using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Application.Requests;

namespace LibraryService.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IOrderRepository _orderRepository;

    public BookService(IBookRepository bookRepository, IOrderRepository orderRepository)
    {
        _bookRepository = bookRepository;
        _orderRepository = orderRepository;
    }
    public async Task<IEnumerable<BookRequest>> GetAllBooksAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<BookDetailedRequest> GetBookByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddBooksAsync(int id, int number)
    {
        
    }

    public async Task<BookRequest> AddNewBooksAsync(BookDTO book)
    {
        throw new NotImplementedException();
    }
}