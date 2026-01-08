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
    
    
    public async Task<PagedBooks<BookDto>> GetPagedBooksAsync(int page, int pageSize, CancellationToken token)
    {
        var books = await bookRepository.GetPagedBooksAsync(page, pageSize, token);
        var a = books.Select(a => new BookDto()
        {
            BookId =  a.BookId,
            Title = a.Title,
            MaxCount = a.MaxCount,
            CurrentCount = a.CurrentCount,
        });

        var count = await bookRepository.GetBookCountAsync(token);

        return new PagedBooks<BookDto>()
        {
            Books = a,
            Page =  page,
            PageSize = pageSize,
            TotalCount = count
        };
    }

    public async Task<BookDetailedDto> GetBookByIdAsync(int id, CancellationToken token)
    {
        var book = await bookRepository.GetBookByIdAsync(id, token) 
                   ?? throw new BookNotFoundException($"Book with id {id} was not found!");
        
        return new BookDetailedDto()
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

    public async Task AddBooksAsync(int id, int number, CancellationToken token)
    {
        var book = await bookRepository.GetBookByIdAsync(id, token) 
                   ?? throw new BookNotFoundException($"Book with id {id} was not found!");
        
        if(number <= 0)
            throw new BadRequestException($"Number of books must be greater than zero!");
        
        book.CurrentCount += number;
        
        await bookRepository.UpdateBookAsync(book, token);
    }

    public async Task<BookDto> AddNewBooksAsync(BookRequest book, CancellationToken token)
    {
        var author = await authorRepository.GetByNameAsync(book.Author, token);
        if(author == null)
        {
            author = new Author(){ Name = book.Author };
            await authorRepository.AddAsync(author, token);
        }
        
        Book newBook = new Book()
        {
            CurrentCount = book.CurrentCount,
            MaxCount = book.CurrentCount,
            Title = book.Title,
            AuthorId =  author.AuthorId,
            Description = book.Description,
            ISBN = book.ISBN,
            PublishedAt = book.PublishedAt
        };
        
        await bookRepository.AddNewBookAsync(newBook, token);

        var result = new BookDto()
        {
            BookId =   newBook.BookId,
            Title = newBook.Title,
            MaxCount = newBook.MaxCount,
            CurrentCount = newBook.CurrentCount,
        };
        return result;
    }
}