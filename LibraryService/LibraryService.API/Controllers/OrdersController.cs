using LibraryService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost("{bookId}")]
    public async Task<IActionResult> OrderBooksAsync([FromRoute]int bookId, [FromBody]int amount, CancellationToken ct = default)
    {
        await orderService.AddOrderAsync(bookId, amount, ct);
        return Ok();
    }
}