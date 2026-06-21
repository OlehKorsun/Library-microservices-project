using Domain.Exceptions;

namespace Api.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger
    )
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception,
        int? statusCodeOverride = null, string? messageOverride = null)
    {
        int statusCode;
        string message;

        switch (exception)
        {
            case NotFoundException _:
                statusCode = StatusCodes.Status404NotFound;
                message = "Notification was not found!";
                break;
            default:
                statusCode = 500;
                message = "Internal Server Error";
                break;
        } 
        
        if (statusCodeOverride.HasValue)
            statusCode = statusCodeOverride.Value;
        if (messageOverride != null)
            message = messageOverride;
        
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var response = new
        {
            error = new
            {
                message,
                type = exception.GetType().Name,
            }
        };
        
        var json = System.Text.Json.JsonSerializer.Serialize(response);
        await httpContext.Response.WriteAsync(json);
    }
}