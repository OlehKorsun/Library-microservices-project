using LibraryService.Application.DTOs;
using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Application.Requests;
using LibraryService.Domain.Entities;
using LibraryService.Domain.Exceptions;

namespace LibraryService.Application.Services;

public class BookService(
    IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    public async Task<PagedBooks<BookDto>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var books = await bookRepository.GetPagedBooksAsync(page, pageSize, ct);
        var a = books.Select(a => new BookDto()
        {
            BookId =  a.Id,
            Title = a.Title,
            MaxCount = a.MaxCount,
            CurrentCount = a.CurrentCount,
        });

        var count = await bookRepository.GetBookCountAsync(ct);

        return new PagedBooks<BookDto>
        {
            Books = a,
            Page =  page,
            PageSize = pageSize,
            TotalCount = count
        };
    }

    public async Task<BookDetailedDto> GetBookByIdAsync(int id, CancellationToken ct = default)
    {
        var book = await bookRepository.GetBookByIdAsync(id, ct) 
                   ?? throw new BookNotFoundException($"Book with id {id} was not found!");
        
        return new BookDetailedDto
        {
            BookId =  book.Id,
            Title = book.Title,
            MaxCount = book.MaxCount,
            CurrentCount = book.CurrentCount,
            Author = book.Author.Name,
            Description = book.Description,
            ISBN = book.ISBN,
            PublishedAt = book.PublishedAt,
        };
    }

    public async Task AddCopiesAsync(int id, int number, CancellationToken ct = default)
    {
        if(number <= 0)
            throw new BadRequestException("Number of books must be greater than zero!");
        
        var book = await bookRepository.GetBookByIdAsync(id, ct) 
                   ?? throw new BookNotFoundException($"Book with id {id} was not found!");
        
        book.CurrentCount += number;
        
        await bookRepository.SaveChangesAsync(ct);
    }

    public async Task AddNewBooksAsync(BookRequest book, CancellationToken ct = default)
    {
        var author = await authorRepository.GetByNameAsync(book.Author, ct) 
                     ?? new Author{Name = book.Author};
        
        if(author.Id == 0) 
            await authorRepository.AddAsync(author, ct);
        
        Book newBook = new Book
        {
            CurrentCount = book.CurrentCount,
            MaxCount = book.CurrentCount,
            Title = book.Title,
            Author =  author,
            Description = book.Description,
            ISBN = book.ISBN,
            PublishedAt = book.PublishedAt
        };
        
        await bookRepository.AddNewBookAsync(newBook, ct);
        await bookRepository.SaveChangesAsync(ct);
    }
}