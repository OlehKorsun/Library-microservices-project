using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct)
    {
        var books = await dbContext.Books
            .OrderBy(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return books;
    }

    public async Task<int> GetBookCountAsync(CancellationToken ct)
    {
        var count = await dbContext.Books.CountAsync(ct);
        return count;
    }

    public async Task<Book?> GetBookByIdAsync(int id, CancellationToken ct)
    {
        var book = await dbContext.Books.FindAsync([id], ct);
        return book;
    }

    public async Task AddNewBookAsync(Book book, CancellationToken ct)
    {
        await dbContext.Books.AddAsync(book, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}