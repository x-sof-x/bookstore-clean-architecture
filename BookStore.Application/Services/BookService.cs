using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Entities;
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

        public IEnumerable<BookResponseDto> GetAll(string? author = null, int? publishedYear = null)
        {
            var query = _context.Books.Include(b => b.Author).AsQueryable();

            if (!string.IsNullOrWhiteSpace(author))
            {
                var searchPattern = $"%{author.Trim()}%";
                query = query.Where(b => b.Author != null && EF.Functions.Like(b.Author.Name, searchPattern));
            }

            if (publishedYear.HasValue)
            {
                query = query.Where(b => b.PublishedYear == publishedYear.Value);
            }

            return query.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                PublishedYear = b.PublishedYear,
                AuthorId = b.AuthorId,
                AuthorName = b.Author != null ? b.Author.Name : string.Empty
            }).ToList();
        }

        public BookResponseDto? GetById(Guid id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);

            if (book == null) return null;

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

        public BookResponseDto Add(CreateBookDto dto)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                
                Price = dto.Price,
                PublishedYear = dto.PublishedYear,
                AuthorId = dto.AuthorId
            };

            if (!_validationService.AddBookValidation(book))
            {
                throw new ArgumentException("Дані книги не пройшли валідацію.");
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return GetById(book.Id)!;
        }

        public bool Update(Guid id, UpdateBookDto dto)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = dto.Title;
            
            existingBook.Price = dto.Price;
            existingBook.PublishedYear = dto.PublishedYear;
            existingBook.AuthorId = dto.AuthorId;

            if (!_validationService.AddBookValidation(existingBook))
            {
                return false;
            }

            _context.SaveChanges();
            return true;
        }

        public bool Delete(Guid id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
    }
}