using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task AddNewBookAsync(Book book);
    Task UpdateBookAsync(Book book);
}