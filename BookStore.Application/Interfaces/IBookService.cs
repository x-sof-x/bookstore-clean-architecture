using BookStore.Application.DTOs.Books;

namespace BookStore.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetAllAsync(string? author = null, int? publishedYear = null, CancellationToken ct = default);
        Task<BookResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<BookResponseDto> AddAsync(CreateBookDto dto, CancellationToken ct = default);
        Task UpdateAsync(Guid id, UpdateBookDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}