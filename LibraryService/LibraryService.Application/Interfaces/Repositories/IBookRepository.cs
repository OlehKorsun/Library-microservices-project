using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct);
    Task<int> GetBookCountAsync(CancellationToken ct);
    Task<Book?> GetBookByIdAsync(int id, CancellationToken ct);
    Task AddNewBookAsync(Book book, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}