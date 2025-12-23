using LibraryService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("{bookId}")]
    public async Task<IActionResult> OrderBooksAsync([FromRoute]int bookId, [FromBody]int amount)
    {
        var order = await _orderService.AddOrderAsync(bookId, amount);
        return CreatedAtAction(
            nameof(OrderBooksAsync),
            new {bookId},
            order
        );
    }

}