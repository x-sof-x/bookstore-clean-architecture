using Microsoft.Extensions.Options;
using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;
       
        private readonly IBookValidationService _validationService;
        public BookService(IBookValidationService validationService, IOptions<DefaultBooksOptions> defaultBooksOptions)
        {
            _validationService = validationService;
            _books = new List<Book>(defaultBooksOptions.Value.Books);
        }

        public IEnumerable<Book> GetAll(string? author = null, int? publishedYear = null)
        {
            var query = _books.AsQueryable();
            if(!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
            }
            if (publishedYear.HasValue)
            {
                query = query.Where(b => b.PublishedYear == publishedYear.Value);
            }
            return query.ToList();
        }
        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }
        public bool Add(Book book)
        {
            if (!_validationService.AddBookValidation(book))
            {
                return false;
            }
            if (book.Id <= 0 || _books.Any(b => b.Id == book.Id))
            {
                book.Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
            }
            _books.Add(book);
            return true;
        }
        public bool Update(Book book)
        {
            if (!_validationService.AddBookValidation(book))
            {
                return false;
            }
            var existingBook = GetById(book.Id);
            if (existingBook == null)
            {
                return false;
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedYear = book.PublishedYear;
            return true;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);
            if (book == null)
            {
                return false;
            }
            _books.Remove(book);
            return true;
        }
    }
}
