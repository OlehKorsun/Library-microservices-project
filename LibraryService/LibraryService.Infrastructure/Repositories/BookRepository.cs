using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    
    private readonly LibraryDbContext _dbContext;

    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        var books = await _dbContext.Books.ToListAsync();
        return books;
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        return book;
    }

    public async Task AddNewBookAsync(Book book)
    {
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();
    }
}