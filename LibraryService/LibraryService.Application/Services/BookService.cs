using LibraryService.Application.DTOs;
using LibraryService.Application.Interfaces.Repositories;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Application.Requests;
using LibraryService.Domain.Entities;
using LibraryService.Domain.Exceptions;

namespace LibraryService.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }
    public async Task<IEnumerable<BookRequest>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllBooksAsync();
        return books.Select(a => new BookRequest()
        {
            Title = a.Title,
            AmountMustBe = a.AmountMustBe,
            CurrentAmount = a.CurrentAmount,
        });
    }

    public async Task<BookDetailedRequest> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }
        return new BookDetailedRequest()
        {
            Title = book.Title,
            AmountMustBe = book.AmountMustBe,
            CurrentAmount = book.CurrentAmount,
            Author = book.Author.Name,
            Description = book.Description,
            ISBN = book.ISBN,
            PublishedAt = book.PublishedAt,
        };
    }

    public async Task AddBooksAsync(int id, int number)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException(id);
        }
        book.AddCopies(number);
        
        await _bookRepository.UpdateBookAsync(book);
    }

    public async Task<BookRequest> AddNewBooksAsync(BookDto book)
    {
        var author = await _authorRepository.GetByNameAsync(book.Author);
        if (author == null)
        {
            author = new Author(book.Author);
            await _authorRepository.AddAsync(author);
        }

        Book newBook = new Book(
            book.Amount,
            book.Amount,
            book.Title,
            author.AuthorId,
            book.Description,
            book.ISBN,
            book.PublishedAt
            );
        
        await _bookRepository.AddNewBookAsync(newBook);

        var result = new BookRequest()
        {
            Title = newBook.Title,
            AmountMustBe = newBook.AmountMustBe,
            CurrentAmount = newBook.CurrentAmount,
        };
        return result;
    }
}