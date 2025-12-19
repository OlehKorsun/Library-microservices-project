using LibraryService.Application.DTOs;
using LibraryService.Application.Requests;

namespace LibraryService.Application.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<BookRequest>> GetAllBooksAsync();
    Task<BookDetailedRequest> GetBookByIdAsync(int id);
    Task AddBooksAsync(int id, int number);
    Task<BookRequest> AddNewBooksAsync(BookDTO book);
}