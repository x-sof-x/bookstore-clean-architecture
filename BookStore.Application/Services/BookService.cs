using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
using BookStore.Domain.Exceptions;
using BookStore.Application.Interfaces;
using BookStore.Application.DTOs.Books;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookStoreDbContext _context;
        private readonly IBookValidationService _validationService;

        public BookService(IBookValidationService validationService, IBookStoreDbContext context)
        {
            _validationService = validationService;
            _context = context;
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllAsync(
            string? author = null, int? publishedYear = null, CancellationToken ct = default)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(author))
            {
                var searchPattern = $"%{author.Trim()}%";
                query = query.Where(b => b.Author != null && EF.Functions.Like(b.Author.Name, searchPattern));
            }

            if (publishedYear.HasValue)
            {
                query = query.Where(b => b.PublishedYear == publishedYear.Value);
            }

            return await query.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                PublishedYear = b.PublishedYear,
                AuthorId = b.AuthorId,
                AuthorName = b.Author != null ? b.Author.Name : string.Empty
            }).ToListAsync(ct);
        }

        public async Task<BookResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id, ct)
                ?? throw new NotFoundException("Книгу", id);

            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                PublishedYear = book.PublishedYear,
                AuthorId = book.AuthorId,
                AuthorName = book.Author != null ? book.Author.Name : string.Empty
            };
        }

        public async Task<BookResponseDto> AddAsync(CreateBookDto dto, CancellationToken ct = default)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Price = dto.Price,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };

            await _validationService.ValidateAsync(book, ct);

            _context.Books.Add(book);
            await _context.SaveChangesAsync(ct);

            return await GetByIdAsync(book.Id, ct);
        }

        public async Task UpdateAsync(Guid id, UpdateBookDto dto, CancellationToken ct = default)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id, ct)
                ?? throw new NotFoundException("Книгу", id);

            book.Title = dto.Title;
            book.Price = dto.Price;
            book.PublishedYear = dto.PublishedYear;
            book.AuthorId = dto.AuthorId;

            await _validationService.ValidateAsync(book, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id, ct)
                ?? throw new NotFoundException("Книгу", id);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(ct);
        }
    }
}