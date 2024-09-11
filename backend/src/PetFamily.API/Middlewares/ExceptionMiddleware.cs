using PetFamily.API.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            var error = Error.Failure("server.internal", ex.Message);
            var envelope = Envelope.Error(error);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(envelope);
        }
    }
}