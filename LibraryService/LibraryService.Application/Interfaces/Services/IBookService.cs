using LibraryService.Application.DTOs;
using LibraryService.Application.Requests;

namespace LibraryService.Application.Interfaces.Services;

public interface IBookService
{
    Task<PagedBooks<BookDto>> GetPagedBooksAsync(int page, int pageSize, CancellationToken token);
    Task<BookDetailedDto> GetBookByIdAsync(int id, CancellationToken token);
    Task AddBooksAsync(int id, int number, CancellationToken token);
    Task<BookDto> AddNewBooksAsync(BookRequest book, CancellationToken token);
}