using LibraryService.Domain.Entities;

namespace LibraryService.Application.Interfaces.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByNameAsync(string name);
    Task AddAsync(Author author);
}