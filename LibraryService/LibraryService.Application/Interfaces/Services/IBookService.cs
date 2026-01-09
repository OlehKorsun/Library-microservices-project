using LibraryService.Application.DTOs;
using LibraryService.Application.Requests;

namespace LibraryService.Application.Interfaces.Services;

public interface IBookService
{
    Task<PagedBooks<BookDto>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct);
    Task<BookDetailedDto> GetBookByIdAsync(int id, CancellationToken ct);
    Task AddCopiesAsync(int id, int number, CancellationToken ct);
    Task<BookDto> AddNewBooksAsync(BookRequest book, CancellationToken ct);
}