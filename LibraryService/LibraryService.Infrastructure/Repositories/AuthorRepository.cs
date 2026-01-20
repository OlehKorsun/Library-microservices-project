using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    public async Task<Author?> GetByNameAsync(string name, CancellationToken ct = default)
    {
        var author = await dbContext.Authors.FirstOrDefaultAsync(a => a.Name == name, ct);
        return author;
    }

    public async Task AddAsync(Author author, CancellationToken ct = default)
    {
        await dbContext.Authors.AddAsync(author, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}