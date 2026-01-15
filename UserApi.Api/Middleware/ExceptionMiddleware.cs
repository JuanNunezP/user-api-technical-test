using System.Net;
using System.Text.Json;
using Npgsql;

namespace UserApi.Api.Middleware;

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
            _logger.LogError(ex, "Unhandled exception");

            context.Response.ContentType = "application/json";

            var (statusCode, message) = ex switch
            {
                ArgumentException =>
                    ((int)HttpStatusCode.BadRequest, ex.Message),

                PostgresException pg when pg.SqlState == "23503" =>
                    ((int)HttpStatusCode.BadRequest, "Error: Data in tables with foreign keys is incorrect"),

                _ =>
                    ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };

            context.Response.StatusCode = statusCode;

            var response = new { error = message };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
