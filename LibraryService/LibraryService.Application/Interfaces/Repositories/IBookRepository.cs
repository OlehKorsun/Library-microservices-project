using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetPagedBooksAsync(int page, int pageSize, CancellationToken token);
    Task<int> GetBookCountAsync(CancellationToken token);
    Task<Book?> GetBookByIdAsync(int id, CancellationToken token);
    Task AddNewBookAsync(Book book, CancellationToken token);
    Task UpdateBookAsync(Book book, CancellationToken token);
}