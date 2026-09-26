using BookStore.Domain.Exceptions;

namespace BookStore.Middlewares
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLoggingMiddleware> _logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
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
            catch (InfoExceptions ex)
            {
                _logger.LogInformation(ex, "[INFO LOG] : {Message}", ex.Message);
                await WriteFalseResponseAsync(context);
            }
            catch (DebugException ex)
            {
                _logger.LogDebug(ex, "[DEBUG LOG] : {Message}", ex.Message);
                await WriteFalseResponseAsync(context);
            }
            catch (WarningException ex)
            {
                _logger.LogWarning(ex, "[WARNING LOG] : {Message}", ex.Message);
                await WriteFalseResponseAsync(context);
            }
            catch (CustomErrorException ex)
            {
                _logger.LogError(ex, "[ERROR LOG] : {Message}", ex.Message);
                await WriteFalseResponseAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[UNHANDLED EXCEPTION] : {Message}", ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
                {
                    error = ex.Message,
                    details = ex.InnerException?.Message
                }));
            }
        }

        private async Task WriteFalseResponseAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("false");
        }
    }
}