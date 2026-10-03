using System.Threading.Tasks;
using System;
using BookStore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Middlewares;

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
        catch (Exception ex)
        {
            var (status, title, level) = ex switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Not Found", LogLevel.Information),
                BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request", LogLevel.Warning),
                ConflictException => (StatusCodes.Status409Conflict, "Conflict", LogLevel.Warning),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", LogLevel.Error)
            };

            _logger.Log(level, ex, "{Title}: {Message}", title, ex.Message);

            if (context.Response.HasStarted) throw;

            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = status == 500 ? "Сталася внутрішня помилка сервера." : ex.Message
            });
        }
    }
}