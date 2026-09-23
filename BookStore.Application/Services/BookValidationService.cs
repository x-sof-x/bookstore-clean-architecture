using BookStore.Domain.Entities;
using BookStore.Application.Interfaces;

namespace BookStore.Application.Interfaces
{
    public class BookValidationService: IBookValidationService
    {
        private readonly IAuthorService _authorService;
        public BookValidationService(IAuthorService authorService)
        {
            _authorService = authorService;
        }
       public bool AddBookValidation(Book book)
        {
            if (book == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(book.Title)|| book.Title.Length >30 )
            {
                return false;
            }
            if (book.PublishedYear > DateTime.Now.Year)
            {
                return false;
            }
            if (!_authorService.AuthorExists(book.Author))
            {
                return false;
            }
            return true;
        }
    }
}
