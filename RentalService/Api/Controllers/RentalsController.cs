using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController(IRentalService rentalService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRentalsAsync(CancellationToken ct = default)
    {
        var  rentals = await rentalService.GetRentalsAsync(ct);
        return Ok(rentals);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetRentalsByUserIdAsync(int userId, CancellationToken ct = default)
    {
        var rentals = await rentalService.GetRentalsByUserIdAsync(userId, ct);
        return Ok(rentals);
    }

    [HttpPost("rent")]
    public async Task<IActionResult> PostRentalAsync([FromBody]RentalDto rentalDto, CancellationToken ct = default)
    {
        await  rentalService.RentAsync(rentalDto, ct);
        return Created();
    }

    [HttpPut("return/{rentId:int}")]
    public async Task<IActionResult> ReturnBookAsync(int rentalId, CancellationToken ct = default)
    {
        await  rentalService.ReturnBookAsync(rentalId, ct);
        return Ok();
    }

    [HttpPut("extend")]
    public async Task<IActionResult> ExtendReturnDateAsync([FromBody]ExtendDateDto dto, CancellationToken ct = default)
    {
        await   rentalService.ExtendReturnDateAsync(dto, ct);
        return Ok();
    }
}