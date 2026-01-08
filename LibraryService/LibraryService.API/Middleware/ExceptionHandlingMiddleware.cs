using LibraryService.Domain.Exceptions;

namespace LibraryService.API.Middleware;

public class ExceptionHandlingMiddleware (
    RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    
    
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (BookNotFoundException ex)
        {
            logger.LogWarning(ex.Message);
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning(ex, "Unauthorized access");
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status403Forbidden, "Access Denied");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
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