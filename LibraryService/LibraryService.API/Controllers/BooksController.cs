using Microsoft.AspNetCore.Mvc;
using LibraryService.Application.Interfaces.Services;
using LibraryService.Application.Requests;

namespace LibraryService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var books = await bookService.GetPagedBooksAsync(page, pageSize, ct);
        return Ok(books);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync([FromRoute]int id, CancellationToken ct = default)
    {
        var book = await bookService.GetBookByIdAsync(id, ct);
        return Ok(book);
    }

    [HttpPatch("{id}/add")]
    public async Task<IActionResult> AddCopiesAsync([FromRoute] int id, [FromBody] int number, CancellationToken ct = default)
    {
        await bookService.AddCopiesAsync(id, number, ct);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddNewBooksAsync([FromBody] BookRequest book, CancellationToken ct =  default)
    {
        await bookService.AddNewBooksAsync(book, ct);
        return Ok();
    }
}