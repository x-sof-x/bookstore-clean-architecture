using BookStore.Application.DTOs.Books;

namespace BookStore.Application.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookResponseDto> GetAll(string? author = null, int? publishedYear = null);
        BookResponseDto GetById(Guid id);
        BookResponseDto Add(CreateBookDto dto);
        void Update(Guid id, UpdateBookDto dto);
        void Delete(Guid id);
    }
}