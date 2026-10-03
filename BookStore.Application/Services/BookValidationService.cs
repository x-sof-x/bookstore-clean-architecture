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

    public void Validate(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title) || book.Title.Length > 30)
            throw new BadRequestException("Назва книги обов'язкова і не довша за 30 символів.");

        if (book.PublishedYear > DateTime.Now.Year)
            throw new BadRequestException("Рік видання не може бути в майбутньому.");

        if (!_authorService.AuthorExists(book.AuthorId))
            throw new BadRequestException($"Автора з Id {book.AuthorId} не існує.");
    }
}