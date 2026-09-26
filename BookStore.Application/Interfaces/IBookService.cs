using BookStore.Application.DTOs.Books;

namespace BookStore.Application.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookResponseDto> GetAll(string? author = null, int? publishedYear = null);
        BookResponseDto? GetById(Guid id);
        BookResponseDto Add(CreateBookDto dto);
        bool Update(Guid id, UpdateBookDto dto);
        bool Delete(Guid id);
    }
}