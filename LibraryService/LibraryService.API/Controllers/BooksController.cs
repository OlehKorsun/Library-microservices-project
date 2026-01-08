using Microsoft.AspNetCore.Mvc;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Application.Requests;

namespace LibraryService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync(int page = 1, int pageSize = 10, CancellationToken token = default)
    {
        var books = await bookService.GetPagedBooksAsync(page, pageSize, token);
        return Ok(books);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync([FromRoute]int id, CancellationToken token)
    {
        var book = await bookService.GetBookByIdAsync(id, token);
        return Ok(book);
    }

    [HttpPatch("{id}/add")]
    public async Task<IActionResult> AddBooksAsync([FromRoute] int id, [FromBody] int number, CancellationToken token)
    {
        await bookService.AddBooksAsync(id, number, token);
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> AddNewBooksAsync([FromBody] BookRequest book, CancellationToken token)
    {
        var createdBook = await bookService.AddNewBooksAsync(book, token);
        return Ok(createdBook);
    }
}