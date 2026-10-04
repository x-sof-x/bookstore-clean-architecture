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

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Authors
            .Select(a => new AuthorResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                BookTitles = a.Books.Select(b => b.Title).ToList()
            })
            .ToListAsync(ct);
    }

    public async Task<AuthorResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("Автора", id);

        return new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            Biography = author.Biography,
            BookTitles = author.Books.Select(b => b.Title).ToList()
        };
    }

    public async Task<AuthorResponseDto> AddAsync(CreateAuthorDto dto, CancellationToken ct = default)
    {
        var name = dto.Name.Trim();

        if (await _context.Authors.AnyAsync(a => a.Name.ToLower() == name.ToLower(), ct))
            throw new ConflictException($"Автор з іменем «{name}» вже існує.");

        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = name,
            Biography = dto.Biography
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync(ct);

        return new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            Biography = author.Biography,
            BookTitles = new List<string>()
        };
    }

    public async Task UpdateAsync(Guid id, UpdateAuthorDto dto, CancellationToken ct = default)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("Автора", id);

        var name = dto.Name.Trim();

        if (await _context.Authors.AnyAsync(a => a.Id != id && a.Name.ToLower() == name.ToLower(), ct))
            throw new ConflictException($"Автор з іменем «{name}» вже існує.");

        author.Name = name;
        author.Biography = dto.Biography;

        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id, ct)
            ?? throw new NotFoundException("Автора", id);

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<bool> AuthorExistsAsync(Guid authorId, CancellationToken ct = default)
    {
        return await _context.Authors.AnyAsync(a => a.Id == authorId, ct);
    }
}