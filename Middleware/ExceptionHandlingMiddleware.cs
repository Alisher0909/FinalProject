using System.Net;
using System.Text.Json;

namespace SkillHub.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                await WriteResponseAsync(context, HttpStatusCode.Unauthorized, "Ruxsat berilmagan: JWT token mavjud emas yoki noto‘g‘ri.");
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                await WriteResponseAsync(context, HttpStatusCode.Forbidden, "Ta’qiqlangan: Sizda ushbu amalni bajarishga ruxsat yo‘q.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Exception caught: {Message}", ex.Message);
            await WriteResponseAsync(context, HttpStatusCode.InternalServerError, "Ichki serverda xatolik yuz berdi.", ex.Message);
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, string message, string? error = null)
    {
        if (context.Response.HasStarted) return;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = message,
            Error = error
        };

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}