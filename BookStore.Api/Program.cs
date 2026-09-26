using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using BookStore.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// 1. Реєстрація DbContext з підключенням до PostgreSQL
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Прив'язка інтерфейсу контексту до зареєстрованого DbContext
builder.Services.AddScoped<IBookStoreDbContext>(sp => sp.GetRequiredService<BookStoreDbContext>());

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ILogsService, LogsService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Сервіси, які працюють з базою, мають бути Scoped
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddTransient<IBookValidationService, BookValidationService>();

var app = builder.Build();

app.UseMiddleware<ExceptionLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();