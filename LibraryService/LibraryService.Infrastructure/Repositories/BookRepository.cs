using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class BookRepository(AppDbContext dbContext) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var books = await dbContext.Books
            .OrderBy(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return books;
    }

    public async Task<int> GetBookCountAsync(CancellationToken ct = default)
    {
        var count = await dbContext.Books.CountAsync(ct);
        return count;
    }

    public async Task<Book?> GetBookByIdAsync(int id, CancellationToken ct = default)
    {
        var book = await dbContext.Books.FindAsync([id], ct);
        return book;
    }

    public async Task AddNewBookAsync(Book book, CancellationToken ct = default)
    {
        await dbContext.Books.AddAsync(book, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}