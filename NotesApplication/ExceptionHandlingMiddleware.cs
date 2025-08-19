using System.Text.Json;

namespace NotesApplication;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case KeyNotFoundException:
                    _logger.LogWarning($"Not found - {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
                    break;
                default:
                    _logger.LogError(ex, "Error occured");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        { error = $"Internal server error - {ex.Message}" }));
                    break;
            }
            
        }
    }
}