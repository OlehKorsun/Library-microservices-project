using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByNameAsync(string name, CancellationToken token);
    Task AddAsync(Author author, CancellationToken token);
}