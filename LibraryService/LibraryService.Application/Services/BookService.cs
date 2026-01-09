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
    public async Task<PagedBooks<BookDto>> GetPagedBooksAsync(int page, int pageSize, CancellationToken ct)
    {
        var books = await bookRepository.GetPagedBooksAsync(page, pageSize, ct);
        var a = books.Select(a => new BookDto()
        {
            BookId =  a.BookId,
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

    public async Task<BookDetailedDto> GetBookByIdAsync(int id, CancellationToken ct)
    {
        var book = await bookRepository.GetBookByIdAsync(id, ct) 
                   ?? throw new BookNotFoundException($"Book with id {id} was not found!");
        
        return new BookDetailedDto
        {
            BookId =  book.BookId,
            Title = book.Title,
            MaxCount = book.MaxCount,
            CurrentCount = book.CurrentCount,
            Author = book.Author.Name,
            Description = book.Description,
            ISBN = book.ISBN,
            PublishedAt = book.PublishedAt,
        };
    }

    public async Task AddCopiesAsync(int id, int number, CancellationToken ct)
    {
        if(number <= 0)
            throw new BadRequestException("Number of books must be greater than zero!");
        
        var book = await bookRepository.GetBookByIdAsync(id, ct) 
                   ?? throw new BookNotFoundException($"Book with id {id} was not found!");
        
        book.CurrentCount += number;
        
        await bookRepository.SaveChangesAsync(ct);
    }

    public async Task<BookDto> AddNewBooksAsync(BookRequest book, CancellationToken ct)
    {
        var author = await authorRepository.GetByNameAsync(book.Author, ct) 
                     ?? new Author{Name = book.Author};
        
        if(author.AuthorId == 0) 
            await authorRepository.AddAsync(author, ct);
        
        Book newBook = new Book
        {
            CurrentCount = book.CurrentCount,
            MaxCount = book.CurrentCount,
            Title = book.Title,
            AuthorId =  author.AuthorId,
            Description = book.Description,
            ISBN = book.ISBN,
            PublishedAt = book.PublishedAt
        };
        
        await bookRepository.AddNewBookAsync(newBook, ct);
        await bookRepository.SaveChangesAsync(ct);

        var result = new BookDto
        {
            BookId =   newBook.BookId,
            Title = newBook.Title,
            MaxCount = newBook.MaxCount,
            CurrentCount = newBook.CurrentCount,
        };
        return result;
    }
}