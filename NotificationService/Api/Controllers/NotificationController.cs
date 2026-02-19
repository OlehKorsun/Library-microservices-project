using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(INotificationService service) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetNotification()
    {
        var notifications = await service.GetNotifications();
        return Ok(notifications);
    }
}