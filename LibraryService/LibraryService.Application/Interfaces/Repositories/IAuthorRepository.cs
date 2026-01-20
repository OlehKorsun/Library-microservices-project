using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByNameAsync(string name, CancellationToken ct = default);
    Task AddAsync(Author author, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}