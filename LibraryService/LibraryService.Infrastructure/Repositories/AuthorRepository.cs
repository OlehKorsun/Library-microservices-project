using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    
    private readonly LibraryDbContext _dbContext;

    public AuthorRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Author?> GetByNameAsync(string name)
    {
        var author = await _dbContext.Authors.FindAsync(name);
        return author;
    }

    public async Task AddAsync(Author author)
    {
        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync();
    }
}