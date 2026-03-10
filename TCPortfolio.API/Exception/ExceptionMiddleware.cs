using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        // Default to 500 Internal Server Error
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = "An internal server error occurred.";

        // You can customize based on exception type (e.g., DbUpdateException)
        if (exception is KeyNotFoundException) statusCode = (int)HttpStatusCode.NotFound;

        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = message,
            // Include details only in development mode for security
            Detail = exception.Message 
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}