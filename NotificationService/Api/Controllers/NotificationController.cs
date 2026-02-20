using Application.Interfaces.Services;
using Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(INotificationService service) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetNotificationAsync(CancellationToken ct = default)
    {
        var notifications = await service.GetNotificationsAsync(ct);
        return Ok(notifications);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetNotificationAsync([FromRoute]int id, CancellationToken ct = default)
    {
        var notification = await service.GetNotificationByIdAsync(id, ct);
        return Ok(notification);
    }

    [HttpPost]
    public async Task<IActionResult> AddNotificationAsync([FromBody]NotificationRequest request, CancellationToken ct = default)
    {
        await  service.AddNotificationAsync(request, ct);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNotificationAsync([FromBody] NotificationRequest request,
        CancellationToken ct = default)
    {
        await service.UpdateNotificationAsync(request, ct);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteNotificationAsync([FromRoute] int id, CancellationToken ct = default)
    {
        await service.DeleteNotificationAsync(id, ct);
        return Ok();
    }
}