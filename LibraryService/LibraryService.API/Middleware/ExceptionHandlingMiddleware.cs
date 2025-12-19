using LibraryService.Domain.Exceptions;

namespace LibraryService.API.Middleware;

public class ExceptionHandlingMiddleware
{
    
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (AppException ex)
        {
            _logger.LogWarning(ex, "Domain exception occured");
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access");
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status403Forbidden, "Access Denied");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, int? statusCodeOverride = null, string? messageOverride = null)
    {
        int statusCode;
        string message;

        switch (exception)
        {
            case BookNotFoundException _:
                statusCode = StatusCodes.Status404NotFound;
                message = "Book not found!";
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

        var responce = new
        {
            error = new
            {
                message,
                type = exception.GetType().Name,
            }
        };
        
        var json = System.Text.Json.JsonSerializer.Serialize(responce);
        await httpContext.Response.WriteAsync(json);


    }
}