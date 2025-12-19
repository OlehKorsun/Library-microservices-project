using LibraryService.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using LibraryService.Application.Interfaces.Services;

namespace LibraryService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync([FromRoute]int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return Ok(book);
    }

    [HttpPatch("{id}/add")]
    public async Task<IActionResult> AddBooksAsync([FromRoute] int id, [FromBody] int number)
    {
        await _bookService.AddBooksAsync(id, number);
        return NoContent();
    }


    [HttpPost]
    public async Task<IActionResult> AddNewBooksAsync([FromBody] BookDTO book)
    {
        var createdBook = await _bookService.AddNewBooksAsync(book);
        return CreatedAtAction(
            nameof(GetBookByIdAsync),
            new {id = createdBook},
            createdBook
        );
    }
}