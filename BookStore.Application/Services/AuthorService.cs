using BookStore.Application.DTOs.Authors;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IBookStoreDbContext _context;

    public AuthorService(IBookStoreDbContext context)
    {
        _context = context;
    }

    public IEnumerable<AuthorResponseDto> GetAll()
    {
        return _context.Authors
            .Include(a => a.Books)
            .Select(a => new AuthorResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                BookTitles = a.Books.Select(b => b.Title).ToList()
            }).ToList();
    }

    public AuthorResponseDto GetById(Guid id)
    {
        var author = _context.Authors
            .Include(a => a.Books)
            .FirstOrDefault(a => a.Id == id)
            ?? throw new NotFoundException("Автора", id);

        return new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            Biography = author.Biography,
            BookTitles = author.Books.Select(b => b.Title).ToList()
        };
    }

    public AuthorResponseDto Add(CreateAuthorDto dto)
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Biography = dto.Biography
        };

        _context.Authors.Add(author);
        _context.SaveChanges();

        return new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            Biography = author.Biography,
            BookTitles = new List<string>()
        };
    }

    public void Update(Guid id, UpdateAuthorDto dto)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id)
            ?? throw new NotFoundException("Автора", id);

        author.Name = dto.Name;
        author.Biography = dto.Biography;

        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id)
            ?? throw new NotFoundException("Автора", id);

        _context.Authors.Remove(author);
        _context.SaveChanges();
    }

    public bool AuthorExists(Guid authorId)
    {
        return _context.Authors.Any(a => a.Id == authorId);
    }
}