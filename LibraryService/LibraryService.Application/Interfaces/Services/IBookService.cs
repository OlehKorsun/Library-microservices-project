using LibraryService.Application.DTOs;
using LibraryService.Application.Requests;

namespace LibraryService.Application.Interfaces.Services;

public interface IBookService
{
    Task<PagedBooks<BookDto>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct = default);
    Task<BookDetailedDto> GetBookByIdAsync(int id, CancellationToken ct = default);
    Task AddCopiesAsync(int id, int number, CancellationToken ct = default);
    Task AddNewBooksAsync(BookRequest book, CancellationToken ct = default);
}