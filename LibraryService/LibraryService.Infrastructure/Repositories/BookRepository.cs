using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Requests;
using LibraryService.Domain.Entities;

namespace LibraryService.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    
    private readonly LibraryDbContext _dbContext;

    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Book> GetBookByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddBookAsync(Book book)
    {
        throw new NotImplementedException();
    }
}