using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class AuthorRepository(AppDbContext dbContext) : IAuthorRepository
{
    
    public async Task<Author?> GetByNameAsync(string name, CancellationToken token)
    {
        var author = await dbContext.Authors.FirstOrDefaultAsync(a => a.Name == name, token);
        return author;
    }

    public async Task AddAsync(Author author, CancellationToken token)
    {
        dbContext.Authors.Add(author);
        await dbContext.SaveChangesAsync(token);
    }
}