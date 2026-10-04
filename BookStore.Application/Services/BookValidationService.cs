using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Exceptions;

namespace BookStore.Application.Services;

public class BookValidationService : IBookValidationService
{
    private readonly IAuthorService _authorService;

    public BookValidationService(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public async Task ValidateAsync(Book book, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(book.Title) || book.Title.Length > 30)
            throw new BadRequestException("Назва книги обов'язкова і не довша за 30 символів.");

        if (book.PublishedYear > DateTime.Now.Year)
            throw new BadRequestException("Рік видання не може бути в майбутньому.");

        if (!await _authorService.AuthorExistsAsync(book.AuthorId, ct))
            throw new BadRequestException($"Автора з Id {book.AuthorId} не існує.");
    }
}