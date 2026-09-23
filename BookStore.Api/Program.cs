using BookStore.Application;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Infrastructure.Logging;
using WebApplication2.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ILogsService, LogsService>();
builder.Services.Configure<DefaultBooksOptions>(builder.Configuration.GetSection(DefaultBooksOptions.SectionName));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();
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