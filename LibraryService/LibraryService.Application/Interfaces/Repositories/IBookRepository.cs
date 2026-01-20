using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct = default);
    Task<int> GetBookCountAsync(CancellationToken ct = default);
    Task<Book?> GetBookByIdAsync(int id, CancellationToken ct = default);
    Task AddNewBookAsync(Book book, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}