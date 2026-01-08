using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    
    public async Task<IEnumerable<Book>> GetPagedBooksAsync(int page, int pageSize, CancellationToken token)
    {
        var books = await dbContext.Books
            .OrderBy(b => b.BookId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
        return books;
    }

    public async Task<int> GetBookCountAsync(CancellationToken token)
    {
        var count = await dbContext.Books.CountAsync(token);
        return count;
    }

    public async Task<Book?> GetBookByIdAsync(int id, CancellationToken token)
    {
        var book = await dbContext.Books.FindAsync(id);
        return book;
    }

    public async Task AddNewBookAsync(Book book, CancellationToken token)
    {
        await dbContext.Books.AddAsync(book, token);
        await dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateBookAsync(Book book, CancellationToken token)
    {
        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync(token);
    }
}