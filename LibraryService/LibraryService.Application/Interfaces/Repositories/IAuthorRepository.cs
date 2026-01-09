using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByNameAsync(string name, CancellationToken ct);
    Task AddAsync(Author author, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}